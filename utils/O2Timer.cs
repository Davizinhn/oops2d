// Heavily inspired by FlxTimer.hx from HaxeFlixel (https://github.com/HaxeFlixel/flixel/blob/master/flixel/util/FlxTimer.hx)
namespace oops2d.utils
{
    public class O2Timer
    {
        public float time { get; private set; }
        public bool looped = false;
        public Action<O2Timer>? onComplete;

        public bool active { get; private set; }
        public bool finished { get; private set; }
        int loops = 0;

        int timeInMillisseconds
        {
            get
            {
                return (int)(time * 1000);
            }
        }
        CancellationTokenSource? cancellationTokenSource = null;

        public O2Timer(float time, Action<O2Timer> onComplete, bool looped = false)
        {
            this.looped = looped;
            this.onComplete = onComplete;
            this.time = time;
            active = false;
            finished = false;
        }

        public void Start()
        {
            if (active) return;

            active = true;
            finished = false;
            _ = RunAsync();
        }

        async Task RunAsync()
        {
            if (!active) return;

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(timeInMillisseconds, cancellationTokenSource.Token);
                OnTimerFinished();
            }
            catch (TaskCanceledException)
            {
                // Timer cancelled
            }
        }

        void OnTimerFinished()
        {
            if (finished) return;

            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;

            finished = true;
            active = false;

            if (onComplete != null)
            {
                onComplete.Invoke(this);
            }

            if (looped)
            {
                loops++;
                Start();
            }
        }

        public void Stop(bool cancelCallback = true)
        {
            if (finished) return;
            if (!active) return;

            cancellationTokenSource?.Cancel();

            finished = true;
            active = false;

            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;

            if (onComplete != null && !cancelCallback)
            {
                onComplete.Invoke(this);
            }
        }

    }
}
