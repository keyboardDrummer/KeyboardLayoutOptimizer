using System.IO;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.Sized
{
    internal class WhiteSpace : SizedDocument
    {
        private readonly IntVector2 size;

        public WhiteSpace(int width, int height) : this(new IntVector2(width,height))
        {
        }

        public WhiteSpace(IntVector2 size)
        {
            this.size = size;
        }

        public override IntVector2 Size
        {
            get { return size; }
        }

        public override void RenderLine(TextWriter builder, int line)
        {
            builder.Write(new string(' ',size.X));
        }

        public bool Equals(WhiteSpace other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.size, size);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(WhiteSpace))
                return false;
            return Equals((WhiteSpace)obj);
        }

        public override int GetHashCode()
        {
            return (size != null ? size.GetHashCode() : 0);
        }

        public static bool operator ==(WhiteSpace left, WhiteSpace right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WhiteSpace left, WhiteSpace right)
        {
            return !Equals(left, right);
        }
    }
}