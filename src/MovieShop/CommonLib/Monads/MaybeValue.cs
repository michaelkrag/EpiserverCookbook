using System;

namespace CommonLib.Monads
{
    public class MaybeValue<T> : Maybe<T>
    {
        private readonly T Value;

        private MaybeValue(T value)
        {
            Value = value;
        }

        public override Maybe<TOut> Map<TOut>(Func<T, TOut> func)
        {
            return func(Value).ToMaybe();
        }

        public override Maybe<TOut> Map<TOut>(Func<T, Maybe<TOut>> func)
        {
            return func(Value);
        }

        public override T ValueOr(T fallbackValue)
        {
            return Value;
        }

        public static Maybe<T> Create(T value)
        {
            return value != null ? new MaybeValue<T>(value) : Maybe<T>.None;
        }
    }
}