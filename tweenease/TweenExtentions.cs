using tweenease.Internal.Contexts;

namespace tweenease;

public static class TweenExtentions
{
    public static ITweenAnimationContext CreateTimerContext(this ITweenAnimation animation, object? target, int period)
    {
        return new TweenTimerAnimationContext(animation, target, TimeSpan.FromMilliseconds(period));
    }

    public static ITweenAnimationContext CreateTimerContext(this ITweenAnimation animation, object? target, TimeSpan period)
    {
        return new TweenTimerAnimationContext(animation, target, period);
    }
}
