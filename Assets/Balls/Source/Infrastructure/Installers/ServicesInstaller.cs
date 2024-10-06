using Balls.Source.Infrastructure.Services.Audio;
using Balls.Source.Infrastructure.Services.Config;
using Balls.Source.Infrastructure.Services.Input;
using Balls.Source.Infrastructure.Services.Level;
using Balls.Source.Infrastructure.Services.Log;
using Reflex.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace Balls.Source.Infrastructure.Installers
{
    public sealed class ServicesInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private AudioPlayService _audioPlayServicePrefab;
        [SerializeField] private AudioMixer _audioMixer;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            AudioPlayService audioPlayService = Instantiate(_audioPlayServicePrefab);
            DontDestroyOnLoad(audioPlayService);

            containerBuilder
                .AddSingletonInterfaces(typeof(LogService))
                .AddSingletonInterfaces(typeof(LoadOperationService))
                .AddSingletonInterfaces(typeof(AudioVolumeService))
                .AddSingletonInterfaces(typeof(ConfigService))
                .AddSingletonInterfaces(typeof(LevelService))
                .AddSingletonInterfaces(typeof(GameBoardInputService))
                .AddSingleton(typeof(PlayerInput))
                .AddSingletonInterfaces(audioPlayService)
                .AddSingleton(_audioMixer);
        }
    }
}