using System;
using System.Collections.Generic;
using System.IO;
using Generic.Common;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers.StreamReaders
{

    public abstract class StreamParser<TOut> : Parser<MyStreamReader, TOut>
    {
        public override ParseResult<TOut> Parse(MyStreamReader input)
        {
            var position = input.Position;
            var result = base.Parse(input);

            if (result.Failure)
            {
                input.Position = position;
                //WriteFailure(input);
            }
            return result;
        }

        public override ParserFuncs<MyStreamReader> Funcs
        {
            get { return StreamFuncs.Instance; }
        }

        public override Parser<MyStreamReader, IList<TOut>> Many()
        {
            return new Many<TOut>(this);
        }

        void WriteFailure(MyStreamReader input)
        {
            Document token;
            try
            {
                var c = input.Read();
                token = new string(c, 1);
                token = token.InBrackets() + ((object)((int)c)).Print().InParens();
            }
            catch (EndOfStreamException)
            {
                token = "end of stream";
            }
            Name.Maybe(() => Console.WriteLine(DocumentUtil.Empty + "could not parse" - Name + ", token =" - token));
        }
    }
    class StreamFuncs : ParserFuncs<MyStreamReader>
    {
        public static readonly ParserFuncs<MyStreamReader> Instance = new StreamFuncs();

        public override Parser<MyStreamReader, TOut> Bind<TInter, TOut>(Parser<MyStreamReader, TInter> first, Func<TInter, Parser<MyStreamReader, TOut>> getSecond)
        {
            return new Bind<TInter, TOut>(first, getSecond);
        }

        public override Parser<MyStreamReader, TOut> Or<TOut>(Parser<MyStreamReader, TOut> first, Parser<MyStreamReader, TOut> second)
        {
            return new Or<TOut>(first, second);
        }

        public override Parser<MyStreamReader, TOut> Return<TOut>(TOut value)
        {
            return new Return<TOut>(value);
        }

        public override Parser<MyStreamReader, IList<TOut>> Many<TOut>(Parser<MyStreamReader, TOut> parser)
        {
            return new Many<TOut>(parser);
        }
    }
}
