namespace tweenease.tests;

public class TweenInterpolatorTests
{
    [TestCase(0.5, 10, 0, 20)]
    [TestCase(0.5, 5, 0, 10)]
    public void Test_InterpolateInt32(double delta, int expected, int begin, int end)
    {
        var value = TweenInterpolator<int>.Default.Interpolate(delta, begin, end);
        Assert.That(value, Is.EqualTo(expected));
    }

    [TestCase(0.5, 0.5, 0, 1)]
    [TestCase(0.5, 1, 0, 2)]
    public void Test_InterpolateDouble(double delta, double expected, double begin, double end)
    {
        var value = TweenInterpolator<double>.Default.Interpolate(delta, begin, end);
        Assert.That(value, Is.EqualTo(expected).Within(0.00001));
    }

    [TestCase(0.5, "my t", "", "my text.")]
    [TestCase(0.5, "my t", "my text.", "")]
    public void Test_InterpolateString(double delta, string expected, string begin, string end)
    {
        var value = TweenInterpolator<string>.Default.Interpolate(delta, begin, end);
        Assert.That(value, Is.EqualTo(expected));
    }
}
