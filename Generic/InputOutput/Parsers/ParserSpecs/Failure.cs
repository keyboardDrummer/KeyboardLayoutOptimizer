using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers.ParserSpecs
{
    class Failure<In,Out> : ParserSpec<In,Out>
    {
        readonly Document errorMessage;

        public Failure(Document errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public Document ErrorMessage
        {
            get { return errorMessage; }
        }

        public override OutResult Apply<OutResult>(ParserSpecImplementation<In, Out, OutResult> implementation)
        {
            return implementation.Failure(this);
        }
    }
}
