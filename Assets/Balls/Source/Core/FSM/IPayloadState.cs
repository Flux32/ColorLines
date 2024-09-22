namespace Balls.Core.FSM
{
    public interface IPayloadState<TValue> : IState, IEnterableState<TValue>, ITickableState, IExitableState { }
}