using Cysharp.Threading.Tasks;
using System;

namespace Balls.Infrastructure.LoadOperations
{
    public interface ILoadOperation
    {
        public OperationID OperationID { get; }
        public float Progress { get; }

        public UniTask Load(Action<OperationID, float> progressChanged);
    }
}