using Balls.Infrastructure.FSM.States;
using Reflex.Core;

namespace Balls.Infrastructure.Factories
{
    public sealed class GlobalFSMStateFactory : IGlobalFSMStateFactory
    {
        private readonly Container _container;

        public GlobalFSMStateFactory(Container container)
        {
            _container = container;
        }

        public BootstrapState CreateBootstrapState()
        {
            return _container.Resolve<BootstrapState>();
        }

        public GameplayState CreateGameplayState()
        {
            return _container.Resolve<GameplayState>();
        }
    }
}