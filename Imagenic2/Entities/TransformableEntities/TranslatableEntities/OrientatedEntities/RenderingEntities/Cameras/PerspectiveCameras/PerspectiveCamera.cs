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

            UpdateViewClippingPlanes();

            // Update view-to-screen matrix
            viewToScreen.m00 = 2 * ZNear / base.ViewWidth;
        }
    }
    public override float ViewHeight
    {
        get => base.ViewHeight;
        set
        {
            base.ViewHeight = value;

            UpdateViewClippingPlanes();

            // Update view-to-screen matrix
            viewToScreen.m11 = 2 * ZNear / base.ViewHeight;
        }
    }
    public override float ZNear
    {
        get => base.ZNear;
        set
        {
            base.ZNear = value;

            UpdateViewClippingPlanes();

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

            UpdateViewClippingPlanes();

            // Update view-to-screen matrix
            viewToScreen.m22 = (base.ZFar + base.ZNear) / (base.ZFar - base.ZNear);
            viewToScreen.m23 = -(2 * base.ZFar * base.ZNear) / (base.ZFar - base.ZNear);

            // Update far clipping plane
            ViewClippingPlanes[5].Point.z = base.ZFar;
        }
    }

    private void UpdateViewClippingPlanes()
    {
        float semiViewWidth = ViewWidth / 2, semiViewHeight = ViewHeight / 2;

        Vector3D nearBottomLeftPoint = new(-semiViewWidth, -semiViewHeight, ZNear);
        Vector3D farTopRightPoint = new(ViewWidth * ZFar / (2 * ZNear), ViewHeight * ZFar / (2 * ZNear), ZFar);
        Vector3D nearTopLeftPoint = new(-semiViewWidth, semiViewHeight, ZNear);
        Vector3D nearTopRightPoint = new(semiViewWidth, semiViewHeight, ZNear);
        Vector3D farBottomLeftPoint = new(-semiViewWidth * ZFar / ZNear, -semiViewHeight * ZFar / ZNear, ZFar);
        Vector3D farBottomRightPoint = new(semiViewWidth * ZFar / ZNear, -semiViewHeight * ZFar / ZNear, ZFar);

        ViewClippingPlanes = new ClippingPlane[]
        {
            new(nearBottomLeftPoint, Vector3D.NormalFromPlane(farBottomLeftPoint, nearTopLeftPoint, nearBottomLeftPoint)), // Left
            new(nearBottomLeftPoint, Vector3D.NormalFromPlane(nearBottomLeftPoint, farBottomRightPoint, farBottomLeftPoint)), // Bottom
            new(nearBottomLeftPoint, Vector3D.UnitZ), // Near
            new(farTopRightPoint, Vector3D.NormalFromPlane(nearTopRightPoint, farTopRightPoint, farBottomRightPoint)), // Right
            new(farTopRightPoint, Vector3D.NormalFromPlane(nearTopLeftPoint, farTopRightPoint, nearTopRightPoint)), // Top
            new(farTopRightPoint, Vector3D.UnitNegativeZ) // Far
        };

        ViewClippingPlanes[0].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(-semiViewWidth, -semiViewHeight, ZNear), new Vector3D(-semiViewWidth, semiViewHeight, ZNear));
        ViewClippingPlanes[3].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(semiViewWidth, semiViewHeight, ZNear), new Vector3D(semiViewWidth, -semiViewHeight, ZNear));

        // Update top and bottom clipping planes
        ViewClippingPlanes[4].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(-semiViewWidth, semiViewHeight, ZNear), new Vector3D(semiViewWidth, semiViewHeight, ZNear));
        ViewClippingPlanes[1].Normal = Vector3D.NormalFromPlane(Vector3D.Zero, new Vector3D(semiViewWidth, -semiViewHeight, ZNear), new Vector3D(-semiViewWidth, -semiViewHeight, ZNear));
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