using Cysharp.Threading.Tasks;
using System;

namespace Balls.Infrastructure.LoadOperations
{
    public sealed class DelayOperation : ILoadOperation
    {
        private readonly float _delay;

        public DelayOperation(float delay)
        {
            _delay = delay;
        }

        public OperationID OperationID => OperationID.Delay;

        public async UniTask Load(Action<OperationID, float> progressChanged)
        {
            await UniTask.WaitForSeconds(_delay);
        }
    }
}