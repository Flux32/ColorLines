using UnityEngine;

namespace Balls.Source.Infrastructure.Services.Audio
{
    public interface IAudioPlayService
    {
        void PlayOneShoot(AudioClip audioClip, SoundType soundType);
        void Play(AudioClip audioClip, SoundType soundType);
    }
}