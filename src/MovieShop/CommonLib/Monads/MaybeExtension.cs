namespace CommonLib.Monads
{
    public static partial class MaybeExtension
    {
        public static Maybe<T> ToMaybe<T>(this T value) => value != null ? MaybeValue<T>.Create(value) : Maybe<T>.None;
    }
}