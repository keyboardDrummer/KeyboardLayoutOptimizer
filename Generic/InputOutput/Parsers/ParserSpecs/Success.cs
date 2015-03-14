namespace Generic.InputOutput.Parsers.ParserSpecs
{
    class Success<In> : ParserSpec<In,In>
    {
        public override OutResult Apply<OutResult>(ParserSpecImplementation<In, In, OutResult> implementation)
        {
            return implementation.Success(this);
        }
    }
}
