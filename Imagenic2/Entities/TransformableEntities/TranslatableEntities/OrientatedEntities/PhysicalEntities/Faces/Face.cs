namespace Imagenic2.Core.Entities;

public abstract class Face
{
    #region Fields and Properties

    private FaceStyle frontStyle, backStyle;
    public FaceStyle FrontStyle
    {
        get => frontStyle;
        set
        {
            frontStyle = value;
        }
    }
    public FaceStyle BackStyle
    {
        get => backStyle;
        set
        {
            backStyle = value;
        }
    }

    #endregion

    #region Constructors

    public Face()
    {

    }

    #endregion

    #region Methods

    #endregion
}