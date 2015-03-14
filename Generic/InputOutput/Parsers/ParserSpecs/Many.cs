using System.Collections.Generic;

namespace Generic.InputOutput.Parsers.ParserSpecs
{
    class Many<In,Out> : ParserSpec<In,IList<Out>>
    {
        readonly ParserSpec<In, Out> parser;

        public Many(ParserSpec<In, Out> parser)
        {
            this.parser = parser;
        }

        public ParserSpec<In, Out> Parser
        {
            get { return parser; }
        }

        public override OutResult Apply<OutResult>(ParserSpecImplementation<In, IList<Out>, OutResult> implementation)
        {
            return implementation.Many(this);
        }
    }
}
