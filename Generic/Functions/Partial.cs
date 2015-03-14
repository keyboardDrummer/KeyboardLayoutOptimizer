using System;

namespace Generic.Functions
{
    public static class Partial
    {        
        public static Func<B,R> Apply<A, B, R>(this Func<A, B, R> f, A a)
        {
            return b => f(a, b);
        }
    }
}
