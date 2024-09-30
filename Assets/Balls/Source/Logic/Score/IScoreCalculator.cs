using System.Collections.ObjectModel;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.Score
{
    public interface IScoreCalculator
    {
        SolveScore Calculate(ReadOnlyCollection<Ball> detectedBalls);
    }
}