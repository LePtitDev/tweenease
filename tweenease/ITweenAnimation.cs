namespace tweenease;

public interface ITweenAnimation
{
    public TimeSpan Duration { get; }

    public void SetUp(TweenStateContext context);

    public void Update(TweenStateContext context);
}
