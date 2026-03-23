namespace Imagenic2.Core.Renderers.RayTracing;

internal struct BoundingBox
{
    #region Fields and Properties

    internal Vector3D corner1, corner2;

    #endregion

    #region Constructors

    public BoundingBox(Vector3D corner1, Vector3D corner2)
    {
        this.corner1 = corner1;
        this.corner2 = corner2;
    }

    #endregion

    #region Methods

    internal void TransformVertices(Matrix4x4 matrix)
    {
        corner1 = (Vector3D)(matrix * new Vector4D(corner1, 1));
        corner2 = (Vector3D)(matrix * new Vector4D(corner2, 1));
    }

    #endregion
}