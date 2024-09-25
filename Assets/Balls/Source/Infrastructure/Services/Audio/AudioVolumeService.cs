using System.Collections.Generic;
using UnityEngine.Audio;

namespace Balls.Source.Infrastructure.Services.Audio
{
    public class AudioVolumeService : IAudioVolumeService
    {
        private const string MasterVolumeMixerKey = "MasterVolume";
        private const string MusicVolumeMixerKey = "MusicVolume";
        private const string SoundVolumeMixerKey = "SoundVolume";
        
        private readonly AudioMixer _audioMixer;
        private readonly SoundController _masterSoundController;
        private readonly Dictionary<SoundType, SoundController> _soundControllers;

        public AudioVolumeService(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;

            SoundController musicController = new SoundController(MusicVolumeMixerKey, "", _audioMixer, GetVolumeLevel(MusicVolumeMixerKey));
            SoundController soundController = new SoundController(SoundVolumeMixerKey, "", _audioMixer, GetVolumeLevel(SoundVolumeMixerKey));
        
            _masterSoundController = new SoundController(MasterVolumeMixerKey, "", _audioMixer, GetVolumeLevel(MasterVolumeMixerKey));

            _soundControllers = new Dictionary<SoundType, SoundController>()
            {
                [SoundType.Effect] = soundController,
                [SoundType.Music] = musicController,
            };
        }

        public void MuteMaster()
        {
            _masterSoundController.Mute();
        }

        public void UnmuteMaster()
        {
            _masterSoundController.Unmute();
        }

        public bool IsMuted(SoundType type) 
        {
            return _soundControllers[type].Muted;
        }

        public void SetMasterVolume(float value)
        {
            _masterSoundController.SetVolume(value);
        }

        public void SetVolumeAll(float value)
        {
            foreach (SoundController soundController in _soundControllers.Values)
                soundController.SetVolume(value);
        }

        public void SetVolume(SoundType type, float value)
        {
            _soundControllers[type].SetVolume(value);
        }

        public void SetPitch(SoundType type, float value)
        {
            _soundControllers[type].SetPitch(value);
        }

        public void UnmuteVolume(SoundType type)
        {
            _soundControllers[type].Unmute();
        }

        public void MuteVolume(SoundType type)
        {
            _soundControllers[type].Mute();
        }

        private float GetVolumeLevel(string exposedParameterName)
        {
            float volumeLevel;
            _audioMixer.GetFloat(exposedParameterName, out volumeLevel);
            return volumeLevel;
        }
    }
}