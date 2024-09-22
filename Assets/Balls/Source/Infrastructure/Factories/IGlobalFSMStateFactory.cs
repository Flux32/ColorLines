using Balls.Infrastructure.FSM.States;

public interface IGlobalFSMStateFactory
{
    BootstrapState CreateBootstrapState();
    GameplayState CreateGameplayState();
}