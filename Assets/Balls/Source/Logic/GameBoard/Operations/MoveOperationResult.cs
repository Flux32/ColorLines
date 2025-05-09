using System.Collections.Generic;
using System.Collections.ObjectModel;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.GameBoard.Pathfinding;

namespace Balls.Source.Logic.GameBoard.Operations
{
    public sealed class MoveOperationResult
    {
        public MoveOperationResult(MoveResult result, 
            GenerationOperationResult generationOperationResult, 
            SolveResult solvedBallsAfterMove,
            ReadOnlyCollection<SolveResult> solvedBallsAfterGeneration,
            BallMovingResult movedResult)
        {
            Result = result;
            GenerationOperationResult = generationOperationResult;
            SolvedBallsAfterMove = solvedBallsAfterMove;
            SolvedBallsAfterGeneration = solvedBallsAfterGeneration;
            MovedResult = movedResult;
        }

        public MoveOperationResult(MoveResult result)
        {
            Result = result;
        }
        
        public MoveOperationResult(MoveResult result, BallMovingResult movingResult)
        {
            Result = result;
            MovedResult = movingResult;
        }
        
        public MoveResult Result { get; private set; }
        
        public GenerationOperationResult GenerationOperationResult { get; private set; }
        public SolveResult SolvedBallsAfterMove { get; private set; }
        public ReadOnlyCollection<SolveResult> SolvedBallsAfterGeneration { get; private set; }
        public BallMovingResult MovedResult { get; private set; }
    }
}