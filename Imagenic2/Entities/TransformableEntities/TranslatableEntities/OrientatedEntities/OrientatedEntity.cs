namespace Imagenic2.Core.Entities;

public abstract class OrientatedEntity : TranslatableEntity
{
    #region Fields and Properties

    private Matrix4x4 rotationMatrix;
    private Orientation worldOrientation;
    public virtual Orientation WorldOrientation
    {
        get => worldOrientation;
        set
        {
            if (value == worldOrientation) return;
            worldOrientation = value;

        }
    }

    #endregion

    #region Constructors

    protected OrientatedEntity(Vector3D worldOrigin, Orientation worldOrientation) : base(worldOrigin)
    {
        this.worldOrientation = worldOrientation;
    }

    #endregion

    #region Methods

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();

        ModelToWorld *= rotationMatrix;
    }

    #endregion
}