using System.Collections.Generic;
using System.Collections.ObjectModel;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.Score;

namespace Balls.Source.Logic.GameBoard.Operations
{
    public sealed class SolveResult
    {
        public SolveResult(ReadOnlyCollection<Ball> balls, SolveScore solveScore)
        {
            Balls = balls;
            SolveScore = solveScore;
        }
        
        public ReadOnlyCollection<Ball> Balls { get; private set; }
        public SolveScore SolveScore { get; private set; }
    }
}