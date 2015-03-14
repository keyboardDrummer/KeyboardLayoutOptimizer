using System;
using System.Collections.Generic;
using Generic.Maybes;

namespace Generic.Parsers
{
    internal class Map<Input, Out1, Out2> : Parser<Input, Out2>
    {
        private readonly Func<Out1, Out2> func;
        private readonly Parser<Input, Out1> parser;

        public Map(Parser<Input, Out1> parser, Func<Out1, Out2> func)
        {
            this.parser = parser;
            this.func = func;
        }

        public override Maybe<Out2> parse(IEnumerator<Input> input)
        {
            return parser.parse(input).Map(func);
        }
    }
}