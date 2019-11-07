// ReSharper disable PossibleNullReferenceException
namespace x3e
{
    using System;
    using System.Drawing;
    using Pastel;
    using Troschuetz.Random.Generators;

    public static class Extension
    {
        public static IReactiveResult If<T, S>(this T a, S b) => new ReactiveResult(a.Equals(b), a);
        public static IReactiveResult More(this float a, float b) => new ReactiveResult(a > b, a);
        public static IReactiveResult Less(this float a, float b) => new ReactiveResult(a < b, a);

        public static IReactiveResult Then<E>(this IReactiveResult result, E value)
        {
            if (result is ReactiveResult v && v)
                return new ReactiveResult(true, value);
            return result;
        }

        public static IReactiveResult Else<E>(this IReactiveResult result, E value)
        {
            if (result is ReactiveResult v && !v)
                return new ReactiveResult(false, value);
            return new ReactiveResult(false, (result as ReactiveResult)._value);
        }

        public static float Fixed(this float value, int round) => (float) Math.Round(value, round);
        public static float At(this float a, float b) => (float)new NR3Generator().NextDouble(Math.Abs(a), Math.Abs(b));
        public static float Normalize(this float a, float b) => (a - b).At(a + b);
        public static W Value<W>(this IReactiveResult result) => (W)Convert.ChangeType((result as ReactiveResult)._value, typeof(W));

        public static string To(this string s, Color color) => s.Pastel(color);
    }
}