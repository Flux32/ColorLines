using Balls.Infrastructure.StateMachine.States;
using Balls.Source.Infrastructure.FSM.States;

namespace Balls.Source.Infrastructure.Factories
{
    public interface IGlobalFsmStateFactory
    {
        BootstrapState CreateBootstrapState();
        GameplayState CreateGameplayState();
    }
}