namespace tweenease;

public interface ITweenInterpolator
{
    public object? Interpolate(double delta, object? begin, object? end);
}
