namespace Balls.Source.Logic.Score
{
    public sealed class SolveScore
    {
        public SolveScore(float scoreForBall, int sumScore)
        {
            ScoreForBall = scoreForBall;
            SumScore = sumScore;
        }

        public float ScoreForBall { get; private set; }
        public int SumScore { get; private set; }
    }
}