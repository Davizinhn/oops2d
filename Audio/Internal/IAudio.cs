using oops2d.Core;

namespace oops2d.Audio.Internal
{
    public class IAudio : Object2D
    {
        public bool IsPlaying { get; set; }
        public bool Loop { get; private set; }
        public float Volume { get; private set; }
        public float Pitch { get; private set; }

        public virtual void Play() { }
        public virtual void Load(string path) { }
        public virtual void Stop() { }
        public virtual void Pause() { }
        public virtual void Resume() { }
        public override void Update(Scene2D scene) { base.Update(scene); }

        public virtual void SetVolume(float volume) {
            Volume = volume;
        }
        public virtual void SetPitch(float pitch) { 
            Pitch = pitch;
        }
        public virtual void SetLoop(bool loop) {
            Loop = loop;
        }
    }
}
