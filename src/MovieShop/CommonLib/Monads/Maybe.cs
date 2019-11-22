using System;

namespace CommonLib.Monads
{
    public abstract class Maybe<T>
    {
        public abstract Maybe<TOut> Map<TOut>(Func<T, TOut> func);

        public abstract Maybe<TOut> Map<TOut>(Func<T, Maybe<TOut>> func);

        public abstract T ValueOr(T fallbackValue);

        public static Maybe<T> None => MaybeNone<T>.Create();
    }
}