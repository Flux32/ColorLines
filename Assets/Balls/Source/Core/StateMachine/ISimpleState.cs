namespace Balls.Core.StateMachine
{
    public interface ISimpleState : IState, IEnterableState, ITickableState, IExitableState { }
}