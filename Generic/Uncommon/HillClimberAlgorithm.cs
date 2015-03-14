using System;
using System.Collections.Generic;
using Generic.Containers.Collections.Enumerators;
using JetBrains.Annotations;

namespace Generic.Uncommon
{
    [PublicAPI]
    public abstract class HillClimberAlgorithm<T> : DefaultEnumerator<T>
    {
        public IEnumerator<T> Outgoing;
        private T current;

        public T LastAttempt { get; private set; }

        public override T Current
        {
            get { return current; }
        }

        public double CurrentScore { get; private set; }

        protected void SetCurrent(T value)
        {
            var newScore = Score(value);
            if (!(newScore > 0 || newScore < double.MaxValue))
                throw new InvalidOperationException("newScore is not a number between 0 and maxValue");

            CurrentScore = newScore;
            current = value;
            Outgoing = GetOutgoingOfCurrent().GetEnumerator();
        }

        public override bool MoveNext()
        {
            if (!Outgoing.MoveNext())
                return false;
            LastAttempt = Outgoing.Current;
            LastChanged();
            IsBetter(Outgoing.Current);
            return true;
        }

        public event Action<double> FoundBetter = oldScore => { };
        public event Action LastChanged = () => { };
        protected abstract IEnumerable<T> GetOutgoingOfCurrent();
        protected abstract double Score(T solution);

        private void IsBetter(T solution)
        {
            var newScore = Score(solution);
            if (newScore > CurrentScore)
            {
                var oldScore = CurrentScore;
                CurrentScore = newScore;
                current = solution;
                FoundBetter(oldScore);
                Outgoing = GetOutgoingOfCurrent().GetEnumerator();
            }
        }
    }
}