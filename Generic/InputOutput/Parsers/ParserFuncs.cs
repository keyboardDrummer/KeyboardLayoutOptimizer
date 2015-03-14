using System;
using System.Collections.Generic;

namespace Generic.InputOutput.Parsers
{
    public abstract class ParserFuncs<TIn>
    {
        public abstract Parser<TIn, TOut> Bind<TInter,TOut>(Parser<TIn,TInter> first, Func<TInter, Parser<TIn, TOut>> getSecond);
        public abstract Parser<TIn, TOut> Or<TOut>(Parser<TIn,TOut> first, Parser<TIn, TOut> second);
        public abstract Parser<TIn, TOut> Return<TOut>(TOut value);

        public virtual Parser<TIn, IList<TOut>> Many<TOut>(Parser<TIn,TOut> parser)
        {
            return ParserUtil.Many(parser);
        }
    }
}
