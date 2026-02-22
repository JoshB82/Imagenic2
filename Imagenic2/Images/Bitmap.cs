using Imagenic2.Core.Renderers;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imagenic2.Core.Images;

public class Bitmap : Image
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public Bitmap(int width, int height) : base(width, height, new Buffer2D<Color>(width, height))
    {

    }

    public Bitmap(Buffer2D<Color> buffer) : base(buffer.FirstDimensionSize, buffer.SecondDimensionSize, buffer)
    {

    }

    #endregion

    #region Methods

    public static implicit operator System.Drawing.Bitmap(Bitmap bitmap)
    {
        System.Drawing.Bitmap systemDrawingBitmap = new System.Drawing.Bitmap(bitmap.Width, bitmap.Height);
        BitmapData data = systemDrawingBitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

        switch (bitmap.PixelFormat)
        {
            case PixelFormat.Format24bppRgb:
                Format24bppRgb(bitmap.Width, bitmap.Height, data, bitmap.ColourBuffer);
                break;
        }

        systemDrawingBitmap.UnlockBits(data);
        return systemDrawingBitmap;
    }

    private static unsafe void Format24bppRgb(
        int width,
        int height,
        BitmapData data,
        Buffer2D<Color> colourBuffer)
    {
        const int noTasks = 4; // TODO: Move to configuration

        int smallHeight = height / noTasks;
        int noSmallHeights = noTasks - height % noTasks;
        int largeHeight = smallHeight + 1;
        int noLargeHeights = height % noTasks;

        Task[] renderTasks = new Task[noTasks];

        for (int i = 0; i < noSmallHeights; i++)
        {
            int ii = i;
            renderTasks[i] = Task.Factory.StartNew(() =>
            {
                for (int y = ii * smallHeight; y < (ii + 1) * smallHeight; y++)
                {
                    byte* rowStart = (byte*)data.Scan0 + y * data.Stride;
                    for (int x = 0; x < width; x++)
                    {
                        rowStart[x * 3] = colourBuffer.Values[x][y * -1 + height - 1].B; // Blue
                        rowStart[x * 3 + 1] = colourBuffer.Values[x][y * -1 + height - 1].G; // Green
                        rowStart[x * 3 + 2] = colourBuffer.Values[x][y * -1 + height - 1].R; // Red
                    }
                }

                #if DEBUG

                Console.WriteLine("Task completed.");

                #endif
            });
        }

        for (int i = 0; i < noLargeHeights; i++)
        {
            int ii = i;
            renderTasks[i + noSmallHeights] = Task.Factory.StartNew(() =>
            {
                for (int y = ii * largeHeight + noSmallHeights * smallHeight; y < (ii + 1) * largeHeight + noSmallHeights * smallHeight; y++)
                {
                    byte* rowStart = (byte*)data.Scan0 + y * data.Stride;
                    for (int x = 0; x < width; x++)
                    {
                        rowStart[x * 3] = colourBuffer.Values[x][y * -1 + height - 1].B; // Blue
                        rowStart[x * 3 + 1] = colourBuffer.Values[x][y * -1 + height - 1].G; // Green
                        rowStart[x * 3 + 2] = colourBuffer.Values[x][y * -1 + height - 1].R; // Red
                    }
                }

                #if DEBUG

                Console.WriteLine("Task completed.");

                #endif
            });
        }

        Task.WaitAll(renderTasks);
    }

    #endregion
}