namespace tweenease.Internal.Animations;

internal class TweenPropertyAnimation : ITweenAnimation
{
    public TweenPropertyAnimation(
        TimeSpan duration,
        ITweenProperty property,
        ITweenTransition transition,
        ITweenInterpolator? interpolator = null)
    {
        Duration = duration;
        Property = property;
        Transition = transition;
        Interpolator = interpolator ?? TweenInterpolator.GetDefault(property.Type);
    }

    public TimeSpan Duration { get; }

    public ITweenProperty Property { get; }

    public ITweenTransition Transition { get; }

    public ITweenInterpolator Interpolator { get; }

    public Func<double, double> Easing { get; set; } = TweenEasing.Linear;

    public void SetUp(TweenStateContext context)
    {
        context.InitialState = Transition.GetSourceValue() ?? Property.Get(context.Target) ?? throw new NotSupportedException("Null value is not supported");
        context.TransitionTarget = Transition.GetTargetValue(context.InitialState) ?? throw new NotSupportedException("Null value is not supported");
    }

    public void Update(TweenStateContext context)
    {
        var initialValue = context.InitialState ?? throw new InvalidOperationException("Tween context must be set up");
        var transition = context.TransitionTarget ?? throw new InvalidOperationException("Tween context must be set up");

        var delta = Math.Clamp(context.Time / Duration, 0, 1);
        if (Easing is not null)
            delta = Math.Clamp(Easing(delta), 0, 1);

        var currentValue = Interpolator.Interpolate(delta, initialValue, transition);
        Property.Set(context.Target, currentValue);
    }
}
