namespace tweenease.Internal.Transitions;

internal static class TweenDecimalTransition
{
    public static ITweenTransition Add(sbyte delta) => new TweenTransition<sbyte>(a => (sbyte)(a + delta));

    public static ITweenTransition Add(byte delta) => new TweenTransition<byte>(a => (byte)(a + delta));

    public static ITweenTransition Add(short delta) => new TweenTransition<short>(a => (short)(a + delta));

    public static ITweenTransition Add(ushort delta) => new TweenTransition<ushort>(a => (ushort)(a + delta));

    public static ITweenTransition Add(int delta) => new TweenTransition<int>(a => a + delta);

    public static ITweenTransition Add(uint delta) => new TweenTransition<uint>(a => a + delta);

    public static ITweenTransition Add(long delta) => new TweenTransition<long>(a => a + delta);

    public static ITweenTransition Add(ulong delta) => new TweenTransition<ulong>(a => a + delta);

    public static ITweenTransition Add(float delta) => new TweenTransition<float>(a => a + delta);

    public static ITweenTransition Add(double delta) => new TweenTransition<double>(a => a + delta);

    public static ITweenTransition Scale(sbyte delta) => new TweenTransition<sbyte>(a => (sbyte)(a * delta));

    public static ITweenTransition Scale(byte delta) => new TweenTransition<byte>(a => (byte)(a * delta));

    public static ITweenTransition Scale(short delta) => new TweenTransition<short>(a => (short)(a * delta));

    public static ITweenTransition Scale(ushort delta) => new TweenTransition<ushort>(a => (ushort)(a * delta));

    public static ITweenTransition Scale(int delta) => new TweenTransition<int>(a => a * delta);

    public static ITweenTransition Scale(uint delta) => new TweenTransition<uint>(a => a * delta);

    public static ITweenTransition Scale(long delta) => new TweenTransition<long>(a => a * delta);

    public static ITweenTransition Scale(ulong delta) => new TweenTransition<ulong>(a => a * delta);

    public static ITweenTransition Scale(float delta) => new TweenTransition<float>(a => a * delta);

    public static ITweenTransition Scale(double delta) => new TweenTransition<double>(a => a * delta);
}
