using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerables
{
    class EnumerableWrapper<T> : DefaultEnumerable<T>
    {
        private readonly IEnumerable<T> core;

        public EnumerableWrapper(IEnumerable<T> core)
        {
            this.core = core;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return core.GetEnumerator();
        }
    }
}
