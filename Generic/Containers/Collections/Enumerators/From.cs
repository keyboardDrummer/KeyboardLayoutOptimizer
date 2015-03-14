namespace Generic.Containers.Collections.Enumerators
{
    class From : DefaultEnumerator<int>
    {
        private int result;

        public From(int from)
        {
            result = from - 1;
        }

        public override int Current
        {
            get { return result; }
        }

        public override bool MoveNext()
        {
            result++;
            return true;
        }
    }
}
