using System;
using System.Threading;
using UnityEngine;
using Reflex.Attributes;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.GameBoard.Balls;
using Balls.Source.View.GameBoard.Jobs;

namespace Balls.Source.View.GameBoard
{
    public class GameBoardView : MonoBehaviour, IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        [SerializeField] private GridView _gridView;
        
        private IBallViewFactory _ballFactory;
        private readonly IJobExecutor _jobExecutor = new JobExecutor();
        
        private Logic.GameBoard.GameBoard _gameBoard;
        
        private BallView _selectedBall;
        private SelectionState _selectionState = SelectionState.Empty;
        
        public IReadOnlyGridView Grid => _gridView;
        
        [Inject]
        private void Constructor(Logic.GameBoard.GameBoard gameBoard, IBallViewFactory ballViewFactory)
        {
            _gameBoard = gameBoard;
            _ballFactory = ballViewFactory;
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
                new SpawnBallJob(_ballFactory, _gridView, generationResult.SpawnedBalls));
        }

        public void RestartGame()
        {
            GenerationOperationResult generationResult = 
                _gameBoard.RestartGame();
            
            _jobExecutor.Execute(_cancellationTokenSource.Token, 
                new ClearGridJob(_gridView, _ballFactory),
                new SpawnBallJob(_ballFactory, _gridView, generationResult.SpawnedBalls));
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
                await _jobExecutor.Execute(_cancellationTokenSource.Token, CreateJobs(moveOperationResult));
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
        
        private IViewJob[] CreateJobs(MoveOperationResult moveOperationResult)
        {
            IViewJob[] solveBallJobsAfterGenerate =
                new IViewJob[moveOperationResult.SolvedBallsAfterGeneration.Count];

            for (int i = 0; i < solveBallJobsAfterGenerate.Length; i++)
                solveBallJobsAfterGenerate[i] = 
                    new SolveBallJob(moveOperationResult.SolvedBallsAfterGeneration[i], _gridView, _ballFactory);
            
            IViewJob[] jobs = {
                new MoveBallJob(moveOperationResult.MovedResult.Path, _gridView),
                new SolveBallJob(moveOperationResult.SolvedBallsAfterMove, _gridView, _ballFactory),
                new SpawnBallJob(_ballFactory, _gridView, moveOperationResult.GenerationOperationResult.SpawnedBalls),
                new WhenAllJobsCompletedJob(solveBallJobsAfterGenerate),
            };

            return jobs;
        }
        
        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}