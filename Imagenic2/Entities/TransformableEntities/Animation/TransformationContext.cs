namespace Imagenic2.Core.Entities.Animation;

public abstract class TransformationContextBase
{
    #region Fields and Properties

    public float StartTime { get; set; }
    internal Transformation Transformation { get; set; }

    #endregion

    #region Constructors

    protected TransformationContextBase(float startTime)
    {
        StartTime = startTime;
        Transformation = new Transformation(new List<IAnimation>());
    }

    #endregion

    #region Methods

    protected Transformation AssembleTransformation(TransformableEntity transformableEntity)
    {
        if (transformableEntity is PhysicalEntity physicalEntity && physicalEntity.ScalingKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(physicalEntity.ScalingKeyFrameAnimation);
        }
        if (transformableEntity is TranslatableEntity translatableEntity && translatableEntity.TranslationKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(translatableEntity.TranslationKeyFrameAnimation);
        }

        return Transformation;
    }

    public abstract Animation End();

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

    public override Animation End()
    {
        AssembleTransformation(TransformableEntity);

        return new Animation(Transformation);
    }

    #endregion
}

public class TransformationContextIEnumerable<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public IEnumerable<TTransformableEntity> TransformableEntities { get; set; }

    #endregion

    #region Constructors

    public TransformationContextIEnumerable(IEnumerable<TTransformableEntity> transformableEntities, float startTime) : base(startTime)
    {
        TransformableEntities = transformableEntities;
    }

    #endregion

    #region Methods

    public override Animation End()
    {
        foreach (TTransformableEntity transformableEntity in TransformableEntities)
        {
            AssembleTransformation(transformableEntity);
        }

        return new Animation(Transformation);
    }

    #endregion
}