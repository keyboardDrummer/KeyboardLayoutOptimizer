using System.Linq;
using Generic.Containers.Collections.Tree;

namespace Generic.Graphs
{
    class DominanceTree<Info> : Tree<Info> where Info : class
    {
        public DominanceTree(DominanceGraph graph) : base(null)
        {
            //
        }

        private DominanceGraph.INode FindRoot(DominanceGraph graph)
        {
            return graph.Nodes.FirstOrDefault(node => !node.BackwardNodes.Any());
        }
    }
}
