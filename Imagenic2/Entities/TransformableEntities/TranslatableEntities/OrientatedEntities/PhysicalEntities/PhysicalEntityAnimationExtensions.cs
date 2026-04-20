using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities.Animation;

public static class PhysicalEntityAnimationExtensions
{
    #region Scale

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorY, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, float time) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalTCtx);

        if (physicalTCtx.ScalingKeyFrameAnimation is null)
        {
            physicalTCtx.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, MathsHelper.Lerp);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(physicalTCtx.StartTime, physicalTCtx.TransformableEntity.Scaling);
            physicalTCtx.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Vector3D latestScaling = physicalTCtx.ScalingKeyFrameAnimation.KeyFrames[^1].Value;
        KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
        physicalTCtx.ScalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return physicalTCtx;
    }

    #endregion

    #region Scale with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorY, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        if (predicate(physicalTCtx.TransformableEntity))
        {
            physicalTCtx.Scale(scaleFactor, time);
        }
        return physicalTCtx;
    }

    #endregion

    #region IEnumerable Scale

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorY, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, float time) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalTCtx);

        foreach (AnimationContext<TPhysicalEntity> tCtx in physicalTCtx.TransformationContexts)
        {
            tCtx.Scale(scaleFactor, time);
        }

        return physicalTCtx;
    }

    #endregion

    #region IEnumerable Scale with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorY, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactorX"></param>
    /// <param name="scaleFactorY"></param>
    /// <param name="scaleFactorZ"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="predicate"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static AnimationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        foreach (AnimationContext<TPhysicalEntity> tCtx in physicalTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Scale(scaleFactor, time);
            }
        }
        return physicalTCtx;
    }

    #endregion
}