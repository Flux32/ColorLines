namespace Balls.Core.StateMachine
{
    public interface IState
    {
        public void Trigger(IFSMCommand command);
    }
}