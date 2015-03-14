using System.Collections.Generic;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    public class Concat<T> : DefaultReadOnlyList<T>
    {
        public IList<T> First { get; private set; }
        public IList<T> Second { get; private set; }

        public Concat(IList<T> a, IList<T> b)
        {
            First = a;
            Second = b;
        }

        public override int Count
        {
            get { return First.Count + Second.Count; }
        }

        protected override T Get(int index)
        {
            return index < First.Count ? First[index] : Second[index - First.Count];
        }

        public override string ToString()
        {
            return "Concat(" + First + "," + Second + ")";
        }
    }
}
