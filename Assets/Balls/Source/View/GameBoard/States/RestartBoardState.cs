using Balls.Core.StateMachine;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.Logic.GameBoard;
using Balls.Source.View.GameBoard;
using Balls.Source.View.GameBoard.Grid;
using System.Threading;
using Balls.Source.View.Factories;

public class RestartBoardState : SimpleState
{
    private readonly IJobExecuter _jobExecuter;
    private readonly IJobFactory _jobFactory;
    private readonly BoardView _gameBoardView;
    private readonly GridView _gridView;
    private readonly Board _gameBoard;

    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public RestartBoardState(
        IJobFactory jobFactory, 
        IJobExecuter jobExecuter,
        Board gameBoard,
        BoardView gameBoardView)
    {
        _jobFactory = jobFactory;
        _jobExecuter = jobExecuter;
        _gameBoard = gameBoard;
        _gameBoardView = gameBoardView;
    }

    public override async void Enter()
    {
        GenerationOperationResult generationResult = _gameBoard.RestartGame();

        await _jobExecuter.Execute(_cancellationTokenSource.Token,
                                   _jobFactory.CreateRestartGameJobs(generationResult, _gridView));

        _gameBoardView.Enter<IdleGameBoardState>();
    }
}

public class CreateGameBoardState : PayloadState<GridSize>
{
    private readonly IJobExecuter _jobExecuter;
    private readonly IJobFactory _jobFactory;
    private readonly BoardView _gameBoardView;
    private readonly GridView _gridView;
    private readonly Board _gameBoard;

    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public CreateGameBoardState(
        IJobFactory jobFactory,
        IJobExecuter jobExecuter,
        Board gameBoard,
        GridView gridView,
        BoardView gameBoardView)
    {
        _jobFactory = jobFactory;
        _jobExecuter = jobExecuter;
        _gameBoard = gameBoard;
        _gridView = gridView;
        _gameBoardView = gameBoardView;
    }

    public override async void Enter(GridSize gridSize)
    {
        GenerationOperationResult generationResult =
        _gameBoard.NewGame(new GridSize(gridSize.Width, gridSize.Height));

        _gridView.CreateGrid(gridSize);
        await _jobExecuter.Execute(_cancellationTokenSource.Token,
                             _jobFactory.CreateInitFirstGameJobs(generationResult, _gridView));

        _gameBoardView.Enter<IdleGameBoardState>();
    }
}