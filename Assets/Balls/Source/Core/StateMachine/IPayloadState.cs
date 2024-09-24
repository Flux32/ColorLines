namespace Balls.Core.StateMachine
{
    public interface IPayloadState<TValue> : IState, IEnterableState<TValue>, ITickableState, IExitableState { }
}