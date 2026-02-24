using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public abstract class RenderingEntity : OrientatedEntity
{
    #region Fields and Properties

    // Clipping Planes
    internal ClippingPlane[] ViewClippingPlanes { get; set; }

    public Matrix4x4 WorldToView { get; private set; }
    internal Matrix4x4 viewToScreen;

    private float viewWidth, viewHeight, zNear, zFar;
    /// <summary>
    /// The width of the <see cref="RenderingEntity">RenderingEntity's</see> view/near plane.
    /// </summary>
    public virtual float ViewWidth
    {
        get => viewWidth;
        set
        {
            if (value == viewWidth) return;
            viewWidth = value;
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    /// <summary>
    /// The height of the <see cref="RenderingEntity">RenderingEntity's</see> view/near plane.
    /// </summary>
    public virtual float ViewHeight
    {
        get => viewHeight;
        set
        {
            if (value == viewHeight) return;
            viewHeight = value;
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    /// <summary>
    /// The depth of the <see cref="RenderingEntity">RenderingEntity's</see> view to the near plane.
    /// </summary>
    public virtual float ZNear
    {
        get => zNear;
        set
        {
            if (value == zNear) return;
            zNear = value;
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    /// <summary>
    /// The depth of the <see cref="RenderingEntity">RenderingEntity's</see> view to the far plane.
    /// </summary>
    public virtual float ZFar
    {
        get => zFar;
        set
        {
            if (value == zFar) return;
            zFar = value;
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }

    private VolumeOutline volumeStyle = VolumeOutline.None;

    #endregion

    #region Constructors

    protected RenderingEntity(Vector3D worldOrigin,
                              Orientation worldOrientation,
                              float viewWidth,
                              float viewHeight,
                              float zNear,
                              float zFar)
        : base(worldOrigin, worldOrientation)
    {
        this.viewWidth = viewWidth;
        this.viewHeight = viewHeight;
        this.zNear = zNear;
        this.zFar = zFar;

        ViewWidth = viewWidth;
        ViewHeight = viewHeight;
        ZNear = zNear;
        ZFar = zFar;
    }

    #endregion

    #region Methods

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();
        WorldToView = ModelToWorld.Inverse();
    }

    #endregion
}