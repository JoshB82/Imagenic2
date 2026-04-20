namespace Imagenic2.Core.Entities.Animation;

public struct KeyFrame<TValue> : IKeyFrame<TValue>
{
    #region Fields and Properties

    public float Time { get; set; }
    public TValue Value { get; set; }

    #endregion

    #region Constructors

    public KeyFrame(float time, TValue value)
    {
        ThrowIfNonpositive(time);
        ThrowIfNull(value);

        Time = time;
        Value = value;
    }

    #endregion
}

public struct ConditionalKeyFrame<TTransformableEntity, TValue> : IKeyFrame<TValue> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public float Time { get; set; }
    public TValue Value { get; set; }
    public Func<TTransformableEntity, bool> Predicate { get; set; }

    #endregion

    #region Constructors

    public ConditionalKeyFrame(float time, TValue value, Func<TTransformableEntity, bool> predicate)
    {
        ThrowIfNonpositive(time);
        ThrowIfNull(value, predicate);

        Time = time;
        Value = value;
        Predicate = predicate;
    }

    #endregion
}

public interface IKeyFrame<TValue>
{
    public float Time { get; set; }
    public TValue Value { get; set; }
}