namespace tweenease.tests.Mocks;

internal class TweenMockAnimationContext : TweenAnimationContext
{
    public TweenMockAnimationContext(ITweenAnimation animation, object? target)
        : base(animation, target)
    {
    }

    public TimeSpan Time { get; set; }

    public void Update() => Update(Time);

    protected override void OnStarted()
    {
    }

    protected override void OnStopped()
    {
    }

    protected override void OnReset()
    {
        Time = TimeSpan.Zero;
    }
}
