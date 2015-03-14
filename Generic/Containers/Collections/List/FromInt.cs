using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    class FromInt : DefaultReadOnlyList<int>
    {
        readonly int fromInt;

        public FromInt(int fromInt)
        {
            this.fromInt = fromInt;
        }

        public override int Count
        {
            get { return int.MaxValue; }
        }

        protected override int Get(int index)
        {
            return index + fromInt;
        }
    }
}
