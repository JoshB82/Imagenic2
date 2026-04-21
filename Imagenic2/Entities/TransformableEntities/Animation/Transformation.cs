namespace Imagenic2.Core.Entities.Animation;

public sealed class Transformation<TTransformableEntity> : ITransformation where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public List<IAnimation<TTransformableEntity>> KeyFrameAnimations { get; set; }
    public TTransformableEntity TransformableEntity { get; set; }

    #endregion

    #region Constructors

    public Transformation(List<IAnimation<TTransformableEntity>> keyFrameAnimations, TTransformableEntity transformableEntity)
    {
        KeyFrameAnimations = keyFrameAnimations;
        TransformableEntity = transformableEntity;
    }

    #endregion

    #region Methods

    public void Apply(float time)
    {
        foreach (IAnimation<TTransformableEntity> animation in KeyFrameAnimations)
        {
            animation.Apply(TransformableEntity, time);
        }
    }

    #endregion
}

public interface ITransformation
{
    public void Apply(float time);
}