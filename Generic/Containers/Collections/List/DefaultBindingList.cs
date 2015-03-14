using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Generic.Containers.Collections.List
{
    public class DefaultBindingList<T> : ListWrapper<T,IList<T>>, IBindingList
    {
        public DefaultBindingList(IList<T> content)
            : base(content)
        {
        }

        public override void Insert(int index, T item)
        {
            base.Insert(index,item);
            ListChanged(this,new ListChangedEventArgs(ListChangedType.ItemAdded,index));
        }

        public override void Add(T item)
        {
            Insert(Count,item);
        }

        public void RaiseListChanged()
        {
            ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public override bool Remove(T item)
        {
            int index = IndexOf(item);
            var removed = base.Remove(item);
            ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
            return removed;
        }

        public override void Clear()
        {
            base.Clear();
            ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        public override T this[int index]
        {
            get { return base[index]; }
            set 
            { 
                base[index] = value;
                ListChanged(this,new ListChangedEventArgs(ListChangedType.ItemChanged,index));
            }
        }

        public object AddNew()
        {
            throw new NotImplementedException();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }

        public bool AllowNew
        {
            get { throw new NotImplementedException(); }
        }

        public bool AllowEdit
        {
            get { throw new NotImplementedException(); }
        }

        public bool AllowRemove
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportsChangeNotification
        {
            get { return true; }
        }

        public bool SupportsSearching
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportsSorting
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSorted
        {
            get { throw new NotImplementedException(); }
        }

        public PropertyDescriptor SortProperty
        {
            get { throw new NotImplementedException(); }
        }

        public ListSortDirection SortDirection
        {
            get { throw new NotImplementedException(); }
        }

        public event ListChangedEventHandler ListChanged;
    }
}
