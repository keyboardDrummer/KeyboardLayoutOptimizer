using System.Collections.Generic;

namespace Generic.Containers.Collections.List
{
    public class ListFromEnumerable<T> : ListWrapper<T,ListFromEnumerator<T>>
    {
        private readonly IEnumerable<T> enumerable;
        public ListFromEnumerable(IEnumerable<T> enumerable) : base(new ListFromEnumerator<T>(enumerable.GetEnumerator()))
        {
            this.enumerable = enumerable;
        }

        public bool HasMinimumCount(int minimum)
        {
            return content.HasMinimumCount(minimum);
        }
    }
}
