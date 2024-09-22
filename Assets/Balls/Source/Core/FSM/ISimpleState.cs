namespace Balls.Core.FSM
{
    public interface ISimpleState : IState, IEnterableState, ITickableState, IExitableState { }
}