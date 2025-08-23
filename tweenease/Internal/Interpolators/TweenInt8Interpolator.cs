namespace tweenease.Internal.Interpolators;

internal class TweenInt8Interpolator : TweenInterpolator<sbyte>
{
    public override sbyte Interpolate(double delta, sbyte begin, sbyte end)
    {
        return (sbyte)Math.Clamp((1 - delta) * begin + delta * end, sbyte.MinValue, sbyte.MaxValue);
    }
}
