using Balls.Source.Infrastructure.Storages;
using Balls.Source.Logic.Score;

namespace Balls.Source.Infrastructure.Repositories
{
    public class BestScoreRepository : IBestScoreRepository 
    {
        private const string BestScoreKey = "BestScore";

        private readonly PlayerPrefsJsonStorage _playerPrefsJsonStorage = new PlayerPrefsJsonStorage();
        
        public void Set(BestScore score)
        {
            _playerPrefsJsonStorage.Save(BestScoreKey, score);
        }

        public BestScore Get()
        {
            return _playerPrefsJsonStorage.Load<BestScore>(BestScoreKey);
        }
    }
}
