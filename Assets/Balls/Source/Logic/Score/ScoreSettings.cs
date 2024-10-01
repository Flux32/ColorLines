namespace Balls.Source.Logic.Score
{
    public sealed class ScoreSettings
    {
        public ScoreSettings(int scoreForBall, int startFromBallIncrement)
        {
            ScoreForBall = scoreForBall;
            StartFromBallIncrement = startFromBallIncrement;
        }

        public int ScoreForBall { get; private set; }
        public int StartFromBallIncrement { get; private set; }
    }
}