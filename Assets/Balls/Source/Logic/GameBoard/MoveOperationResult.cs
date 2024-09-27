using System.Collections.Generic;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class MoveOperationResult
    {
        public MoveOperationResult(MoveResult result, 
            IEnumerable<Ball> ballsPlaced, 
            IEnumerable<Ball> solvedBallsAfterMove,
            IReadOnlyList<Ball[]> solvedBallsAfterGeneration,
            BallMovingResult movedResult)
        {
            Result = result;
            BallsPlaced = ballsPlaced;
            SolvedBallsAfterMove = solvedBallsAfterMove;
            SolvedBallsAfterGeneration = solvedBallsAfterGeneration;
            MovedResult = movedResult;
        }

        public MoveOperationResult(MoveResult result)
        {
            Result = result;
        }
        
        public MoveResult Result { get; private set; }
        public IEnumerable<Ball> BallsPlaced { get; private set; }
        public IEnumerable<Ball> SolvedBallsAfterMove { get; private set; }
        public IReadOnlyList<Ball[]> SolvedBallsAfterGeneration { get; private set; }
        public BallMovingResult MovedResult { get; private set; }
    }
}