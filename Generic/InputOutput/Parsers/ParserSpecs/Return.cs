namespace Generic.InputOutput.Parsers.ParserSpecs
{
    class Return<In,Out> : ParserSpec<In,Out>
    {
        readonly Out result;

        public Return(Out result)
        {
            this.result = result;
        }

        public Out Result
        {
            get { return result; }
        }

        public override OutResult Apply<OutResult>(ParserSpecImplementation<In, Out, OutResult> implementation)
        {
            return implementation.Return(this);
        }
    }
}
