namespace Imagenic2.Core.Entities.Animation;

public sealed class InstantaneousAnimation<TTransformableEntity> : IAnimation where TTransformableEntity : TransformableEntity
{
    #region Fields and Properties

    public TTransformableEntity TransformableEntity { get; set; }
    public List<KeyFrame<Action<TTransformableEntity>>> KeyFrames { get; set; }

    #endregion

    #region Constructors

    public InstantaneousAnimation(TTransformableEntity transformableEntity, List<KeyFrame<Action<TTransformableEntity>>> keyFrames)
    {
        ThrowIfNull(transformableEntity, keyFrames);

        TransformableEntity = transformableEntity;
        KeyFrames = keyFrames;
    }

    #endregion

    #region Methods

    public InstantaneousAnimation<TTransformableEntity> ShallowCopy() => (InstantaneousAnimation<TTransformableEntity>)MemberwiseClone();
    public InstantaneousAnimation<TTransformableEntity> DeepCopy()
    {
        return new InstantaneousAnimation<TTransformableEntity>((TTransformableEntity)(TransformableEntity.DeepCopy()), KeyFrames.ToList());
    }

    public void Apply(float time)
    {
        foreach (KeyFrame<Action<TTransformableEntity>> keyFrame in KeyFrames)
        {
            if (keyFrame.Time.ApproxEquals(time))
            {
                keyFrame.Value(TransformableEntity);
                return;
            }
        }
    }

    #endregion
}