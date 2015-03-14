using System.Collections.Generic;
using Generic.Maybes;

namespace Generic.Parsers
{
    internal class Return<Input, Output> : Parser<Input, Output>
    {
        private readonly Output value;

        public Return(Output value)
        {
            this.value = value;
        }

        public override Maybe<Output> parse(IEnumerator<Input> input)
        {
            return MaybeUtil.Just(value);
        }
    }
}