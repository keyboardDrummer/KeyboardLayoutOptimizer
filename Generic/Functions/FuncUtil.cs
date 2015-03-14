using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic.Functions
{
    public static class FuncUtil
    {
        public delegate Boolean Equals<in A>(A a, A b);

        public static Equals<A> ObjectEquality<A>()
        {
            return (a, b) => a.Equals(b);
        }

        public static Func<A, A> Identity<A>()
        {
            return x => x;
        }

        public static T FixPoint<T>(Func<Func<T>,T> func)
        {
            return func(() => FixPoint(func));
        }
    }
}
