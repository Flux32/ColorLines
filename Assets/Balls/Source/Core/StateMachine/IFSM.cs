using Balls.Core.StateMachine;

namespace Balls.Source.Core.StateMachine
{
    public interface IFsm
    {
        void Enter<TState>() where TState : ISimpleState;
        void Enter<TState, TValue>(TValue value) where TState : IPayloadState<TValue>;
        public void Trigger(IFsmCommand command);
    }
}