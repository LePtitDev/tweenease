namespace tweenease.Internal.Interpolators;

internal class TweenDoubleInterpolator : TweenInterpolator<double>
{
    public override double Interpolate(double delta, double begin, double end)
    {
        return (1 - delta) * begin + delta * end;
    }
}
