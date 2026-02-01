namespace Imagenic2.Core.Entities;

public class Mesh : PhysicalEntity
{
    #region Fields and Properties

    private bool drawEdges;
    public bool DrawEdges
    {
        get => drawEdges;
        set
        {
            if (value == drawEdges) return;
            drawEdges = value;
            RequestNewRenders();
        }
    }
    private bool drawFaces;
    public bool DrawFaces
    {
        get => drawFaces;
        set
        {
            if (value == drawFaces) return;
            drawFaces = value;
            RequestNewRenders();
        }
    }


    private MeshStructure structure;
    public MeshStructure Structure
    {
        get => structure;
        set
        {
            structure = value;
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

    #endregion
}
