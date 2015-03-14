using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerables
{
    class NamedEnumerable<T> : EnumerableWrapper<T>
    {
        private readonly string name;
        public NamedEnumerable(IEnumerable<T> core, string name) : base(core)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
