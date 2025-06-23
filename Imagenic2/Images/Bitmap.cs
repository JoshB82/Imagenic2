using Imagenic2.Core.Renderers;
using System.Drawing;

namespace Imagenic2.Core.Images;

public class Bitmap : Image
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public Bitmap(int width, int height) : base(width, height)
    {

    }

    public Bitmap(Buffer2D<Color> buffer) : base(buffer.FirstDimensionSize, buffer.SecondDimensionSize)
    {

    }

    #endregion

    #region Methods

    #endregion
}