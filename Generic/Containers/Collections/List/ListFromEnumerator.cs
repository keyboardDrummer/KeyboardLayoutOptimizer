using System.Collections.Generic;
using Generic.Containers.Collections.Enumerators;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    /*
     * Do not use with infinite enumerators.
     */

    public class ListFromEnumerator<T> : DefaultReadOnlyList<T>
    {
        private readonly IEnumerator<T> rator;
        private readonly List<T> soFar = new List<T>();

        public ListFromEnumerator(IEnumerator<T> rator)
        {
            this.rator = rator;
        }

        public override int Count
        {
            get
            {
                FillToSize(int.MaxValue);
                return soFar.Count;
            }
        }

        public bool HasMinimumCount(int minimum)
        {
            return FillToSize(minimum);
        }

        protected override T Get(int index)
        {
            if (index < soFar.Count)
            {
                return soFar[index];
            }
            FillToSize(index + 1);
            return soFar[index];
        }

        //Using the default list enumerator does not work when the enumeration is infinite.
        public override IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        private bool FillToSize(int upTo)
        {
            //soFar.Capacity = upTo;
            for (int i = soFar.Count;; i++)
            {
                if (i >= upTo)
                    return true;

                if (!rator.MoveNext())
                    return false;

                soFar.Add(rator.Current);
            }
        }

        private class MyEnumerator : DefaultEnumerator<T>
        {
            private readonly ListFromEnumerator<T> parent;
            private int i = -1;

            public MyEnumerator(ListFromEnumerator<T> parent)
            {
                this.parent = parent;
            }

            public override T Current
            {
                get { return parent[i]; }
            }

            public override bool MoveNext()
            {
                i++;
                return parent.FillToSize(i + 1);
            }
        }
    }
}