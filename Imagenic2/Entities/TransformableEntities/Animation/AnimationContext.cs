using Imagenic2.Core.Maths.Transformations;
using Imagenic2.Core.Utilities;

namespace Imagenic2.Core.Entities.Animation;

public abstract class AnimationContextBase
{
    #region Fields and Properties

    public float StartTime { get; set; }
    internal Transformation Transformation { get; set; }

    #endregion

    #region Constructors

    protected AnimationContextBase(float startTime)
    {
        StartTime = startTime;
        Transformation = new Transformation(new List<IAnimation>());
    }

    #endregion

    #region Methods

    public abstract Animation End();

    #endregion
}

public sealed class AnimationContext<TTransformableEntity> : AnimationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    
    internal KeyFrameAnimation<Quaternion>? OrientationKeyFrameAnimation { get; set; }
    internal KeyFrameAnimation<Vector3D>? ScalingKeyFrameAnimation { get; set; }
    internal InstantaneousAnimation<TTransformableEntity>? TransformationAnimation { get; set; }
    internal KeyFrameAnimation<Vector3D>? TranslationKeyFrameAnimation { get; set; }

    //internal KeyFrameFuncAnimation<Vector3D>? TranslationKeyFrameFuncAnimation { get; set; }

    #endregion

    #region Constructors

    public AnimationContext(TTransformableEntity transformableEntity, float startTime) : base(startTime)
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

public sealed class AnimationContextIEnumerable<TTransformableEntity> : AnimationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    internal List<AnimationContext<TTransformableEntity>> TransformationContexts { get; set; } = new List<AnimationContext<TTransformableEntity>>();

    #endregion

    #region Constructors

    public AnimationContextIEnumerable(IEnumerable<TTransformableEntity> transformableEntities, float startTime) : base(startTime)
    {
        foreach (TTransformableEntity transformableEntity in transformableEntities)
        {
            AnimationContext<TTransformableEntity> tCtx = new AnimationContext<TTransformableEntity>(transformableEntity, startTime);
            TransformationContexts.Add(tCtx);
        }
    }

    private AnimationContextIEnumerable(float startTime) : base(startTime) { }
    public async static Task<AnimationContextIEnumerable<TTransformableEntity>> Create(IAsyncEnumerable<TTransformableEntity> transformableEntities, float startTime)
    {
        AnimationContextIEnumerable<TTransformableEntity> tCtxIE = new AnimationContextIEnumerable<TTransformableEntity>(startTime);

        await foreach (TTransformableEntity transformableEntity in transformableEntities)
        {
            AnimationContext<TTransformableEntity> tCtx = new AnimationContext<TTransformableEntity>(transformableEntity, startTime);
            tCtxIE.TransformationContexts.Add(tCtx);
        }

        return tCtxIE;
    }

    #endregion

    #region Methods

    public override Animation End()
    {
        foreach (AnimationContext<TTransformableEntity> transformationContext in TransformationContexts)
        {
            transformationContext.AssembleTransformation();
        }        

        return new Animation(Transformation);
    }

    #endregion
}

public static class AnimationExtensions2
{

    public static Animation End<TTransformableEntity>(this IEnumerable<AnimationContext<TTransformableEntity>> tCtxs) where TTransformableEntity : TransformableEntity
    {
        foreach (AnimationContext<TTransformableEntity> transformationContext in tCtxs)
        {
            transformationContext.AssembleTransformation();
        }

        return new Animation(tCtxs.Select(t => t.Transformation));
    }
}


public sealed class AnimationContextNode : AnimationContextBase
{
    #region Fields and Properties

    public Node TransformableEntityNode { get; set; }
    internal List<AnimationContext<TransformableEntity>> TransformationContexts { get; set; } = new List<AnimationContext<TransformableEntity>>();

    #endregion

    #region Constructors

    public AnimationContextNode(Node transformableEntityNode, float startTime) : base(startTime)
    {
        TransformableEntityNode = transformableEntityNode;
    }

    #endregion

    #region Methods

    public override Animation End()
    {
        foreach (AnimationContext<TransformableEntity> transformationContext in TransformationContexts)
        {
            transformationContext.AssembleTransformation();
        }

        return new Animation(Transformation);
    }

    #region Orientate

    public AnimationContextNode Orientate(Orientation orientation, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity? orientatedEntity = (OrientatedEntity?)node.Content;
            if (orientatedEntity is null) continue;

            AnimationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(orientatedEntity, StartTime);
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

    public AnimationContextNode Rotate(Quaternion q, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity? orientatedEntity = (OrientatedEntity?)node.Content;
            if (orientatedEntity is null) continue;

            AnimationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(orientatedEntity, StartTime);
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
            Quaternion latestQuaternion = orientationKeyFrameAnimation.KeyFrames[^1].Value;
            Quaternion newQuaternion = q * latestQuaternion;
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    public AnimationContextNode Rotate(Vector3D axis, float angle, float time)
    {
        return Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region Scale

    public AnimationContextNode ScaleX(float scaleFactor, float time)
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling, time);
    }

    public AnimationContextNode ScaleY(float scaleFactor, float time)
    {
        Vector3D scaling = new Vector3D(1, scaleFactor, 1);
        return Scale(scaling, time);
    }

    public AnimationContextNode ScaleZ(float scaleFactor, float time)
    {
        Vector3D scaling = new Vector3D(1, 1, scaleFactor);
        return Scale(scaling, time);
    }

    public AnimationContextNode Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time)
    {
        Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return Scale(scaling, time);
    }

    public AnimationContextNode Scale(Vector3D scaleFactor, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<PhysicalEntity>();
        foreach (Node node in descendants)
        {
            PhysicalEntity? physicalEntity = (PhysicalEntity?)node.Content;
            if (physicalEntity is null) continue;

            AnimationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == physicalEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(physicalEntity, StartTime);
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
            Vector3D latestScaling = scalingKeyFrameAnimation.KeyFrames[^1].Value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
            scalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Transform

    public AnimationContextNode Transform(Action<TransformableEntity> transformation, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TransformableEntity>();
        foreach (Node node in descendants)
        {
            TransformableEntity? transformableEntity = (TransformableEntity?)node.Content;
            if (transformableEntity is null) continue;

            AnimationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == transformableEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(transformableEntity, StartTime);
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

    public AnimationContextNode TranslateX(float distance, float time)
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return Translate(displacement, time);
    }

    public AnimationContextNode TranslateY(float distance, float time)
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return Translate(displacement, time);
    }

    public AnimationContextNode TranslateZ(float distance, float time)
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return Translate(displacement, time);
    }

    public AnimationContextNode Translate(float distanceX, float distanceY, float distanceZ, float time)
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return Translate(displacement, time);
    }

    public AnimationContextNode Translate(Vector3D displacement, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TranslatableEntity>();
        foreach (Node node in descendants)
        {
            TranslatableEntity? translatableEntity = (TranslatableEntity?)node.Content;
            if (translatableEntity is null) continue;

            AnimationContext<TransformableEntity>? transformationContext = TransformationContexts.FirstOrDefault(c => c.TransformableEntity == translatableEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(translatableEntity, StartTime);
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
            Vector3D latestWorldOrigin = translationKeyFrameAnimation.KeyFrames[^1].Value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, latestWorldOrigin + displacement);
            translationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #endregion
}