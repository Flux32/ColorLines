namespace Balls.Core.StateMachine
{
    public interface IFSM
    {
        void Enter<TState>() where TState : ISimpleState;
        void Enter<TState, TValue>(TValue value) where TState : IPayloadState<TValue>;
        public void Trigger(IFSMCommand command);
    }
}