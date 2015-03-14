using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.List.Final;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;
using JetBrains.Annotations;

namespace Generic.Containers.Collections.List
{
    public static class ListUtil
    {
        public static IList<T> ToIList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        [PublicAPI]
        public static T[,] ToMulti<T>(this IList<IList<T>> list)
        {
            var result = new T[list.Count,list.Any() ? list.First().Count : 0];
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                for(var j = 0;j<item.Count;j++)
                {
                    result[i,j] = item[j];
                }
            }
            return result;
        }

        [PublicAPI]
        public static T[][] ToJagged<T>(this IList<IList<T>> list)
        {
            var result = new T[list.Count][];
            for (int index = 0; index < list.Count; index++)
            {
                var item = list[index];
                result[index] = item.ToArray();
            }
            return result;
        }

        [PublicAPI]
        public static void Remove<T>(this IList<T> list, Func<T,bool> pred)
        {
            list.RemoveAt(list.IndexOfPred(pred));
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> range)
        {
            foreach(var x in range)
            {
                list.Add(x);
            }
        }

        [PublicAPI]
        public static void SelectDestructively<A>(this IList<A> xs, Func<A,A> f)
        {
            foreach(var i in xs.Indexes())
            {
                xs[i] = f(xs[i]);
            }
        }

        public static IList<T> New<T>(params T[] xs)
        {
            return new List<T>(xs); 
        }

        [PublicAPI]
        public static IList<T> TakeRewrite<T>(this IList<T> xs, int amount)
        {
            if (xs is Concat<T>)
            {
                var concat = (Concat<T>)xs;
                if (concat.First.Count >= amount)
                {
                    return concat.First.TakeRewrite(amount);
                }
            }
            else if (xs is Take<T>)
            {
                var take = (Take<T>)xs;
                return take.Core.TakeRewrite(take.Amount + amount);
            }
            return xs.TakeList(amount);
        }

        [PublicAPI]
        public static IList<T> DropRewrite<T>(this IList<T> xs, int amount)
        {
            if (xs is Concat<T>)
            {
                var concat = (Concat<T>)xs;
                if (concat.First.Count <= amount)
                {
                    return concat.Second.DropRewrite(amount - concat.First.Count);
                }
            }
            if (xs is Drop<T>)
            {
                var drop = (Drop<T>)xs;
                return drop.Core.DropRewrite(drop.Amount + amount);
            }
            return xs.Drop(amount);
        }

        [PublicAPI]
        public static IList<T> InsertLazily<T>(this IList<T> list, T item, int at)
        {
            return list.TakeList(at).ConcatList(Singleton(item)).ConcatList(list.Drop(at));
        }

        [PublicAPI]
        public static IList<T> RemoveAtLazily<T>(this IList<T> list, int at)
        {
            return list.TakeList(at).ConcatList(list.Drop(at + 1));
        }

        public static IEnumerable<Tuple<T, T>> ShakeHands<T>(this IList<T> xs)
        {
            return new ShakeHands<T>(xs);
        }

        public static IList<Tuple<T, T>> HoldHandsCycle<T>(this IList<T> xs)
        {
            return new HoldHands<T>(xs);
        }

        [PublicAPI]
        public static IList<Tuple<T, T>> HoldHandsLine<T>(this IList<T> xs)
        {
            return TakeList(new HoldHands<T>(xs), xs.Count - 1);
        }

        public static IList<T> Singleton<T>(T unknown)
        {
            return new Singleton<T>(unknown);
        }

        [PublicAPI]
        public static IEnumerable<B> Diagonalize<A, B>(this IList<IList<A>> xs, Func<IList<A>, B> aggregator)
        {
            return DiagonalizePerDepth(xs, aggregator).Aggregate((a, b) => a.Concat(b));
        }

        public static IEnumerable<IEnumerable<B>> DiagonalizePerDepth<A, B>(this IList<IList<A>> xs, Func<IList<A>, B> aggregator)
        {
            return new Diagonalize<A, B>(xs, aggregator);
        }

        [PublicAPI]
        public static IList<T> Empty<T>()
        {
            return new Empty<T>();
        }

        [PublicAPI]
        public static IList<T> Concat<T>(T unknown, IList<T> b)
        {
            return new Cons<T>(unknown, b);
        }

        [PublicAPI]
        public static IList<T> Array<T>(params T[] unknown)
        {
            return new List<T>(unknown);
        }

        [PublicAPI]
        [Obsolete]
        public static bool IsEmpty<T>(this IList<T> xs)
        {
            return !xs.Any();
        }

        public static IList<T> ConcatList<T>(this IList<T> a, IList<T> b)
        {
            return new Concat<T>(a, b);
        }

        public static IList<T> ConcatRewrite<T>(this IList<T> a, IList<T> b)
        {
            if (!a.Any())
                return b;

            if (!b.Any())
                return a;

            return new Concat<T>(a, b);
        }

        private static IList<int> TakeIntRewrite(this IList<int> xs, int amount)
        {
            if (xs is Concat<int>)
            {
                var concat = (Concat<int>)xs;
                if (concat.First.Count >= amount)
                    return concat.First.TakeIntRewrite(amount);
                return concat.First.ConcatRewrite(concat.Second.TakeIntRewrite(amount - concat.First.Count));
            }

            var fromTo = (FromToInt)xs;
            return FromTo(fromTo.From, fromTo.From + amount - 1);
        }

        public static IList<T> Repeat<T>(Func<T> creator, int count = Int32.MaxValue)
        {
            return new Repeat<T>(creator, count);
        }

        private static IList<int> DropIntRewrite(this IList<int> xs, int amount)
        {
            if (xs is Concat<int>)
            {
                var concat = (Concat<int>)xs;
                if (concat.First.Count <= amount)
                    return concat.Second.DropIntRewrite(amount - concat.First.Count);

                return concat.First.DropIntRewrite(amount).ConcatRewrite(concat.Second);
            }
            var fromTo = (FromToInt)xs;
            return FromTo(fromTo.From + amount, fromTo.To);
        }

        [PublicAPI]
        public static IList<int> LazyRemoveIntRewrite(this IList<int> xs, int index)
        {
            return xs.TakeIntRewrite(index).ConcatRewrite(xs.DropIntRewrite(index + 1));
        }

        public static IList<IList<T>> DivideInfinite<T>(this IEnumerable<T> core, int amount)
        {
            return new DivideInfinite<T>(core, amount);
        }

        public static IList<IList<T>> Divide<T>(this IList<T> core, int amount)
        {
            return new Divide<T>(core, amount);
        }

        public static IList<B> Transform<A, B>(this IList<A> core, Func<int, A, B> f)
        {
            return new Transform<Tuple<A,int>,B>(new Zip<A,int>(core,core.Indexes()), a => f(a.Item2,a.Item1));
        }

        public static IList<B> SelectList<A, B>(this IList<A> core, Func<A, B> f)
        {
            return new Transform<A, B>(core, f);
        }

        [PublicAPI]
        [Obsolete]
        public static IList<TB> Transform<TA, TB>(this IList<TA> core, Func<TA, TB> f)
        {
            return SelectList(core,f);
        }

        public static IList<int> FromTo(int from, int to)
        {
            return new FromToInt(from, to);
        }

        [PublicAPI]
        public static IList<T> TakeInfinite<T>(this IList<T> xs, int amount)
        {
            return new TakeInfinite<T>(xs, amount);
        }

        public static IList<T> TakeList<T>(this IList<T> xs, int amount)
        {
            return new Take<T>(xs, amount);
        }

        public static IList<T> Drop<T>(this IList<T> xs, int amount)
        {
            return new Drop<T>(xs, amount);
        }

        public static IList<int> Indexes<T>(this IList<T> xs)
        {
            return FromTo(0, xs.Count - 1);
        }

        public static IList<T> ToLazyList<T>(this IEnumerator<T> rator)
        {
            return new ListFromEnumerator<T>(rator);
        }

        public static ListFromEnumerable<T> ToLazyList<T>(this IEnumerable<T> rator)
        {
            return new ListFromEnumerable<T>(rator);
        }
    }
}