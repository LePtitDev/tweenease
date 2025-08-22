namespace tweenease;

public class TweenTransition : ITweenTransition
{
    private readonly Func<object, object> _func;
    private readonly object? _initialValue;

    public TweenTransition(Func<object, object> func, object? initialValue = null)
    {
        _func = func;
        _initialValue = initialValue;
    }

    public object? GetSourceValue() => _initialValue;

    public object GetTargetValue(object initialValue) => _func(initialValue);

    public static ITweenTransition To(object value, object? initialValue = null) => new TweenTransition(_ => value, initialValue);

    public static ITweenTransition To<T>(T value, T? initialValue = null) where T : struct => new TweenTransition<T>(_ => value, initialValue);
}

public class TweenTransition<T> : ITweenTransition where T : struct
{
    private readonly Func<T, T> _func;
    private readonly T? _initialValue;

    public TweenTransition(Func<T, T> func, T? initialValue = null)
    {
        _func = func;
        _initialValue = initialValue;
    }

    public object? GetSourceValue() => _initialValue;

    public object GetTargetValue(object initialValue) => _func(initialValue is T val ? val : throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'"));

    public static ITweenTransition To(T value) => new TweenTransition<T>(_ => value);
}
