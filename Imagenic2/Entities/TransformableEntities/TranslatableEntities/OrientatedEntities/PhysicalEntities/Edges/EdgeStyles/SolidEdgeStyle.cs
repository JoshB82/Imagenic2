using System.Drawing;

namespace Imagenic2.Core.Entities;

public class SolidEdgeStyle : EdgeStyle
{
    #region Fields and Properties

    public Colour Colour { get; set; } = Colour.Red;

    #endregion

    #region Constructors

    public SolidEdgeStyle(Colour colour)
    {
        Colour = colour;
    }

    #endregion
}