using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerators;
using Generic.Containers.Collections.List;
using Generic.Functions;

namespace Generic.Containers.Collections.Enumerables
{
    public static class EnumerableUtil
    {
        // ReSharper disable FunctionNeverReturns
        public static IEnumerable<T> Repeat<T>(T value)
        {
            while (true)
                yield return value;
        }
        // ReSharper restore FunctionNeverReturns

        // ReSharper disable FunctionRecursiveOnAllPaths
        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> enumerable)
        {
            foreach (var x in enumerable)
                yield return x;
            foreach (var x in Repeat(enumerable))
                yield return x;
        }
        // ReSharper restore FunctionRecursiveOnAllPaths

        public static IEnumerable<Tuple<A,B>> ZipTuple<A,B>(this IEnumerable<A> xs, IEnumerable<B> ys)
        {
            return xs.Zip(ys, Tuple.Create);
        }

        public static int IndexOfPred<T>(this IEnumerable<T> xs, Func<T,bool> pred)
        {
            return xs.WithIndex().First(t => pred(t.Item1)).Item2;
        }

        public static string ToArrayString(this IEnumerable<char> xs)
        {
            return new string(xs.ToArray());
        }

        public static IEnumerable<T> ConcatUtil<T>(this IEnumerable<T> xs, params T[] t)
        {
            return xs.Concat(t);
        }

        public static IEnumerable<T> ConcatUtil<T>(T t, IEnumerable<T> xs)
        {
            return ListUtil.Singleton(t).Concat(xs);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> xs)
        {
            return xs.SelectMany(a => a);
        }

        public static IEnumerable<int> From(int from)
        {
            return FromEnumeratorFunc(() => new From(from));
        }
        public static IEnumerable<int> FromTo(int from, int to)
        {
            return Enumerable.Range(from, to - from + 1);
        }

        public static IList<T> EvaluateEnumerableDeep<T>(this IEnumerable<T> xs)
        {
            return xs.ToList();
        }

        public static IList<List<T>> EvaluateEnumerableDeep<T>(this IEnumerable<IEnumerable<T>> xs)
        {
            return xs.Select(Enumerable.ToList).ToList();
        }

        public static IList<List<List<T>>> EvaluateEnumerableDeep<T>(this IEnumerable<IEnumerable<IEnumerable<T>>> xs)
        {
            return xs.Select(ys => ys.Select(Enumerable.ToList).ToList()).ToList();
        }
		
        public static IEnumerable<Tuple<T, int>> WithIndex<T>(this IEnumerable<T> xs)
        {
            return Zip(xs, new FromInt(0));
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> xs, T defaultValue)
        {
            var rator = xs.GetEnumerator();
            return rator.MoveNext() ? rator.Current : defaultValue;
        }

        static IEnumerable<Tuple<A, B>> Zip<A, B>(IEnumerable<A> first, IEnumerable<B> second)
        {
            return first.Zip(second, Tuple.Create);
        }

        public static void ForEachIndex<T>(this IEnumerable<T> xs, Action<int, T> action)
        {
            xs.GetEnumerator().ForEachIndex(action);
        }

        public static Tuple<IList<T>, IList<T>> SplitAt<T>(this IList<T> xs, int at)
        {
            return Tuple.Create(xs.TakeList(at), xs.Drop(at));
        }

        [Obsolete]
        public static IEnumerable<TB> Transform<TA, TB>(this IEnumerable<TA> xs, Func<TA, TB> f)
        {
            return new FromEnumeratorFunc<TB>(() => xs.GetEnumerator().Transform(f));
        }

        [Obsolete]
        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> action)
        {
            foreach(var x in xs)
            {
                action(x);
            }
        }

        public static int IndexOf<T>(this IEnumerable<T> e, T target, FuncUtil.Equals<T> equals)
        {
            var tor = e.GetEnumerator();
            int index = 0;
            while (tor.MoveNext())
            {
                if (equals(tor.Current, target))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static B FoldL<A,B>(this IEnumerable<A> xs, B seed, Func<B,A,B> f)
        {
            if (xs.Any())
            {
                var first = xs.ElementAt(0);
                var rest = xs.Skip(1);
                var r = f(seed,first);
                return rest.FoldL(r, f);
            }
            return seed;
        }

        public static B FoldR<A,B>(this IEnumerable<A> xs, B seed, Func<A,B,B> f)
        {
            if (xs.Any())
            {
                var first = xs.ElementAt(0);
                var rest = xs.Skip(1);
                return f(first, rest.FoldR(seed, f));
            }
            return seed;
        }

        public static B FoldRLazy<A, B>(this IEnumerable<A> xs, B seed, Func<A, Func<B>, B> f)
        {
            if (xs.Any())
            {
                var first = xs.ElementAt(0);
                var rest = xs.Skip(1);
                return f(first,() => rest.FoldRLazy(seed, f));
            }
            return seed;
        }

        public static IEnumerable<A> ConcatLazy<A>(this IEnumerable<A> xs, Func<IEnumerable<A>> ys)
        {
            foreach(var x in xs)
            {
                yield return x;
            }
            var ys2 = ys();
            foreach(var y in ys2)
            {
                yield return y;
            }
        }

        public static Tuple<A,IEnumerable<A>> Split<A>(this IEnumerable<A> xs)
        {
            var rator = xs.GetEnumerator();
            if (rator.MoveNext())
            {
                var current = rator.Current;
                var rest = rator.ToLazyList();
                return Tuple.Create(current, (IEnumerable<A>)rest);
            }
            throw new ArgumentException("xs is empty");
        }

        public static int IndexOf<T>(this IEnumerable<T> e, T target) where T : IEquatable<T>
        {
            return e.IndexOf(target, (a, b) => a.Equals(b));
        }

        [Obsolete]
        public static IEnumerable<T> ReverseEnumerable<T>(this IEnumerable<T> xs)
        {
            return xs.Reverse();
        }

        public static void SplitAt<T>(this IEnumerator<T> enumerator, int at, out IEnumerable<T> first,
                                      out IEnumerator<T> second)
        {
            var firstList = new List<T>();
            int count = 0;
            while (enumerator.MoveNext() && count < at)
            {
                firstList.Add(enumerator.Current);
                count++;
            }
            first = firstList;
            second = enumerator;
        }

        public static IEnumerable<T> IntertwineRate<T>(this IList<T> xs, IEnumerable<T> ys, int rate)
        {
            return FromEnumeratorFunc(() => new Intertwine<IList<T>>(xs.Divide(rate).GetEnumerator(),
                ys.Select(ListUtil.Singleton).GetEnumerator())).Flatten();
        }

        public static IEnumerable<T> IntertwineRate<T>(this IList<IEnumerable<T>> a, int rate)
        {
            return Intertwine(a.Transform((index, l) => (IEnumerable<IList<T>>)l.DivideInfinite(rate))).Flatten();
        }

        public static IEnumerable<T> IntertwineRate<T>(this IList<IList<T>> a, int rate)
        {
            return Intertwine(a.Transform((index,l) => (IEnumerable<IList<T>>)l.Divide(rate))).Flatten();
        }

        public static IEnumerable<T> Intertwine<T>(this IList<IEnumerable<T>> a)
        {
            return FromEnumeratorFunc(() => new IntertwineList<T>(a));
        }

        public static bool IsEmpty<T>(this IEnumerable<T> xs)
        {
            return !xs.GetEnumerator().MoveNext();
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> core, Func<T, Boolean> pred)
        {
            return new FromEnumeratorFunc<T>(() => EnumeratorUtil.Filter(core.GetEnumerator(), pred));
        }

        public static IEnumerable<T> FromEnumeratorFunc<T>(Func<IEnumerator<T>> func)
        {
            return new FromEnumeratorFunc<T>(func);
        }

        public static int CustomCount<T>(this IEnumerable<T> xs)
        {
            return xs.GetEnumerator().Count();
        }

    }
}