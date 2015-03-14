namespace Generic.InputOutput.Parsers.StreamReaders
{
    class Or<TOut> : StreamParser<TOut>, IOr<MyStreamReader,TOut>
    {
        private readonly Parser<MyStreamReader, TOut> first;
        private readonly Parser<MyStreamReader, TOut> second;

        public Or(Parser<MyStreamReader, TOut> first, Parser<MyStreamReader, TOut> second)
        {
            this.first = first;
            this.second = second;
        }

        public override ParseResult<TOut> InnerParse(MyStreamReader input)
        {
            var position = input.Position;
            var firstResult = first.Parse(input);
            if (firstResult.Failure)
            {
                input.Position = position;
                return second.Parse(input).Or(firstResult.FinalLog);
            }
            return firstResult;
        }

        public Parser<MyStreamReader, TOut> First
        {
            get { return first; }
        }

        public Parser<MyStreamReader, TOut> Second
        {
            get { return second; }
        }
    }
}
