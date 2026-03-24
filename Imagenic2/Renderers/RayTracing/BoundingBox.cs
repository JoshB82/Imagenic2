namespace Imagenic2.Core.Renderers.RayTracing;

internal struct BoundingBox
{
    #region Fields and Properties

    //internal Vector3D corner1, corner2;

    internal Vector3D centre;
    internal Vector3D axisX, axisY, axisZ;
    internal Vector3D halfSizes;

    #endregion

    #region Constructors

    internal BoundingBox(Vector3D centre, Vector3D axisX, Vector3D axisY, Vector3D axisZ, Vector3D halfSizes)
    {
        this.centre = centre;
        this.axisX = axisX;
        this.axisY = axisY;
        this.axisZ = axisZ;
        this.halfSizes = halfSizes;
    }

    /*
    public BoundingBox(Vector3D corner1, Vector3D corner2)
    {
        this.corner1 = corner1;
        this.corner2 = corner2;

        this.corner1 -= new Vector3D(1e-2f, 1e-2f, 1e-2f);
        this.corner2 += new Vector3D(1e-2f, 1e-2f, 1e-2f);
    }
    */

    #endregion

    #region Methods

    internal void TransformVertices(Matrix4x4 matrix)
    {
        //corner1 = (Vector3D)(matrix * new Vector4D(corner1, 1));
        //corner2 = (Vector3D)(matrix * new Vector4D(corner2, 1));

        centre = (Vector3D)(matrix * new Vector4D(centre, 1));
    }

    #endregion
}