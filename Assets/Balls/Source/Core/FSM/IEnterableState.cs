namespace Balls.Core.FSM
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