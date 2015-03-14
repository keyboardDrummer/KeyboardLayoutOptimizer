using System;
using System.IO;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.Sized
{
    internal class SizedLeftRight : SizedDocument
    {
        private readonly SizedDocument left;
        private readonly SizedDocument right;
        readonly IntVector2 size;

        public SizedLeftRight(SizedDocument left, SizedDocument right)
        {
            this.left = left; this.right = right;
            this.size = new IntVector2(left.Size.X + right.Size.X,Math.Max(left.Size.Y,right.Size.Y));

            int diff = left.Size.Y - right.Size.Y;
            if (diff > 0)
                this.right = new WhiteSpace(right.Size.X, diff) ^ right;
            else if (diff < 0)
                this.left = left ^ new WhiteSpace(left.Size.X, -diff);
        }

        public override IntVector2 Size
        {
            get { return size; }
        }

        public override void RenderLine(TextWriter textWriter, int line)
        {
            left.RenderLine(textWriter, line);
            right.RenderLine(textWriter, line);
        }
    }
}