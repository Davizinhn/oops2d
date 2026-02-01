namespace oops2d.Audio.Internal
{
    public class IAudio
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

        public void SetVolume(float volume) { 
            Volume = volume;    
        }
        public void SetPitch(float pitch) { 
            Pitch = pitch;
        }
        public void SetLoop(bool loop) {
            Loop = loop;
        }
    }
}
