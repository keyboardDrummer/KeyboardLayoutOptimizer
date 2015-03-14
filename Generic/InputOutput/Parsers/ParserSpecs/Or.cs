namespace Generic.InputOutput.Parsers.ParserSpecs
{
    class Or<In,Out> : ParserSpec<In,Out>
    {
        readonly ParserSpec<In, Out> first;
        readonly ParserSpec<In, Out> second;

        public Or(ParserSpec<In, Out> first, ParserSpec<In, Out> second)
        {
            this.first = first;
            this.second = second;
        }

        public ParserSpec<In, Out> First
        {
            get { return first; }
        }

        public ParserSpec<In, Out> Second
        {
            get { return second; }
        }

        public override OutResult Apply<OutResult>(ParserSpecImplementation<In, Out, OutResult> implementation)
        {
            return implementation.Or(this);
        }
    }
}
