namespace Imagenic2.Core.Entities.Animation;

public struct KeyFrame<TValue>
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

public class Instruction<TTransformableEntity, TOutput> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public float Time { get; set; }

    public Func<TTransformableEntity, TOutput> Func { get; set; }
    public Func<TTransformableEntity, bool>? Predicate { get; set; }
    public TOutput PredicateFailValue { get; set; }
    public bool Resolved { get; set; }

    #endregion

    #region Constructors

    public Instruction(float time, Func<TTransformableEntity, TOutput> func, TOutput predicateFailValue, Func<TTransformableEntity, bool>? predicate = null)
    {
        ThrowIfNonpositive(time);

        Time = time;
        Func = func;
        Predicate = predicate;
        PredicateFailValue = predicateFailValue;
    }

    #endregion

    #region Methods

    public TOutput Resolve(TTransformableEntity transformableEntity)
    {
        return Predicate is not null && Predicate(transformableEntity) ? Func(transformableEntity) : PredicateFailValue;
    }

    #endregion
}