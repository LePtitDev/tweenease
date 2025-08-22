namespace tweenease;

public interface ITweenProperty
{
    public Type Type { get; }

    public object? Get(object? target);

    public void Set(object? target, object? value);
}
