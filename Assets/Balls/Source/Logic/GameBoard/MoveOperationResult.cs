using System.Collections.Generic;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class MoveOperationResult
    {
        public MoveOperationResult(MoveResult result, 
            IEnumerable<Ball> ballsPlaced, 
            IEnumerable<Ball> solvedBallsAfterMove, 
            BallMovingResult movedResult)
        {
            Result = result;
            BallsPlaced = ballsPlaced;
            SolvedBallsAfterMove = solvedBallsAfterMove;
            MovedResult = movedResult;
        }

        public MoveOperationResult(MoveResult result)
        {
            Result = result;
        }
        
        public MoveResult Result { get; private set; }
        public IEnumerable<Ball> BallsPlaced { get; private set; }
        public IEnumerable<Ball> SolvedBallsAfterMove { get; private set; }
        public IEnumerable<Ball> SolvedBallsAfterBallsPlaced { get; private set; }
        public BallMovingResult MovedResult { get; private set; }
    }
}