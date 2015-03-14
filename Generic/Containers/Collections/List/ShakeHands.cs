using System;
using System.Collections.Generic;
using Generic.Containers.Collections.Collection;

namespace Generic.Containers.Collections.List
{
    internal class ShakeHands<T> : DefaultReadOnlyCollection<Tuple<T, T>>
    {
        private readonly IList<T> xs;

        public ShakeHands(IList<T> xs)
        {
            this.xs = xs;
        }

        public override int Count
        {
            get { return xs.Count * (xs.Count - 1) / 2; }
        }

        public override IEnumerator<Tuple<T, T>> GetEnumerator()
        {
            for (var i = 0; i < xs.Count; i++)
            {
                for (int j = i + 1; j < xs.Count; j++)
                {
                    yield return new Tuple<T, T>(xs[i], xs[j]);
                }
            }
        }
    }
}