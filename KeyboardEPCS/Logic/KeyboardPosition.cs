using System;
using Generic;
using Generic.Common;
using Generic.Containers.Collections.List;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;
using KeyboardEPCS.Logic.Inputs;

namespace KeyboardEPCS.Logic
{
    [Serializable]
    public class KeyboardPosition : DefaultObject, IPrintable
    {
        readonly int row;
        readonly int column;

        public KeyboardPosition(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public int Row
        {
            get { return row; }
        }

        public int Column
        {
            get { return column; }
        }

        public Document Print()
        {
            return ListUtil.New(Row,Column).Parameters();
        }

        public bool Equals(KeyboardPosition other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.row == row && other.column == column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(KeyboardPosition))
                return false;
            return Equals((KeyboardPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (row * 397) ^ column;
            }
        }
    }
}
