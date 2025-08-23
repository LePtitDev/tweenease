namespace tweenease.Internal.Animations;

internal class TweenParallelAnimation : ITweenAnimation
{
    private readonly List<ITweenAnimation> _animations = new();

    public TweenParallelAnimation()
    {
        Animations = _animations.AsReadOnly();
    }

    public TimeSpan Duration => _animations.Select(a => a.Duration).DefaultIfEmpty(TimeSpan.Zero).Max();

    public IReadOnlyList<ITweenAnimation> Animations { get; }

    public void Add(ITweenAnimation animation)
    {
        _animations.Add(animation);
    }

    public void SetUp(TweenStateContext context)
    {
        if (_animations.Count == 0)
            throw new InvalidOperationException("No animation added in the group");

        var contexts = new ParallelContext[_animations.Count];
        for (var i = 0; i < contexts.Length; i++)
        {
            var subContext = new ParallelContext(context.Target);
            contexts[i] = subContext;
            _animations[i].SetUp(subContext);
        }

        context.Initialize(contexts, null);
    }

    public void Update(TweenStateContext context)
    {
        if (context.InitialState is not ParallelContext[] contexts)
            throw new InvalidOperationException("Animation must be set up");

        for (var i = 0; i < contexts.Length; i++)
        {
            var subContext = contexts[i];
            if (subContext.IsCompleted)
                continue;

            var animation = _animations[i];
            var isCompleted = context.Time >= animation.Duration;
            subContext.Time = isCompleted ? animation.Duration : context.Time;
            animation.Update(subContext);
            subContext.IsCompleted = isCompleted;
        }
    }

    private class ParallelContext : TweenStateContext
    {
        public ParallelContext(object? target)
            : base(target)
        {
        }

        public bool IsCompleted { get; set; }
    }
}
