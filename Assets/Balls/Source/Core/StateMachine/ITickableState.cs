namespace Balls.Core.StateMachine
{
    public interface ITickableState
    {
        public void Tick(float deltaTime);
    }
}