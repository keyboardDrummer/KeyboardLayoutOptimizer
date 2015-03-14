using System;

namespace Generic.Containers.Maybes
{
    [Serializable]
    public class Nothing<T> : Maybe<T>
    {
        public override bool IsJust
        {
            get { return false; }
        }

        public override T FromJust
        {
            get { throw new FromJustOnNothingException(); }
        }

        public override R Eval<R>(Func<R> nothing, Func<T, R> just)
        {
            return nothing();
        }

        public override Maybe<R> Select<R>(Func<T, R> func)
        {
            return Nothing<R>.Nothing;
        }

        public override Maybe<T> Clone(Func<T, T> cloneElement)
        {
            return this;
        }

        public override void Exec(Action nothing, Action<T> just)
        {
            nothing();
        }

        private class FromJustOnNothingException : Exception
        {
        }

        public bool Equals(Nothing<T> other)
        {
            return !ReferenceEquals(null, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(Nothing<T>))
                return false;
            return Equals((Nothing<T>)obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}