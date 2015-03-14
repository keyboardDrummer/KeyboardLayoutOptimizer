using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    public class FromToInt : DefaultReadOnlyList<int>
    {
        private readonly int from;

        public int From
        {
            get { return from; }
        }

        public int To
        {
            get { return to; }
        }

        private readonly int to;

        public FromToInt(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public override int Count
        {
            get { return to - from + 1; }
        }

        protected override int Get(int index)
        {
            return index + from;
        }

        public override string ToString()
        {
            return "{" + from + ".." + to + "}";
        }
    }
}