namespace Balls.Core.FSM
{
    public interface IState
    {
        public bool Trigger(IFSMCommand command);
        public bool Trigger<TArgs>(IFSMCommand<TArgs> command);
    }
}