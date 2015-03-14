using System;
using Generic.Common;
using Generic.InputOutput.Printing.Sized;

namespace Generic.InputOutput.Printing.Sizable
{
    internal class TopBottom : Document
    {
        readonly Document bottom;
        readonly Document top;

        public TopBottom(Document top, Document bottom)
        {
            this.top = top;
            this.bottom = bottom;
            _hashCode = new Memory<int>(CalculateHashCode); 
        }

        internal override int GetMinimumWidth()
        {
            return Math.Max(top.GetMinimumWidth(), bottom.GetMinimumWidth());
        }

        internal override SizedDocument GetSizedDocument(int? preferredWidth, Func<Document, int?, SizedDocument> recursiveCall)
        {
            var pTop = recursiveCall(top,preferredWidth);
            var pBottom = recursiveCall(bottom,preferredWidth);
            return pTop ^ pBottom;
        }

        public bool Equals(TopBottom other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.bottom, bottom) && Equals(other.top, top);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(TopBottom))
                return false;
            return Equals((TopBottom)obj);
        }

        readonly Memory<int> _hashCode;
        public override int GetHashCode()
        {
            return _hashCode.Value;
        }

        int CalculateHashCode()
        {
            unchecked
            {
                return ((bottom != null ? bottom.GetHashCode() : 0) * 397) ^ (top != null ? top.GetHashCode() : 0);
            }
        }

        public static bool operator ==(TopBottom left, TopBottom right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TopBottom left, TopBottom right)
        {
            return !Equals(left, right);
        }
    }
}
