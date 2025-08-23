using tweenease.tests.Helpers;

namespace tweenease.tests;

public class TweenTests
{
    [Test]
    public void Test_01()
    {
        var state = new TestState { X = 8 };
        var context = Tween.Sequence(
            Tween.Property<TestState, double>(s => s.X)
                .To(24, TimeSpan.FromSeconds(3))
                .To(12, TimeSpan.FromSeconds(3)))
            .Build()
            .CreateTestContext(state);

        context.Start();

        context.Time += TimeSpan.FromSeconds(3);
        context.Update();

        Assert.That(state.X, Is.EqualTo(24).Within(0.01));

        context.Time += TimeSpan.FromSeconds(3);
        context.Update();

        Assert.That(state.X, Is.EqualTo(12).Within(0.01));
    }

    [Test]
    [Ignore("This test is only made for manual test")]
    public async Task Test_02()
    {
        var state = new TestState { X = 8 };
        var context = Tween.Sequence(
            Tween.Property<TestState, double>(s => s.X)
                .To(24, TimeSpan.FromSeconds(1))
                .To(12, TimeSpan.FromSeconds(1)))
            .Build()
            .CreateTimerContext(state, 100);

        context.Start();

        await Task.Delay(1000);

        Assert.That(state.X, Is.EqualTo(24).Within(0.5));

        await Task.Delay(1000);

        Assert.That(state.X, Is.EqualTo(12).Within(0.5));
    }

    [Test]
    public void Test_03()
    {
        var state = new TestState { X = 8 };
        var context = Tween.Sequence(
            Tween.Property<TestState, string>(s => s.Str)
                .To("my text.", TimeSpan.FromSeconds(3))
                .To("", TimeSpan.FromSeconds(3)))
            .Build()
            .CreateTestContext(state);

        context.Start();

        context.Time += TimeSpan.FromSeconds(3);
        context.Update();

        Assert.That(state.Str, Is.EqualTo("my text."));

        context.Time += TimeSpan.FromSeconds(3);
        context.Update();

        Assert.That(state.Str, Is.EqualTo(""));
    }

    private class TestState
    {
        public double X { get; set; }

        public double Y { get; set; }

        public string Str { get; set; } = "";
    }
}
