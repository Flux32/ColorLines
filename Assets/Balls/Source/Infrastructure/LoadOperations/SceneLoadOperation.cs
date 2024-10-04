using System;
using Balls.Infrastructure.LoadOperations;
using Balls.Source.Infrastructure.Services.Config;
using Balls.Source.Infrastructure.Services.Level;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balls.Source.Infrastructure.LoadOperations
{
    public sealed class SceneLoadOperation : ILoadOperation
    {
        private readonly ILevelService _levelService;
        private readonly LevelId _levelId;
        
        public SceneLoadOperation(OperationID operationID, ILevelService levelService, LevelId levelId)
        {
            _levelService = levelService;
            OperationID = operationID;
            _levelId = levelId;
        }

        public OperationID OperationID { get; private set; }
        public float Progress { get; private set; }

        public async UniTask Load(Action<OperationID, float> progressChanged)
        {
            progressChanged?.Invoke(OperationID.LoadScene, 0f);
            await _levelService.LoadLevel(_levelId);
            progressChanged?.Invoke(OperationID.LoadScene, 0f);
        }
    }

    public sealed class ConfigLoadOperation : ILoadOperation
    {
        private readonly IConfigService _configService;

        public ConfigLoadOperation(IConfigService configService)
        {
            _configService = configService;
        }

        public OperationID OperationID { get; }
        public float Progress { get; }
        
        public async UniTask Load(Action<OperationID, float> progressChanged)
        {
            progressChanged.Invoke(OperationID.LoadConfig, 0f);
            await _configService.Load();
            progressChanged.Invoke(OperationID.LoadConfig, 1f);
        }
    }
}