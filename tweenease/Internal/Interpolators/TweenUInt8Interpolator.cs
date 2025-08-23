namespace tweenease.Internal.Interpolators;

internal class TweenUInt8Interpolator : TweenInterpolator<byte>
{
    public override byte Interpolate(double delta, byte begin, byte end)
    {
        return (byte)Math.Clamp((1 - delta) * begin + delta * end, byte.MinValue, byte.MaxValue);
    }
}
