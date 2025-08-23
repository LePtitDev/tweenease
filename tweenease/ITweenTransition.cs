namespace tweenease;

public interface ITweenTransition
{
    public object? GetSourceValue();

    public object? GetTargetValue(object? initialValue);
}
