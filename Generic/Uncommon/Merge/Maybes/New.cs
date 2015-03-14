namespace Generic.Uncommon.Merge.Maybes
{
    class New<T> : IMaybeChange
    {
        private T t;

        public New(T t)
        {
            this.t = t;
        }
    }
}
