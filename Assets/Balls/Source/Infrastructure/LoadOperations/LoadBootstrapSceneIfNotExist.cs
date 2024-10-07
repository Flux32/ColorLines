using System;
using Balls.Infrastructure.LoadOperations;
using Balls.Source.Infrastructure.Services.Level;
using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.LoadOperations
{
    public sealed class LoadBootstrapSceneIfNotExist : ILoadOperation
    {
        private readonly ILevelService _levelService;

        public LoadBootstrapSceneIfNotExist(ILevelService levelService)
        {
            _levelService = levelService;
        }

        public OperationID OperationID { get; }
        
        public async UniTask Load(Action<OperationID, float> progressChanged)
        {
            progressChanged.Invoke(OperationID.LoadConfig, 0f);
            await _levelService.LoadLevel(LevelId.Bootstrap);
            progressChanged.Invoke(OperationID.LoadConfig, 1f);
        }
    }
}