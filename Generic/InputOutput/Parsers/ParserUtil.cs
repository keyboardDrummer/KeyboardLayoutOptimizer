using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Common;
using Generic.Containers.Collections.List;
using Generic.Containers.Eithers;
using Generic.Containers.Maybes;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers
{
    public static class ParserUtil
    {

        public static Parser<TIn, TOut2> Map<TIn,TOut,TOut2>(this Parser<TIn, TOut> parser, Func<TOut, TOut2> func)
        {
            return parser.Bind(p => parser.Funcs.Return(func(p)));
        }

        public static Parser<TIn, TOut2> IgnoreLeft<TIn, TOut, TOut2>(this Parser<TIn, TOut> left, Parser<TIn, TOut2> right)
        {
            return left.Bind(l => right);
        }

        public static Parser<TIn, TOut> IgnoreRight<TIn, TOut, TOut2>(this Parser<TIn, TOut> left, Parser<TIn, TOut2> right)
        {
            return left.Bind(l => right.Map(r => l));
        }

        public static Parser<TIn, IList<TOut>> Some<TIn, TOut>(this Parser<TIn, TOut> parser)
        {
            return parser.Bind(x => Many(parser).Map(xs =>
            {
                IList<TOut> result = new List<TOut>(xs.Count + 1);
                result.Add(x);
                result.AddRange(xs);
                return result;
            }));
        }

        public static Parser<TIn, IList<TOut>> ManyTill<TIn, TOut, TIgnore>(this Parser<TIn, TOut> parser, Parser<TIn, TIgnore> stop)
        {
            Func<Parser<TIn, IList<TOut>>, Parser<TIn, IList<TOut>>> recursive = manyTill => 
                parser.Some().Bind(x => stop.Map(s => x).Or(manyTill.Map(xs =>
            {
                xs.AddRange(x);
                return xs;
            })));
            return Recursive(recursive);
        }
 
        public static Parser<TIn,TOut> Recursive<TIn,TOut>(Func<Parser<TIn,TOut>,Parser<TIn,TOut>> func)
        {
            var wrapper = new ParserWrapper<TIn, TOut>();
            var result = func(wrapper);
            wrapper.Inner = result;
            return result;
        }  

        public static Parser<TIn, IList<TOut>> Many<TIn, TOut>(Parser<TIn, TOut> parser)
        {
            IList<TOut> list = new List<TOut>();
            return ManyHelper(list,parser).IgnoreLeft(parser.Funcs.Return(list));
        }
        
        public static Parser<TIn, Unit> ManyHelper<TIn, TOut>(IList<TOut> list, Parser<TIn, TOut> parser)
        {
            var recursive = Lazy(() => ManyHelper(list,parser));
            return parser.Bind(x =>
            {
                list.Add(x);
                return recursive;
            }).Or(parser.Funcs.Return(Unit.Null));
        }

        public static Parser<TIn, TOut> InBraces<TIn, TOut>(this Parser<TIn, char> charParser, Parser<TIn, TOut> first)
        {
            return charParser.Between(first,'{','}');
        }
        
        public static Parser<TIn,TOut> MaybeBind<TIn,TInter,TOut>(this Parser<TIn, TInter> parser, Func<Maybe<TInter>,Parser<TIn,TOut>> getNext)
        {
            return parser.Map(Either<TInter, TOut>.Create)
                .Or(getNext(MaybeUtil.Nothing<TInter>()).Map(Either<TInter, TOut>.Create)).Bind(
                e => e.Eval(inter => getNext(MaybeUtil.Just(inter)), ou => parser.Funcs.Return(ou)));
            //return parser.Map(MaybeUtil.Just).Bind(getNext).Or(getNext(MaybeUtil.Nothing<TInter>()));
        }

        public static Parser<TIn, TOut> Lazy<TIn, TOut>(Func<Parser<TIn, TOut>> getParser)
        {
            return new LazyParser<TIn, TOut>(getParser);
        }

        public static Parser<TIn,Tuple<TOut,TOut2>> Sequence<TIn,TOut,TOut2>(this Parser<TIn,TOut> first,
            Parser<TIn,TOut2> second)
        {
            return first.Bind(f => second.Map(s => Tuple.Create(f, s)));
        }

        public static Parser<TIn, Tuple<TOut, TOut2,TOut3>> Sequence<TIn, TOut, TOut2,TOut3>(
            this Parser<TIn, Tuple<TOut, TOut2>> first,
            Parser<TIn, TOut3> second)
        {
            return first.Bind(fs => second.Map(t => Tuple.Create(fs.Item1,fs.Item2, t)));
        }

        public static Parser<TIn, TOut> Not<TIn, TOut>(this Parser<TIn,TOut> parser, TOut c)
        {
            return parser.Satisfy(c2 => !c2.Equals(c));
        }

        public static Parser<TIn, char> AlphaNum<TIn>(this Parser<TIn, char> parser)
        {
            return Alpha(parser).Or(Numeric(parser));
        }

        public static Parser<TIn, char> Alpha<TIn>(Parser<TIn, char> parser)
        {
            return parser.Satisfy(char.IsLetter);
        }

        public static Parser<TIn, char> Numeric<TIn>(Parser<TIn, char> parser)
        {
            return parser.Satisfy(char.IsNumber);
        }

        public static Parser<TIn, string> BetweenQuotes<TIn>(this Parser<TIn, char> parser)
        {
            const char quote = '"';
            return parser.Between(parser.Not(quote).ParserChars(),quote, quote);
        }

        public static Parser<TIn,string> ParserChars<TIn>(this Parser<TIn,char> parseChar)
        {
            return parseChar.Many().Map(i => new string(i.ToArray()));
        }

        public static Parser<TIn, TOut> Between<TIn, TOut, TOut2>(this Parser<TIn, TOut2> parseSymbol, Parser<TIn, TOut> parser, TOut2 left, TOut2 right)
        {
            return parseSymbol.Symbol(left).IgnoreLeft(parser).IgnoreRight(parseSymbol.Symbol(right));
        }

        public static Parser<TIn, TOut> Satisfy<TIn, TOut>(this Parser<TIn,TOut> parseSymbol, Func<TOut,bool> pred)
        {
            return parseSymbol.Bind(c => pred(c) ? parseSymbol.Funcs.Return(c) 
                : parseSymbol.Fail("Value" - ((object)c).Print() - "did not satisfy"));
        }

        static Parser<TIn,TOut> MapResult<TIn,TOut>(this Parser<TIn,TOut> parser, Func<ParseResult<TOut>,ParseResult<TOut>> func)
        {
            return new MapResult<TIn, TOut>(parser, func);
        }

        public static Parser<TIn, TOut> Symbols<TIn, TOut>(this Parser<TIn, TOut> parseSymbol, ISet<TOut> symbols)
        {
            return parseSymbol.Satisfy(symbols.Contains);
        }

        public static Parser<TIn, TOut> Symbol<TIn, TOut>(this Parser<TIn, TOut> parseSymbol, TOut symbol)
        {
            return parseSymbol.Bind(c => c.Equals(symbol) ? parseSymbol.Funcs.Return(c)
                : parseSymbol.Fail("Found" - ((object)c).Print() - "but expected" - ((object)symbol).Print()));
        }

        public static Parser<TIn, TOut> Fail<TIn, TOut>(this Parser<TIn, TOut> parser, Document log)
        {
            return Fail<TIn,TOut>(parser.Funcs,log);
        }

        public static Parser<TIn, TOut> Fail<TIn, TFoo, TOut>(this Parser<TIn,TFoo> parser, TOut x, Document log)
        {
            return Fail<TIn, TOut>(parser.Funcs, log);
        }

        public static Parser<TIn,TOut> Fail<TIn,TOut>(this ParserFuncs<TIn> parser, Document log)
        {
            return new Fail<TIn, TOut>(parser,log);
        }
    }
}
