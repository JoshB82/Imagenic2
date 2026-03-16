using System.Drawing;

namespace Imagenic2.Core.Entities;

public class SolidStyle : FaceStyle
{
    #region Fields and Parameters

    public Color Colour { get; set; }

    #endregion

    #region Constructors

    public SolidStyle() : base()
    {

    }

    #endregion

    #region Methods

    public override SolidStyle DeepCopy()
    {
        SolidStyle newSolidStyle = new SolidStyle();
        newSolidStyle.Colour = Colour;
        return newSolidStyle;
    }

    #endregion
}