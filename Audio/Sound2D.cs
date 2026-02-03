using Raylib_cs;
using oops2d.Audio.Internal;
using oops2d.Core.Internal;
using oops2d.Core;

namespace oops2d.Audio
{
    public class Sound2D : IAudio
    {
        public Raylib_cs.Sound Data { get; private set; }

        public Sound2D(string path, float volume = 1, bool loop = false, bool playOnCreation = false, float pitch = 1)
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
            if (Scene == null) return;

            IsPlaying = true;
            Raylib.PlaySound(Data);
        }

        public override void Load(string path) {
            Data = Cache.Instance.LoadSound(path);
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

        public override void Update(Scene2D scene)
        {
            if (!IsPlaying) return;

            if (!Raylib.IsSoundPlaying(Data))
            {
                Stop();
                if (Loop)
                {
                    Play();
                }
            }

            base.Update(scene);
        }

        public override void Destroy(bool? unload = true)
        {
            if (unload == true)
            {
                Cache.Instance.UnloadSound(Data);
            }

            base.Destroy(unload);
        }

        public override void SetVolume(float volume)
        {
            Raylib.SetSoundVolume(Data, volume);
            base.SetVolume(volume);
        }

        public override void SetPitch(float pitch)
        {
            Raylib.SetSoundPitch(Data, pitch);
            base.SetPitch(pitch);
        }
    }
}
