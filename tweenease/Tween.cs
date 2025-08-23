using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using tweenease.Internal.Animations;
using tweenease.Internal.Properties;

namespace tweenease;

public static class Tween
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyBuilder Property(Type targetType, string propertyName)
    {
        var info = targetType.GetProperty(propertyName) ?? throw new ArgumentException($"Cannot find '{propertyName}' in type '{targetType.FullName}'");
        var property = new TweenReflectionProperty(info);
        return new PropertyBuilder(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyBuilder<TValue> Property<TTarget, TValue>(Expression<Func<TTarget, TValue?>> expression)
        where TTarget : class
    {
        var property = new TweenExpressionProperty<TTarget, TValue?>(expression);
        return new PropertyBuilder<TValue>(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyBuilder Property(Type valueType, Func<object?, object?> getter, Action<object?, object?> setter)
    {
        var property = new TweenDelegateProperty(valueType, getter, setter);
        return new PropertyBuilder(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyBuilder Property(Type valueType, Func<object?> getter, Action<object?> setter)
    {
        var property = new TweenDelegateProperty(valueType, getter, setter);
        return new PropertyBuilder(property);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyBuilder<TValue> Property<TTarget, TValue>(Func<TTarget, TValue> getter, Action<TTarget, TValue> setter)
        where TTarget : class
    {
        var property = new TweenDelegateProperty(
            typeof(TValue),
            (object? t) => t is TTarget tar ? (object?)getter(tar) : throw new ArgumentException($"Target argument must be of type '{typeof(TTarget).FullName}'"),
            (object? t, object? o) =>
            {
                if (t is TTarget tar && o is TValue val)
                    setter(tar, val);
                else
                    throw new ArgumentException($"Target argument must be of type '{typeof(TTarget).FullName}' and value argument must ben of type '{typeof(TValue).FullName}'");
            });

        return new PropertyBuilder<TValue>(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyBuilder<TValue> Property<TTarget, TValue>(Func<TValue> getter, Action<TValue> setter)
    {
        var property = new TweenDelegateProperty(
            typeof(TValue),
            () => getter(),
            (object? o) =>
            {
                if (o is TValue val)
                    setter(val);
                else
                    throw new ArgumentException($"Value argument must ben of type '{typeof(TValue).FullName}'");
            });

        return new PropertyBuilder<TValue>(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SequenceBuilder Sequence(params Builder[] tweens)
    {
        return new SequenceBuilder(tweens);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParallelBuilder Parallel(params Builder[] tweens)
    {
        return new ParallelBuilder(tweens);
    }

    public abstract class Builder
    {
        protected internal Builder()
        {
        }

        public TimeSpan Duration { get; protected set; }

        public abstract ITweenAnimation Build();
    }

    public abstract class PropertyBuilderBase : Builder
    {
        protected internal PropertyBuilderBase(ITweenProperty property)
        {
            Property = property;
        }

        public ITweenProperty Property { get; }

        public ITweenInterpolator? Interpolator { get; protected set; }

        public Func<double, double> DefaultEasing { get; protected set; } = TweenEasing.Linear;

        public override ITweenAnimation Build()
        {
            var transitions = GetTransitions().ToArray();
            if (transitions.Length == 0)
                throw new InvalidOperationException("Transition(s) must be set");

            if (transitions.Length == 1)
            {
                var transition = transitions[0];
                return new TweenPropertyAnimation(transition.Duration, Property, transition.Transition, Interpolator)
                {
                    Easing = transition.Easing
                };
            }

            var sequence = new TweenSequenceAnimation();
            for (var i = 0; i < transitions.Length; i++)
            {
                var transition = transitions[i];
                sequence.Add(new TweenPropertyAnimation(transition.Duration, Property, transition.Transition, Interpolator)
                {
                    Easing = transition.Easing
                });
            }

            return sequence;
        }

        protected abstract IEnumerable<(ITweenTransition Transition, TimeSpan Duration, Func<double, double> Easing)> GetTransitions();
    }

    public class PropertyBuilder : PropertyBuilderBase
    {
        private readonly List<(object Val, TimeSpan Duration, Func<double, double>? Easing)> _values = new();
        private object? _initialValue;

        public PropertyBuilder(ITweenProperty property)
            : base(property)
        {
        }

        protected override IEnumerable<(ITweenTransition Transition, TimeSpan Duration, Func<double, double> Easing)> GetTransitions()
        {
            for (var i = 0; i < _values.Count; i++)
            {
                yield return (TweenTransition.To(_values[i].Val, i == 0 ? _initialValue : null), _values[i].Duration, _values[i].Easing ?? DefaultEasing);
            }
        }

        public PropertyBuilder From(double value)
        {
            _initialValue = value;
            return this;
        }

        public PropertyBuilder To(object value, TimeSpan duration, Func<double, double>? easing = null)
        {
            _values.Add((value, duration, easing));
            return this;
        }

        public PropertyBuilder Ease(Func<double, double> func)
        {
            DefaultEasing = func;
            return this;
        }
    }

    public class PropertyBuilder<T> : PropertyBuilderBase
    {
        private readonly List<(T Val, TimeSpan Duration, Func<double, double>? Easing)> _values = new();
        private T? _initialValue;

        public PropertyBuilder(ITweenProperty property)
            : base(property)
        {
        }

        protected override IEnumerable<(ITweenTransition Transition, TimeSpan Duration, Func<double, double> Easing)> GetTransitions()
        {
            for (var i = 0; i < _values.Count; i++)
            {
                yield return (TweenTransition.To(_values[i].Val, i == 0 ? _initialValue : null), _values[i].Duration, _values[i].Easing ?? DefaultEasing);
            }
        }

        public PropertyBuilder<T> From(T value)
        {
            _initialValue = value;
            return this;
        }

        public PropertyBuilder<T> To(T value, TimeSpan duration, Func<double, double>? easing = null)
        {
            _values.Add((value, duration, easing));
            return this;
        }

        public PropertyBuilder<T> Ease(Func<double, double> func)
        {
            DefaultEasing = func;
            return this;
        }
    }

    public sealed class SequenceBuilder : Builder
    {
        internal SequenceBuilder(Builder[] builders)
        {
            Builders = builders;
            Duration = builders.Aggregate(TimeSpan.Zero, (a, t) => a + t.Duration);
        }

        public Builder[] Builders { get; }

        public override ITweenAnimation Build()
        {
            var animation = new TweenSequenceAnimation();
            foreach (var sub in Builders)
            {
                animation.Add(sub.Build());
            }

            return animation;
        }
    }

    public sealed class ParallelBuilder : Builder
    {
        internal ParallelBuilder(Builder[] builders)
        {
            Builders = builders;
            Duration = builders.Max(t => t.Duration);
        }

        public Builder[] Builders { get; }

        public override ITweenAnimation Build()
        {
            var animation = new TweenParallelAnimation();
            foreach (var sub in Builders)
            {
                animation.Add(sub.Build());
            }

            return animation;
        }
    }
}
