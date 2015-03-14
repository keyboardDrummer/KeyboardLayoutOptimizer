using System.Collections.Generic;
using Generic.Common;

namespace Generic.Graphs
{
    public class Graph : Graph<Unit>
    {
        public INode AddNode()
        {
            return AddNode(null);
        }
    }

    public class Graph<NodeInfo> : Graph<NodeInfo, Unit>
    {
        public void AddEdge(INode from, INode to)
        {
            AddEdge(from, to, null);
        }
    }

    public class Graph<NodeInfo,EdgeInfo>
    {
        public IEnumerable<INode> Nodes { get { return nodes; } } 
        public IEnumerable<IEdge> Edges { get { return edges; } }

        public INode AddNode(NodeInfo info)
        {
            Node node = new Node(info);
            nodes.Add(node);
            return node;
        }

        public void AddNodeRange(IEnumerable<NodeInfo> infos)
        {
            foreach (NodeInfo info in infos) { AddNode(info); }
        }

        public void AddEdge(INode iFrom, INode iTo, EdgeInfo info)
        {
            Node from = iFrom as Node;
            Node to = iTo as Node;
            Edge edge = new Edge(from, to, info);
            from.AddEdge(edge);
            to.AddEdge(edge);
            edges.Add(edge);
        }

        public void RemoveEdge(IEdge iEdge)
        {
            RemoveEdge(iEdge as Edge);
        }

        private void RemoveEdge(Edge edge)
        {
            edges.Remove(edge);
            edge.From.RemoveEdge(edge);
            edge.To.RemoveEdge(edge);
        }

        public void RemoveEdge(INode iFrom, INode iTo)
        {
            Node from = iFrom as Node; Node to = iTo as Node;
            Edge edge = from.GetForwardEdge(to);
            RemoveEdge(edge);
        }

        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();

        public interface INode
        {
            IEnumerable<IEdge> ForwardEdges { get; }
            IEnumerable<IEdge> BackwardEdges { get; }
            IEnumerable<INode> ForwardNodes { get; }
            IEnumerable<INode> BackwardNodes { get; }
            NodeInfo Info { get; }
        }

        public interface IEdge
        {
            INode From { get; }
            INode To { get; }
            EdgeInfo Info { get; }
        }

        class Edge : IEdge
        {
            INode IEdge.From { get { return To; } }
            INode IEdge.To { get { return From; } }

            public Node From { get; private set; }
            public Node To { get; private set; }
            public EdgeInfo Info { get; private set; }

            public Edge(Node from, Node to, EdgeInfo info)
            {
                this.From = from; this.To = to; this.Info = info;
            }
        }

        class Node : INode
        {
            private Dictionary<Node, Edge> Forward = new Dictionary<Node, Edge>();
            private Dictionary<Node, Edge> Backward = new Dictionary<Node, Edge>();
            public NodeInfo Info { get; private set; }

            public Node(NodeInfo info)
            {
                this.Info = info;
            }

            public void AddEdge(Edge edge)
            {
                bool forward = edge.From == this;
                bool backward = edge.To == this;
                if (forward) { Forward.Add(edge.To, edge); }
                if (backward) { Backward.Add(edge.From, edge); }
            }

            public void RemoveEdge(Edge edge)
            {
                bool forward = edge.From == this;
                bool backward = edge.To == this;
                if (forward) { Forward.Remove(edge.To); }
                if (backward) { Backward.Remove(edge.From); }
            }

            public Edge GetForwardEdge(Node to)
            {
                return Forward[to];
            }

            public Edge GetBackwardEdge(Node from)
            {
                return Backward[from];
            }

            public IEnumerable<IEdge> ForwardEdges
            {
                get { return Forward.Values; }
            }

            public IEnumerable<IEdge> BackwardEdges
            {
                get { return Backward.Values; }
            }


            public IEnumerable<INode> ForwardNodes
            {
                get { return Forward.Keys; }
            }

            public IEnumerable<INode> BackwardNodes
            {
                get { return Backward.Keys; }
            }
        }
    }
}
