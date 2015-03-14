using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    internal class Transform<A, B> : DefaultEnumerator<B>
    {
        private B current;
        private readonly Func<A, B> f;
        private readonly IEnumerator<A> origin;

        public Transform(IEnumerator<A> origin, Func<A, B> f)
        {
            this.origin = origin;
            this.f = f;
        }

        public override B Current
        {
            get { return current; }
        }

        public override bool MoveNext()
        {
            var result = origin.MoveNext();
            if (result == false)
                return false;
            current = f(origin.Current);
            return true;
        }
    }
}