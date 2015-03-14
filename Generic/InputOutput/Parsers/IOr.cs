namespace Generic.InputOutput.Parsers
{
    interface IOr<In,Out>
    {
        Parser<In, Out> First { get; }
        Parser<In, Out> Second { get; }
    }
}
