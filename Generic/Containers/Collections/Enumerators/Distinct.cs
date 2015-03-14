using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    internal class Distinct<A> : DefaultEnumerator<A>
    {
        private readonly ISet<A> had = new HashSet<A>();
        private readonly IEnumerator<A> rator;

        public Distinct(IEnumerator<A> rator)
        {
            this.rator = rator;
        }

        public override A Current
        {
            get { return rator.Current; }
        }

        public override bool MoveNext()
        {
            do
            {
                if (!rator.MoveNext())
                    return false;
            } while (had.Contains(Current));
            had.Add(Current);
            return true;
        }
    }
}