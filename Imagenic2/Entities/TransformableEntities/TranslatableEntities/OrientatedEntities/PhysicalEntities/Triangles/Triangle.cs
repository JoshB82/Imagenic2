namespace Imagenic2.Core.Entities;

public class Triangle
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

    public Vertex P1 { get; set; }
    public Vertex P2 { get; set; }
    public Vertex P3 { get; set; }

    #endregion

    #region Constructors

    public Triangle(FaceStyle frontStyle, FaceStyle backStyle, Vertex p1, Vertex p2, Vertex p3)
    {
        FrontStyle = frontStyle;
        BackStyle = backStyle;
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }

    #endregion

    #region Methods

    #endregion
}