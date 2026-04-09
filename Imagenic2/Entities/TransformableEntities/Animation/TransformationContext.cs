using Imagenic2.Core.Maths.Transformations;
using Imagenic2.Core.Utilities;

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

    public abstract Animation End();

    #endregion
}

public sealed class TransformationContext<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    
    internal KeyFrameAnimation<Quaternion>? OrientationKeyFrameAnimation { get; set; }
    internal KeyFrameAnimation<Vector3D>? ScalingKeyFrameAnimation { get; set; }
    internal InstantaneousAnimation<TTransformableEntity>? TransformationAnimation { get; set; }
    internal KeyFrameAnimation<Vector3D>? TranslationKeyFrameAnimation { get; set; }

    #endregion

    #region Constructors

    public TransformationContext(TTransformableEntity transformableEntity, float startTime) : base(startTime)
    {
        TransformableEntity = transformableEntity;
    }

    #endregion

    #region Methods

    internal void AssembleTransformation()
    {
        if (OrientationKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(OrientationKeyFrameAnimation);
        }
        if (ScalingKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(ScalingKeyFrameAnimation);
        }
        if (TransformationAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(TransformationAnimation);
        }
        if (TranslationKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(TranslationKeyFrameAnimation);
        }
    }

    public override Animation End()
    {
        AssembleTransformation();

        return new Animation(Transformation);
    }

    #endregion
}

public sealed class TransformationContextIEnumerable<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    internal List<TransformationContext<TTransformableEntity>> TransformationContexts { get; set; } = new List<TransformationContext<TTransformableEntity>>();

    #endregion

    #region Constructors

    public TransformationContextIEnumerable(IEnumerable<TTransformableEntity> transformableEntities, float startTime) : base(startTime)
    {
        foreach (TTransformableEntity transformableEntity in transformableEntities)
        {
            TransformationContext<TTransformableEntity> tCtx = new TransformationContext<TTransformableEntity>(transformableEntity, startTime);
            TransformationContexts.Add(tCtx);
        }
    }

    private TransformationContextIEnumerable(float startTime) : base(startTime) { }
    public async static Task<TransformationContextIEnumerable<TTransformableEntity>> Create(IAsyncEnumerable<TTransformableEntity> transformableEntities, float startTime)
    {
        TransformationContextIEnumerable<TTransformableEntity> tCtxIE = new TransformationContextIEnumerable<TTransformableEntity>(startTime);

        await foreach (TTransformableEntity transformableEntity in transformableEntities)
        {
            TransformationContext<TTransformableEntity> tCtx = new TransformationContext<TTransformableEntity>(transformableEntity, startTime);
            tCtxIE.TransformationContexts.Add(tCtx);
        }

        return tCtxIE;
    }

    #endregion

    #region Methods

    public override Animation End()
    {
        foreach (TransformationContext<TTransformableEntity> transformationContext in TransformationContexts)
        {
            transformationContext.AssembleTransformation();
        }        

        return new Animation(Transformation);
    }

    #endregion
}

public sealed class TransformationContextNode : TransformationContextBase
{
    #region Fields and Properties

    public Node TransformableEntityNode { get; set; }
    internal List<TransformationContext<TransformableEntity>> TransformationContexts { get; set; }

    #endregion

    #region Constructors

    public TransformationContextNode(Node transformableEntityNode, float startTime) : base(startTime)
    {
        TransformableEntityNode = transformableEntityNode;
    }

    #endregion

    #region Methods

    public override Animation End()
    {
        foreach (TransformationContext<TransformableEntity> transformationContext in TransformationContexts)
        {
            transformationContext.AssembleTransformation();
        }

        return new Animation(Transformation);
    }

    #region Orientate

    public TransformationContextNode Orientate(Orientation orientation, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity orientatedEntity = (OrientatedEntity)node.Content;

            TransformationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (transformationContext is null)
            {
                transformationContext = new TransformationContext<TransformableEntity>(orientatedEntity, StartTime);
                TransformationContexts.Add(transformationContext);
            }

            KeyFrameAnimation<Quaternion>? orientationKeyFrameAnimation = transformationContext.OrientationKeyFrameAnimation;

            if (orientationKeyFrameAnimation is null)
            {
                orientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(StartTime, startingQuaternion);
                orientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            transformationContext.OrientationKeyFrameAnimation = orientationKeyFrameAnimation;
            Quaternion newQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation);
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Rotate

    public TransformationContextNode Rotate(Quaternion q, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity orientatedEntity = (OrientatedEntity)node.Content;

            TransformationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (transformationContext is null)
            {
                transformationContext = new TransformationContext<TransformableEntity>(orientatedEntity, StartTime);
                TransformationContexts.Add(transformationContext);
            }

            KeyFrameAnimation<Quaternion>? orientationKeyFrameAnimation = transformationContext.OrientationKeyFrameAnimation;

            if (orientationKeyFrameAnimation is null)
            {
                orientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(StartTime, startingQuaternion);
                orientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            transformationContext.OrientationKeyFrameAnimation = orientationKeyFrameAnimation;
            Quaternion latestQuaternion = orientationKeyFrameAnimation.KeyFrames[^1].value;
            Quaternion newQuaternion = q * latestQuaternion;
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    public TransformationContextNode Rotate(Vector3D axis, float angle, float time)
    {
        return Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region Scale

    public TransformationContextNode ScaleX(float scaleFactor, float time)
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling, time);
    }

    public TransformationContextNode ScaleY(float scaleFactor, float time)
    {
        Vector3D scaling = new Vector3D(1, scaleFactor, 1);
        return Scale(scaling, time);
    }

    public TransformationContextNode ScaleZ(float scaleFactor, float time)
    {
        Vector3D scaling = new Vector3D(1, 1, scaleFactor);
        return Scale(scaling, time);
    }

    public TransformationContextNode Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time)
    {
        Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return Scale(scaling, time);
    }

    public TransformationContextNode Scale(Vector3D scaleFactor, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<PhysicalEntity>();
        foreach (Node node in descendants)
        {
            PhysicalEntity physicalEntity = (PhysicalEntity)node.Content;

            TransformationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == physicalEntity);
            if (transformationContext is null)
            {
                transformationContext = new TransformationContext<TransformableEntity>(physicalEntity, StartTime);
                TransformationContexts.Add(transformationContext);
            }

            KeyFrameAnimation<Vector3D>? scalingKeyFrameAnimation = transformationContext.ScalingKeyFrameAnimation;

            if (scalingKeyFrameAnimation is null)
            {
                scalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalEntity.Scaling = v, MathsHelper.Lerp);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(StartTime, physicalEntity.Scaling);
                scalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            transformationContext.ScalingKeyFrameAnimation = scalingKeyFrameAnimation;
            Vector3D latestScaling = scalingKeyFrameAnimation.KeyFrames[^1].value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
            scalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Transform

    public TransformationContextNode Transform(Action<TransformableEntity> transformation, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TransformableEntity>();
        foreach (Node node in descendants)
        {
            TransformableEntity transformableEntity = (TransformableEntity)node.Content;

            TransformationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == transformableEntity);
            if (transformationContext is null)
            {
                transformationContext = new TransformationContext<TransformableEntity>(transformableEntity, StartTime);
                TransformationContexts.Add(transformationContext);
            }

            InstantaneousAnimation<TransformableEntity>? transformationAnimation = transformationContext.TransformationAnimation;

            transformationAnimation ??= new InstantaneousAnimation<TransformableEntity>(transformableEntity, new List<KeyFrame<Action<TransformableEntity>>>());

            transformationContext.TransformationAnimation = transformationAnimation;
            KeyFrame<Action<TransformableEntity>> newKeyFrame = new KeyFrame<Action<TransformableEntity>>(time, transformation);
            transformationAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Translate

    public TransformationContextNode TranslateX(float distance, float time)
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return Translate(displacement, time);
    }

    public TransformationContextNode TranslateY(float distance, float time)
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return Translate(displacement, time);
    }

    public TransformationContextNode TranslateZ(float distance, float time)
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return Translate(displacement, time);
    }

    public TransformationContextNode Translate(float distanceX, float distanceY, float distanceZ, float time)
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return Translate(displacement, time);
    }

    public TransformationContextNode Translate(Vector3D displacement, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TranslatableEntity>();
        foreach (Node node in descendants)
        {
            TranslatableEntity translatableEntity = (TranslatableEntity)node.Content;

            TransformationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == translatableEntity);
            if (transformationContext is null)
            {
                transformationContext = new TransformationContext<TransformableEntity>(translatableEntity, StartTime);
                TransformationContexts.Add(transformationContext);
            }

            KeyFrameAnimation<Vector3D>? translationKeyFrameAnimation = transformationContext.TranslationKeyFrameAnimation;

            if (translationKeyFrameAnimation is null)
            {
                translationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableEntity.WorldOrigin = v, MathsHelper.Lerp);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(StartTime, translatableEntity.WorldOrigin);
                translationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            transformationContext.TranslationKeyFrameAnimation = translationKeyFrameAnimation;
            Vector3D latestWorldOrigin = translationKeyFrameAnimation.KeyFrames[^1].value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, latestWorldOrigin + displacement);
            translationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #endregion
}