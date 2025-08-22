namespace tweenease.Internal.Interpolators;

internal class TweenDelegateInterpolator<T> : TweenInterpolator<T> where T : notnull
{
    public override T Interpolate(double delta, T begin, T end)
    {
        return ((ITweenInterpolable<T>)begin!).Interpolate(delta, end);
    }
}
