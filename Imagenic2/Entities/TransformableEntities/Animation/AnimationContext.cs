using Imagenic2.Core.Maths.Transformations;
using Imagenic2.Core.Utilities;

namespace Imagenic2.Core.Entities.Animation;

public sealed class AnimationContext<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public float StartTime { get; set; }
    internal Transformation<TTransformableEntity> Transformation { get; set; }

    public TTransformableEntity TransformableEntity { get; set; }
    
    internal KeyFrameAnimation<TTransformableEntity, Quaternion>? OrientationKeyFrameAnimation { get; set; }
    internal KeyFrameAnimation<TTransformableEntity, Vector3D>? ScalingKeyFrameAnimation { get; set; }
    internal InstantaneousAnimation<TTransformableEntity>? TransformationAnimation { get; set; }
    internal KeyFrameAnimation<TTransformableEntity, Vector3D>? TranslationKeyFrameAnimation { get; set; }

    #endregion

    #region Constructors

    public AnimationContext(TTransformableEntity transformableEntity, float startTime)
    {
        StartTime = startTime;
        TransformableEntity = transformableEntity;
        Transformation = new Transformation<TTransformableEntity>(new List<IAnimation<TTransformableEntity>>(), transformableEntity);
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

    public Animation End()
    {
        AssembleTransformation();

        return new Animation((ITransformation)Transformation);
    }

    #endregion
}

public sealed class AnimationContextNode
{
    #region Fields and Properties

    public float StartTime { get; set; }
    public Node TransformableEntityNode { get; set; }
    internal List<AnimationContext<TransformableEntity>> AnimationContexts { get; set; } = new List<AnimationContext<TransformableEntity>>();

    #endregion

    #region Constructors

    public AnimationContextNode(Node transformableEntityNode, float startTime)
    {
        StartTime = startTime;
        TransformableEntityNode = transformableEntityNode;
    }

    #endregion

    #region Methods

    public Animation End()
    {
        foreach (AnimationContext<TransformableEntity> animationContext in AnimationContexts)
        {
            animationContext.AssembleTransformation();
        }

        return new Animation(AnimationContexts.Select(t => t.Transformation));
    }

    #region Orientate

    public AnimationContextNode Orientate(Orientation orientation, float time)
    {
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity? orientatedEntity = (OrientatedEntity?)node.Content;
            if (orientatedEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(orientatedEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.OrientationKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);

            Instruction<TransformableEntity, Quaternion> instruction = new(
                time: time,
                func: t => MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation),
                predicateFailValue: MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, ((OrientatedEntity)animationContext.TransformableEntity).WorldOrientation),
                predicate: null
            );

            animationContext.OrientationKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    #endregion

    #region Orientate with predicate

    public AnimationContextNode Orientate<TOrientatedEntity>(Orientation orientation, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity? orientatedEntity = (OrientatedEntity?)node.Content;
            if (orientatedEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(orientatedEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.OrientationKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);

            Instruction<TransformableEntity, Quaternion> instruction = new(
                time: time,
                func: t => MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation),
                predicateFailValue: MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, ((OrientatedEntity)animationContext.TransformableEntity).WorldOrientation),
                predicate: t => predicate((TOrientatedEntity)t)
            );

            animationContext.OrientationKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    #endregion

    #region Rotate

    public AnimationContextNode Rotate(Quaternion q, float time)
    {
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity? orientatedEntity = (OrientatedEntity?)node.Content;
            if (orientatedEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(orientatedEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.OrientationKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);

            Instruction<TransformableEntity, Quaternion> instruction = new(
                time: time,
                func: t => q * MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, ((OrientatedEntity)t).WorldOrientation),
                predicateFailValue: MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, ((OrientatedEntity)animationContext.TransformableEntity).WorldOrientation),
                predicate: null
            );

            animationContext.OrientationKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    public AnimationContextNode Rotate(Vector3D axis, float angle, float time)
    {
        return Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region Rotate with predicate

    public AnimationContextNode Rotate<TOrientatedEntity>(Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<OrientatedEntity>();
        foreach (Node node in descendants)
        {
            OrientatedEntity? orientatedEntity = (OrientatedEntity?)node.Content;
            if (orientatedEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == orientatedEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(orientatedEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.OrientationKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);

            Instruction<TransformableEntity, Quaternion> instruction = new(
                time: time,
                func: t => q * MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, ((OrientatedEntity)t).WorldOrientation),
                predicateFailValue: MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, ((OrientatedEntity)animationContext.TransformableEntity).WorldOrientation),
                predicate: t => predicate((TOrientatedEntity)t)
            );

            animationContext.OrientationKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    public AnimationContextNode Rotate<TOrientatedEntity>(Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        return Rotate(MathsHelper.QuaternionRotate(axis, angle), predicate, time);
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
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<PhysicalEntity>();
        foreach (Node node in descendants)
        {
            PhysicalEntity? physicalEntity = (PhysicalEntity?)node.Content;
            if (physicalEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == physicalEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(physicalEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.ScalingKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalEntity.Scaling = v, MathsHelper.Lerp);

            Instruction<TransformableEntity, Vector3D> instruction = new(
                time: time,
                func: t => new Vector3D(((PhysicalEntity)t).Scaling.x * scaleFactor.x, ((PhysicalEntity)t).Scaling.y * scaleFactor.y, ((PhysicalEntity)t).Scaling.z * scaleFactor.z),
                predicateFailValue: ((PhysicalEntity)animationContext.TransformableEntity).Scaling,
                predicate: null
            );

            animationContext.ScalingKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    #endregion

    #region Scale with predicate

    public AnimationContextNode ScaleX<TPhysicalEntity>(float scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
        return Scale(scaling, predicate, time);
    }

    public AnimationContextNode ScaleY<TPhysicalEntity>(float scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaling = new Vector3D(1, scaleFactor, 1);
        return Scale(scaling, predicate, time);
    }

    public AnimationContextNode ScaleZ<TPhysicalEntity>(float scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaling = new Vector3D(1, 1, scaleFactor);
        return Scale(scaling, predicate, time);
    }

    public AnimationContextNode Scale<TPhysicalEntity>(float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return Scale(scaling, predicate, time);
    }

    public AnimationContextNode Scale<TPhysicalEntity>(Vector3D scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<PhysicalEntity>();
        foreach (Node node in descendants)
        {
            PhysicalEntity? physicalEntity = (PhysicalEntity?)node.Content;
            if (physicalEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == physicalEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(physicalEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.ScalingKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalEntity.Scaling = v, MathsHelper.Lerp);

            Instruction<TransformableEntity, Vector3D> instruction = new(
                time: time,
                func: t => new Vector3D(((PhysicalEntity)t).Scaling.x * scaleFactor.x, ((PhysicalEntity)t).Scaling.y * scaleFactor.y, ((PhysicalEntity)t).Scaling.z * scaleFactor.z),
                predicateFailValue: ((PhysicalEntity)animationContext.TransformableEntity).Scaling,
                predicate: t => predicate((TPhysicalEntity)t)
            );

            animationContext.ScalingKeyFrameAnimation.Instructions.Add(instruction);
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

            AnimationContext<TransformableEntity>? transformationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == transformableEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(transformableEntity, StartTime);
                AnimationContexts.Add(transformationContext);
            }

            InstantaneousAnimation<TransformableEntity>? transformationAnimation = transformationContext.TransformationAnimation;

            transformationAnimation ??= new InstantaneousAnimation<TransformableEntity>(new List<KeyFrame<Action<TransformableEntity>>>());

            transformationContext.TransformationAnimation = transformationAnimation;
            KeyFrame<Action<TransformableEntity>> newKeyFrame = new KeyFrame<Action<TransformableEntity>>(time, transformation);
            transformationAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Transform with predicate

    public AnimationContextNode Transform<TTransformableEntity>(Action<TransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TransformableEntity>();
        foreach (Node node in descendants)
        {
            TransformableEntity? transformableEntity = (TransformableEntity?)node.Content;
            if (transformableEntity is null) continue;

            AnimationContext<TransformableEntity>? transformationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == transformableEntity);
            if (transformationContext is null)
            {
                transformationContext = new AnimationContext<TransformableEntity>(transformableEntity, StartTime);
                AnimationContexts.Add(transformationContext);
            }

            InstantaneousAnimation<TransformableEntity>? transformationAnimation = transformationContext.TransformationAnimation;

            transformationAnimation ??= new InstantaneousAnimation<TransformableEntity>(new List<KeyFrame<Action<TransformableEntity>>>());

            transformationContext.TransformationAnimation = transformationAnimation;
            KeyFrame<Action<TransformableEntity>> newKeyFrame = new KeyFrame<Action<TransformableEntity>>(time, t => { if (predicate((TTransformableEntity)t)) transformation(t); });
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
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TranslatableEntity>();
        foreach (Node node in descendants)
        {
            TranslatableEntity? translatableEntity = (TranslatableEntity?)node.Content;
            if (translatableEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == translatableEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(translatableEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.TranslationKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableEntity.WorldOrigin = v, MathsHelper.Lerp);

            Instruction<TransformableEntity, Vector3D> instruction = new(
                time: time,
                func: t => ((TranslatableEntity)t).WorldOrigin + displacement,
                predicateFailValue: ((TranslatableEntity)animationContext.TransformableEntity).WorldOrigin,
                predicate: null);

            animationContext.TranslationKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    #endregion

    #region Translate with predicate

    public AnimationContextNode TranslateX<TTranslatableEntity>(float distance, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return Translate(displacement, predicate, time);
    }

    public AnimationContextNode TranslateY<TTranslatableEntity>(float distance, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return Translate(displacement, predicate, time);
    }

    public AnimationContextNode TranslateZ<TTranslatableEntity>(float distance, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return Translate(displacement, predicate, time);
    }

    public AnimationContextNode Translate<TTranslatableEntity>(float distanceX, Func<TTranslatableEntity, bool> predicate, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return Translate(displacement, predicate, time);
    }

    public AnimationContextNode Translate<TTranslatableEntity>(Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNotFinite(time);

        var descendants = TransformableEntityNode.GetDescendantsAndThisOfType<TranslatableEntity>();
        foreach (Node node in descendants)
        {
            TranslatableEntity? translatableEntity = (TranslatableEntity?)node.Content;
            if (translatableEntity is null) continue;

            AnimationContext<TransformableEntity>? animationContext = AnimationContexts.FirstOrDefault(c => c.TransformableEntity == translatableEntity);
            if (animationContext is null)
            {
                animationContext = new AnimationContext<TransformableEntity>(translatableEntity, StartTime);
                AnimationContexts.Add(animationContext);
            }

            animationContext.TranslationKeyFrameAnimation ??= new KeyFrameAnimation<TransformableEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableEntity.WorldOrigin = v, MathsHelper.Lerp);

            Instruction<TransformableEntity, Vector3D> instruction = new(
                time: time,
                func: t => ((TranslatableEntity)t).WorldOrigin + displacement,
                predicateFailValue: ((TranslatableEntity)animationContext.TransformableEntity).WorldOrigin,
                predicate: t => predicate((TTranslatableEntity)t)
            );

            animationContext.TranslationKeyFrameAnimation.Instructions.Add(instruction);
        }

        return this;
    }

    #endregion

    #endregion
}