using System.Collections.Generic;

namespace Generic.Containers.Collections
{
    public class PartnerSet<A> : PartnerSet<A, A>
    {
    }

    public class PartnerSet<A, B>
    {
        private Dictionary<A, B> ab = new Dictionary<A, B>();
        private Dictionary<B, A> ba = new Dictionary<B, A>();

        public A this[B b]
        {
            get { return ba[b]; }
        }

        public B this[A a]
        {
            get { return ab[a]; }
        }

        public void AddPair(A a, B b)
        {
            ab.Add(a, b);
            ba.Add(b, a);
        }

        public A GetLeft(B b)
        {
            return this[b];
        }

        public B GetRight(A a)
        {
            return this[a];
        }
    }
}