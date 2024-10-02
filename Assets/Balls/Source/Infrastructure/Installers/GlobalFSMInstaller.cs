using Balls.Infrastructure.StateMachine;
using Balls.Infrastructure.StateMachine.States;
using Balls.Source.Infrastructure.Factories;
using Balls.Source.Infrastructure.FSM.States;
using Reflex.Core;
using UnityEngine;

namespace Balls.Source.Infrastructure.Installers
{
    public sealed class GlobalFsmInstaller : MonoBehaviour, IInstaller
    {
        private Container _container;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallStates(containerBuilder);
            InstallFactories(containerBuilder);
            InstallFsm(containerBuilder);

            containerBuilder.OnContainerBuilt += OnContainerBuild;
        }

        private void OnContainerBuild(Container container)
        {
            _container = container;

            GlobalFsm fsm = _container.Resolve<GlobalFsm>();
            fsm.Initialize<BootstrapState>();
        }

        private void InstallFsm(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(GlobalFsm));
        }

        private void InstallFactories(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingletonInterfaces(typeof(GlobalFsmStateFactory));
        }

        private void InstallStates(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(BootstrapState))
                .AddSingleton(typeof(GameplayState));
        }
    }
}