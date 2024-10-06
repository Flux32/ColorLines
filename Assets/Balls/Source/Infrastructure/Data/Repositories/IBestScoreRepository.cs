using Balls.Source.Logic.Score;

namespace Balls.Source.Infrastructure.Repositories
{
    public interface IBestScoreRepository
    {
        public void Set(BestScore score);
        public BestScore Get();
    }
}