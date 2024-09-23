using System;
using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.GameBoard.Detectors;
using Balls.Source.Logic.GameBoard.Generators;
using Balls.Source.Logic.GameBoard.Pathfinding;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class GameBoard
    {
        private readonly Grid _grid;
        private readonly IBallGenerator _ballGenerator = new RandomBallGenerator(3);
        private readonly IPathfinder _pathfinder = new Pathfinder(150);
        private readonly ISolveDetector _solveDetector = new LineDetector(3);

        public GameBoard(Grid grid)
        {
            _grid = grid;
        }
        
        public event Action Filled;

        public IEnumerable<Ball> InitializeGame()
        {
            return _ballGenerator.Generate(_grid);
        }

        public MoveOperationResult MakeMove(GridPosition fromPosition, GridPosition toPosition)
        {
            if (_grid.IsBallExist(fromPosition) == false)
                return new MoveOperationResult(MoveResult.BallDoesNotExist);
            
            Path path = _pathfinder.FindPath(fromPosition, toPosition, _grid);

            if (path.Failed == true)
                return new MoveOperationResult(MoveResult.PathFailed);

            BallMovingResult ballMovingResult = null;
            
            if (_grid.TryReplaceBall(fromPosition, toPosition) == true) 
                ballMovingResult = new BallMovingResult(path, _grid[toPosition]);

            Ball[] solvedBalls = Solve(toPosition);

            List<Ball> generatedBalls = new List<Ball>();
            
            if (solvedBalls.Length <= 0 || _grid.IsEmpty() == true)
                generatedBalls = _ballGenerator.Generate(_grid);

            if (_grid.IsFilled() == true)
                Filled?.Invoke();

            return new MoveOperationResult(MoveResult.Success, generatedBalls, solvedBalls, ballMovingResult);
        }

        private Ball[] Solve(GridPosition position)
        {
            Ball[] solvedBalls = _solveDetector.Detect(position, _grid);

            if (solvedBalls.Length <= 0) 
                return solvedBalls;
            
            foreach (Ball ball in solvedBalls)
                _grid.TryRemoveBall(ball.Position);

            return solvedBalls;
        }
    }
}