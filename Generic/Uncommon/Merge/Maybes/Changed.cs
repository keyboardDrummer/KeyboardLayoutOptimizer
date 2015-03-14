namespace Generic.Uncommon.Merge.Maybes
{
    class Changed<T> : IMaybeChange
    {
        private T newValue;

        public Changed(T newValue)
        {
            this.newValue = newValue;
        }

        public T NewValue
        {
            get { return newValue; }
        }
    }
}
