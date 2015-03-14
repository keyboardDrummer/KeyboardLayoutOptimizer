using System;

namespace Generic.InputOutput.Parsers
{
    interface IBind<In, Inter,Out>
    {
        Parser<In, Inter> First { get; }
        Func<Inter, Parser<In, Out>> GetSecond { get; }
    }
}
