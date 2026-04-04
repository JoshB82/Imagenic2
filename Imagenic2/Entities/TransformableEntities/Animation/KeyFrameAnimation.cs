namespace Imagenic2.Core.Entities.Animation;

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
        this.setProperty = setProperty;
        this.interpolator = interpolator;
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