using Generic.InputOutput.Parsers.StreamReaders;

namespace Generic.InputOutput.Parsers.ParserSpecs
{
    public class StreamParserSpecImplementation<Out> : ParserSpecImplementation<char,Out,Parser<MyStreamReader,Out>>
    {
        public static readonly StreamParserSpecImplementation<Out> Instance = new StreamParserSpecImplementation<Out>();

        internal override Parser<MyStreamReader, Out> Or(Or<char, Out> or)
        {
            return StreamFuncs.Instance.Or(or.First.Apply(this), or.Second.Apply(this));
        }

        internal override Parser<MyStreamReader, Out> Bind<Temp>(Bind<char, Temp, Out> bind)
        {
            return StreamFuncs.Instance.Bind(bind.First.Apply(StreamParserSpecImplementation<Temp>.Instance), x => bind.Second(x).Apply(this));
        }

        internal override Parser<MyStreamReader, Out> Return(Return<char, Out> ret)
        {
            return StreamFuncs.Instance.Return(ret.Result);
        }

        internal override Parser<MyStreamReader, Out> Success(Success<char> success)
        {
            return (Parser<MyStreamReader,Out>)(object)new CharParser();
        }

        internal override Parser<MyStreamReader, Out> Many<One>(Many<char, One> many)
        {
            return (Parser<MyStreamReader, Out>)(object)StreamFuncs.Instance.Many(many.Parser.Apply(StreamParserSpecImplementation<One>.Instance));
        }

        internal override Parser<MyStreamReader, Out> Failure(Failure<char, Out> failure)
        {
            return new Fail<MyStreamReader, Out>(StreamFuncs.Instance,failure.ErrorMessage);
        }
    }
}
