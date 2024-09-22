namespace Balls.Core.FSM
{
    public interface ITickableState
    {
        public void Tick(float deltaTime);
    }
}