using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.List.Final
{
    class Zip<A,B> : DefaultReadOnlyList<Tuple<A,B>>
    {
        private readonly IList<A> xs;
        private readonly IList<B> ys;

        public Zip(IList<A> xs, IList<B> ys)
        {
            this.xs = xs;
            this.ys = ys;
        }

        public override int Count
        {
            get { return Math.Min(xs.Count, ys.Count); }
        }

        protected override Tuple<A, B> Get(int index)
        {
            return Tuple.Create(xs[index], ys[index]);
        }
    }
}
