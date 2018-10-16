// ReSharper disable PossibleNullReferenceException
namespace EtherReactorSimulator
{
    using System;

    public static class Extension
    {
        public static IReactiveResult If<T, S>(this T a, S b) => new ReactiveResult(a.Equals(b), a);
        public static IReactiveResult More(this float a, float b) => new ReactiveResult(a > b, a);
        public static IReactiveResult Less(this float a, float b) => new ReactiveResult(a < b, a);

        public static IReactiveResult Then<E>(this IReactiveResult result, E value)
        {
            if (result is ReactiveResult v && v._b)
                return new ReactiveResult(true, value);
            return result;
        }

        public static IReactiveResult Else<E>(this IReactiveResult result, E value)
        {
            if (result is ReactiveResult v && !v._b)
                return new ReactiveResult(false, value);
            return new ReactiveResult(false, (result as ReactiveResult)._value);
        }
        public static W Value<W>(this IReactiveResult result) => (W)Convert.ChangeType((result as ReactiveResult)._value, typeof(W));
    }

    public interface IReactiveResult
    {
        
    }

    public class ReactiveResult : IReactiveResult
    {
        internal readonly bool _b;
        internal readonly object _value;

        internal ReactiveResult(bool b, object value)
        {
            _b = b;
            _value = value;
        }
    }
}