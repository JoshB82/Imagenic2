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

    public unsafe Bitmap(System.Drawing.Bitmap bitmap) : base(bitmap.Width, bitmap.Height, new Buffer2D<Color>(bitmap.Width, bitmap.Height))
    {
        PixelFormat = bitmap.PixelFormat;
        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        int width = bitmap.Width, height = bitmap.Height;
        Parallel.For(0, height, y =>
        {
            byte* rowStart = (byte*)data.Scan0 + y * data.Stride;
            int yIndex = height - 1 - y;
            for (int x = 0; x < width; x++)
            {
                ColourBuffer.Values[x][yIndex] = Color.FromArgb(rowStart[x * 3 + 2], rowStart[x * 3 + 1], rowStart[x * 3]);
            }
        });

        bitmap.UnlockBits(data);
    }

    #endregion

    #region Methods

    public override Bitmap DeepCopy()
    {
        return new Bitmap(ColourBuffer.DeepCopy());
    }

    public System.Drawing.Bitmap ToSystemDrawingBitmap(System.Drawing.Bitmap? systemDrawingBitmap = null)
    {
        systemDrawingBitmap ??= new System.Drawing.Bitmap(Width, Height);
        BitmapData data = systemDrawingBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat);

        switch (PixelFormat)
        {
            case PixelFormat.Format24bppRgb:
                Format24bppRgb(Width, Height, data, ColourBuffer);
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
        //Parallel.For(0, height, y =>
        //{
        for (int y = 0; y < height; y++)
        {
            byte* rowStart = (byte*)data.Scan0 + y * data.Stride;
            int yIndex = height - 1 - y;
            for (int x = 0; x < width; x++)
            {
                rowStart[x * 3] = colourBuffer.Values[x][yIndex].B; // Blue
                rowStart[x * 3 + 1] = colourBuffer.Values[x][yIndex].G; // Green
                rowStart[x * 3 + 2] = colourBuffer.Values[x][yIndex].R; // Red
            }
        }
        //});

        /*
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
                        rowStart[x * 3] = colourBuffer.Values[x][height - 1 - y].B; // Blue
                        rowStart[x * 3 + 1] = colourBuffer.Values[x][height - 1 - y].G; // Green
                        rowStart[x * 3 + 2] = colourBuffer.Values[x][height - 1 - y].R; // Red
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
                        rowStart[x * 3] = colourBuffer.Values[x][height - 1 - y].B; // Blue
                        rowStart[x * 3 + 1] = colourBuffer.Values[x][height - 1 - y].G; // Green
                        rowStart[x * 3 + 2] = colourBuffer.Values[x][height - 1 - y].R; // Red
                    }
                }

                #if DEBUG

                Console.WriteLine("Task completed.");

                #endif
            });
        }

        Task.WaitAll(renderTasks);

        */
    }

    #endregion
}