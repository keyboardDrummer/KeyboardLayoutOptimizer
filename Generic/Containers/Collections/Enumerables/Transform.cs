using System;
using System.Collections.Generic;
using Generic.Containers.Collections.Enumerators;

namespace Generic.Containers.Collections.Enumerables
{
    public class Transform<A, B> : DefaultEnumerator<B>
    {
        private readonly IEnumerator<A> core;
        private readonly Func<A, B> f;
        private B current;

        public Transform(IEnumerator<A> core, Func<A, B> f)
        {
            this.core = core;
            this.f = f;
        }

        public override B Current
        {
            get { return current; }
        }

        public override bool MoveNext()
        {
            bool result = core.MoveNext();
            current = f(core.Current);
            return result;
        }
    }
}