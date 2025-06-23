namespace Imagenic2.Core.Entities;

internal sealed class ClippingPlane
{
    #region Fields and Properties

    /// <summary>
    /// A point on the clipping plane.
    /// </summary>
    internal Vector3D Point;

    internal Vector3D Normal;

    #endregion

    #region Constructors

    internal ClippingPlane(Vector3D point, Vector3D normal)
    {
        Point = point;
        Normal = normal;
    }

    #endregion
}