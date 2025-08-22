namespace tweenease;

public interface ITweenInterpolable<T>
{
    public T Interpolate(double delta, T end);
}
