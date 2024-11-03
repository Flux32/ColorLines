using System.Collections.ObjectModel;
using System.Linq;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.Score;

namespace Balls.Source.Logic.GameBoard.Operations
{
    public sealed class SolveResult
    {
        public SolveResult(ReadOnlyCollection<Ball> balls, 
            SolveScore solveScore,
            Ball solveOrigin)
        {
            Balls = balls;
            SolveScore = solveScore;
            SolveOrigin = solveOrigin;
        }
        
        public ReadOnlyCollection<Ball> Balls { get; }
        public Ball SolveOrigin { get; }
        public SolveScore SolveScore { get; }
        
        public bool SolveExecuted => Balls.Any();
    }
}