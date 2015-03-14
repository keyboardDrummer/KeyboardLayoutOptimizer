using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Maybes
{
    public static class MaybeUtil
    {
        public static Maybe<U> MaybeCast<U>(this object value) where U : class
        {
            var maybeCast = value as U;
            return maybeCast == null ? Nothing<U>() : Just(maybeCast);
        }

        public static IEnumerable<T> SkipNothing<T>(this IEnumerable<Maybe<T>> enumerable)
        {
            return enumerable.Where(i => i.IsJust).Select(i => i.FromJust);
        }

        public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.FirstOrDefault().FromNull();
        }

        public static Maybe<T> ToIdentity<T>(this T value)
        {
            return Just(value);
        }

        public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> id, Func<T, Maybe<U>> k, Func<T, U, V> s)
        {
            return id.Eval(Nothing<V>(), t => k(t).Select(u => s(t, u)));
        }

        public static Maybe<U> SelectMany<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> func)
        {
            return maybe.Eval(Maybes.Nothing<U>.Nothing, func);
        }

        public static T ToNull<T>(this Maybe<T> maybe) where T : class
        {
            return maybe.Eval((T)null, x => x);
        }

        public static Maybe<A> Nothing<A>()
        {
            return Maybe<A>.Nothing;
        }

        public static Maybe<A> ToMaybe<A>(this A a)
        {
            return new Just<A>(a);
        }

        public static Maybe<A> Just<A>(A a)
        {
            return new Just<A>(a);
        }

        public static Maybe<T> FromNull<T>(this T nullable) where T : class
        {
            return nullable == null ? Nothing<T>() : Just(nullable);
        }
    }
}