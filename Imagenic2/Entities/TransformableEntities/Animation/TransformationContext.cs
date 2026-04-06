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

    protected void AssembleTransformation(TransformableEntity transformableEntity)
    {
        if (transformableEntity is PhysicalEntity physicalEntity && physicalEntity.ScalingKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(physicalEntity.ScalingKeyFrameAnimation);
        }
        if (transformableEntity is TranslatableEntity translatableEntity && translatableEntity.TranslationKeyFrameAnimation is not null)
        {
            Transformation.KeyFrameAnimations.Add(translatableEntity.TranslationKeyFrameAnimation);
        }
    }

    public abstract Animation End();

    #endregion
}

public sealed class TransformationContext<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    internal InstantaneousAnimation<TTransformableEntity>? TransformationAnimation { get; set; }

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

public sealed class TransformationContextIEnumerable<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public IEnumerable<TTransformableEntity> TransformableEntities { get; set; }
    internal List<InstantaneousAnimation<TTransformableEntity>> TransformationAnimations { get; set; }

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

public sealed class TransformationContextNode : TransformationContextBase
{
    #region Fields and Properties

    public Node TransformableEntityNode { get; set; }
    internal List<InstantaneousAnimation<TransformableEntity>> TransformationAnimations { get; set; }

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
        var descendants = TransformableEntityNode.GetDescendantsAndThis(n => n.Content is TransformableEntity);
        foreach (Node node in descendants)
        {
            AssembleTransformation((TransformableEntity)(node.Content));
        }

        return new Animation(Transformation);
    }

    #region Orientate

    public TransformationContextNode Orientate(Orientation orientation, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThis(n => n.Content is OrientatedEntity);
        foreach (Node node in descendants)
        {
            OrientatedEntity orientatedEntity = (OrientatedEntity)node.Content;

            if (orientatedEntity.OrientationKeyFrameAnimation is null)
            {
                orientatedEntity.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), NumberHelpers.Interpolate);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(StartTime, startingQuaternion);
                orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Quaternion newQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation);
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Rotate

    public TransformationContextNode Rotate(Quaternion q, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThis(n => n.Content is OrientatedEntity);
        foreach (Node node in descendants)
        {
            OrientatedEntity orientatedEntity = (OrientatedEntity)node.Content;

            if (orientatedEntity.OrientationKeyFrameAnimation is null)
            {
                orientatedEntity.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), NumberHelpers.Interpolate);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(StartTime, startingQuaternion);
                orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Quaternion latestQuaternion = orientatedEntity.OrientationKeyFrameAnimation.KeyFrames[^1].value;
            Quaternion newQuaternion = q * latestQuaternion;
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
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
        var descendants = TransformableEntityNode.GetDescendantsAndThis(n => n.Content is PhysicalEntity);
        foreach (Node node in descendants)
        {
            PhysicalEntity physicalEntity = (PhysicalEntity)node.Content;

            if (physicalEntity.ScalingKeyFrameAnimation is null)
            {
                physicalEntity.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalEntity.Scaling = v, NumberHelpers.Interpolate);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(StartTime, physicalEntity.Scaling);
                physicalEntity.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Vector3D latestScaling = physicalEntity.ScalingKeyFrameAnimation.KeyFrames[^1].value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
            physicalEntity.ScalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Transform

    public TransformationContextNode Transform(Action<TransformableEntity> transformation, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThis(n => n.Content is TransformableEntity).ToList();
        for (int i = 0; i < descendants.Count; i++)
        {
            TransformableEntity transformableEntity = (TransformableEntity)descendants[i].Content;

            TransformationAnimations[i] ??= new InstantaneousAnimation<TransformableEntity>(transformableEntity, new List<KeyFrame<Action<TransformableEntity>>>());
            KeyFrame<Action<TransformableEntity>> newKeyFrame = new KeyFrame<Action<TransformableEntity>>(time, transformation);
            TransformationAnimations[i].KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #region Translate

    public TransformationContextNode Translate(Vector3D displacement, float time)
    {
        var descendants = TransformableEntityNode.GetDescendantsAndThis(n => n.Content is TranslatableEntity);
        foreach (Node node in descendants)
        {
            TranslatableEntity translatableEntity = (TranslatableEntity)node.Content;

            if (translatableEntity.TranslationKeyFrameAnimation is null)
            {
                translatableEntity.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableEntity.WorldOrigin = v, NumberHelpers.Interpolate);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(StartTime, translatableEntity.WorldOrigin);
                translatableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Vector3D latestWorldOrigin = translatableEntity.TranslationKeyFrameAnimation.KeyFrames[^1].value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, latestWorldOrigin + displacement);
            translatableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return this;
    }

    #endregion

    #endregion
}

/*
public class TransformationContextIAsyncEnumerable<TTransformableEntity> : TransformationContextBase where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public IAsyncEnumerable<TTransformableEntity> TransformableEntities { get; set; }

    #endregion

    #region Constructors

    public TransformationContextIAsyncEnumerable(IAsyncEnumerable<TTransformableEntity> transformableEntities, float startTime) : base(startTime)
    {
        TransformableEntities = transformableEntities;
    }

    #endregion

    #region Methods

    public override Animation End()
    {
        await foreach (TTransformableEntity transformableEntity in TransformableEntities)
        {
            AssembleTransformation(transformableEntity);
        }

        return new Animation(Transformation);
    }

    #endregion
}*/