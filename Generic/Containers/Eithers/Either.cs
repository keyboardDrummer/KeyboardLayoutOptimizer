using System;
using Generic.Common;

namespace Generic.Containers.Eithers
{
    internal abstract class Either<A, B>
    {
        public static Either<A, B> Create(A a)
        {
            return new Left<A, B>(a);
        }

        public static Either<A, B> Create(B b)
        {
            return new Right<A, B>(b);
        }

        public abstract T Eval<T>(Func<A, T> left, Func<B, T> right);

        public void Exec(Action<A> left, Action<B> right)
        {
            Eval(a =>
            {
                left(a);
                return Unit.Null;
            }, b =>
            {
                right(b);
                return Unit.Null;
            });
        }
    }
}
