using Balls.View.UI;
using Reflex.Core;
using UnityEngine;

namespace Balls.Infrastructure.Installers
{
    public sealed class UIInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            LoadingCurtain loadingCurtain = Instantiate(_loadingCurtainPrefab);
            DontDestroyOnLoad(loadingCurtain);

            containerBuilder
                .AddSingletonInterfaces(loadingCurtain);
        }
    }
}