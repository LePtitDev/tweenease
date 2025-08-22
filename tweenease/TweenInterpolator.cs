using System.Reflection;
using tweenease.Internal.Interpolators;

namespace tweenease;

public abstract class TweenInterpolator : ITweenInterpolator
{
    internal static readonly Dictionary<Type, ITweenInterpolator> NativeInterpolators = new()
    {
        { typeof(double), new TweenDoubleInterpolator() },
    };

    public abstract object Interpolate(double delta, object begin, object end);

    public static ITweenInterpolator GetDefault(Type type)
    {
        var genericType = typeof(TweenInterpolator<>).MakeGenericType(type);
        var property = genericType.GetProperty(nameof(TweenInterpolator<object>.Default), BindingFlags.Public | BindingFlags.Static);
        if (property == null)
            throw new InvalidOperationException("Cannot find Default static property");

        return (ITweenInterpolator)property.GetValue(null)!;
    }
}

public abstract class TweenInterpolator<T> : ITweenInterpolator where T : notnull
{
    private static readonly Lazy<ITweenInterpolator> _default = new(GetDefaultInterpolator);

    public static ITweenInterpolator Default => _default.Value;

    public abstract T Interpolate(double delta, T begin, T end);

    object ITweenInterpolator.Interpolate(double delta, object begin, object end)
    {
        return Interpolate(delta,
            begin is T a ? a : throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'"),
            end is T b ? b : throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'"));
    }

    private static ITweenInterpolator GetDefaultInterpolator()
    {
        if (TweenInterpolator.NativeInterpolators.TryGetValue(typeof(T), out var interpolator))
            return interpolator;

        if (!typeof(ITweenInterpolable<T>).IsAssignableFrom(typeof(T)))
            throw new NotSupportedException($"Type '{typeof(T).FullName}' does not implement '{nameof(ITweenInterpolable<T>)}'");

        return new TweenDelegateInterpolator<T>();
    }
}
