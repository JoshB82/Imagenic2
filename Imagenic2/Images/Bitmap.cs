using Imagenic2.Core.Renderers;
using Imagenic2.Core.Enums;
using Imagenic2.Core.Utilities;

namespace Imagenic2.Core.Images;

public class Bitmap : Image, IFactory<Bitmap>
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public static Bitmap CreateFromBuffer(Buffer2D<Colour> colourBuffer) => new Bitmap(colourBuffer);

    public static Bitmap CreateFromFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".bmp");

        int width = 0, height = 0;
        Buffer2D<Colour>? buffer = null;
        short bitsPerPixel = 0;
        int fileSize = 0;

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] top = br.ReadBytes(2);
                if (top[0] != 'B' || top[1] != 'M')
                {
                    throw new MalformedDataException("Not a valid .BMP file.");
                }

                fileSize = br.ReadInt32();

                br.ReadInt32();

                int pixelDataOffset = br.ReadInt32();
                int headerSize = br.ReadInt32();
                width = br.ReadInt32();
                height = br.ReadInt32();

                buffer = new Buffer2D<Colour>(width, height < 0 ? height * -1 : height);

                br.ReadInt16();

                bitsPerPixel = br.ReadInt16();

                fs.Position = pixelDataOffset;
                
                int rowSize = (bitsPerPixel * width + 31) / 32 * 4;

                (int start, int finish, int step) = height < 0 ? (0, height, 1) : ((height - 1) * -1, -1, -1);
                for (int y = start; y != finish; y += step)
                {
                    byte[] row = br.ReadBytes(rowSize);

                    for (int x = 0; x < width; x++)
                    {
                        int i = x * 3;
                        
                        byte r = row[i + 2]; // R
                        byte g = row[i + 1]; // G
                        byte b = row[i + 0]; // B

                        buffer[x, y] = new Colour(r, g, b);
                    }
                }
            }
        }

        return new Bitmap(buffer)
        {
            FileSize = fileSize,
            PixelFormat = bitsPerPixel switch
            {
                24 => PixelFormat._24bpp,
                32 => PixelFormat._32bpp,
                _ => throw new InvalidOperationException()
            }
        };
    }

    public Bitmap(int width, int height) : base(new Buffer2D<Colour>(width, height)) { }

    public Bitmap(Buffer2D<Colour> buffer) : base(buffer) { }

    public unsafe Bitmap(System.Drawing.Bitmap bitmap) : base(new Buffer2D<Colour>(bitmap.Width, bitmap.Height))
    {
        /*
        PixelFormat = bitmap.PixelFormat;
        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        int width = bitmap.Width, height = bitmap.Height;
        Parallel.For(0, height, y =>
        {
            byte* rowStart = (byte*)data.Scan0 + y * data.Stride;
            int yIndex = height - 1 - y;
            for (int x = 0; x < width; x++)
            {
                ColourBuffer[x, yIndex] = Color.FromArgb(rowStart[x * 3 + 2], rowStart[x * 3 + 1], rowStart[x * 3]);
            }
        });

        bitmap.UnlockBits(data);
        */
    }

    #endregion

    #region Methods

    public override Bitmap DeepCopy()
    {
        return new Bitmap(ColourBuffer.DeepCopy());
    }

    //public System.Drawing.Bitmap ToSystemDrawingBitmap(System.Drawing.Bitmap? systemDrawingBitmap = null)
    //{
        /*
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
        */
    //}

    /*
    private static unsafe void Format24bppRgb(
        int width,
        int height,
        BitmapData data,
        Buffer2D<Colour> colourBuffer)
    {
        //Parallel.For(0, height, y =>
        //{
        for (int y = 0; y < height; y++)
        {
            byte* rowStart = (byte*)data.Scan0 + y * data.Stride;
            int yIndex = height - 1 - y;
            for (int x = 0; x < width; x++)
            {
                rowStart[x * 3] = colourBuffer[x, yIndex].B; // Blue
                rowStart[x * 3 + 1] = colourBuffer[x, yIndex].G; // Green
                rowStart[x * 3 + 2] = colourBuffer[x, yIndex].R; // Red
            }
        }*/
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
    //}

    #endregion
}