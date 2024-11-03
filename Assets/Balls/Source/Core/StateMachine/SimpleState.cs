using Balls.Source.Core.StateMachine;

namespace Balls.Core.StateMachine
{
    public abstract class SimpleState : ISimpleState
    {
        public virtual void Enter() { }
        public virtual void Exit() { }

        public virtual void Trigger(IFsmCommand command) { }

        public virtual void Tick(float deltaTime) { }
    }
}