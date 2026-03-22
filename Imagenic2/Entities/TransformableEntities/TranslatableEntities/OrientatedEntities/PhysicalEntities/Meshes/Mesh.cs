using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public class Mesh : PhysicalEntity
{
    #region Fields and Properties

    private bool drawEdges = true, drawTriangles = true, drawFaces = true;
    public bool DrawEdges
    {
        get => drawEdges;
        set
        {
            if (value == drawEdges) return;
            drawEdges = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }
    public bool DrawTriangles
    {
        get => drawTriangles;
        set
        {
            if (value == drawTriangles) return;
            drawTriangles = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }
    public bool DrawFaces
    {
        get => drawFaces;
        set
        {
            if (value == drawFaces) return;
            drawFaces = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }

    private MeshStructure structure;
    public MeshStructure Structure
    {
        get => structure;
        internal set
        {
            ThrowIfNull(value);
            structure = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    public Mesh(Vector3D worldOrigin,
                Orientation worldOrientation,
                MeshStructure structure) : base(worldOrigin, worldOrientation)
    {
        Structure = structure;
    }

    #endregion

    #region Methods

    public override Mesh ShallowCopy() => (Mesh)MemberwiseClone();

    public override Mesh DeepCopy()
    {
        var mesh = (Mesh)base.DeepCopy();
        mesh.Structure = Structure;
        return mesh;
    }

    public void SetAllTrianglesToFaceStyle(FaceStyle faceStyle)
    {
        foreach (Triangle triangle in Structure.Triangles)
        {
            triangle.FrontStyle = faceStyle;
        }
    }

    #endregion
}
