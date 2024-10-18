using System.Threading;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.Factories;
using Balls.Core.StateMachine;
using Balls.Source.View.GameBoard;
using Balls.Source.View.GameBoard.Grid;

public sealed class MakeMoveBoardState : PayloadState<MoveRequest>
{
    private readonly IJobExecuter _jobExecuter;
    private readonly IJobFactory _jobFactory;
    private readonly Board _gameBoard;
    private readonly GridView _gridView;
    private readonly BoardView _gameBoardView;

    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public MakeMoveBoardState(
        IJobExecuter jobExecutor,
        IJobFactory jobFactory,
        Board gameBoard, 
        GridView gridView,
        BoardView gameBoardView)
    {
        _jobExecuter = jobExecutor;
        _jobFactory = jobFactory;
        _gridView = gridView;
        _gameBoard = gameBoard;
        _gameBoardView = gameBoardView;
    }

    public override async void Enter(MoveRequest moveRequest)
    {
        MoveOperationResult moveOperationResult = _gameBoard.MakeMove(moveRequest.FromPosition, moveRequest.ToPosition);

        await _jobExecuter.Execute(_cancellationTokenSource.Token, 
                                   _jobFactory.CreateSolveJobs(moveOperationResult, _gridView));

        if (_gameBoard.Grid.IsFilled() == true)
            _gameBoardView.Enter<FilledBoardState>();
        else
            _gameBoardView.Enter<IdleGameBoardState>();
    }
}
