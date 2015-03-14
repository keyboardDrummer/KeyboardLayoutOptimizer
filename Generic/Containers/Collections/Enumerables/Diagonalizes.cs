using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.List;

namespace Generic.Containers.Collections.Enumerables
{
    public static class Diagonalizes
    {

        public static IEnumerable<IEnumerable<TC>> Diagonalize<TA, TB, TC>(IEnumerable<TA> xs, IEnumerable<TB> ys, Func<TA, TB, TC> aggregator)
        {
            return new DiagonalizeTwoEnumerable<TA, TB, TC>(xs, ys, aggregator);
        }

        public static IEnumerable<IEnumerable<TC>> DiagonalizeRate<TA, TB, TC>(IEnumerable<TA> xs, IEnumerable<TB> ys, int rate, Func<TA, TB, TC> aggregator)
        {
            return Diagonalize(xs.DivideInfinite(rate), ys, (xs2, y) => xs2.SelectList(x => aggregator(x, y))).Select(zs => zs.Flatten());
        }

        public static IEnumerable<IEnumerable<IEnumerable<TA>>> DiagonalizeList<TA>(IEnumerable<IEnumerable<TA>> xs)
        {
            return xs.FoldR<IEnumerable<TA>, IEnumerable<IEnumerable<IEnumerable<TA>>>>(
                ListUtil.Singleton(ListUtil.Singleton(Enumerable.Empty<TA>())),
                (first, rec) => Diagonalize(first, rec, (a, b) => b.Select(c => EnumerableUtil.ConcatUtil(a, c))).Select(EnumerableUtil.Flatten));
        }

        public static IEnumerable<IEnumerable<IEnumerable<TA>>> DiagonalizeListRate<TA>(IEnumerable<IEnumerable<TA>> xs, int rate)
        {
            return xs.FoldR<IEnumerable<TA>, IEnumerable<IEnumerable<IEnumerable<TA>>>>(
                ListUtil.Singleton(ListUtil.Singleton(Enumerable.Empty<TA>())),
                (first, rec) => DiagonalizeRate(first, rec, rate, (a, b) => b.Select(c => EnumerableUtil.ConcatUtil(a, c))).Select(EnumerableUtil.Flatten));
        }

        public static IEnumerable<IEnumerable<IEnumerable<TA>>> DiagonalizeListRateEmpty<TA>(IEnumerable<IEnumerable<TA>> xs, int rate)
        {
            var empty2 = Enumerable.Empty<IEnumerable<IEnumerable<TA>>>();
            var empty = ListUtil.Singleton(ListUtil.Singleton(Enumerable.Empty<TA>()));
            return xs.FoldRLazy(empty2,
                (first, recLazy) => DiagonalizeRate(first, empty.ConcatLazy(recLazy), rate, (a, b) => b.Select(c => EnumerableUtil.ConcatUtil(a, c))).Select(EnumerableUtil.Flatten));
        }
    }
}
