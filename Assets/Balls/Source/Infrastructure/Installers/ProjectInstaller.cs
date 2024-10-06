using Reflex.Core;
using UnityEngine;

namespace Balls.Source.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Camera _cameraPrefab;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallCamera(containerBuilder);
        }

        private void InstallCamera(ContainerBuilder containerBuilder)
        {
            Camera mainCamera = Instantiate(_cameraPrefab);
            DontDestroyOnLoad(mainCamera.gameObject);
            
            containerBuilder.AddSingleton(mainCamera);
            containerBuilder.AddSingleton(typeof(PlayerInput));
        }
    }
}