using System;
using System.Threading;
using UnityEngine;
using Reflex.Attributes;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard.Balls;

namespace Balls.Source.View.GameBoard
{
    public sealed class GameBoardView : MonoBehaviour, IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly IJobExecutor _jobExecutor = new JobExecutor();
        
        [SerializeField] private GridView _gridView;
        
        private IJobFactory _jobFactory;
        private Logic.GameBoard.GameBoard _gameBoard;
        private BallView _selectedBall;
        private SelectionState _selectionState = SelectionState.Empty;
        
        public IReadOnlyGridView Grid => _gridView;
        
        [Inject]
        private void Constructor(
            Logic.GameBoard.GameBoard gameBoard, 
            IJobFactory jobFactory)
        {
            _gameBoard = gameBoard;
            _jobFactory = jobFactory;
        }

        private void OnEnable()
        {
            _gameBoard.Filled += Filled;
        }

        private void OnDisable()
        {
            _gameBoard.Filled -= Filled;
        }
        
        public void StartNewGame(GridSize size)
        {
            GenerationOperationResult generationResult = 
                _gameBoard.NewGame(new GridSize(size.Width, size.Height));
            
            _gridView.CreateGrid(size);
            _jobExecutor.Execute(_cancellationTokenSource.Token, 
                                 _jobFactory.CreateInitFirstGameJobs(generationResult, _gridView));
        }

        public void RestartGame()
        {
            GenerationOperationResult generationResult = _gameBoard.RestartGame();
            
            _jobExecutor.Execute(_cancellationTokenSource.Token, 
                                 _jobFactory.CreateRestartGameJobs(generationResult, _gridView));
        }

        public bool CanSelect(GridPosition position)
        {
            return _selectionState == SelectionState.Empty && _gridView.IsBallExist(position) ||
                   _selectionState == SelectionState.BallSelected && _gridView.IsCellExist(position);
        }
        
        public async void Select(GridPosition position)
        {
            if (_selectionState == SelectionState.Disabled)
                return;
                
            BallView ballView = _gridView[position];

            if (_selectionState == SelectionState.BallSelected && _selectedBall == ballView)
                return;
            
            
            if (ballView == null && _selectionState == SelectionState.BallSelected)
            { 
                MoveOperationResult moveOperationResult = _gameBoard.MakeMove(_selectedBall.CellPosition, position);
                
                UnselectBallIfSelected();

                if (moveOperationResult.Result != MoveResult.Success) 
                    return;
                
                _selectionState = SelectionState.Disabled;
                await _jobExecutor.Execute(_cancellationTokenSource.Token, 
                                           _jobFactory.CreateSolveJobs(moveOperationResult, _gridView));
                
                _selectionState = SelectionState.Empty;

                return;
            }
            
            SelectBall(ballView);
        }
        
        private void Filled()
        {
            _selectionState = SelectionState.Disabled;
        }

        private void SelectBall(BallView ballView)
        {
            UnselectBallIfSelected();
            ballView.Select();
            _selectionState = SelectionState.BallSelected;
            _selectedBall = ballView;
        }
        
        private void UnselectBallIfSelected()
        {
            if (_selectionState != SelectionState.BallSelected) 
                return;
            
            _selectedBall.Unselect();
            _selectionState = SelectionState.Empty;
            _selectedBall = null;
        }
        
        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}