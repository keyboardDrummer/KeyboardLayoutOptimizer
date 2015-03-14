using System;

namespace Generic.Containers.Maybes
{
    [Serializable]
    public class Just<A> : Maybe<A>
    {
        private readonly A data;

        public Just(A data)
        {
            this.data = data;
        }

        public override bool IsJust
        {
            get { return true; }
        }

        public override A FromJust
        {
            get { return data; }
        }

        public override R Eval<R>(Func<R> nothing, Func<A, R> just)
        {
            return just(data);
        }

        public override void Exec(Action nothing, Action<A> just)
        {
            just(data);
        }

        public override Maybe<R> Select<R>(Func<A, R> func)
        {
            return new Just<R>(func(data));
        }

        public override Maybe<A> Clone(Func<A, A> cloneElement)
        {
            return MaybeUtil.Just(cloneElement(data));
        }

        public bool Equals(Just<A> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.data, data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(Just<A>))
                return false;
            return Equals((Just<A>)obj);
        }

        public override int GetHashCode()
        {
            return data.GetHashCode();
        }
    }
}