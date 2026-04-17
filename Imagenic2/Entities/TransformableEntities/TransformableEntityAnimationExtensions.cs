using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities.Animation;

public static partial class TransformableEntityAnimationExtensions
{
    #region Transform

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="transformation"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
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
    /// <param name="transformableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="transformation"></param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        return predicate(transformableTCtx.TransformableEntity) ? transformableTCtx.Transform(transformation, time) : transformableTCtx;
    }

    #endregion

    #region IEnumerable Transform

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="transformation"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTransformableEntity>> Transform<TTransformableEntity>(this IEnumerable<AnimationContext<TTransformableEntity>> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        foreach (AnimationContext<TTransformableEntity> tCtx in transformableTCtx)
        {
            yield return tCtx.Transform(transformation, time);
        }
    }

    #endregion

    #region IEnumerable Transform with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="transformation"></param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTransformableEntity>> Transform<TTransformableEntity>(this IEnumerable<AnimationContext<TTransformableEntity>> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        foreach (AnimationContext<TTransformableEntity> tCtx in transformableTCtx)
        {
            yield return tCtx.Transform(transformation, predicate, time);
        }
    }

    #endregion
}