using Balls.Infrastructure.Fsm.States;

namespace Balls.Source.Infrastructure.Factories
{
    public interface IGlobalFsmStateFactory
    {
        BootstrapState CreateBootstrapState();
        GameplayState CreateGameplayState();
    }
}