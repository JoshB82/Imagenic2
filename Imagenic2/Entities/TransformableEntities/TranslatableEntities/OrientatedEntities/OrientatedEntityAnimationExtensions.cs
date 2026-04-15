using Imagenic2.Core.Entities.Animation;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities;

public static partial class TransformableEntityAnimationExtensions
{
    #region Orientate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="orientation"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> Orientate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        if (orientatedTCtx.OrientationKeyFrameAnimation is null)
        {
            orientatedTCtx.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedTCtx.TransformableEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
            Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedTCtx.TransformableEntity.WorldOrientation);
            KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
            orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Quaternion newQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation);
        KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
        orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return orientatedTCtx;
    }

    #endregion

    #region Rotate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="q"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Quaternion q, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        if (orientatedTCtx.OrientationKeyFrameAnimation is null)
        {
            orientatedTCtx.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedTCtx.TransformableEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
            Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedTCtx.TransformableEntity.WorldOrientation);
            KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
            orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Quaternion latestQuaternion = orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames[^1].value;
        Quaternion newQuaternion = q * latestQuaternion;
        KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
        orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return orientatedTCtx;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, float time) where TOrientatedEntity : OrientatedEntity
    {
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region Look At

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="translatableEntity"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> LookAt<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, TranslatableEntity translatableEntity, float time) where TOrientatedEntity : OrientatedEntity
    {
        Vector3D newDirectionForward = translatableEntity.WorldOrigin - orientatedTCtx.TransformableEntity.WorldOrigin;
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotateBetweenVectors(orientatedTCtx.TransformableEntity.WorldOrientation.DirectionForward, newDirectionForward), time);
    }

    #endregion

    #region Orientate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="orientation"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> Orientate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Orientation orientation, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Orientate(orientation, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region Rotate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="q"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Rotate(q, time);
        }
        return orientatedTCtx;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Rotate(axis, angle, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region Look At with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="translatableEntity"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TOrientatedEntity> LookAt<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, TranslatableEntity translatableEntity, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.LookAt(translatableEntity, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Orientate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="orientation"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> Orientate<TOrientatedEntity>(this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return tCtx.Orientate(orientation, time);
        }
    }

    #endregion

    #region IEnumerable Rotate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="q"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> Rotate<TOrientatedEntity>(
        this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx,
        Quaternion q,
        float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return tCtx.Rotate(q, time);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> Rotate<TOrientatedEntity>(
        this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx,
        Vector3D axis,
        float angle,
        float time) where TOrientatedEntity : OrientatedEntity
    {
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region IEnumerable Look At

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="translatableEntity"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> LookAt<TOrientatedEntity>(
        this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx,
        TranslatableEntity translatableEntity,
        float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return tCtx.LookAt(translatableEntity, time);
        }
    }

    #endregion

    #region IEnumerable Orientate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="orientation"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> Orientate<TOrientatedEntity>(
        this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx,
        Orientation orientation,
        Func<TOrientatedEntity, bool> predicate,
        float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return predicate(tCtx.TransformableEntity) ? tCtx.Orientate(orientation, time) : tCtx;
        }
    }

    #endregion

    #region IEnumerable Rotate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="q"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> Rotate<TOrientatedEntity>(this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx, Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return predicate(tCtx.TransformableEntity) ? tCtx.Rotate(q, time) : tCtx;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> Rotate<TOrientatedEntity>(
        this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx,
        Vector3D axis,
        float angle,
        Func<TOrientatedEntity, bool> predicate,
        float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return predicate(tCtx.TransformableEntity)? tCtx.Rotate(axis, angle, time) : tCtx;
        }
    }

    #endregion

    #region IEnumerable Look At with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="translatableEntity"></param>
    /// <param name="predicate"></param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TOrientatedEntity>> LookAt<TOrientatedEntity>(this IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx, TranslatableEntity translatableEntity, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
        {
            yield return predicate(tCtx.TransformableEntity) ? tCtx.LookAt(translatableEntity, time) : tCtx;
        }
    }

    #endregion
}