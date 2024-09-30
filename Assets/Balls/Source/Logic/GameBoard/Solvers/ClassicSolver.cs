using System;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.GameBoard.Detectors;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.Logic.Score;

namespace Balls.Source.Logic.GameBoard.Solvers
{
    public sealed class ClassicSolver : ISolver
    {
        private readonly IPatternDetector _patternDetector;
        private readonly IScoreCalculator _scoreCalculator;
        
        public ClassicSolver(IPatternDetector patternDetector, IScoreCalculator scoreCalculator)
        {
            _patternDetector = patternDetector;
            _scoreCalculator = scoreCalculator;
        }

        public SolveResult Solve(GridPosition position, Grid grid)
        {
            Ball[] detectedPattern = _patternDetector.Detect(position, grid);
            SolveScore solveScore = _scoreCalculator.Calculate(Array.AsReadOnly(detectedPattern));
            
            foreach (Ball ball in detectedPattern)
                grid.TryRemoveBall(ball.Position);
            
            return new SolveResult(Array.AsReadOnly(detectedPattern), solveScore);
        }
    }
}