namespace Balls.Source.Infrastructure.Services.Audio
{
    public interface IAudioVolumeService
    {
        public void SetVolume(SoundType type, float value);
        public void MuteVolume(SoundType type);
        public void UnmuteVolume(SoundType type);
        void SetVolumeAll(float value);
        void MuteMaster();
        void UnmuteMaster();
        bool IsMuted(SoundType type);
        void SetPitch(SoundType type, float value);
    }
}