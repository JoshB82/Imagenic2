namespace Imagenic2.Core.Entities.TransformableEntities.Animation;

public class TransformationContext<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    public float StartTime { get; set; }
    public Transformation<TTransformableEntity> Transformation { get; set; }

    internal KeyFrameAnimation<Vector3D> TranslationKeyFrameAnimation = null;
    internal Vector3D position = Vector3D.Zero;

    internal KeyFrameAnimation<Vector3D> ScalingKeyFrameAnimation = null;
    internal Vector3D scaling = Vector3D.One;

    #endregion

    #region Constructors

    public TransformationContext(TTransformableEntity transformableEntity, float startTime)
    {
        TransformableEntity = transformableEntity;
        StartTime = startTime;
        Transformation = new Transformation<TTransformableEntity>(transformableEntity, new List<IAnimation>());
    }

    #endregion

    #region Methods

    public Animation End()
    {
        if (ScalingKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(ScalingKeyFrameAnimation);
        }
        if (TranslationKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(TranslationKeyFrameAnimation);
        }

        return new Animation(Transformation);
    }

    #endregion
}