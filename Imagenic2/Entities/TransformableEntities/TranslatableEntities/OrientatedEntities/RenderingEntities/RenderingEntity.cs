namespace Imagenic2.Core.Entities;

public abstract class RenderingEntity : OrientatedEntity
{
    #region Fields and Properties

    public Matrix4x4 WorldToView { get; private set; }

    private float viewWidth, viewHeight, zNear, zFar;

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