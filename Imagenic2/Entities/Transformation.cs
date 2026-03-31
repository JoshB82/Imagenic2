namespace Imagenic2.Core.Entities;

internal class Transformation<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public Func<TTransformableEntity, TTransformableEntity> transformation;

    public List<KeyFrame> KeyFrames = new List<KeyFrame>();

    #endregion

    #region Constructors

    public Transformation()
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
    public InterpolationStyle interpolationBetweenFrames = InterpolationStyle.Linear;
    private Func<TValue, TValue, float> interpolator;

    #endregion

    #region Constructors

    public KeyFrameAnimation(List<KeyFrame<TValue>> keyFrames)
    {
        KeyFrames = keyFrames;
        interpolator = NumberHelpers.Interpolate;
    }

    #endregion

    #region Methods

    public TValue Evaluate(float time)
    {
        for (int i = 0; i < KeyFrames.Count; i++)
        {
            
        }
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