namespace Imagenic2.Core.Entities.Animation;

public class KeyFrameAnimation<TValue> : IAnimation
{
    #region Fields and Properties

    public List<KeyFrame<TValue>> KeyFrames { get; set; }
    public Action<TValue> SetProperty { get; set; }
    public Func<TValue, TValue, float, TValue> Interpolator { get; set; }

    #endregion

    #region Constructors

    public KeyFrameAnimation(List<KeyFrame<TValue>> keyFrames, Action<TValue> setProperty, Func<TValue, TValue, float, TValue> interpolator)
    {
        ThrowIfNull(keyFrames, setProperty, interpolator);

        KeyFrames = keyFrames;
        SetProperty = setProperty;
        Interpolator = interpolator;
    }

    #endregion

    #region Methods

    public KeyFrameAnimation<TValue> ShallowCopy() => (KeyFrameAnimation<TValue>)MemberwiseClone();
    public KeyFrameAnimation<TValue> DeepCopy()
    {
        return new KeyFrameAnimation<TValue>(KeyFrames.ToList(), SetProperty, Interpolator);
    }

    public virtual TValue Evaluate(float time)
    {
        for (int i = 0; i < KeyFrames.Count - 1; i++)
        {
            KeyFrame<TValue> a = KeyFrames[i];
            KeyFrame<TValue> b = KeyFrames[i + 1];

            if (time >= a.Time && time <= b.Time)
            {
                float t = (time - a.Time) / (b.Time - a.Time);
                return Interpolator(a.Value, b.Value, t);
            }
        }

        return KeyFrames[^1].Value;
    }

    public void Apply(float time)
    {
        SetProperty(Evaluate(time));
    }

    #endregion
}