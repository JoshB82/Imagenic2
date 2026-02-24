namespace Imagenic2.Core.Entities;

public sealed class Vertex : PhysicalEntity
{
    #region Fields and Properties

    private Vector3D? normal;
    public Vector3D? Normal
    {
        get => normal;
        set
        {
            if (value == normal) return;
            normal = value;
        }
    }

    #endregion

    #region Constructors

    public Vertex(Vector3D worldOrigin) : base(worldOrigin, Orientation.OrientationZY)
    {
        
    }

    public Vertex(Vector3D worldOrigin, Vector3D? normal) : base(worldOrigin, Orientation.OrientationZY)
    {
        Normal = normal;
    }

    #endregion

    #region Methods

    public override string ToString() => $"WorldOrigin: {WorldOrigin}, Normal: {Normal}";

    #endregion
}