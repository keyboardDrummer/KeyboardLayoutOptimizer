using System.Drawing;
using System.Runtime.InteropServices;

namespace Generic.InputOutput.UI
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Pixel
    {
        // These fields provide access to the individual
        // components (A, R, G, and B), or the data as
        // a whole in the form of a 32-bit integer
        // (signed or unsigned). Raw fields are used
        // instead of properties for performance considerations.
        [FieldOffset(0)]
        public int Int32;
        [FieldOffset(0)]
        public uint UInt32;
        [FieldOffset(0)]
        public byte Blue;
        [FieldOffset(1)]
        public byte Green;
        [FieldOffset(2)]
        public byte Red;
        [FieldOffset(3)]
        public byte Alpha;

        // Gets an initialized Pixel object so that the compiler
        // doesn't complain about an uninitialized object.
        public static Pixel GetNew()
        {
            Pixel value;
            value.Int32 = 0;
            value.UInt32 = 0;
            value.Red = 0;
            value.Green = 0;
            value.Blue = 0;
            value.Alpha = 0;

            return value;
        }

        // Gets a Pixel object initialized with data 
        // for a 32-bit pixel.
        public Pixel(int val)
        {
            Red = 0;
            Green = 0;
            Blue = 0;
            Alpha = 0;
            UInt32 = 0;
            Int32 = val;
        }

        // Gets a Pixel object initialized with data 
        // for a 32-bit pixel.
        public Pixel(uint val)
        {
            Red = 0;
            Green = 0;
            Blue = 0;
            Alpha = 0;
            Int32 = 0;
            UInt32 = val;
        }

        // Converts this object to/from a System.Drawing.Color object.
        public Color Color
        {
            get
            {
                return Color.FromArgb(Int32);
            }
            set
            {
                Int32 = value.ToArgb();
            }
        }
    }
}
