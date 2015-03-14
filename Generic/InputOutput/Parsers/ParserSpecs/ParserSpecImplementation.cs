namespace Generic.InputOutput.Parsers.ParserSpecs
{
    public abstract class ParserSpecImplementation<In, Out, Result>
    {
        internal abstract Result Or(Or<In, Out> or);
        internal abstract Result Bind<Temp>(Bind<In, Temp, Out> bind);
        internal abstract Result Return(Return<In, Out> ret);
        internal abstract Result Success(Success<In> success);
        internal abstract Result Many<One>(Many<In, One> many);
        internal abstract Result Failure(Failure<In, Out> failure);
    }
}
