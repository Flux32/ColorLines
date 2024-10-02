using System;
using System.Threading;
using Balls.Core.StateMachine;
using Balls.Infrastructure.LoadOperations;
using Balls.Infrastructure.StateMachine;
using Balls.Infrastructure.StateMachine.States;
using Balls.Source.Infrastructure.LoadOperations;
using Balls.Source.Infrastructure.Services.Config;
using Balls.View.UI;

namespace Balls.Source.Infrastructure.FSM.States
{
    public sealed class BootstrapState : SimpleState, IDisposable
    {
        private const string GameplaySceneName = "Scene_Gameplay";
        
        private readonly ILoadOperationService _loadOperationService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IConfigService _configService;
        private readonly GlobalFsm _fsm;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public BootstrapState(
            ILoadOperationService loadOperationService, 
            ILoadingCurtain loadingCurtain,
            IConfigService configService,
            GlobalFsm fsm)
        {
            _fsm = fsm;
            _loadOperationService = loadOperationService;
            _loadingCurtain = loadingCurtain;
            _configService = configService;
        }

        public override async void Enter()
        {
            _loadingCurtain.SetOpenedState();

            await _loadOperationService.Load((opId, progress) => { },
                new ConfigLoadOperation(_configService),
                new DelayOperation(2f),
                new SceneLoadOperation(OperationID.LoadGameplayScene, GameplaySceneName));

            await _loadingCurtain.Close(_cancellationTokenSource.Token);
            _fsm.Enter<GameplayState>();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}