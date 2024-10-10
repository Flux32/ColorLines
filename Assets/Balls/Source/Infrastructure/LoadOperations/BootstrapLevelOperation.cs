using Balls.Source;
using Cysharp.Threading.Tasks;
using System;

namespace Balls.Infrastructure.LoadOperations
{
    public sealed class BootstrapLevelOperation : ILoadOperation
    {
        public OperationID OperationID => OperationID.LevelEnter;

        public async UniTask Load(Action<OperationID, float> progressChange)
        {
            progressChange?.Invoke(OperationID, 0f);
            ILevelBootstrapper levelBootstrapper = UnityEngine.Object.FindAnyObjectByType<LevelBootstrapper>();

            if (levelBootstrapper == null)
                return;

            await levelBootstrapper.Bootstrap();
            progressChange?.Invoke(OperationID, 1f);
        }
    }
}