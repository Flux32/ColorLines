using System.Collections.Generic;
using UnityEngine;

namespace Balls.Source.Infrastructure.Services.Audio
{
    public sealed class AudioPlayService : MonoBehaviour, IAudioPlayService
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;

        private Dictionary<SoundType, AudioSource> _audioSources = new Dictionary<SoundType, AudioSource>();
    
        private void Awake()
        {
            _audioSources = new Dictionary<SoundType, AudioSource>()
            {
                [SoundType.Effect] = _soundSource,
                [SoundType.Music] = _musicSource,
            };
        }

        public void PlayOneShoot(AudioClip audioClip, SoundType soundType)
        {
            _audioSources[soundType].PlayOneShot(audioClip);
        }

        public void Play(AudioClip audioClip, SoundType soundType)
        {
            AudioSource source = _audioSources[soundType];

            source.Stop();
            source.clip = audioClip;
            source.Play();
        }
    }
}