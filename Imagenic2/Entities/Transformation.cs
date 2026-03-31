namespace Imagenic2.Core.Entities;

internal class Transformation<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    //public Func<float, TProperty, TProperty> PropertyTransformation { get; set; }


    #endregion

    #region Constructors

    public Transformation(TTransformableEntity transformableEntity, params IEnumerable<Func<TTransformableEntity>> selectors)
    {
        
    }

    #endregion

    #region Methods

    #endregion
}


public class KeyFrameAnimation<TValue>
{
    #region Fields and Properties

    public List<KeyFrame<TValue>> KeyFrames { get; set; }
    private Func<TValue, TValue, float, TValue> interpolator;

    #endregion

    #region Constructors

    public KeyFrameAnimation(List<KeyFrame<TValue>> keyFrames, Func<TValue, TValue, float, TValue> interpolator)
    {
        KeyFrames = keyFrames;
        this.interpolator = interpolator;
    }

    #endregion

    #region Methods

    public TValue Evaluate(float time)
    {
        for (int i = 0; i < KeyFrames.Count; i++)
        {
            KeyFrame<TValue> a = KeyFrames[i];
            KeyFrame<TValue> b = KeyFrames[i];

            if (time >= a.time && time <= b.time)
            {
                float t = (time - a.time) / (b.time - a.time);
                return interpolator(a.value, b.value, t);
            }
        }

        return KeyFrames[^1].value;
    }

    #endregion
}

public enum InterpolationStyle
{
    Linear,
    Cosine
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