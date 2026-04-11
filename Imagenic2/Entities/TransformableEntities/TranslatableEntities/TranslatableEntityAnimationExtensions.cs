using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities.Animation;

public static class TranslatableEntityAnimationExtensions
{
    #region Translate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceY"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceY, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="displacement"></param>
    /// <param name="time"></param>
    /// <returns></returns>
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
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceY"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceY, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="displacement"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        if (predicate(translatableTCtx.TransformableEntity))
        {
            translatableTCtx.Translate(displacement, time);
        }
        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Translate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceY"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceY, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="displacement"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, Vector3D displacement, float time) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableTCtx);

        foreach (AnimationContext<TTranslatableEntity> tCtx in translatableTCtx.TransformationContexts)
        {
            tCtx.Translate(displacement, time);
        }

        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Translate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceY"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceY, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity"></typeparam>
    /// <param name="translatableTCtx"></param>
    /// <param name="displacement"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        foreach (AnimationContext<TTranslatableEntity> tCtx in translatableTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Translate(displacement, time);
            }
        }
        return translatableTCtx;
    }

    #endregion
}