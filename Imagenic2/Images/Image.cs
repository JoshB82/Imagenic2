using Imagenic2.Core.Entities;
using Imagenic2.Core.Renderers;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imagenic2.Core.Images;

public abstract class Image
{
    #region Fields and Properties

    public int Width { get; set; }
    public int Height { get; set; }

    public Buffer2D<Color> ColourBuffer { get; set; }
    public PixelFormat PixelFormat { get; set; } = PixelFormat.Format24bppRgb;

    #endregion

    #region Constructors

    public Image(int width, int height, Buffer2D<Color> colourBuffer)
    {
        Width = width;
        Height = height;
        ColourBuffer = colourBuffer;
    }

    #endregion

    #region Methods

    public bool Export(string filePath)
    {
        return false; // Temporary
    }

    #endregion
}