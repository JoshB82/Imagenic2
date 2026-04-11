namespace Imagenic2.Core.Entities.Animation;

public static class TransformableEntityAnimationExtensions
{
    #region Transform

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx"></param>
    /// <param name="transformation"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        transformableTCtx.TransformationAnimation ??= new InstantaneousAnimation<TTransformableEntity>(transformableTCtx.TransformableEntity, new List<KeyFrame<Action<TTransformableEntity>>>());
        KeyFrame<Action<TTransformableEntity>> newKeyFrame = new KeyFrame<Action<TTransformableEntity>>(time, transformation);
        transformableTCtx.TransformationAnimation.KeyFrames.Add(newKeyFrame);

        return transformableTCtx;
    }

    #endregion

    #region Transform with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx"></param>
    /// <param name="transformation"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        if (predicate(transformableTCtx.TransformableEntity))
        {
            transformableTCtx.Transform(transformation, time);
        }
        return transformableTCtx;
    }

    #endregion

    #region IEnumerable Transform

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx"></param>
    /// <param name="transformation"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContextIEnumerable<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        foreach (AnimationContext<TTransformableEntity> tCtx in transformableTCtx.TransformationContexts)
        {
            tCtx.Transform(transformation, time);
        }

        return transformableTCtx;
    }

    #endregion

    #region IEnumerable Transform with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx"></param>
    /// <param name="transformation"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContextIEnumerable<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        foreach (AnimationContext<TTransformableEntity> tCtx in transformableTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Transform(transformation, time);
            }
        }
        return transformableTCtx;
    }

    #endregion
}