using System;

namespace Balls.Source.Logic.Score
{
    public class BestScore
    {
        public BestScore(DateTime date, int value)
        {
            Date = date;
            Value = value;
        }

        public DateTime Date { get; }
        public int Value { get; }
    }
}
