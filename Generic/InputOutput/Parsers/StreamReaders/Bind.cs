using System;

namespace Generic.InputOutput.Parsers.StreamReaders
{

    internal class Bind<Inter,Out> : StreamParser<Out>, IBind<MyStreamReader,Inter,Out>
    {
        private readonly Parser<MyStreamReader, Inter> first;
        private readonly Func<Inter, Parser<MyStreamReader, Out>> getSecond;

        public Bind(Parser<MyStreamReader,Inter> first, Func<Inter, Parser<MyStreamReader, Out>> getSecond)
        {
            this.first = first;
            this.getSecond = getSecond;
        }


        public override ParseResult<Out> InnerParse(MyStreamReader input)
        {
            var firstResult = first.Parse(input);
            return firstResult.Bind(i => getSecond(i).Parse(input));
        }

        public Parser<MyStreamReader, Inter> First
        {
            get { return first; }
        }

        public Func<Inter, Parser<MyStreamReader, Out>> GetSecond
        {
            get { return getSecond; }
        }
    }
}
