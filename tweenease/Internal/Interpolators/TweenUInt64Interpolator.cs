namespace tweenease.Internal.Interpolators;

internal class TweenUInt64Interpolator : TweenInterpolator<ulong>
{
    public override ulong Interpolate(double delta, ulong begin, ulong end)
    {
        return (ulong)Math.Clamp((1 - delta) * begin + delta * end, ulong.MinValue, ulong.MaxValue);
    }
}
