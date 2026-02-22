using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities;

public abstract class TranslatableEntity : TransformableEntity
{
    #region Fields and Properties

    private Matrix4x4 translationMatrix = Matrix4x4.Identity;
    private Vector3D worldOrigin;
    public virtual Vector3D WorldOrigin
    {
        get => worldOrigin;
        set
        {
            if (value == worldOrigin) return;
            worldOrigin = value;

            UpdateTranslationMatrix();
        }
    }

    #endregion

    #region Constructors

    private protected TranslatableEntity(Vector3D worldOrigin)
    {
        WorldOrigin = worldOrigin;
    }

    #endregion

    #region Methods

    private void UpdateTranslationMatrix()
    {
        translationMatrix = Transform.Translate(worldOrigin);
        UpdateModelToWorldMatrix();
    }

    protected override void UpdateModelToWorldMatrix()
    {
        ModelToWorld *= translationMatrix;
    }

    #endregion
}