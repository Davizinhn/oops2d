using Raylib_cs;
using oops2d.Audio.Internal;

namespace oops2d.Audio
{
    public class Sound : IAudio
    {
        public Raylib_cs.Sound Data { get; private set; }

        public Sound(string path, float volume = 1, bool loop = false, bool playOnCreation = false, float pitch = 1)
        {
            if (String.IsNullOrEmpty(path)) return;

            SetVolume(volume);
            SetLoop(loop);
            SetPitch(pitch);

            Load(path);

            if (!playOnCreation) return;
            Play();
        }

        public override void Play() {
            IsPlaying = true;
            Raylib.PlaySound(Data);
        }
        public override void Load(string path) {
            Data = Raylib.LoadSound(path);
        }
        public override void Stop() {
            if (!IsPlaying) return;

            IsPlaying = false;
            Raylib.StopSound(Data);
        }
        public override void Pause() {
            if (!IsPlaying) return;

            IsPlaying = false;
            Raylib.PauseSound(Data);
        }
        public override void Resume() {
            if (IsPlaying) return;

            IsPlaying = true;
            Raylib.ResumeSound(Data);
        }
    }
}
