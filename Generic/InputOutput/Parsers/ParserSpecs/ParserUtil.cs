using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Common;
using Generic.Containers.Collections.List;
using Generic.Containers.Maybes;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers.ParserSpecs
{
    public static class ParserUtil
    {

        public static ParserSpec<In, IList<Out>> Many<In, Out>(this ParserSpec<In, Out> parser)
        {
            return new Many<In, Out>(parser);
        }

        public static ParserSpec<In, Out> Bind<In, Temp, Out>(this ParserSpec<In, Temp> parser, Func<Temp, ParserSpec<In,Out>> func)
        {
            return new Bind<In,Temp,Out>(parser,func);
        }

        public static ParserSpec<In, Out> Select<In, TOut, Out>(this ParserSpec<In, TOut> parser, Func<TOut, Out> func)
        {
            return parser.Bind(p => new Return<In, Out>(func(p)));
        }

        public static ParserSpec<In, Out> IgnoreLeft<In, TOut, Out>(this ParserSpec<In, TOut> left, ParserSpec<In, Out> right)
        {
            return left.Bind(l => right);
        }

        public static ParserSpec<In, Out> IgnoreRight<In, Out, TOut2>(this ParserSpec<In, Out> left, ParserSpec<In, TOut2> right)
        {
            return left.Bind(l => right.Select(r => l));
        }

        public static ParserSpec<In, Out> Satisfy<In, Out>(Func<In, Maybe<Out>> pred)
        {
            return new Success<In>().Bind(c => pred(c).Eval(new Failure<In, Out>("Value" - ((object)c).Print() - "did not satisfy"),
                o => (ParserSpec<In, Out>)new Return<In, Out>(o)));
        }

        public static ParserSpec<In, In> Satisfy<In>(Func<In, bool> pred)
        {
            return new Success<In>().Bind(c => pred(c) ? (ParserSpec<In, In>)new Return<In, In>(c)
                : new Failure<In, In>("Value" - ((object)c).Print() - "did not satisfy"));
        }

        public static ParserSpec<char, int> Int()
        {
            return Satisfy<char>(char.IsDigit).Some().Bind(chars => new string(chars.ToArray()).ParseInt().Eval<ParserSpec<char, int>>(
                new Failure<char, int>(((object)"Could not parse int").Print()),i => new Return<char,int>(i)));
        }

        public static ParserSpec<In, V> SelectMany<In, T, U, V>(this ParserSpec<In, T> id, Func<T, ParserSpec<In, U>> k, Func<T, U, V> s)
        {
            return id.Bind(t => k(t).Select(u => s(t, u)));
        }

        public static ParserSpec<char, string> Word()
        {
            return Satisfy<char>(c => c != ' ').Some().Select(chars => new string(chars.ToArray()));
        }

        public static ParserSpec<char, Out> EndLine<Out>(this ParserSpec<char,Out> parser)
        {
            return parser.IgnoreRight(Symbol('\r'));
        }

        public static ParserSpec<In, In> Symbol<In>(In symbol)
        {
            return new Success<In>().Bind(c => c.Equals(symbol) ? (ParserSpec<In, In>)new Return<In, In>(c)
                : new Failure<In, In>("Found" - ((object)c).Print() - "but expected" - ((object)symbol).Print()));
        }

        public static ParserSpec<In, IList<Out>> Some<In, Out>(this ParserSpec<In, Out> parser)
        {
            return parser.Bind(first => parser.Many().Select(rest => first.Singleton().ConcatList(rest)));
        }

        public static ParserSpec<In, IList<Out>> Times<In, Out>(this ParserSpec<In, Out> parser, int count)
        {
            if (count == 0)
                return new Return<In, IList<Out>>(new List<Out>());
            return parser.Bind(first => parser.Times(count - 1).Select(rest => first.Singleton().ConcatList(rest)));
        }

        //public static Parser<TIn, IList<TOut>> Some<TIn, TOut>(this Parser<TIn, TOut> parser)
        //{
        //    return parser.Bind(x => Many(parser).Map(xs =>
        //    {
        //        IList<TOut> result = new List<TOut>(xs.Count + 1);
        //        result.Add(x);
        //        result.AddRange(xs);
        //        return result;
        //    }));
        //}

        //public static Parser<TIn, IList<TOut>> ManyTill<TIn, TOut, TIgnore>(this Parser<TIn, TOut> parser, Parser<TIn, TIgnore> stop)
        //{
        //    Func<Parser<TIn, IList<TOut>>, Parser<TIn, IList<TOut>>> recursive = manyTill =>
        //        parser.Some().Bind(x => stop.Map(s => x).Or(manyTill.Map(xs =>
        //        {
        //            xs.AddRange(x);
        //            return xs;
        //        })));
        //    return Recursive(recursive);
        //}

        //public static Parser<TIn, TOut> Recursive<TIn, TOut>(Func<Parser<TIn, TOut>, Parser<TIn, TOut>> func)
        //{
        //    var wrapper = new ParserWrapper<TIn, TOut>();
        //    var result = func(wrapper);
        //    wrapper.Inner = result;
        //    return result;
        //}

        //public static Parser<TIn, IList<TOut>> Many<TIn, TOut>(Parser<TIn, TOut> parser)
        //{
        //    IList<TOut> list = new List<TOut>();
        //    return ManyHelper(list, parser).IgnoreLeft(parser.Funcs.Return(list));
        //}

        //public static Parser<TIn, Unit> ManyHelper<TIn, TOut>(IList<TOut> list, Parser<TIn, TOut> parser)
        //{
        //    var recursive = Lazy(() => ManyHelper(list, parser));
        //    return parser.Bind(x =>
        //    {
        //        list.Add(x);
        //        return recursive;
        //    }).Or(parser.Funcs.Return(Unit.Null));
        //}

        //public static Parser<TIn, TOut> InBraces<TIn, TOut>(this Parser<TIn, char> charParser, Parser<TIn, TOut> first)
        //{
        //    return charParser.Between(first, '{', '}');
        //}

        //public static Parser<TIn, TOut> MaybeBind<TIn, TInter, TOut>(this Parser<TIn, TInter> parser, Func<Maybe<TInter>, Parser<TIn, TOut>> getNext)
        //{
        //    return parser.Map(Either<TInter, TOut>.Create)
        //        .Or(getNext(MaybeUtil.Nothing<TInter>()).Map(Either<TInter, TOut>.Create)).Bind(
        //        e => e.Eval(inter => getNext(MaybeUtil.Just(inter)), ou => parser.Funcs.Return(ou)));
        //    //return parser.Map(MaybeUtil.Just).Bind(getNext).Or(getNext(MaybeUtil.Nothing<TInter>()));
        //}

        //public static Parser<TIn, TOut> Lazy<TIn, TOut>(Func<Parser<TIn, TOut>> getParser)
        //{
        //    return new LazyParser<TIn, TOut>(getParser);
        //}

        //public static Parser<TIn, Tuple<TOut, TOut2>> Sequence<TIn, TOut, TOut2>(this Parser<TIn, TOut> first,
        //    Parser<TIn, TOut2> second)
        //{
        //    return first.Bind(f => second.Map(s => Tuple.Create(f, s)));
        //}

        //public static Parser<TIn, Tuple<TOut, TOut2, TOut3>> Sequence<TIn, TOut, TOut2, TOut3>(
        //    this Parser<TIn, Tuple<TOut, TOut2>> first,
        //    Parser<TIn, TOut3> second)
        //{
        //    return first.Bind(fs => second.Map(t => Tuple.Create(fs.Item1, fs.Item2, t)));
        //}

        //public static Parser<TIn, TOut> Not<TIn, TOut>(this Parser<TIn, TOut> parser, TOut c)
        //{
        //    return parser.Satisfy(c2 => !c2.Equals(c));
        //}

        //public static Parser<TIn, char> AlphaNum<TIn>(this Parser<TIn, char> parser)
        //{
        //    return Alpha(parser).Or(Numeric(parser));
        //}

        //public static Parser<TIn, char> Alpha<TIn>(Parser<TIn, char> parser)
        //{
        //    return parser.Satisfy(char.IsLetter);
        //}

        //public static Parser<TIn, char> Numeric<TIn>(Parser<TIn, char> parser)
        //{
        //    return parser.Satisfy(char.IsNumber);
        //}

        //public static Parser<TIn, string> BetweenQuotes<TIn>(this Parser<TIn, char> parser)
        //{
        //    const char quote = '"';
        //    return parser.Between(parser.Not(quote).ParserChars(), quote, quote);
        //}

        //public static Parser<TIn, string> ParserChars<TIn>(this Parser<TIn, char> parseChar)
        //{
        //    return parseChar.Many().Map(i => new string(i.ToArray()));
        //}

        //public static Parser<TIn, TOut> Between<TIn, TOut, TOut2>(this Parser<TIn, TOut2> parseSymbol, Parser<TIn, TOut> parser, TOut2 left, TOut2 right)
        //{
        //    return parseSymbol.Symbol(left).IgnoreLeft(parser).IgnoreRight(parseSymbol.Symbol(right));
        //}

        //static Parser<TIn, TOut> MapResult<TIn, TOut>(this Parser<TIn, TOut> parser, Func<ParseResult<TOut>, ParseResult<TOut>> func)
        //{
        //    return new MapResult<TIn, TOut>(parser, func);
        //}

        //public static Parser<TIn, TOut> Symbols<TIn, TOut>(this Parser<TIn, TOut> parseSymbol, ISet<TOut> symbols)
        //{
        //    return parseSymbol.Satisfy(symbols.Contains);
        //}

        //public static Parser<TIn, TOut> Fail<TIn, TOut>(this Parser<TIn, TOut> parser, Document log)
        //{
        //    return Fail<TIn, TOut>(parser.Funcs, log);
        //}

        //public static Parser<TIn, TOut> Fail<TIn, TFoo, TOut>(this Parser<TIn, TFoo> parser, TOut x, Document log)
        //{
        //    return Fail<TIn, TOut>(parser.Funcs, log);
        //}

        //public static Parser<TIn, TOut> Fail<TIn, TOut>(this ParserFuncs<TIn> parser, Document log)
        //{
        //    return new Fail<TIn, TOut>(parser, log);
        //}
    }
}
