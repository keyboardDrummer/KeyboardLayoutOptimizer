using System;

namespace Generic.InputOutput.Parsers.ListParsers
{
    public abstract class ListParser<T,U> : Parser<ListStream<T>,U>
    {
        public override ParserFuncs<ListStream<T>> Funcs
        {
            get { return ListFuncs<T>.Instance; }
        }
    }
    class ListFuncs<TIn> : ParserFuncs<ListStream<TIn>>
    {
        public static readonly ListFuncs<TIn> Instance = new ListFuncs<TIn>(); 
        public override Parser<ListStream<TIn>, TOut> Bind<TInter, TOut>(Parser<ListStream<TIn>, TInter> first, Func<TInter, Parser<ListStream<TIn>, TOut>> getSecond)
        {
            return new Bind<TIn,TInter, TOut>(first, getSecond);
        }

        public override Parser<ListStream<TIn>, TOut> Or<TOut>(Parser<ListStream<TIn>, TOut> first, Parser<ListStream<TIn>, TOut> second)
        {
            return new Or<TIn, TOut>(first, second);
        }

        public override Parser<ListStream<TIn>, TOut> Return<TOut>(TOut value)
        {
            return new Return<TIn, TOut>(value);
        }
    }
}
