using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers
{
    class Fail<TIn,TOut> : Parser<TIn,TOut>
    {
        readonly ParserFuncs<TIn> funcs;
        readonly Document log;

        public Fail(ParserFuncs<TIn> parser, Document log)
        {
            this.funcs = parser;
            this.log = log;
        }

        public override ParseResult<TOut> InnerParse(TIn input)
        {
            return FailResult(log);
        }

        public override ParserFuncs<TIn> Funcs
        {
            get { return funcs; }
        }
    }
}
