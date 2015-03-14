using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.List.Final
{
    public class Take<T> : DefaultReadOnlyList<T>
    {
        public Take(IList<T> core, int amount)
        {
            Core = core;
            Amount = amount;
        }

        public IList<T> Core { get; set; }
        public int Amount { get; set; }

        public override int Count
        {
            get { return Math.Min(Amount,Core.Count); }
        }

        protected override T Get(int index)
        {
            return Core[index];
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return Core.Take(Amount).GetEnumerator();
        }

        public override string ToString()
        {
            return "Take(" + Core + "," + Amount + ")";
        }
    }
}