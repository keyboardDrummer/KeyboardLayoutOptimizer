using System;

namespace Generic.InputOutput.Parsers.ParserSpecs
{
    class Bind<In,Temp,Out> : ParserSpec<In,Out>
    {
        readonly ParserSpec<In, Temp> first;
        readonly Func<Temp, ParserSpec<In, Out>> second;

        public Bind(ParserSpec<In, Temp> first, Func<Temp, ParserSpec<In, Out>> second)
        {
            this.second = second;
            this.first = first;
        }

        public ParserSpec<In, Temp> First
        {
            get { return first; }
        }

        public Func<Temp, ParserSpec<In, Out>> Second
        {
            get { return second; }
        }

        public override OutResult Apply<OutResult>(ParserSpecImplementation<In, Out, OutResult> implementation)
        {
            return implementation.Bind(this);
        }
    }
}
