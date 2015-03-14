using System;
using Generic.Common;

namespace Generic.Containers.Collections.ForwardLinkedList
{
    internal class Empty<T> : ForwardList<T>
    {
        public override U Eval<U>(Func<U> empty, Func<T, ForwardList<T>, U> cons)
        {
            return empty();
        }

        public override ForwardList<T> AddSorted(T t, Func<T, T, Ordering> compare)
        {
            return new Cons<T>(t,() => this);
        }

        public override Tuple<ForwardList<T>, ForwardList<T>> SplitAt(Func<T, bool> predicate)
        {
            return Tuple.Create(Empty,Empty);
        }

        public override ForwardList<T> Remove(Func<T, bool> predicate)
        {
            return this;
        }

        public override int Count
        {
            get { return 0; }
        }

        protected override T Get(int index)
        {
            throw new InvalidOperationException();
        }
    }
}
