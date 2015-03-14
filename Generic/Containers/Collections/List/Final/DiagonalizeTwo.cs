using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.List.Final
{
    public class DiagonalizeTwo<TA, TB, TC> : DefaultReadOnlyList<TC>
    {
        private readonly Func<TA, TB, TC> aggregator;
        private readonly IList<TA> xs;
        private readonly IList<TB> ys;

        public DiagonalizeTwo(IList<TA> xs, IList<TB> ys, Func<TA, TB, TC> aggregator)
        {
            this.xs = xs;
            this.aggregator = aggregator;
            this.ys = ys;
        }

        public override int Count
        {
            get { return xs.Count * ys.Count; }
        }

        protected override TC Get(int index)
        {
            var inner = index%xs.Count;
            var outer = (index - inner)/xs.Count;
            return aggregator(xs[outer], ys[inner]);
        }
    }
}