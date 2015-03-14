using System.IO;

namespace Generic.InputOutput.Parsers.ListParsers
{
    public class CharParser<T> : ListParser<T,T>
    {

        public override ParseResult<T> InnerParse(ListStream<T> input)
        {
            try
            {
                return Success(input.Read());
            }
            catch (EndOfStreamException)
            {
                return FailResult("end of stream");
            }
        }
    }
}
