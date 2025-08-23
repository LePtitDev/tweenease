namespace tweenease.Internal.Interpolators;

internal class TweenSingleInterpolator : TweenInterpolator<float>
{
    public override float Interpolate(double delta, float begin, float end)
    {
        return (float)((1 - delta) * begin + delta * end);
    }
}
