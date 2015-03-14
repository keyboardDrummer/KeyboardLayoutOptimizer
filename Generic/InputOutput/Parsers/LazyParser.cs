using System;

namespace Generic.InputOutput.Parsers
{
    class LazyParser<TIn,TOut> : Parser<TIn,TOut>
    {
        Func<Parser<TIn, TOut>> lazy;
        ParserFuncs<TIn> funcs;

        public LazyParser(Func<Parser<TIn, TOut>> lazy)
        {
            this.lazy = () =>
            {
                var result = lazy();
                this.lazy = () => result;
                return result;
            };
        }

        public override ParserFuncs<TIn> Funcs
        {
            get { return funcs; }
        }

        public override ParseResult<TOut> InnerParse(TIn input)
        {
            return lazy().Parse(input);
        }
    }
}
