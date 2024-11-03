using Balls.Source.Core.StateMachine;

namespace Balls.Core.StateMachine
{
    public class PayloadState<TValue> : IPayloadState<TValue>
    {
        public virtual void Enter(TValue value) { }
        public virtual void Exit() { }
        public virtual void Tick(float deltaTime) { }

        public virtual void Trigger(IFsmCommand command) { }
    }
}