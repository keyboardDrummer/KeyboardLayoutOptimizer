using System.IO;
using System.Text;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.Sized
{
    internal abstract class SizedDocument
    {
        public abstract void RenderLine(TextWriter builder, int line);
        public abstract IntVector2 Size { get; }

        public static readonly SizedDocument Empty = new SizedEmpty();

        public static SizedDocument operator +(SizedDocument left, SizedDocument right)
        {
            if (left is SizedEmpty)
                return right;
            if (right is SizedEmpty)
                return left;
            return new SizedLeftRight(left, right);
        }

        public static SizedDocument operator ^(SizedDocument top, SizedDocument bottom)
        {
            if (top is SizedEmpty)
                return bottom;
            if (bottom is SizedEmpty)
                return top;
            return new SizedTopBottom(top, bottom);
        }

        public string Render()
        {
            var builder = new StringBuilder((Size.X+2) * Size.Y);
            var writer = new StringWriter(builder);
            var line = 0;
            for (; line < Size.Y - 1; line++)
            {
                RenderLine(writer, line);
                builder.AppendLine();
            }
            RenderLine(writer, line);
            return builder.ToString();
        }

        public override string ToString()
        {
            return Render();
        }
    }
}
