namespace tweenease.Internal.Interpolators;

internal class TweenInt64Interpolator : TweenInterpolator<long>
{
    public override long Interpolate(double delta, long begin, long end)
    {
        return (long)Math.Clamp((1 - delta) * begin + delta * end, long.MinValue, long.MaxValue);
    }
}
