using Balls.Infrastructure.LoadOperations;
using Cysharp.Threading.Tasks;
using System;

public sealed class LoadOperationService : ILoadOperationService
{
    public async UniTask Load(Action<OperationID, float> progressChanged, params ILoadOperation[] loadOperations)
    {
        foreach (ILoadOperation operation in loadOperations)
            await operation.Load(progressChanged);
    }
}