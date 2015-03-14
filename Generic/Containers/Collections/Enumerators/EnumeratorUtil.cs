using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    public static class EnumeratorUtil
    {
        public static IEnumerator<T1> Select<T, T1>(this IEnumerator<T> xs, Func<T, T1> func)
        {
            while (xs.MoveNext())
            {
                yield return func(xs.Current);
            }
        }

        public static void ForEachIndex<T>(this IEnumerator<T> xs, Action<int, T> action)
        {
            for (int i = 0; xs.MoveNext(); i++)
            {
                action(i, xs.Current);
            }
        }

        [Obsolete]
        public static IEnumerator<TB> Transform<TA, TB>(this IEnumerator<TA> rator, Func<TA, TB> f)
        {
            return new Transform<TA, TB>(rator, f);
        }

        public static IEnumerator<T> Flatten<T>(this IEnumerator<IEnumerator<T>> rators)
        {
            return new Flatten<T>(rators);
        }

        public static int Count<T>(this IEnumerator<T> xs)
        {
            int size = 0;
            while (xs.MoveNext())
            {
                size++;
            }
            return size;
        }

        public static TB Fold<TA, TB>(this IEnumerator<TA> xs, TB nil, Func<TA, TB, TB> f)
        {
            while (xs.MoveNext())
            {
                nil = f(xs.Current, nil);
            }
            return nil;
        }

        public static T Fold<T>(this IEnumerator<T> xs, Func<T, T, T> f)
        {
            xs.MoveNext();
            var nil = xs.Current;
            while (xs.MoveNext())
            {
                nil = f(xs.Current, nil);
            }
            return nil;
        }

        public static IEnumerator<T> Filter<T>(IEnumerator<T> rator, Func<T, Boolean> pred)
        {
            return new Filter<T>(rator, pred);
        }
    }
}