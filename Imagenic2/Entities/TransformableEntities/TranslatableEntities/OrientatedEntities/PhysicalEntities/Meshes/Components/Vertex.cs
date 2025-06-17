namespace Imagenic2.Core.Entities;

public sealed class Vertex : TranslatableEntity
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

    public Vertex(Vector3D worldOrigin) : base(worldOrigin)
    {
        
    }

    public Vertex(Vector3D worldOrigin, Vector3D? normal) : base(worldOrigin)
    {
        Normal = normal;
    }

    #endregion
}