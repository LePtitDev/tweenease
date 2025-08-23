namespace tweenease.Internal.Interpolators;

internal class TweenBooleanInterpolator : TweenInterpolator<bool>
{
    public override bool Interpolate(double delta, bool begin, bool end)
    {
        return delta >= 0.5;
    }
}
