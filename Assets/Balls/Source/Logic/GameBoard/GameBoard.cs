using System;
using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.GameBoard.Detectors;
using Balls.Source.Logic.GameBoard.Pathfinding;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class GameBoard
    {
        private readonly Grid _grid;
        private readonly IBallGenerator _ballGenerator = new RandomBallGenerator(3);
        private readonly IPathfinder _pathfinder = new Pathfinder(150);
        private readonly ISolveDetector _solver = new LineDetector(5);

        public GameBoard(Grid grid)
        {
            _grid = grid;
        }

        public event Action<Path, Ball> BallMoved;
        public event Action<IEnumerable<Ball>> BallsPlaced;
        public event Action<IEnumerable<Ball>> BallsSolved;
        public event Action Filled;

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

            if (_grid.TryRemoveItem(fromPosition) == true)
            {
                if (_grid.TryPlaceBall(toPosition, needMove.Id, out Ball placedBall) == true)
                    BallMoved?.Invoke(path, placedBall);
            }
            
            Ball[] detectedLines = _solver.Detect(toPosition, _grid);

            if (detectedLines.Length > 0)
            {
                foreach (Ball ball in detectedLines)
                    _grid.TryRemoveItem(ball.Position);
                
                BallsSolved?.Invoke(detectedLines);
            }

            List<Ball> balls = _ballGenerator.Generate(_grid);

            if (_grid.IsFilled() == true)
                Filled?.Invoke();

            BallsPlaced?.Invoke(balls);
        }
    }
}