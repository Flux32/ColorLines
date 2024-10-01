using System;

namespace Balls.Source.Logic.Score
{
    public interface IGameScore
    {
        event Action<int> ScoreChanged;
        event Action<BestScore> BestScoreChanged;
        BestScore BestScore { get; }
        int CurrentScore { get; }
    }
}