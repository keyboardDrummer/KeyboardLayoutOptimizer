using System;

namespace Generic.Containers.Maybes
{
    public interface IMaybe<out T>
    {
        bool IsJust { get; }
        T FromJust { get; }

        bool IsNothing { get; }

        R Eval<R>(R nothing, Func<T, R> just);
        void Exec(Action<T> just);
        void Exec(Action nothing, Action<T> just);
        Maybe<R> Map<R>(Func<T, R> func);

        Maybe<B> Select<B>(Func<T, B> func);
    }
}