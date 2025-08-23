namespace tweenease.Internal.Interpolators;

internal class TweenUInt32Interpolator : TweenInterpolator<uint>
{
    public override uint Interpolate(double delta, uint begin, uint end)
    {
        return (uint)Math.Clamp((1 - delta) * begin + delta * end, uint.MinValue, uint.MaxValue);
    }
}
