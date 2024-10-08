using Balls.Source.Logic.Score;
using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.Data.Repositories
{
    public interface IBestScoreRepository
    {
        public UniTask Set(BestScore score);
        public UniTask<BestScore> Get();
    }
}