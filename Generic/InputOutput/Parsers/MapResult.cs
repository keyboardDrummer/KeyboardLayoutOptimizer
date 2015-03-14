using System;

namespace Generic.InputOutput.Parsers
{
    class MapResult<TIn,TOut> : Parser<TIn,TOut>
    {
        readonly Parser<TIn, TOut> parser;
        readonly Func<ParseResult<TOut>, ParseResult<TOut>> func;

        public MapResult(Parser<TIn, TOut> parser, Func<ParseResult<TOut>, ParseResult<TOut>> func)
        {
            this.parser = parser;
            this.func = func;
        }

        public override ParserFuncs<TIn> Funcs
        {
            get { return parser.Funcs; }
        }

        public override ParseResult<TOut> InnerParse(TIn input)
        {
            return func(parser.Parse(input));
        }
    }
}
