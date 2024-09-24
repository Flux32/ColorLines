using Balls.Infrastructure.StateMachine.States;

namespace Balls.Source.Infrastructure.Factories
{
    public interface IGlobalFsmStateFactory
    {
        BootstrapState CreateBootstrapState();
        GameplayState CreateGameplayState();
    }
}