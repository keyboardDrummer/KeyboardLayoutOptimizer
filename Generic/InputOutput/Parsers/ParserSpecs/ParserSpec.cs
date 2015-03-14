namespace Generic.InputOutput.Parsers.ParserSpecs
{
    public abstract class ParserSpec<In, Out>
    {
        public abstract OutResult Apply<OutResult>(ParserSpecImplementation<In, Out, OutResult> implementation);
    }
}
