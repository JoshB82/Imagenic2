namespace Imagenic2.Core.Entities;

public class Face
{
    #region Fields and Properties

    public IList<Triangle> Triangles {get; set; }

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

    public Face(params IList<Triangle> triangles)
    {
        Triangles = triangles;
    }

    #endregion

    #region Methods

    #endregion
}