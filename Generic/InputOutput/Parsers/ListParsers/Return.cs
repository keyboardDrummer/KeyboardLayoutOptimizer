namespace Generic.InputOutput.Parsers.ListParsers
{
    class Return<TIn,TOut> : ListParser<TIn,TOut>
    {
        readonly TOut value;

        public Return(TOut value)
        {
            this.value = value;
        }

        public override ParseResult<TOut> InnerParse(ListStream<TIn> input)
        {
            return Success(value);
        }
    }
}
