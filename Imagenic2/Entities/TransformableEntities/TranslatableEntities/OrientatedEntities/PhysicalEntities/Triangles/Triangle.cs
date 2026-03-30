using System.Drawing;

namespace Imagenic2.Core.Entities;

public struct Triangle
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

    public Vertex p1;
    public Vertex p2;
    public Vertex p3;

    //internal Vector4D TransformedP1 { get; set; }
    //internal Vector4D TransformedP2 { get; set; }
    //internal Vector4D TransformedP3 { get; set; }

    //internal Vector4D ViewSpaceP1 { get; set; }
    //internal Vector4D ViewSpaceP2 { get; set; }
    //internal Vector4D ViewSpaceP3 { get; set; }

    //internal float invW1 { get; set; } = 1;
    //internal float invW2 { get; set; } = 1;
    //internal float invW3 { get; set; } = 1;

    //public Vector2D TextureP1 { get; set; }
    //public Vector2D TextureP2 { get; set; }
    //public Vector2D TextureP3 { get; set; }

    //public Vector2D TransformedTextureP1 { get; set; }
    //public Vector2D TransformedTextureP2 { get; set; }
    //public Vector2D TransformedTextureP3 { get; set; }

    #endregion

    #region Constructors

    public Triangle()
    {
        
    }

    public Triangle(Vertex p1, Vertex p2, Vertex p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    public Triangle(FaceStyle frontStyle, FaceStyle backStyle, Vertex p1, Vertex p2, Vertex p3)
    {
        FrontStyle = frontStyle;
        BackStyle = backStyle;
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    public Triangle(Vertex p1, Vertex p2, Vertex p3, Vector2D txp1, Vector2D txp2, Vector2D txp3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        //TextureP1 = txp1;
        //TextureP2 = txp2;
        //TextureP3 = txp3;
    }

    #endregion

    #region Methods

    public Triangle ShallowCopy() => (Triangle)MemberwiseClone();
    public Triangle DeepCopy()
    {
        var triangle = new Triangle(p1, p2, p3);
        //triangle.TransformedP1 = TransformedP1;
        //triangle.TransformedP2 = TransformedP2;
        //triangle.TransformedP3 = TransformedP3;
        //triangle.ViewSpaceP1 = ViewSpaceP1;
        //triangle.ViewSpaceP2 = ViewSpaceP2;
        //triangle.ViewSpaceP3 = ViewSpaceP3;
        //triangle.invW1 = invW1;
        //triangle.invW2 = invW2;
        //triangle.invW3 = invW3;
        //triangle.TextureP1 = TextureP1;
        //triangle.TextureP2 = TextureP2;
        //triangle.TextureP3 = TextureP3;
        triangle.FrontStyle = FrontStyle;
        triangle.BackStyle = BackStyle;
        return triangle;
    }

    internal void TransformTriangleVertices(Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        //TransformedP1 = transformationMatrix * TransformedP1;
        //TransformedP2 = transformationMatrix * TransformedP2;
        //TransformedP3 = transformationMatrix * TransformedP3;

        Vertex p1 = p1.DeepCopy(), p2 = p2.DeepCopy(), p3 = p3.DeepCopy();

        p1.transformedPosition = transformationMatrix * p1.transformedPosition;
        p2.transformedPosition = transformationMatrix * p2.transformedPosition;
        p3.transformedPosition = transformationMatrix * p3.transformedPosition;

        p1 = p1;
        p2 = p2;
        p3 = p3;
    }

    internal Vector3D CalculateNormal()
    {
        Vector3D edge1 = p2.WorldOrigin - p1.WorldOrigin;
        Vector3D edge2 = p3.WorldOrigin - p1.WorldOrigin;

        return edge2.CrossProduct(edge1).Normalise();
    }

    #endregion
}