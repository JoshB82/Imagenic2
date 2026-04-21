namespace Imagenic2.Core.Entities.Animation;

public sealed class InstantaneousAnimation<TTransformableEntity> : IAnimation<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public List<KeyFrame<Action<TTransformableEntity>>> KeyFrames { get; set; }

    #endregion

    #region Constructors

    public InstantaneousAnimation(List<KeyFrame<Action<TTransformableEntity>>> keyFrames)
    {
        ThrowIfNull(keyFrames);

        KeyFrames = keyFrames;
    }

    #endregion

    #region Methods

    public InstantaneousAnimation<TTransformableEntity> ShallowCopy() => (InstantaneousAnimation<TTransformableEntity>)MemberwiseClone();
    public InstantaneousAnimation<TTransformableEntity> DeepCopy()
    {
        return new InstantaneousAnimation<TTransformableEntity>(KeyFrames.ToList());
    }

    public void Apply(TTransformableEntity transformableEntity, float time)
    {
        foreach (KeyFrame<Action<TTransformableEntity>> keyFrame in KeyFrames)
        {
            if (keyFrame.Time.ApproxEquals(time))
            {
                keyFrame.Value(transformableEntity);
                return;
            }
        }
    }

    #endregion
}