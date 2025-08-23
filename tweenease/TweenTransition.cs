namespace tweenease;

public class TweenTransition : ITweenTransition
{
    private readonly Func<object?, object?> _func;
    private readonly object? _initialValue;

    public TweenTransition(Func<object?, object?> func, object? initialValue = null)
    {
        _func = func;
        _initialValue = initialValue;
    }

    public object? GetSourceValue() => _initialValue;

    public object? GetTargetValue(object? initialValue) => _func(initialValue);

    public static ITweenTransition To(object? value, object? initialValue = null) => new TweenTransition(_ => value, initialValue);

    public static ITweenTransition To<T>(T? value) => new TweenTransition<T>(_ => value);

    public static ITweenTransition To<T>(T? value, T? initialValue) => new TweenTransition<T>(_ => value, initialValue);
}

public class TweenTransition<T> : ITweenTransition
{
    private readonly Func<T?, T?> _func;
    private readonly T? _initialValue;
    private readonly bool _hasInitialValue;

    public TweenTransition(Func<T?, T?> func)
    {
        _func = func;
        _initialValue = default;
        _hasInitialValue = false;
    }
    
    public TweenTransition(Func<T?, T?> func, T? initialValue)
    {
        _func = func;
        _initialValue = initialValue;
        _hasInitialValue = true;
    }

    public object? GetSourceValue() => _hasInitialValue ? _initialValue : null;

    public object? GetTargetValue(object? initialValue)
    {
        if (initialValue is null)
        {
            if (typeof(T).IsValueType)
                throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'");

            return _func(default);
        }

        return _func(initialValue is T val ? val : throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'"));
    }

    public static ITweenTransition To(T? value) => new TweenTransition<T>(_ => value);
}
