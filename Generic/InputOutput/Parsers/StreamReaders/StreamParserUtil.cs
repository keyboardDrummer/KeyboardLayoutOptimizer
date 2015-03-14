using System;
using System.Collections.Generic;
using Generic.Common;

namespace Generic.InputOutput.Parsers.StreamReaders
{
    public static class StreamParserUtil
    {
        static readonly CharParser CharParser = new CharParser();

        public static Parser<MyStreamReader, char> Not(char c)
        {
            return CharParser.Satisfy(c2 => !c2.Equals(c));
        }

        public static Parser<MyStreamReader, char> Symbols(ISet<char> symbols)
        {
            return CharParser.Symbols(symbols);
        }

        public static Parser<MyStreamReader, char> Symbol(char c)
        {
            return CharParser.Symbol(c);
        }

        public static Word Word(string word)
        {
            return new Word(word);
        }

        public static Parser<MyStreamReader, char> AlphaNum()
        {
            return CharParser.AlphaNum();
        }

        public static Parser<MyStreamReader, Unit> EOF()
        {
            return new EOFParser();
        }

        class EOFParser: StreamParser<Unit>
        {
            public override ParseResult<Unit> InnerParse(MyStreamReader input)
            {
                return input.EndOfStream ? Success(Unit.Null) : FailResult("end of stream");
            }
        }

        public static Parser<MyStreamReader, string> BetweenQuotes()
        {
            return CharParser.BetweenQuotes();
        }

        public static Parser<MyStreamReader, TOut> InBraces<TOut>(this Parser<MyStreamReader, TOut> first)
        {
            return CharParser.InBraces(first);
        }

        public static Parser<MyStreamReader, char> Satisfy(Func<char,bool> c)
        {
            return CharParser.Satisfy(c);
        }

    }
}
