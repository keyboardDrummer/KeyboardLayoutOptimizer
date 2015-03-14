using System;
using Generic.Common;

namespace Generic.Containers.Collections.ForwardLinkedList
{
    public class Cons<T> : ForwardList<T>
    {
        readonly T head;
        readonly Memory<ForwardList<T>> _tail;

        public Cons(T head, Func<ForwardList<T>> tail)
        {
            this._tail = tail.Memorize();
            this.head = head;
        }

        public override U Eval<U>(Func<U> empty, Func<T, ForwardList<T>, U> cons)
        {
            return cons(head, Tail);
        }

        public override ForwardList<T> AddSorted(T t, Func<T, T, Ordering> compare)
        {
            var ordering = compare(t, head);
            switch(ordering)
            {
                case Ordering.Lesser:
                case Ordering.Equal:
                    return new Cons<T>(t, () => this);
                case Ordering.Greater:
                    return new Cons<T>(head, () => Tail.AddSorted(t, compare));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override Tuple<ForwardList<T>, ForwardList<T>> SplitAt(Func<T, bool> predicate)
        {
            if (predicate(head))
            {
                var recursive = Tail.SplitAt(predicate);
                return Tuple.Create(recursive.Item1.Prepend(head),recursive.Item2);
            }
            return Tuple.Create(ForwardListUtil.New<T>(),(ForwardList<T>)this);
        }

        public override ForwardList<T> Remove(Func<T, bool> predicate)
        {
            if (predicate(head))
                return Tail;
            return new Cons<T>(Head, _tail.Select(t => t.Remove(predicate)));
        }

        public override int Count
        {
            get { return 1 + Tail.Count; }
        }

        public T Head
        {
            get { return head; }
        }

        public ForwardList<T> Tail
        {
            get { return _tail.Value; }
        }

        protected override T Get(int index)
        {
            return index == 0 ? head : Tail[index - 1];
        }
    }
}
