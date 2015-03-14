using System.IO;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.Sized
{
    internal class SizedEmpty : SizedDocument
    {
        public override void RenderLine(TextWriter builder, int line)
        {
        }

        public override IntVector2 Size
        {
            get { return new IntVector2(0,0); }
        }
    }
}