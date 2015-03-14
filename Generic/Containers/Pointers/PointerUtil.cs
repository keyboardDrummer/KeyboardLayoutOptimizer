using System;

namespace Generic.Containers.Pointers
{
    public class PointerUtil
    {
        public static Pointer<T> New<T>(Func<T> getter, Action<T> setter)
        {
            return new Pointer<T>(getter, setter);
        }
    }
}
