using Imagenic2.Core.Enums;
using Imagenic2.Core.Renderers;
//using System.Drawing.Imaging;

namespace Imagenic2.Core.Images;

public abstract class Image : IDisposable
{
    #region Fields and Properties

    public int FileSize { get; protected set; }

    public int Width { get; set; }
    public int Height { get; set; }

    public Buffer2D<Colour> ColourBuffer { get; set; }
    public PixelFormat PixelFormat { get; set; } = PixelFormat._24bpp;

    #endregion

    #region Constructors

    public Image(/*int width, int height, */Buffer2D<Colour> colourBuffer)
    {
        //Width = width;
        //Height = height;
        Width = colourBuffer.Width;
        Height = colourBuffer.Height;
        ColourBuffer = colourBuffer;
    }

    #endregion

    #region Methods

    public abstract Image DeepCopy();

    public bool Export(string filePath)
    {
        System.Drawing.Bitmap bitmap = ((Bitmap)(this)).ToSystemDrawingBitmap();
        bitmap.Save(filePath);
        return true; // Temporary
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    #endregion
}