namespace Generic.InputOutput.Parsers.ListParsers
{
    class Or<TIn, TOut> : ListParser<TIn,TOut>, IOr<ListStream<TIn>,TOut>
    {
        private readonly Parser<ListStream<TIn>, TOut> first;
        private readonly Parser<ListStream<TIn>,TOut> second;

        public Or(Parser<ListStream<TIn>, TOut> first, Parser<ListStream<TIn>, TOut> second)
        {
            this.first = first;
            this.second = second;
        }

        public override ParseResult<TOut> InnerParse(ListStream<TIn> input)
        {
            var position = input.Position;
            var firstResult = first.Parse(input);
            if (firstResult.Success)
                return firstResult;
            input.Position = position;
            return second.Parse(input).Or(firstResult.FinalLog);
        }

        public Parser<ListStream<TIn>, TOut> Second
        {
            get { return second; }
        }

        Parser<ListStream<TIn>, TOut> IOr<ListStream<TIn>, TOut>.First
        {
            get { return first; }
        }
    }
}
