namespace tweenease.Internal.Animations;

internal class TweenSequenceAnimation : ITweenAnimation
{
    private readonly List<ITweenAnimation> _animations = new();

    public TweenSequenceAnimation()
    {
        Animations = _animations.AsReadOnly();
    }

    public TimeSpan Duration => _animations.Aggregate(TimeSpan.Zero, (total, anim) => total + anim.Duration);

    public IReadOnlyList<ITweenAnimation> Animations { get; }

    public void Add(ITweenAnimation animation) => _animations.Add(animation);

    public void SetUp(TweenStateContext context)
    {
        if (_animations.Count == 0)
            throw new InvalidOperationException("No animation added in the sequence");

        var sequenceContext = new SequenceContext(context.Target);
        context.InitialState = sequenceContext;
        _animations[0].SetUp(sequenceContext);
    }

    public void Update(TweenStateContext context)
    {
        if (context.InitialState is not SequenceContext sequenceContext)
            throw new InvalidOperationException("Animation must be set up");

        if (sequenceContext.Index == _animations.Count)
            return;

        var totalDuration = TimeSpan.Zero;
        for (var i = 0; i < _animations.Count; i++)
        {
            var animation = _animations[i];
            if (totalDuration + animation.Duration < context.Time)
            {
                totalDuration += animation.Duration;
                continue;
            }

            if (sequenceContext.Index < i)
            {
                var previousAnimation = _animations[i - 1];
                sequenceContext.Time = previousAnimation.Duration;
                previousAnimation.Update(sequenceContext);
                sequenceContext.Index = i;
                animation.SetUp(sequenceContext);
            }

            sequenceContext.Time = context.Time - totalDuration;
            animation.Update(sequenceContext);
            return;
        }

        var lastIndex = _animations.Count - 1;
        var lastAnimation = _animations[lastIndex];
        if (sequenceContext.Index != lastIndex)
            lastAnimation.SetUp(sequenceContext);

        sequenceContext.Time = lastAnimation.Duration;
        lastAnimation.Update(sequenceContext);
        sequenceContext.Index = _animations.Count;
    }

    private class SequenceContext : TweenStateContext
    {
        public SequenceContext(object? target)
            : base(target)
        {
        }

        public int Index { get; set; }
    }
}
