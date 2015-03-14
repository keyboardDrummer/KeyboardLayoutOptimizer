using System.Collections.Generic;

namespace Generic.Graphs.Algorithm
{
    public interface INode<out G> where G : INode<G>
    {
        IEnumerable<G> Outgoing();
    }
}
