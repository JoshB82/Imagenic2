using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public abstract class RenderingEntity : OrientatedEntity
{
    #region Fields and Properties

    // Clipping planes
    internal ClippingPlane[]? ViewClippingPlanes { get; set; }

    // Matrices
    public Matrix4x4 WorldToView { get; private set; }
    internal Matrix4x4 viewToScreen;

    // View volume
    private float viewWidth, viewHeight, zNear, zFar;
    /// <summary>
    /// The width of the <see cref="RenderingEntity">RenderingEntity's</see> view/near plane.
    /// </summary>
    public virtual float ViewWidth
    {
        get => viewWidth;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(viewWidth)) return;
            viewWidth = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
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
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(viewHeight)) return;
            viewHeight = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
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
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(zNear)) return;
            zNear = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
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
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(zFar)) return;
            zFar = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    private VolumeOutline volumeStyle = VolumeOutline.None;
    public VolumeOutline VolumeStyle
    {
        get => volumeStyle;
        set
        {
            if (value == volumeStyle) return;
            volumeStyle = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }

    #endregion

    #region Constructors

    private protected RenderingEntity(Vector3D worldOrigin,
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

    public override RenderingEntity ShallowCopy() => (RenderingEntity)MemberwiseClone();
    public override RenderingEntity DeepCopy()
    {
        var renderingEntity = (RenderingEntity)base.DeepCopy();
        renderingEntity.ViewClippingPlanes = (ClippingPlane[]?)ViewClippingPlanes?.Clone();
        return renderingEntity;
    }

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();
        WorldToView = ModelToWorld.Inverse();
    }

    #endregion
}