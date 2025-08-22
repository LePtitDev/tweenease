using System.Diagnostics;

namespace tweenease.Internal.Contexts;

internal class TweenTimerAnimationContext : TweenAnimationContext
{
    private readonly Timer _timer;
    private readonly Stopwatch _stopwatch = new();

    public TweenTimerAnimationContext(ITweenAnimation animation, object? target, TimeSpan period)
        : base(animation, target)
    {
        _timer = new Timer(OnTimer);
        Period = period;
    }

    public TimeSpan Period { get; }

    protected override void OnStarted()
    {
        _stopwatch.Start();
        _timer.Change(Period, Period);
    }

    protected override void OnStopped()
    {
        _stopwatch.Stop();
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    protected override void OnReset()
    {
        _stopwatch.Reset();
    }

    private void OnTimer(object? state)
    {
        Update(_stopwatch.Elapsed);
    }
}
