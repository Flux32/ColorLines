namespace Minesweeper.Core.Application
{
    public interface ISimpleState : IState, IEnterableState, ITickableState, IExitableState { }
}