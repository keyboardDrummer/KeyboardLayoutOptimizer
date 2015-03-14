using System;

namespace Generic.InputOutput.Parsers.ListParsers
{
    class Bind<TIn,TInter,TOut> : ListParser<TIn,TOut>, IBind<ListStream<TIn>,TInter,TOut>
    {
        readonly Parser<ListStream<TIn>, TInter> leftParser;
        readonly Func<TInter,Parser<ListStream<TIn>, TOut>> getRightParser;

        public Bind(Parser<ListStream<TIn>, TInter> leftParser, Func<TInter, Parser<ListStream<TIn>, TOut>> getRightParser)
        {
            this.leftParser = leftParser;
            this.getRightParser = getRightParser;
        }

        public override ParseResult<TOut> InnerParse(ListStream<TIn> input)
        {
            var left = leftParser.Parse(input);
            return left.Bind(i => getRightParser(i).Parse(input));
        }

        public Parser<ListStream<TIn>, TInter> First
        {
            get { return leftParser; }
        }

        public Func<TInter, Parser<ListStream<TIn>, TOut>> GetSecond
        {
            get { return getRightParser; }
        }
    }
}
