// ReSharper disable PossibleNullReferenceException
namespace x3e
{
    public class ReactiveResult : IReactiveResult
    {
        internal readonly bool _condition;
        internal readonly object _value;

        internal ReactiveResult(bool condition, object value)
        {
            _condition = condition;
            _value = value;
        }

        public static implicit operator bool(ReactiveResult result) => result._condition;
    }
}