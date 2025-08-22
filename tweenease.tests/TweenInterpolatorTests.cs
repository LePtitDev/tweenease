namespace tweenease.tests;

public class TweenInterpolatorTests
{
    [TestCase(0.5, 0.5, 0, 1)]
    [TestCase(0.5, 1, 0, 2)]
    public void Test_InterpolateDouble(double delta, double expected, double begin, double end)
    {
        var value = TweenInterpolator<double>.Default.Interpolate(delta, begin, end);
        Assert.That(value, Is.EqualTo(expected).Within(0.00001));
    }
}
