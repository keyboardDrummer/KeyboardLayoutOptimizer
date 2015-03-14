using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    class Intertwine<T> : DefaultEnumerator<T>
    {
        private readonly IEnumerator<T> xs;
        private readonly IEnumerator<T> ys;
        private bool xsTurn;

        public Intertwine(IEnumerator<T> xs, IEnumerator<T> ys)
        {
            this.xs = xs;
            this.ys = ys;
        }

        public override T Current
        {
            get { return xsTurn ? xs.Current : ys.Current; }
        }

        public override bool MoveNext()
        {
            xsTurn = !xsTurn;
            return xsTurn ? xs.MoveNext() : ys.MoveNext();
        }
    }
}
