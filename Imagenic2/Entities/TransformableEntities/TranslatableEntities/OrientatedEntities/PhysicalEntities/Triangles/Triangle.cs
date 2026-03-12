using System.Drawing;

namespace Imagenic2.Core.Entities;

public class Triangle
{
    #region Fields and Properties

    private FaceStyle frontStyle = new SolidStyle() { Colour = Color.Black };
    private FaceStyle backStyle = new SolidStyle() { Colour = Color.Black };

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

    internal Vector4D ViewSpaceP1 { get; set; }
    internal Vector4D ViewSpaceP2 { get; set; }
    internal Vector4D ViewSpaceP3 { get; set; }

    internal float invW1 { get; set; } = 1;
    internal float invW2 { get; set; } = 1;
    internal float invW3 { get; set; } = 1;

    public Vector2D TextureP1 { get; set; }
    public Vector2D TextureP2 { get; set; }
    public Vector2D TextureP3 { get; set; }

    public Vector2D TransformedTextureP1 { get; set; }
    public Vector2D TransformedTextureP2 { get; set; }
    public Vector2D TransformedTextureP3 { get; set; }

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

    public Triangle(Vertex p1, Vertex p2, Vertex p3, Vector2D txp1, Vector2D txp2, Vector2D txp3)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
        TextureP1 = txp1;
        TextureP2 = txp2;
        TextureP3 = txp3;
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
        triangle.ViewSpaceP1 = ViewSpaceP1;
        triangle.ViewSpaceP2 = ViewSpaceP2;
        triangle.ViewSpaceP3 = ViewSpaceP3;
        triangle.invW1 = invW1;
        triangle.invW2 = invW2;
        triangle.invW3 = invW3;
        triangle.TextureP1 = TextureP1;
        triangle.TextureP2 = TextureP2;
        triangle.TextureP3 = TextureP3;
        triangle.FrontStyle = FrontStyle;
        triangle.BackStyle = BackStyle;
        return triangle;
    }

    #endregion
}