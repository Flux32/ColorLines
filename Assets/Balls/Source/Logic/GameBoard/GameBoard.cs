﻿using System;
using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.GameBoard.Detectors;
using Balls.Source.Logic.GameBoard.Pathfinding;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class MoveOperationResult
    {
        public MoveOperationResult(MoveResult result, 
            IEnumerable<Ball> ballsPlaced, 
            IEnumerable<Ball> solvedBalls, 
            BallMovingResult movedResult)
        {
            Result = result;
            BallsPlaced = ballsPlaced;
            SolvedBalls = solvedBalls;
            MovedResult = movedResult;
        }

        public MoveOperationResult(MoveResult result)
        {
            Result = result;
        }
        
        public MoveResult Result { get; private set; }
        public IEnumerable<Ball> BallsPlaced { get; private set; }
        public IEnumerable<Ball> SolvedBalls { get; private set; }
        public BallMovingResult MovedResult { get; private set; }
    }

    public enum MoveResult
    {
        BallDoesNotExist = 0,
        PathFailed = 1,
        Success = 2,
    }
    
    public sealed class BallMovingResult
    {
        public BallMovingResult(Path path, Ball ball)
        {
            Path = path;
            Ball = ball;
        }

        public Path Path { get; private set; }
        public Ball Ball { get; private set; }
    }
    
    public sealed class GameBoard
    {
        private readonly Grid _grid;
        private readonly IBallGenerator _ballGenerator = new RandomBallGenerator(3);
        private readonly IPathfinder _pathfinder = new Pathfinder(150);
        private readonly ISolveDetector _solver = new LineDetector(3);

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

            Ball needMove = _grid[fromPosition];
            Path path = _pathfinder.FindPath(fromPosition, toPosition, _grid);

            if (path.Failed == true)
                return new MoveOperationResult(MoveResult.PathFailed);

            BallMovingResult ballMovingResult = null;
            
            if (_grid.TryRemoveItem(fromPosition) == true)
            {
                if (_grid.TryPlaceBall(toPosition, needMove.Id, out Ball placedBall) == true)
                    ballMovingResult = new BallMovingResult(path, placedBall);
            }
            
            Ball[] detectedLines = _solver.Detect(toPosition, _grid);

            if (detectedLines.Length > 0)
            {
                foreach (Ball ball in detectedLines)
                    _grid.TryRemoveItem(ball.Position);
            }

            List<Ball> balls = _ballGenerator.Generate(_grid);

            if (_grid.IsFilled() == true)
                Filled?.Invoke();

            return new MoveOperationResult(MoveResult.Success, balls, detectedLines, ballMovingResult);
        }
    }
}