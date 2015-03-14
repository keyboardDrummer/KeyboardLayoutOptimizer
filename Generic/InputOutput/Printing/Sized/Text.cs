using System.IO;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.Sized
{
    class Text : SizedDocument
    {
        readonly string text;

        public Text(string text)
        {
            this.text = text;
        }

        public override void RenderLine(TextWriter builder, int line)
        {
            builder.Write(text);
        }

        public override IntVector2 Size
        {
            get { return new IntVector2(text.Length,1); }
        }

        public bool Equals(Text other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.text, text);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(Text))
                return false;
            return Equals((Text)obj);
        }

        public override int GetHashCode()
        {
            return (text != null ? text.GetHashCode() : 0);
        }

        public static bool operator ==(Text left, Text right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Text left, Text right)
        {
            return !Equals(left, right);
        }
    }
}
