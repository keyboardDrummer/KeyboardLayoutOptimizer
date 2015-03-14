namespace Generic.InputOutput.Parsers.StreamReaders
{
    class Return<TOut> : StreamParser<TOut>
    {
        readonly TOut value;

        public Return(TOut value)
        {
            this.value = value;
        }

        public override ParseResult<TOut> InnerParse(MyStreamReader input)
        {
            return Success(value);
        }
    }
}
