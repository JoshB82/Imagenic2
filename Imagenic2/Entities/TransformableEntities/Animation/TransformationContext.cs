namespace Imagenic2.Core.Entities.Animation;

public abstract class TransformationContextBase
{
    #region Fields and Properties

    public float StartTime { get; set; }
    public Transformation Transformation { get; set; }

    internal KeyFrameAnimation<Vector3D> ScalingKeyFrameAnimation = null;
    internal KeyFrameAnimation<Vector3D> TranslationKeyFrameAnimation = null;

    #endregion

    #region Constructors

    protected TransformationContextBase(float startTime)
    {
        StartTime = startTime;
        Transformation = new Transformation(new List<IAnimation>());
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

public class TransformationContext<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }

    #endregion

    #region Constructors

    public TransformationContext(TTransformableEntity transformableEntity, float startTime) : base(startTime)
    {
        TransformableEntity = transformableEntity;
    }

    #endregion

    #region Methods

    #endregion
}

public class TransformationContextIEnumerable<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public IEnumerable<TTransformableEntity> TransformableEntities { get; set; }
    public List<KeyFrameAnimation<Vector3D>> ScalingKeyFrameAnimations = new List<KeyFrameAnimation<Vector3D>>();
    public List<KeyFrameAnimation<Vector3D>> TranslationKeyFrameAnimations = new List<KeyFrameAnimation<Vector3D>>();

    #endregion

    #region Constructors

    public TransformationContextIEnumerable(IEnumerable<TTransformableEntity> transformableEntities, float startTime) : base(startTime)
    {
        TransformableEntities = transformableEntities;
    }

    #endregion

    #region Methods

    public Animation End()
    {
        foreach (KeyFrameAnimation<Vector3D> kfa in ScalingKeyFrameAnimations)
        {
            Transformation.KeyFrameAnimations.Add(kfa);
        }
        foreach (KeyFrameAnimation<Vector3D> kfa in TranslationKeyFrameAnimations)
        {
            Transformation.KeyFrameAnimations.Add(kfa);
        }

        return new Animation(Transformation);
    }

    #endregion
}