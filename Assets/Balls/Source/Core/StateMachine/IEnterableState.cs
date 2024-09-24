namespace Balls.Core.StateMachine
{
    public interface IEnterableState
    {
        public void Enter();
    }

    public interface IEnterableState<TValue>
    {
        public void Enter(TValue value);
    }
}