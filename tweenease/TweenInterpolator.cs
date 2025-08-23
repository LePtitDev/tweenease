using System.Reflection;
using tweenease.Internal.Interpolators;

namespace tweenease;

public abstract class TweenInterpolator : ITweenInterpolator
{
    internal static readonly Dictionary<Type, ITweenInterpolator> NativeInterpolators = new()
    {
        { typeof(bool), new TweenBooleanInterpolator() },
        { typeof(sbyte), new TweenInt8Interpolator() },
        { typeof(byte), new TweenUInt8Interpolator() },
        { typeof(short), new TweenInt16Interpolator() },
        { typeof(ushort), new TweenUInt16Interpolator() },
        { typeof(int), new TweenInt32Interpolator() },
        { typeof(uint), new TweenUInt32Interpolator() },
        { typeof(long), new TweenInt64Interpolator() },
        { typeof(ulong), new TweenUInt64Interpolator() },
        { typeof(double), new TweenDoubleInterpolator() },
        { typeof(string), new TweenStringInterpolator() },
    };

    public abstract object? Interpolate(double delta, object? begin, object? end);

    public static ITweenInterpolator GetDefault(Type type)
    {
        var genericType = typeof(TweenInterpolator<>).MakeGenericType(type);
        var property = genericType.GetProperty(nameof(TweenInterpolator<object>.Default), BindingFlags.Public | BindingFlags.Static);
        if (property == null)
            throw new InvalidOperationException("Cannot find Default static property");

        return (ITweenInterpolator)property.GetValue(null)!;
    }
}

public abstract class TweenInterpolator<T> : ITweenInterpolator
{
    private static readonly Lazy<ITweenInterpolator> _default = new(GetDefaultInterpolator);

    public static ITweenInterpolator Default => _default.Value;

    public abstract T? Interpolate(double delta, T? begin, T? end);

    object? ITweenInterpolator.Interpolate(double delta, object? begin, object? end)
    {
        T? a;
        if (begin is null)
        {
            if (typeof(T).IsValueType)
                throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'");

            a = default;
        }
        else
        {
            if (begin is not T val)
                throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'");

            a = val;
        }

        T? b;
        if (end is null)
        {
            if (typeof(T).IsValueType)
                throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'");

            b = default;
        }
        else
        {
            if (end is not T val)
                throw new ArgumentException($"Value must be of type '{typeof(T).FullName}'");

            b = val;
        }

        return Interpolate(delta, a, b);
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
