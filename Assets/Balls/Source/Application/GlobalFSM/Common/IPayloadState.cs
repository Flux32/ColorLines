namespace Minesweeper.Core.Application
{
    public interface IPayloadState<TValue> : IState, IEnterableState<TValue>, ITickableState, IExitableState { }
}