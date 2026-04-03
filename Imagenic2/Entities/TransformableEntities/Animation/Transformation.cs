namespace Imagenic2.Core.Entities.TransformableEntities.Animation;

public class Transformation<TTransformableEntity> : IAnimation where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    public List<IAnimation> KeyFrameAnimations { get; set; }

    #endregion

    #region Constructors

    public Transformation(TTransformableEntity transformableEntity, List<IAnimation> keyFrameAnimations)
    {
        TransformableEntity = transformableEntity;
        KeyFrameAnimations = keyFrameAnimations;
    }

    #endregion

    #region Methods

    public void Apply(float time)
    {
        foreach (IAnimation animation in KeyFrameAnimations)
        {
            animation.Apply(time);
        }
    }

    #endregion
}