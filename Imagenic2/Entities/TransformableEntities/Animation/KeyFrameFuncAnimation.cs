namespace Imagenic2.Core.Entities.Animation;

public sealed class KeyFrameFuncAnimation<TValue> : KeyFrameAnimation<TValue>
{
    #region Fields and Properties

    public List<KeyFrame<Func<TValue>>> KeyFrameFuncs { get; set; } = new();

    #endregion

    #region Constructors

    public KeyFrameFuncAnimation(List<KeyFrame<TValue>> keyFrames, Action<TValue> setProperty, Func<TValue, TValue, float, TValue> interpolator) : base(keyFrames, setProperty, interpolator)
    {
    
    }

    #endregion

    #region Methods

    public override TValue Evaluate(float time)
    {
        for (int i = 0; i < KeyFrameFuncs.Count; i++)
        {
            KeyFrames[i] = new KeyFrame<TValue>(KeyFrameFuncs[i].Time, KeyFrameFuncs[i].Value());
        }

        return base.Evaluate(time);
    }

    #endregion
}