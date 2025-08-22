# tweenease

[![](https://img.shields.io/github/license/LePtitDev/tweenease)](https://github.com/LePtitDev/tweenease/blob/main/LICENSE) [![](https://github.com/LePtitDev/tweenease/actions/workflows/ci.yml/badge.svg)](https://github.com/LePtitDev/tweenease/actions)

Standalone C# library to make animations with tweens.

## How to use

```csharp
public class MyClass
{
    public double X { get; set; }
}

var animation = Tween.Sequence(
    Tween.Property<TestState, double>(s => s.X)
        .To(24, TimeSpan.FromSeconds(3))
        .To(12, TimeSpan.FromSeconds(3)))
    .Build();

var state = new MyClass { X = 8 };
var context = animation.CreateTimerContext(state, period: 100);
context.Start();
```
