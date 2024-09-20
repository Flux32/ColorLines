using Balls.Core;
using System;
using System.Collections.Generic;

public class GameBoard
{
    private readonly GameBoardGrid _grid;
    private readonly BallGenerator _ballGenerator;
    private readonly Pathfinder _pathfinder = new Pathfinder();

    public GameBoard(GameBoardGrid grid, BallGenerator ballGenerator)
    {
        _grid = grid;
        _ballGenerator = ballGenerator;
    }

    public event Action<Path> BallMoved;
    public event Action<IEnumerable<Ball>> BallsPlaced;

    public void InitializeGame()
    {
        List<Ball> balls = _ballGenerator.Generate(_grid);

        BallsPlaced?.Invoke(balls);
    }

    public void MakeMove(GridPosition fromPosition, GridPosition toPosition)
    {
        if (_grid.IsBallExist(fromPosition) == false)
            return;

        Ball needMove = _grid[fromPosition];

        Path path = _pathfinder.FindPath(fromPosition, toPosition, _grid);

        if (path.Failed == true)
            return;

        if (_grid.TryRemoveBall(fromPosition) == true)
        {
            if (_grid.TryPlaceBall(toPosition, needMove.Id, out Ball placedBall) == true)
            {
                BallMoved?.Invoke(path);
            }
        }
        
        List<Ball> balls = _ballGenerator.Generate(_grid);

        BallsPlaced?.Invoke(balls);
    }
}