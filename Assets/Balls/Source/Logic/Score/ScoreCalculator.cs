using System.Collections.ObjectModel;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.Score
{
    public class ScoreCalculator : IScoreCalculator
    {
        private readonly ScoreSettings _scoreSettings;

        public ScoreCalculator(ScoreSettings scoreSettings)
        {
            _scoreSettings = scoreSettings;
        }

        public SolveScore Calculate(ReadOnlyCollection<Ball> detectedBalls)
        {
            int scoreForBall = _scoreSettings.ScoreForBall;
            int sumScore = scoreForBall * detectedBalls.Count;
            return new SolveScore(scoreForBall, sumScore);
        }
    }
}