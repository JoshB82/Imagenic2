namespace Imagenic2.Core.Entities;

public struct Vertex
{
    #region Fields and Properties

    public Vector3D WorldOrigin { get; set; }

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

    public Vertex(Vector3D worldOrigin)
    {
        WorldOrigin = worldOrigin;
    }

    public Vertex(Vector3D worldOrigin, Vector3D? normal)
    {
        WorldOrigin = worldOrigin;
        Normal = normal;
    }

    #endregion

    #region Methods

    public override string ToString() => $"WorldOrigin: {WorldOrigin}, Normal: {Normal}";

    #endregion
}