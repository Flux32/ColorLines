namespace Balls.Core.FSM
{
    public interface IFSMCommand { }

    public interface IFSMCommand<T>
    {
        public T Args { get; }
    }
}