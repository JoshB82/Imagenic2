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

        this.corner1 -= new Vector3D(1e-2f, 1e-2f, 1e-2f);
        this.corner2 += new Vector3D(1e-2f, 1e-2f, 1e-2f);
    }
    

    #endregion

    #region Methods

    #endregion
}