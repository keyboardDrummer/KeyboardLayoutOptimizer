using System;
using System.Collections.Generic;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    internal class HoldHands<T> : DefaultReadOnlyList<Tuple<T, T>>
    {
        private readonly IList<T> xs;

        public HoldHands(IList<T> xs)
        {
            this.xs = xs;
        }

        public override int Count
        {
            get { return xs.Count; }
        }

        protected override Tuple<T, T> Get(int index)
        {
            return new Tuple<T, T>(xs[index], xs[(index + 1)%xs.Count]);
        }
    }
}