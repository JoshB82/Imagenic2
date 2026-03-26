namespace Imagenic2.Core.Entities;

public abstract class TransformableEntity : Entity
{
    #region Fields and Properties

    private Matrix4x4 modelToWorld = Matrix4x4.Identity;
    public Matrix4x4 ModelToWorld
    {
        get => modelToWorld;
        protected set
        {
            modelToWorld = value;
            WorldToModel = modelToWorld.Inverse();
        }
    }

    public Matrix4x4 WorldToModel { get; private set; }

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