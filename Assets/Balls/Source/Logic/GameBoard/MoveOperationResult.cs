using System.Collections.Generic;
using Balls.Source.Logic.GameBoard.Balls;

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
}