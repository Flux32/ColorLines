namespace Balls.Core.StateMachine
{
    public interface IFSMCommand { }

    public interface IFSMCommand<T>
    {
        public T Args { get; }
    }
}