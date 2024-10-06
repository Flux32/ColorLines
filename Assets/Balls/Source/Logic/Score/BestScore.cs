using System;

namespace Balls.Source.Logic.Score
{
    public class BestScore
    {
        public BestScore(DateTime date, int score)
        {
            Date = date;
            Score = score;
        }

        public DateTime Date { get; }
        public int Score { get; }
    }
}
