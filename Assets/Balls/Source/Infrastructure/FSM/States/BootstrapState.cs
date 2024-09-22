using Balls.Core.FSM;
using Balls.Infrastructure.LoadOperations;
using Balls.View.UI;
using System;
using System.Threading;

namespace Balls.Infrastructure.FSM.States
{
    public sealed class BootstrapState : SimpleState, IDisposable
    {
        private readonly ILoadOperationService _loadOperationService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly GlobalFSM _fsm;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public BootstrapState(ILoadOperationService loadOperationService, ILoadingCurtain loadingCurtain, GlobalFSM fsm)
        {
            _fsm = fsm;
            _loadOperationService = loadOperationService;
            _loadingCurtain = loadingCurtain;
        }

        public override async void Enter()
        {
            _loadingCurtain.SetOpenedState();

            await _loadOperationService.Load((opId, progress) => { },
                new DelayOperation(2f),
                new SceneLoadOperation(OperationID.LoadGameplayScene, "Scene_Gameplay"));

            await _loadingCurtain.Close(_cancellationTokenSource.Token);
            _fsm.Enter<GameplayState>();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}