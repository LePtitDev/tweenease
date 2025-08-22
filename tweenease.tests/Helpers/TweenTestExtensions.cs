using tweenease.tests.Mocks;

namespace tweenease.tests.Helpers;

internal static class TweenTestExtensions
{
    public static TweenMockAnimationContext CreateTestContext(this ITweenAnimation animation, object? target) => new TweenMockAnimationContext(animation, target);
}
