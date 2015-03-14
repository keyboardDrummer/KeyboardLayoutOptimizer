using System;
using System.IO;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.Sized
{
    internal class SizedTopBottom : SizedDocument
    {
        readonly SizedDocument bottom;
        readonly SizedDocument top;
        readonly IntVector2 size;

        public SizedTopBottom(SizedDocument top, SizedDocument bottom)
        {
            this.top = top;
            this.bottom = bottom;
            size = new IntVector2(Math.Max(top.Size.X, bottom.Size.X), bottom.Size.Y + top.Size.Y);

            var diff = top.Size.X - bottom.Size.X;
            if (diff > 0)
                this.bottom = bottom + new WhiteSpace(new IntVector2(diff, bottom.Size.Y));
            else if (diff < 0)
                this.top = top + new WhiteSpace(new IntVector2(-diff, top.Size.Y));
        }

        public override IntVector2 Size
        {
            get { return size; }
        }

        public override void RenderLine(TextWriter textWriter, int line)
        {
            if (line < top.Size.Y)
                top.RenderLine(textWriter, line);
            else
                bottom.RenderLine(textWriter, line - top.Size.Y);
        }
    }
}
