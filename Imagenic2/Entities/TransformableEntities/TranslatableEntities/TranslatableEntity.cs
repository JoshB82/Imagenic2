using Imagenic2.Core.Entities.Animation;
using Imagenic2.Core.Enums;
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
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(worldOrigin)) return;
            worldOrigin = value;

            UpdateTranslationMatrix();
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    internal KeyFrameAnimation<Vector3D>? TranslationKeyFrameAnimation { get; set; }

    #endregion

    #region Constructors

    private protected TranslatableEntity(Vector3D worldOrigin)
    {
        WorldOrigin = worldOrigin;
    }

    #endregion

    #region Methods

    public override TranslatableEntity ShallowCopy() => (TranslatableEntity)MemberwiseClone();
    public override TranslatableEntity DeepCopy()
    {
        var translatableEntity = (TranslatableEntity)base.DeepCopy();
        translatableEntity.TranslationKeyFrameAnimation = TranslationKeyFrameAnimation?.DeepCopy();
        return translatableEntity;
    }

    private void UpdateTranslationMatrix()
    {
        translationMatrix = MathsHelper.Translate(worldOrigin);
        UpdateModelToWorldMatrix();
    }

    protected override void UpdateModelToWorldMatrix()
    {
        base.UpdateModelToWorldMatrix();
        ModelToWorld *= translationMatrix;
    }

    #endregion
}