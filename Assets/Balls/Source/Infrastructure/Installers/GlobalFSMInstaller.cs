using Reflex.Core;
using UnityEngine;
using Balls.Infrastructure.StateMachine;
using Balls.Infrastructure.StateMachine.States;
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

            GlobalFsm fsm = _container.Resolve<GlobalFsm>();
            fsm.Initialize<BootstrapState>();
        }

        public void InstallFSM(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(GlobalFsm));
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