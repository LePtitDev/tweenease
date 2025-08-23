namespace tweenease.Internal.Interpolators;

internal class TweenUInt16Interpolator : TweenInterpolator<ushort>
{
    public override ushort Interpolate(double delta, ushort begin, ushort end)
    {
        return (ushort)Math.Clamp((1 - delta) * begin + delta * end, ushort.MinValue, ushort.MaxValue);
    }
}
