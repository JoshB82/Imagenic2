using Imagenic2.Core.Enums;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities;

public abstract class PhysicalEntity : OrientatedEntity
{
    #region Fields and Properties

    private bool castsShadows = true;
    public bool CastsShadows
    {
        get => castsShadows;
        set
        {
            if (value == castsShadows) return;
            castsShadows = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }

    private float opacity = 1;
    public float Opacity
    {
        get => opacity;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(opacity)) return;
            ThrowIfNotWithinRange(value, 0, 1);
            opacity = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }

    private bool visible = true;
    public bool Visible
    {
        get => visible;
        set
        {
            if (value == visible) return;
            visible = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }

    private Matrix4x4 scalingMatrix = Matrix4x4.Identity;
    private Vector3D scaling = Vector3D.One;
    public Vector3D Scaling
    {
        get => scaling;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(scaling)) return;
            scaling = value;

            UpdateScalingMatrix();
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    private protected PhysicalEntity(Vector3D worldOrigin, Orientation worldOrientation) : base(worldOrigin, worldOrientation)
    {
        
    }

    #endregion

    #region Methods

    public override PhysicalEntity ShallowCopy() => (PhysicalEntity)MemberwiseClone();
    public override PhysicalEntity DeepCopy()
    {
        var physicalEntity = (PhysicalEntity)base.DeepCopy();
        return physicalEntity;
    }

    private void UpdateScalingMatrix()
    {
        scalingMatrix = MathsHelper.Scale(scaling);
        UpdateModelToWorldMatrix();
    }

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();
        ModelToWorld *= scalingMatrix;
    }

    #endregion
}