using System.Collections.Generic;

namespace Generic.InputOutput.Parsers.StreamReaders
{
    class Many<TOut> : StreamParser<IList<TOut>>
    {
        readonly Parser<MyStreamReader, TOut> parser;

        public Many(Parser<MyStreamReader,TOut> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<IList<TOut>> InnerParse(MyStreamReader input)
        {
            IList<TOut> list = new List<TOut>();
            start:
            var position = input.Position;
            var parseResult = parser.Parse(input);
            if (parseResult.Success)
            {
                list.Add(parseResult.JustResult);
                goto start;
            }
            input.Position = position;
            return Success(list);
        }
    }
}
