namespace Imagenic2.Core.Entities;

public sealed class OrthogonalCamera : Camera
{
    #region Fields and Properties

    public override float ViewWidth
    {
        get => base.ViewWidth;
        set
        {
            base.ViewWidth = value;

            viewToScreen.m00 = 2 / base.ViewWidth;
            // Update left and right clipping planes
            ViewClippingPlanes[0].Point.x = -base.ViewWidth / 2;
            ViewClippingPlanes[3].Point.x = base.ViewWidth / 2;
        }
    }
    public override float ViewHeight
    {
        get => base.ViewHeight;
        set
        {
            base.ViewHeight = value;

            // Update view-to-screen matrix
            viewToScreen.m11 = 2 / base.ViewHeight;

            // Update top and bottom clipping planes
            ViewClippingPlanes[1].Point.y = -base.ViewHeight / 2;
            ViewClippingPlanes[4].Point.y = base.ViewHeight / 2;
        }
    }
    public override float ZNear
    {
        get => base.ZNear;
        set
        {
            base.ZNear = value;

            // Update view-to-screen matrix
            viewToScreen.m22 = 2 / (base.ZFar - base.ZNear);
            viewToScreen.m23 = -(base.ZFar + base.ZNear) / (base.ZFar - base.ZNear);

            // Update near clipping plane
            ViewClippingPlanes[2].Point.z = base.ZNear;
        }
    }
    public override float ZFar
    {
        get => base.ZFar;
        set
        {
            base.ZFar = value;

            // Update view-to-screen matrix
            viewToScreen.m22 = 2 / (base.ZFar - base.ZNear);
            viewToScreen.m23 = -(base.ZFar + base.ZNear) / (base.ZFar - base.ZNear);

            // Update far clipping plane
            ViewClippingPlanes[5].Point.z = base.ZFar;
        }
    }

    #endregion

    #region Constructors

    public OrthogonalCamera() : base()
    {

    }

    #endregion

    #region Methods

    #endregion
}