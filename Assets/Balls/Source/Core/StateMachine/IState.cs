using Balls.Source.Core.StateMachine;

namespace Balls.Core.StateMachine
{
    public interface IState
    {
        public void Trigger(IFsmCommand command);
    }
}