namespace Minesweeper.Core.Application
{
    public interface IFSMCommand { }

    public interface IFSMCommand<T>
    {
        public T Args { get; }
    }
}