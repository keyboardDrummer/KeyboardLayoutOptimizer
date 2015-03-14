using System;
using Generic.Common;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.ForwardLinkedList
{
    public abstract class ForwardList<T> : DefaultReadOnlyList<T>
    {
        public static readonly ForwardList<T> Empty = new Empty<T>();
        public abstract U Eval<U>(Func<U> empty, Func<T, ForwardList<T>, U> cons);
        public abstract ForwardList<T> AddSorted(T t, Func<T, T, Ordering> compare);
        public abstract Tuple<ForwardList<T>,ForwardList<T>>  SplitAt(Func<T, bool> predicate);
        
        public Tuple<ForwardList<T>, ForwardList<T>> SplitAt(int amount)
        {
            return SplitAt(t => amount-- > 0);
        }

        public ForwardList<T> Skip(Func<T,bool> predicate)
        {
            return SplitAt(predicate).Item2;
        }

        public ForwardList<T> Skip(int amount)
        {
            return Skip(item => amount-- > 0);
        }

        public ForwardList<T> Prepend(T item)
        {
            return new Cons<T>(item, () => this);
        }

        public U Aggregate<U>(U root, Func<T,U,U> collector)
        {
            var result = root;
            var cons = this as Cons<T>;
            while(cons != null)
            {
                result = collector(cons.Head, result);
                cons = cons.Tail as Cons<T>;
            }
            return result;
        }

        public abstract ForwardList<T> Remove(Func<T, bool> predicate);  
        public new ForwardList<T> Remove(T item)
        {
            int count = 0;
            return Remove(i =>
            {
                count++;
                Console.WriteLine("depth = " + count);
                return i.Equals(item);
            });
        }
    }
}
