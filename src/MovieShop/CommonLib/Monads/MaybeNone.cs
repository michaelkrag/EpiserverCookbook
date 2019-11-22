using System;

namespace CommonLib.Monads
{
    public class MaybeNone<T> : Maybe<T>
    {
        private MaybeNone()
        {
        }

        public override Maybe<TOut> Map<TOut>(Func<T, TOut> func)
        {
            return Maybe<TOut>.None;
        }

        public override Maybe<TOut> Map<TOut>(Func<T, Maybe<TOut>> func)
        {
            return Maybe<TOut>.None;
        }

        public override T ValueOr(T fallbackValue)
        {
            return fallbackValue;
        }

        public static Maybe<T> Create()
        {
            return new MaybeNone<T>();
        }
    }
}