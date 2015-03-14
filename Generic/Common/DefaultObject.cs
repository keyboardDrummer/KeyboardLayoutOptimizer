using System;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.Common
{
    [Serializable]
    public abstract class DefaultObject
    {
        public override string ToString()
        {
            return this.Print().ToString();
        }
    }
}
