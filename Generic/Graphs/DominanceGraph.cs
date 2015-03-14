using System;
using System.Collections.Generic;
using Generic.Common;
using Generic.Containers.Collections;

namespace Generic.Graphs
{
    internal class DominanceGraph : Graph
    {
        private class DominanceAlgorithm<NodeInfo>
        {
            private DominanceGraph graph;

            private PartnerSet<INode, Graph<NodeInfo, Unit>.INode> partnerSet =
                new PartnerSet<INode, Graph<NodeInfo, Unit>.INode>();

            public DominanceAlgorithm(Graph<NodeInfo> controlFlowGraph)
            {
                graph = new DominanceGraph();

                //Copy nodes.
                foreach (Graph<NodeInfo, Unit>.INode node in controlFlowGraph.Nodes)
                {
                    INode domNode = graph.AddNode();
                    partnerSet.AddPair(domNode, node);
                }

                //Fill edges.
                foreach (INode from in graph.Nodes)
                {
                    foreach (INode to in graph.Nodes)
                    {
                        graph.AddEdge(from, to);
                    }
                }

                //Prune
                const bool changed = false;
                do
                {
                    foreach (INode node in graph.Nodes)
                    {
                        var dominators = new List<INode>();
                        var preds = getPreds(node);
                        //dominators.Intersect
                        //newBackward.Add(node);


                        //foreach (Graph<NodeInfo>.INode node2 in 
                    }
                } while (changed);
            }

            private IEnumerable<INode> getPreds(INode node)
            {
                foreach (Graph<NodeInfo, Unit>.INode partner in partnerSet[node].BackwardNodes)
                {
                    yield return partnerSet[partner];
                }
            }

            private IEnumerable<INode> getSucs(INode node)
            {
                foreach (Graph<NodeInfo, Unit>.INode partner in partnerSet[node].ForwardNodes)
                {
                    yield return partnerSet[partner];
                }
            }
        }
    }
}