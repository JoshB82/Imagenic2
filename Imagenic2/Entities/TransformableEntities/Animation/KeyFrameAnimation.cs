namespace Imagenic2.Core.Entities.Animation;

public class KeyFrameAnimation<TTransformableEntity, TValue> : IAnimation<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public List<Instruction<TTransformableEntity, TValue>> Instructions { get; set; } = new();

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

    public KeyFrameAnimation<TTransformableEntity, TValue> ShallowCopy() => (KeyFrameAnimation<TTransformableEntity, TValue>)MemberwiseClone();
    public KeyFrameAnimation<TTransformableEntity, TValue> DeepCopy()
    {
        return new KeyFrameAnimation<TTransformableEntity, TValue>(KeyFrames.ToList(), SetProperty, Interpolator);
    }

    private TValue Evaluate(TTransformableEntity transformableEntity, float time)
    {
        bool additionalFrame = true;

        for (int i = 0; i < Instructions.Count; i++)
        {
            if (additionalFrame || (!Instructions[i].Resolved && Instructions[i].Time <= time))
            {
                KeyFrame<TValue> kf = new KeyFrame<TValue>(Instructions[i].Time, Instructions[i].Resolve(transformableEntity));
                KeyFrames.Add(kf);
                Instructions[i].Resolved = true;
            }
            else
            {
                if (additionalFrame)
                {
                    additionalFrame = false;
                }
                else
                {
                    break;
                }
            }
        }

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

    public void Apply(TTransformableEntity transformableEntity, float time)
    {
        SetProperty(Evaluate(transformableEntity, time));
    }

    #endregion
}