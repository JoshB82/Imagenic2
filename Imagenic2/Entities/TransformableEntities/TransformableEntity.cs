namespace Imagenic2.Core.Entities;

public abstract class TransformableEntity : Entity
{
    #region Fields and Properties

    public Matrix4x4 ModelToWorld { get; protected set; }

    #endregion

    #region Constructors

    public TransformableEntity()
    {

    }

    #endregion

    #region Methods

    protected virtual void UpdateModelToWorldMatrix()
    {

    }

    #endregion
}