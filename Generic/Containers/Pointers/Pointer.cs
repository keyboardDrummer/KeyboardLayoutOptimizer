using System;

namespace Generic.Containers.Pointers
{
    public class Pointer<T> : IPointer<T>
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;

        public Pointer(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public T Value
        {
            get { return getter(); }
            set { setter(value); }
        }
    }
}
