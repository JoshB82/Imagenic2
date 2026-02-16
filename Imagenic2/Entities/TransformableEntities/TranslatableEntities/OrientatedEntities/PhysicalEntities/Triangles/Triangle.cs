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

    internal Vector4D TransformedP1 { get; set; }
    internal Vector4D TransformedP2 { get; set; }
    internal Vector4D TransformedP3 { get; set; }

    #endregion

    #region Constructors

    public Triangle(Vertex p1, Vertex p2, Vertex p3)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }

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

    public Triangle ShallowCopy() => (Triangle)MemberwiseClone();
    public Triangle DeepCopy()
    {
        var triangle = new Triangle(P1, P2, P3);
        triangle.TransformedP1 = TransformedP1;
        triangle.TransformedP2 = TransformedP2;
        triangle.TransformedP3 = TransformedP3;
        triangle.FrontStyle = FrontStyle;
        triangle.BackStyle = BackStyle;
        return triangle;
    }

    #endregion
}