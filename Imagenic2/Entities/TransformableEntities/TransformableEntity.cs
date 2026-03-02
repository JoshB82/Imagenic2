namespace Imagenic2.Core.Entities;

public abstract class TransformableEntity : Entity
{
    #region Fields and Properties

    public Matrix4x4 ModelToWorld { get; protected set; } = Matrix4x4.Identity;

    #endregion

    #region Constructors

    protected TransformableEntity()
    {

    }

    #endregion

    #region Methods

    public override TransformableEntity ShallowCopy() => (TransformableEntity)MemberwiseClone();
    public override TransformableEntity DeepCopy()
    {
        var transformableEntity = (TransformableEntity)base.DeepCopy();
        return transformableEntity;
    }

    protected virtual void UpdateModelToWorldMatrix()
    {
        ModelToWorld = Matrix4x4.Identity;
    }

    #endregion
}