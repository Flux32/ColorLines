using System.Threading;
using Balls.Core.StateMachine;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard.Grid;

namespace Balls.Source.View.GameBoard.States
{
    public sealed class CreateGameBoardState : PayloadState<GridSize>
    {
        private readonly IJobExecutor _jobExecutor;
        private readonly IJobFactory _jobFactory;
        private readonly BoardView _gameBoardView;
        private readonly GridView _gridView;
        private readonly Board _gameBoard;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CreateGameBoardState(
            IJobFactory jobFactory,
            IJobExecutor jobExecutor,
            Board gameBoard,
            GridView gridView,
            BoardView gameBoardView)
        {
            _jobFactory = jobFactory;
            _jobExecutor = jobExecutor;
            _gameBoard = gameBoard;
            _gridView = gridView;
            _gameBoardView = gameBoardView;
        }

        public override async void Enter(GridSize gridSize)
        {
            GenerationOperationResult generationResult =
                _gameBoard.NewGame(new GridSize(gridSize.Width, gridSize.Height));

            _gridView.CreateGrid(gridSize);
            await _jobExecutor.Execute(_cancellationTokenSource.Token,
                _jobFactory.CreateInitFirstGameJobs(generationResult, _gridView));

            _gameBoardView.Enter<IdleGameBoardState>();
        }
    }
}