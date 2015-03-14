namespace Generic.InputOutput.Parsers
{

    class ParserWrapper<TIn,TOut> : Parser<TIn,TOut>
    {
        Parser<TIn, TOut> inner;

        public override ParserFuncs<TIn> Funcs
        {
            get { return inner.Funcs; }
        }

        public Parser<TIn, TOut> Inner
        {
            set { inner = value; }
        }

        public override ParseResult<TOut> InnerParse(TIn input)
        {
            return inner.Parse(input);
        }
    }
}
