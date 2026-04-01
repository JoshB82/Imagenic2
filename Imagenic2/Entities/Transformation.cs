namespace Imagenic2.Core.Entities;

public interface IAnimation
{
    public void Apply(float time);
}

public class Transformation<TTransformableEntity> where TTransformableEntity : TransformableEntity
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


public class KeyFrameAnimation<TValue> : IAnimation
{
    #region Fields and Properties

    public List<KeyFrame<TValue>> KeyFrames { get; set; }
    public Func<TValue, TValue, float, TValue> interpolator;
    public Action<TValue> setProperty;

    #endregion

    #region Constructors

    public KeyFrameAnimation(List<KeyFrame<TValue>> keyFrames, Action<TValue> setProperty, Func<TValue, TValue, float, TValue> interpolator)
    {
        KeyFrames = keyFrames;
        this.interpolator = interpolator;
        this.setProperty = setProperty;
    }

    #endregion

    #region Methods

    public TValue Evaluate(float time)
    {
        for (int i = 0; i < KeyFrames.Count - 1; i++)
        {
            KeyFrame<TValue> a = KeyFrames[i];
            KeyFrame<TValue> b = KeyFrames[i + 1];

            if (time >= a.time && time <= b.time)
            {
                float t = (time - a.time) / (b.time - a.time);
                return interpolator(a.value, b.value, t);
            }
        }

        return KeyFrames[^1].value;
    }

    public void Apply(float time)
    {
        setProperty(Evaluate(time));
    }

    #endregion
}

public struct KeyFrame<TValue>
{
    #region Fields and Properties

    public float time;
    public TValue value;

    #endregion

    #region Constructors

    public KeyFrame(float time, TValue value)
    {
        this.time = time;
        this.value = value;
    }

    #endregion
}

public class TransformationContext<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    public float StartTime { get; set; }
    public Transformation<TTransformableEntity> Transformation { get; set; }

    private KeyFrameAnimation<Vector3D> TranslationKeyFrameAnimation = null;
    private Vector3D displacement = Vector3D.Zero;

    #endregion

    #region Constructors

    public TransformationContext(TTransformableEntity transformableEntity, float startTime)
    {
        TransformableEntity = transformableEntity;
        StartTime = startTime;
        Transformation = new Transformation<TTransformableEntity>(transformableEntity, new List<IAnimation>());
        TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => (TransformableEntity as TranslatableEntity).WorldOrigin = v, NumberHelpers.Interpolate);
    }

    #endregion

    #region Methods

    public TransformationContext<TTransformableEntity> Translate(Vector3D displacement)
    {
        this.displacement += displacement;
        KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(StartTime, (TransformableEntity as TranslatableEntity).WorldOrigin);
        TranslationKeyFrameAnimation.KeyFrames.Add(keyFrame);
        return this;
    }

    public Animation End(float time)
    {
        if (TranslationKeyFrameAnimation is not null)
        {
            KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(time, displacement);
            TranslationKeyFrameAnimation.KeyFrames.Add(keyFrame);
            Transformation.KeyFrameAnimations.Add(TranslationKeyFrameAnimation);
        }

        return new Animation(/* ... */);
    }

    #endregion
}

public static class TransformationExtensions
{
    public static TransformationContext<TTransformableEntity> Start<TTransformableEntity>(this TTransformableEntity transformableEntity, float time) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContext<TTransformableEntity>(transformableEntity, time);
    }
}