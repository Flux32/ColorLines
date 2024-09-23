using Reflex.Core;
using UnityEngine;
using Balls.Infrastructure.Fsm;
using Balls.Infrastructure.Fsm.States;
using Balls.Source.Infrastructure.Factories;

namespace Balls.Infrastructure.Installers
{
    public sealed class GlobalFSMInstaller : MonoBehaviour, IInstaller
    {
        private Container _container;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallStates(containerBuilder);
            InstallFactories(containerBuilder);
            InstallFSM(containerBuilder);

            containerBuilder.OnContainerBuilt += OnContainerBuild;
        }

        private void OnContainerBuild(Container container)
        {
            _container = container;

            GlobalFSM fsm = _container.Resolve<GlobalFSM>();
            fsm.Initialize<BootstrapState>();
        }

        public void InstallFSM(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(GlobalFSM));
        }

        public void InstallFactories(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingletonInterfaces(typeof(GlobalFsmStateFactory));
        }

        public void InstallStates(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(BootstrapState))
                .AddSingleton(typeof(GameplayState));
        }
    }
}