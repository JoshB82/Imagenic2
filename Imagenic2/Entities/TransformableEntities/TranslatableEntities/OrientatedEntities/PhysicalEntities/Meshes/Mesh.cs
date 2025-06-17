namespace Imagenic2.Core.Entities;

internal class Mesh
{
    #region Fields and Properties

    private bool drawEdges;
    private bool drawFaces;

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
