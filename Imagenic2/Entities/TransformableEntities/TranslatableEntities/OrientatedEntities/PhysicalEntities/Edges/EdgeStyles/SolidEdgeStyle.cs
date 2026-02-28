using System.Drawing;

namespace Imagenic2.Core.Entities;

public class SolidEdgeStyle : EdgeStyle
{
    #region Fields and Properties

    public Color Colour { get; set; } = Color.Red;

    #endregion

    #region Constructors

    public SolidEdgeStyle(Color colour)
    {
        Colour = colour;
    }

    #endregion
}