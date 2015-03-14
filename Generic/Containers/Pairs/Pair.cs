namespace Generic.Containers.Pairs
{
    public static class Pair
    {
        public static IPair<A,B> Create<A,B>(A a, B b)
        {
            return new Pair<A, B>(a, b);
        }
    }

    public class Pair<A,B> : IPair<A,B>
    {
        public Pair(A item1, B item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        A item1;
        public A Item1
        {
            get { return item1; }
            private set { item1 = value; }
        }

        B item2;
        public B Item2
        {
            get { return item2; }
            private set { item2 = value; }
        }
    }
}
