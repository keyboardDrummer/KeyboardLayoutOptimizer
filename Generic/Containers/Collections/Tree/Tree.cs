using System.Collections.Generic;
using Generic.Common;

namespace Generic.Containers.Collections.Tree
{
    public class Tree : Tree<Unit>
    {

    }

    public class Tree<T> where T : class
    {
        public IEnumerable<Tree<T>> Children { get { return children; } }
        public T Data { get; set; }

        public Tree() : this(null) { }
        public Tree(T data)
        {
            Data = data;
        }

        public Tree(T data, IEnumerable<Tree<T>> children) : this(data)
        {
            this.children.AddRange(children);
        }

        public void Add(Tree<T> child)
        {
            children.Add(child);
            child.parent = this;
        }

        public void Add(T unknown)
        {
            var child = new Tree<T>(unknown);
            Add(child);
        }

        private Tree<T> parent;
        private List<Tree<T>> children = new List<Tree<T>>();
    }
}
