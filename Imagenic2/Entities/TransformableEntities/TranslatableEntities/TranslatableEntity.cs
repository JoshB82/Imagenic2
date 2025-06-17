namespace Imagenic2.Core.Entities;

public abstract class TranslatableEntity : TransformableEntity
{
    #region Fields and Properties

    private Matrix4x4 translationMatrix;
    private Vector3D worldOrigin;
    public virtual Vector3D WorldOrigin
    {
        get => worldOrigin;
        set
        {
            if (value == worldOrigin) return;
            worldOrigin = value;
        }
    }

    #endregion

    #region Constructors

    private protected TranslatableEntity(Vector3D worldOrigin)
    {
        
    }

    #endregion

    #region Methods

    protected override void UpdateModelToWorldMatrix()
    {
        ModelToWorld = translationMatrix;
    }

    #endregion
}