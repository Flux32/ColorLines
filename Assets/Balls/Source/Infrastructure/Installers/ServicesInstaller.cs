using Reflex.Core;
using UnityEngine;

namespace Balls.Infrastructure.Installers
{
    public sealed class ServicesInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingletonInterfaces(typeof(LoadOperationService));
        }
    }
}