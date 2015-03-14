using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic.Graphs.Algorithm
{
    class BreadthFirst<G> : IEnumerator<G> where G : class, INode<G>
    {
        private Queue<G> queue = new Queue<G>();
        private ISet<G> closed = new HashSet<G>();

        public BreadthFirst(G start)
        {
            queue.Enqueue(start);
            closed.Add(start);
        }

        public G Current
        {
            get { return queue.Peek(); }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            queue.Dequeue();
            if (Current == null)
                return false;
            closed.Add(Current);
            foreach (var child in Current.Outgoing().Where(child => !closed.Contains(child)))
            {
                queue.Enqueue(child);
                closed.Add(child);
            }
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
