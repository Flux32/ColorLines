using Balls.Source.Infrastructure.Data.Entities;
using Balls.Source.Infrastructure.Data.Storages;
using Balls.Source.Logic.Score;
using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.Data.Repositories
{
    public class BestScoreRepository : IBestScoreRepository 
    {
        private const string BestScoreKey = "BestScore";

        private readonly IDataStorage _dataStorage;

        public BestScoreRepository(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        public UniTask Set(BestScore score)
        {
            return _dataStorage.Save(BestScoreKey, Map(score));
        }

        public async UniTask<BestScore> Get()
        {
            BestScoreEntity score = await _dataStorage.Load<BestScoreEntity>(BestScoreKey);
            return Map(score);
        }

        private BestScoreEntity Map(BestScore bestScore)
        {
            return new BestScoreEntity() { Value = bestScore.Value, Date  = bestScore.Date };
        }

        private BestScore Map(BestScoreEntity bestScoreEntity)
        {
            return new BestScore(bestScoreEntity.Date, bestScoreEntity.Value);
        }
    }
}
