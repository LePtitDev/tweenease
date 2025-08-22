namespace tweenease;

public abstract class TweenProperty : ITweenProperty
{
    protected TweenProperty(Type type)
    {
        Type = type;
    }

    public Type Type { get; }

    public abstract object? Get(object? target);

    public abstract void Set(object? target, object? value);
}

public abstract class TweenProperty<T> : ITweenProperty where T : notnull
{
    Type ITweenProperty.Type => typeof(T);

    public abstract T Get(object? target);

    public abstract void Set(object? target, T value);

    object? ITweenProperty.Get(object? target) => Get(target);

    void ITweenProperty.Set(object? target, object? value) => Set(target, value is T x ? x : throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'"));
}
