using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.Enumerators
{
    public class IntertwineList<T> : DefaultEnumerator<T>
    {
        private readonly IList<IEnumerator<T>> rators;
        private int currentRatorIndex;

        public IntertwineList(IList<IEnumerable<T>> rators)
        {
            this.rators = rators.Select(x => x.GetEnumerator()).ToList();
            currentRatorIndex = rators.Count;
        }

        public override T Current
        {
            get { return CurrentRator.Current; }
        }

        private IEnumerator<T> CurrentRator
        {
            get { return rators[currentRatorIndex]; }
        }

        public override bool MoveNext()
        {
            currentRatorIndex = (currentRatorIndex + 1)%rators.Count;
            var hasCurrent = CurrentRator.MoveNext();
            if (!hasCurrent && rators.Count > 1)
            {
                rators.RemoveAt(currentRatorIndex);
                currentRatorIndex--;
                return MoveNext();
            }
            return hasCurrent;
        }
    }
}