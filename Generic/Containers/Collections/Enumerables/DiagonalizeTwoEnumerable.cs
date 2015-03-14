using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerators;
using Generic.Containers.Collections.List;

namespace Generic.Containers.Collections.Enumerables
{
    public class DiagonalizeTwoEnumerable<TA, TB, TC> : DefaultEnumerable<IEnumerable<TC>>
    {
        private readonly Func<TA, TB, TC> aggregator;
        private readonly ListFromEnumerable<TA> xs;
        private readonly ListFromEnumerable<TB> ys;

        public DiagonalizeTwoEnumerable(IEnumerable<TA> xs, IEnumerable<TB> ys, Func<TA, TB, TC> aggregator)
        {
            this.xs = xs.ToLazyList();
            this.ys = ys.ToLazyList();
            this.aggregator = aggregator;
        }

        public override IEnumerator<IEnumerable<TC>> GetEnumerator()
        {
            var allDepths = EnumerableUtil.From(0).Select(depth => EnumerableUtil.FromEnumeratorFunc(() => new DiagonalizeHelper(depth, xs, ys, aggregator)));
            var untilEmpty = allDepths.TakeWhile(Enumerable.Any);
            return untilEmpty.GetEnumerator();
        }

        private class DiagonalizeHelper : DefaultEnumerator<TC>
        {
            private readonly Func<TA, TB, TC> aggregator;
            private readonly ListFromEnumerable<TA> xs;
            private readonly ListFromEnumerable<TB> ys;
            private readonly int maxDepth;
            private int firstDepth = -1;
            private readonly int end;

            public DiagonalizeHelper(int maxDepth, ListFromEnumerable<TA> xs, ListFromEnumerable<TB> ys, Func<TA, TB, TC> aggregator)
            {
                this.end = xs.HasMinimumCount(maxDepth + 1) ? maxDepth : xs.Count - 1;
                this.aggregator = aggregator;
                this.maxDepth = maxDepth;
                this.xs = xs;
                this.ys = ys;
                firstDepth = ys.HasMinimumCount(maxDepth+1) ? -1 : maxDepth-ys.Count;
            }

            public override TC Current
            {
                get { return aggregator(xs[firstDepth],ys[maxDepth-firstDepth]); }
            }

            public override bool MoveNext()
            {
                firstDepth++;
                return end >= firstDepth;
            }

        }
    }
}