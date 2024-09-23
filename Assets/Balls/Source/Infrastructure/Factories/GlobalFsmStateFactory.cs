using Balls.Infrastructure.Fsm.States;
using Reflex.Core;

namespace Balls.Source.Infrastructure.Factories
{
    public sealed class GlobalFsmStateFactory : IGlobalFsmStateFactory
    {
        private readonly Container _container;

        public GlobalFsmStateFactory(Container container)
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