using System.Threading;
using Balls.Core.StateMachine;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard.Grid;

namespace Balls.Source.View.GameBoard.States
{
    public sealed class MakeMoveBoardState : PayloadState<MoveRequest>
    {
        private readonly IJobExecutor _jobExecutor;
        private readonly IJobFactory _jobFactory;
        private readonly Board _gameBoard;
        private readonly GridView _gridView;
        private readonly BoardView _gameBoardView;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public MakeMoveBoardState(
            IJobExecutor jobExecutor,
            IJobFactory jobFactory,
            Board gameBoard, 
            GridView gridView,
            BoardView gameBoardView)
        {
            _jobExecutor = jobExecutor;
            _jobFactory = jobFactory;
            _gridView = gridView;
            _gameBoard = gameBoard;
            _gameBoardView = gameBoardView;
        }

        public override async void Enter(MoveRequest moveRequest)
        {
            MoveOperationResult moveOperationResult = _gameBoard.MakeMove(moveRequest.FromPosition, moveRequest.ToPosition);

            await _jobExecutor.Execute(_cancellationTokenSource.Token, 
                _jobFactory.CreateSolveJobs(moveOperationResult, _gridView));

            if (_gameBoard.Grid.IsFilled() == true)
                _gameBoardView.Enter<FilledBoardState>();
            else
                _gameBoardView.Enter<IdleGameBoardState>();
        }
    }
}
