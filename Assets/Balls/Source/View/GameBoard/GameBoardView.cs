using System;
using System.Threading;
using UnityEngine;
using Reflex.Attributes;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard.Balls;
using Cysharp.Threading.Tasks;

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
        
        public void Input(GameBoardInputAction action, GridPosition position)
        { 
            if (_selectionState == SelectionState.Disabled)
                return;

            if (_gridView.IsCellExist(position) == false)
                return;
            
            CellView cellView = _gridView[position];

            if (_selectionState == SelectionState.Empty)
                InputBallSelection(action, cellView);
            else if (_selectionState == SelectionState.BallSelected)
                InputTargetPosition(action, position);
        }

        private async void InputTargetPosition(GameBoardInputAction action, GridPosition position)
        {
            CellView cellView = _gridView[position];
            
            switch (action)
            {
                case GameBoardInputAction.None:
                {
                    cellView.TransitToNormalState();
                    break;
                }
                case GameBoardInputAction.Hold:
                {
                    cellView.TransitToHoldState(_selectedBall.AccentColor);
                    break;
                }
                case GameBoardInputAction.Press:
                {
                    cellView.TransitToPressedState();
                    break;
                }
                case GameBoardInputAction.CancelPress:
                {
                    cellView.TransitToNormalState();
                    break;
                }
                case GameBoardInputAction.Performed:
                {
                    cellView.TransitToNormalState();
                    await MakeMove(position);
                    cellView.TransitToPerformedState();
                    _selectionState = SelectionState.Empty;
                    break;
                }
            }
        }
        
        private void InputBallSelection(GameBoardInputAction action, CellView cellView)
        {
            switch (action)
            {
                case GameBoardInputAction.None:
                    cellView.TransitToNormalState();
                    break;
                case GameBoardInputAction.Hold:
                {
                    cellView.TransitToHoldState();
                    break;
                }
                case GameBoardInputAction.Press:
                    break;
                case GameBoardInputAction.CancelPress:
                    break;
                case GameBoardInputAction.Performed:
                {
                    if (cellView.TryGetBall(out BallView ballView))
                    {
                        _selectedBall = ballView;
                        SelectBall(ballView);
                        cellView.TransitToPerformedState();
                        _selectionState = SelectionState.BallSelected;
                    } 
                    break;
                }
            }
        }
        
        private async UniTask MakeMove(GridPosition position)
        {
            if (_selectionState != SelectionState.BallSelected) 
                return;
                
            MoveOperationResult moveOperationResult = _gameBoard.MakeMove(_selectedBall.CellPosition, position);
                
            UnselectBallIfSelected();

            if (moveOperationResult.Result != MoveResult.Success) 
                return;
                
            _selectionState = SelectionState.Disabled;
                
            await _jobExecutor.Execute(_cancellationTokenSource.Token, 
                _jobFactory.CreateSolveJobs(moveOperationResult, _gridView));
                
            _selectionState = SelectionState.Empty;
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

public enum GameBoardInputAction : uint
{
    None = 0,
    Hold = 1,
    Press = 2,
    CancelPress = 3,
    Performed = 4,
}