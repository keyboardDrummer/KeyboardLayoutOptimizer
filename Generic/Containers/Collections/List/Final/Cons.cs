

using System.Collections.Generic;

namespace Generic.Containers.Collections.List.Final
{
    public class Cons<T> : DefaultReadOnlyList<T>
    {
        private readonly T head;
        private readonly IList<T> tail;

        public Cons(T head, IList<T> tail)
        {
            this.head = head;
            this.tail = tail;
        }

        public override int Count
        {
            get { return tail.Count + 1; }
        }

        protected override T Get(int index)
        {
            return index == 0 ? head : tail[index - 1];
        }
    }
}