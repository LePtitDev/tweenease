namespace tweenease;

public abstract class TweenAnimationContext : ITweenAnimationContext
{
    private readonly TweenStateContext _tweenContext;
    private bool _isStarted;
    private bool _isRunning;

    protected TweenAnimationContext(ITweenAnimation animation, object? target)
    {
        _tweenContext = new TweenStateContext(target);
        Animation = animation;
    }

    public ITweenAnimation Animation { get; }

    public void Start()
    {
        if (_isRunning)
            return;

        if (!_isStarted)
        {
            _isStarted = true;
            _tweenContext.Time = TimeSpan.Zero;
            Animation.SetUp(_tweenContext);
        }

        _isRunning = true;
        OnStarted();
    }

    public void Stop()
    {
        if (!_isRunning)
            return;

        _isRunning = false;
        OnStopped();
    }

    public void Reset()
    {
        if (!_isStarted)
            return;

        _isStarted = false;
        _tweenContext.Time = TimeSpan.Zero;

        if (_isRunning)
        {
            _isRunning = false;
            OnStopped();
        }

        OnReset();
    }

    protected void Update(TimeSpan time)
    {
        _tweenContext.Time = time;
        Animation.Update(_tweenContext);
    }

    protected abstract void OnStarted();

    protected abstract void OnStopped();

    protected abstract void OnReset();
}
