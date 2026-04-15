using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities.Animation;

public static partial class TransformableEntityAnimationExtensions
{
    #region Translate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distance, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distance, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distance, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="displacement"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, Vector3D displacement, float time) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableTCtx);

        if (translatableTCtx.TranslationKeyFrameAnimation is null)
        {
            translatableTCtx.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableTCtx.TransformableEntity.WorldOrigin = v, MathsHelper.Lerp);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(translatableTCtx.StartTime, translatableTCtx.TransformableEntity.WorldOrigin);
            translatableTCtx.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Vector3D latestWorldOrigin = translatableTCtx.TranslationKeyFrameAnimation.KeyFrames[^1].value;
        KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, latestWorldOrigin + displacement);
        translatableTCtx.TranslationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return translatableTCtx;
    }

    #endregion

    #region Translate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distance, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distance, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distance, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="displacement"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        return predicate(translatableTCtx.TransformableEntity) ? translatableTCtx.Translate(displacement, time) : translatableTCtx;
    }

    #endregion

    #region IEnumerable Translate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> TranslateX<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distance,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> TranslateY<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distance,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> TranslateZ<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distance,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> Translate<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distanceX,
        float distanceY,
        float distanceZ,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="displacement"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> Translate<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        Vector3D displacement,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableTCtx);

        foreach (AnimationContext<TTranslatableEntity> tCtx in translatableTCtx)
        {
            yield return tCtx.Translate(displacement, time);
        }
    }

    #endregion

    #region IEnumerable Translate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> TranslateX<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distance,
        Func<TTranslatableEntity, bool> predicate,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> TranslateY<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distance,
        Func<TTranslatableEntity, bool> predicate,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> TranslateZ<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distance,
        Func<TTranslatableEntity, bool> predicate,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> Translate<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        float distanceX,
        float distanceY,
        float distanceZ,
        Func<TTranslatableEntity, bool> predicate,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="displacement"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TTranslatableEntity>> Translate<TTranslatableEntity>(
        this IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx,
        Vector3D displacement,
        Func<TTranslatableEntity, bool> predicate,
        float time) where TTranslatableEntity : TranslatableEntity
    {
        foreach (AnimationContext<TTranslatableEntity> tCtx in translatableTCtx)
        {
            yield return predicate(tCtx.TransformableEntity) ? tCtx.Translate(displacement, predicate, time) : tCtx;
        }
    }

    #endregion
}