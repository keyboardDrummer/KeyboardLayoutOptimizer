using System;

namespace Generic.Containers.Eithers
{
    class Left<A,B> : Either<A,B>
    {
        A a;

        public Left(A a)
        {
            this.a = a;
        }

        public override T Eval<T>(Func<A, T> left, Func<B, T> right)
        {
            return left(a);
        }
    }
}
