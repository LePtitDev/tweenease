namespace tweenease;

public class TweenStateContext
{
    public TweenStateContext(object? target)
    {
        Target = target;
    }

    public object? Target { get; }

    public TimeSpan Time { get; set; }

    public object? InitialState { get; set; }

    public object? TransitionTarget { get; set; }
}
