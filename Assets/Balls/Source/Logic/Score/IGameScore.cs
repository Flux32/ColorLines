using System;

namespace Balls.Source.Logic.Score
{
    public interface IGameScore
    {
        event Action<int> ScoreChanged;
        event Action<BestScore> BestScoreChanged;
        event Action<int, BestScore> ScoreInitialized;

        BestScore BestScore { get; }
        int CurrentScore { get; }
    }
}