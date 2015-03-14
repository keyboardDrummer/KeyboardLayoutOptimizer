using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    public class Drop<T> : DefaultReadOnlyList<T>
    {
        public IList<T> Core { get; private set; }
        public int Amount { get; set; }

        public Drop(IList<T> core, int amount)
        {
            Core = core;
            Amount = amount;
        }

        public override int Count
        {
            get { return Core.Count - Amount; }
        }

        protected override T Get(int index)
        {
            return Core[index + Amount];
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return Core.Skip(Amount).GetEnumerator();
        }

        public override string ToString()
        {
            return "Drop(" + Core + "," + Amount + ")";
        }
    }
}
