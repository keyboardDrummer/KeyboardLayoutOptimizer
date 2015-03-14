using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Common;
using Generic.InputOutput.Printing.Sized;

namespace Generic.InputOutput.Printing.Sizable
{
    internal class LeftRight : Document
    {
        private readonly Document left;
        private readonly Document right;

        public LeftRight(Document left, Document right)
        {
            this.left = left; this.right = right;
            _hashCode = new Memory<int>(CalculateHashCode); 
        }

        internal override int GetMinimumWidth()
        {
            return left.GetMinimumWidth() + right.GetMinimumWidth();
        }

        internal override SizedDocument GetSizedDocument(int? preferredWidth, Func<Document, int?, SizedDocument> recursiveCall)
        {
            var leftPrinting = recursiveCall(left,preferredWidth - right.MinimumWidth);
            var rightPrinting = recursiveCall(right,preferredWidth - leftPrinting.Size.X);
            return leftPrinting + rightPrinting;
        }

        public bool Equals(LeftRight other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.left, left) && Equals(other.right, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(LeftRight))
                return false;
            return Equals((LeftRight)obj);
        }

        readonly Memory<int> _hashCode;
        public override int GetHashCode()
        {
            unchecked
            {
                return _hashCode.Value;
            }
        }

        int CalculateHashCode()
        {
            unchecked
            {
                return ((left != null ? left.GetHashCode() : 0) * 397) ^ (right != null ? right.GetHashCode() : 0);
            }
        }

        public static bool operator ==(LeftRight left, LeftRight right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LeftRight left, LeftRight right)
        {
            return !Equals(left, right);
        }
    }
}