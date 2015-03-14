using System.Collections.Generic;

namespace Generic.Containers.Collections.List.Final
{
    class TakeInfinite<T> : Take<T>
    {
        public TakeInfinite(IList<T> core, int amount) : base(core, amount)
        {
        }

        public override int Count
        {
            get { return Amount; }
        }
    }
}
