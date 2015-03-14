using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    internal class Flatten<T> : DefaultEnumerator<T>
    {
        private readonly IEnumerator<IEnumerator<T>> rators;

        public Flatten(IEnumerator<IEnumerator<T>> rators)
        {
            this.rators = rators;
        }

        public override T Current
        {
            get { throw new NotImplementedException(); }
        }

        public override bool MoveNext()
        {
            if (rators.Current == null || !rators.Current.MoveNext())
            {
                if (rators.MoveNext())
                {
                    return MoveNext();
                }
                return false;
            }
            return true;
        }
    }
}