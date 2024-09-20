using Minesweeper.Core.Application;

public interface IFSM
{
    void Enter<TState>() where TState : ISimpleState;
    void Enter<TState, TValue>(TValue value) where TState : IPayloadState<TValue>;
    public bool Trigger(IFSMCommand command);
    public bool Trigger<TArgs>(IFSMCommand<TArgs> command);
}