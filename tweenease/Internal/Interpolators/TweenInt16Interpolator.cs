namespace tweenease.Internal.Interpolators;

internal class TweenInt16Interpolator : TweenInterpolator<short>
{
    public override short Interpolate(double delta, short begin, short end)
    {
        return (short)Math.Clamp((1 - delta) * begin + delta * end, short.MinValue, short.MaxValue);
    }
}
