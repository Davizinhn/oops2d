using Raylib_cs;
using oops2d.audio._internal;
using oops2d.core._internal;
using oops2d.core;

namespace oops2d.audio
{
    public class Music2D : IAudio
    {
        public Music Data { get; private set; }

        public Music2D(string path, float volume = 1, bool loop = false, bool playOnCreation = false, float pitch = 1)
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
            Raylib.PlayMusicStream(Data);
        }

        public override void Load(string path) {
            Data = Cache.Instance.LoadMusic(path);
        }

        public override void Stop() {
            if (!IsPlaying) return;

            IsPlaying = false;
            Raylib.StopMusicStream(Data);
        }

        public override void Pause() {
            if (!IsPlaying) return;

            IsPlaying = false;
            Raylib.PauseMusicStream(Data);
        }

        public override void Resume() {
            if (IsPlaying) return;

            IsPlaying = true;
            Raylib.ResumeMusicStream(Data);
        }

        public override void Update(Scene2D scene)
        {
            if (!IsPlaying) return;

            if (Raylib.GetMusicTimePlayed(Data) >= Raylib.GetMusicTimeLength(Data) - 0.05f && !Loop)
            {
                Stop();
            }
            Raylib.UpdateMusicStream(Data);

            base.Update(scene);
        }

        public override void Destroy(bool? unload = true)
        {
            if (unload == true)
            {
                Cache.Instance.UnloadMusic(Data);
            }

            base.Destroy(unload);
        }

        public override void SetVolume(float volume)
        {
            Raylib.SetMusicVolume(Data, volume);
            base.SetVolume(volume);
        }
        public override void SetPitch(float pitch)
        {
            Raylib.SetMusicPitch(Data, pitch);
            base.SetPitch(pitch);
        }
    }
}
