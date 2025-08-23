namespace tweenease.Internal.Interpolators;

internal class TweenInt32Interpolator : TweenInterpolator<int>
{
    public override int Interpolate(double delta, int begin, int end)
    {
        return (int)Math.Clamp((1 - delta) * begin + delta * end, int.MinValue, int.MaxValue);
    }
}
