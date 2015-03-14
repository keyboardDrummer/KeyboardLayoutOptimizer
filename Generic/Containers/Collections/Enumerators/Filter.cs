using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    public class Filter<A> : DefaultEnumerator<A>
    {
        private readonly IEnumerator<A> core;
        private readonly Func<A, Boolean> pred;

        public Filter(IEnumerator<A> core, Func<A, bool> pred)
        {
            this.core = core;
            this.pred = pred;
        }

        public override A Current
        {
            get { return core.Current; }
        }

        public override bool MoveNext()
        {
            while (core.MoveNext())
            {
                if (pred(Current))
                    return true;
            }
            return false;
        }
    }
}