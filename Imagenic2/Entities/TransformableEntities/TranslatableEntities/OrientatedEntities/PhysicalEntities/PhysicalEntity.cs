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
        }
    }

    private float opacity = 1;
    public float Opacity
    {
        get => opacity;
        set
        {
            if (value == opacity) return;
            if (opacity < 0 || opacity > 1)
            {
                throw new Exception("Opacity must be between 0 and 1 inclusive.");
            }
            opacity = value;
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
        }
    }

    private Matrix4x4 scalingMatrix;
    private Vector3D scaling = Vector3D.One;

    public Vector3D Scaling
    {
        get => scaling;
        set
        {
            if (value == scaling) return;
            scaling = value;
            UpdateScalingMatrix();
        }
    }

    #endregion

    #region Constructors

    protected PhysicalEntity(Vector3D worldOrigin, Orientation worldOrientation) : base(worldOrigin, worldOrientation)
    {
        
    }

    #endregion

    #region Methods

    private void UpdateScalingMatrix()
    {
        scalingMatrix = Transform.Scale(scaling);
        UpdateModelToWorldMatrix();
    }

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();

        ModelToWorld *= scalingMatrix;
    }

    #endregion
}