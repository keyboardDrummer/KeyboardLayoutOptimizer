using System.IO;

namespace Generic.InputOutput.Parsers.StreamReaders
{
    class CharParser : StreamParser<char>
    {
        public override ParseResult<char> InnerParse(MyStreamReader input)
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
