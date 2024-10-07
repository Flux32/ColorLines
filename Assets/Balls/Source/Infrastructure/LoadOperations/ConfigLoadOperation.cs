using System;
using Balls.Infrastructure.LoadOperations;
using Balls.Source.Infrastructure.Services.Config;
using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.LoadOperations
{
    public sealed class ConfigLoadOperation : ILoadOperation
    {
        private readonly IConfigService _configService;

        public ConfigLoadOperation(IConfigService configService)
        {
            _configService = configService;
        }

        public OperationID OperationID { get; }
        
        public async UniTask Load(Action<OperationID, float> progressChanged)
        {
            progressChanged.Invoke(OperationID.LoadConfig, 0f);
            await _configService.Load();
            progressChanged.Invoke(OperationID.LoadConfig, 1f);
        }
    }
}