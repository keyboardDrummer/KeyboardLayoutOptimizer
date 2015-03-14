using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Generic.InputOutput.UI
{
    class BitmapUtil
    {
        public unsafe void ForEachPixel(Bitmap bitmap, Action<int,int,Pixel> handlePixel)
        {
            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            var pixelSize = GetPixelSize(data.PixelFormat);

            var rowPosition = data.Scan0;
            for(var y = 0;y<bitmap.Height;rowPosition+=data.Stride,y++)
            {
                var position = rowPosition;
                for(int x=0;x<bitmap.Width;x+=1,position+=pixelSize)
                {
                    handlePixel(x, y, *((Pixel*)position));
                }
            }
        }

        static int GetPixelSize(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format32bppArgb: return 4;
                default: throw new InvalidOperationException();
            }
        }
    }
}
