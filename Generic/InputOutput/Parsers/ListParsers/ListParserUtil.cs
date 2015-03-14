using Generic.Common;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers.ListParsers
{
    public static class ListParserUtil
    {
        //static readonly CharParser CharParser = new CharParser();

        //public static Parser<StreamReader, char> Not(char c)
        //{
        //    return CharParser.Satisfy(c2 => !c2.Equals(c));
        //}

        //public static Parser<StreamReader, char> Symbols(ISet<char> symbols)
        //{
        //    return CharParser.Symbols(symbols);
        //}

        //public static Word Word(string word)
        //{
        //    return new Word(word);
        //}

        //public static Parser<StreamReader, char> AlphaNum()
        //{
        //    return CharParser.AlphaNum();
        //}

        //public static Parser<StreamReader, Bottom> EOF()
        //{
        //    return CharParser.EOF();
        //}

        //public static Parser<StreamReader, string> BetweenQuotes()
        //{
        //    return CharParser.BetweenQuotes();
        //}

        //public static Parser<StreamReader, TOut> InBraces<TOut>(this Parser<StreamReader, TOut> first)
        //{
        //    return CharParser.InBraces(first);
        //}

        //public static Parser<ListStream<string>,MetaObject> FromFuncStream(Func<object, MetaObject> func)
        //{
        //    throw new NotImplementedException();
        //}


        public static Parser<ListStream<TIn>, Unit> EOF<TIn>()
        {
            return new EOFParser<TIn>();
        }

        class EOFParser<T> : ListParser<T,Unit>
        {
            public override ParseResult<Unit> InnerParse(ListStream<T> input)
            {
                if (input.EndOfStream)
                    return Success(Unit.Null);
                return FailResult(("Failed to parse eof, found token" - ((object)input.Read()).Print()));
            }
        }
    }
}
