using Balls.Infrastructure.LoadOperations;
using Cysharp.Threading.Tasks;
using System;

public interface ILoadOperationService
{
    UniTask Load(Action<OperationID, float> progressChanged, params ILoadOperation[] loadOperations);
}