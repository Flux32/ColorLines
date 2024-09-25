using UnityEngine;
using UnityEngine.Audio;

namespace Balls.Source.Infrastructure.Services.Audio
{
    public class SoundController
    {
        private const float LowerVolumeBound = -80.0f;
        private const float UpperVolumeBound = 0.0f;

        public float Volume { get; private set; }
        public string VolumeMixerKey { get; private set; }
        public string PitchMixerKey { get; private set; }
        public bool Muted { get; private set; }

        private readonly AudioMixer _audioMixer;

        public SoundController(string volumeMixerKey, 
            string pitchMixerKey, 
            AudioMixer audioMixer, 
            float volume = 0.5f, 
            bool muted = false)
        {
            Volume = volume;
            VolumeMixerKey = volumeMixerKey;
            PitchMixerKey = pitchMixerKey;
            Muted = muted;
            _audioMixer = audioMixer;
        }

        public void Mute()
        {
            if (Muted == true)
                return;

            Muted = true;
            SetAudioMixerVolume(LowerVolumeBound);
        }

        public void Unmute()
        {
            if (Muted == false)
                return;

            Muted = false;
            SetAudioMixerVolume(Volume);
        }

        public void SetPitch(float value)
        {
            _audioMixer.SetFloat(PitchMixerKey, value);
        }

        public void SetVolume(float volume)
        {
            Volume = volume;

            if (Muted == false)
                SetAudioMixerVolume(volume);
        }

        private void SetAudioMixerVolume(float volume)
        {
            _audioMixer.SetFloat(VolumeMixerKey, ConvertNormalizedToDb(volume));
        }

        private float ConvertNormalizedToDb(float normalizedValue)
        {
            return Mathf.Lerp(normalizedValue, LowerVolumeBound, UpperVolumeBound);
        }
    }
}
