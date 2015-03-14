using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    class EnumeratorFromList<T> : DefaultEnumerator<T>
    {
        private readonly IList<T> core;
        private int index = -1;

        public EnumeratorFromList(IList<T> core)
        {
            this.core = core;
        }

        public override T Current
        {
            get { return core[index]; }
        }

        public override bool MoveNext()
        {
            return ++index < core.Count;
        }
    }
}
