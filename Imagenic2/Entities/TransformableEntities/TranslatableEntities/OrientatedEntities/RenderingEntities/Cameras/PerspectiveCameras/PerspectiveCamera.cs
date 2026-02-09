namespace Imagenic2.Core.Entities;

public sealed class PerspectiveCamera : Camera
{
    #region Fields and Properties

    public override float ViewWidth
    {
        get => base.ViewWidth;
        set
        {
            base.ViewWidth = value;

            viewToScreen.m00 = 2 * ZNear / base.ViewWidth;
            float semiWidth = base.ViewWidth / 2, semiHeight = ViewHeight / 2;
            ViewClippingPlanes[0].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(-semiWidth, -semiHeight, ZNear), new Vector3D(-semiWidth, semiHeight, ZNear));
            ViewClippingPlanes[3].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(semiWidth, semiHeight, ZNear), new Vector3D(semiWidth, -semiHeight, ZNear));
        }
    }
    public override float ViewHeight
    {
        get => base.ViewHeight;
        set
        {
            base.ViewHeight = value;

            // Update view-to-screen matrix
            viewToScreen.m11 = 2 * ZNear / base.ViewHeight;

            // Update top and bottom clipping planes
            float semiWidth = base.ViewWidth / 2, semiHeight = base.ViewHeight / 2;
            ViewClippingPlanes[4].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(-semiWidth, semiHeight, ZNear), new Vector3D(semiWidth, semiHeight, ZNear));
            ViewClippingPlanes[1].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(semiWidth, -semiHeight, ZNear), new Vector3D(-semiWidth, -semiHeight, ZNear));
        }
    }
    public override float ZNear
    {
        get => base.ZNear;
        set
        {
            base.ZNear = value;

            // Update view-to-screen matrix
            viewToScreen.m00 = 2 * base.ZNear / base.ViewWidth;
            viewToScreen.m11 = 2 * base.ZNear / base.ViewHeight;
            viewToScreen.m22 = (base.ZFar + base.ZNear) / (base.ZFar - base.ZNear);
            viewToScreen.m23 = -(2 * base.ZFar * base.ZNear) / (base.ZFar - base.ZNear);

            // Update near clipping plane
            ViewClippingPlanes[2].Point.z = base.ZNear;
        }
    }
    public override float ZFar
    {
        get => base.ZFar;
        set
        {
            base.ZNear = value;

            // Update view-to-screen matrix
            viewToScreen.m22 = (base.ZFar + base.ZNear) / (base.ZFar - base.ZNear);
            viewToScreen.m23 = -(2 * base.ZFar * base.ZNear) / (base.ZFar - base.ZNear);

            // Update far clipping plane
            ViewClippingPlanes[5].Point.z = base.ZFar;
        }
    }

    #endregion

    #region Constructors

    public PerspectiveCamera(Vector3D worldOrigin, Orientation worldOrientation, float viewWidth, float viewHeight, float zNear, float zFar) : base(worldOrigin, worldOrientation, viewWidth, viewHeight, zNear, zFar)
    {

    }

    #endregion

    #region Methods

    #endregion
}