using System;

namespace Generic.Containers.Eithers
{
    class Right<A,B> : Either<A,B>
    {
        B b;

        public Right(B b)
        {
            this.b = b;
        }

        public override T Eval<T>(Func<A, T> left, Func<B, T> right)
        {
            return right(b);
        }
    }
}
