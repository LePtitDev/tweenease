namespace tweenease;

public class TweenStateContext
{
    public TweenStateContext(object? target)
    {
        Target = target;
    }

    public object? Target { get; }

    public TimeSpan Time { get; set; }

    public object? InitialState { get; private set; }

    public object? TransitionTarget { get; private set; }

    public bool Initialized { get; private set; }

    public void Initialize(object? initialState, object? transitionTarget)
    {
        InitialState = initialState;
        TransitionTarget = transitionTarget;
        Initialized = true;
    }
}
