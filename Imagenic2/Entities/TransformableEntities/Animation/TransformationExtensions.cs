using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities.Animation;

public static class TransformationExtensions
{
    #region Start

    public static AnimationContext<TTransformableEntity> Start<TTransformableEntity>(this TTransformableEntity transformableEntity, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return new AnimationContext<TTransformableEntity>(transformableEntity, time);
    }

    public static AnimationContextIEnumerable<TTransformableEntity> Start<TTransformableEntity>(this IEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return new AnimationContextIEnumerable<TTransformableEntity>(transformableEntities, time);
    }

    public static async Task<AnimationContextIEnumerable<TTransformableEntity>> Start<TTransformableEntity>(this IAsyncEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return await AnimationContextIEnumerable<TTransformableEntity>.Create(transformableEntities, time);
    }

    #endregion

    #region Orientate

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

    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, float time) where TOrientatedEntity : OrientatedEntity
    {
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region Scale

    public static AnimationContext<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContext<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorY, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContext<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, float time) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalTCtx);

        if (physicalTCtx.ScalingKeyFrameAnimation is null)
        {
            physicalTCtx.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, MathsHelper.Lerp);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(physicalTCtx.StartTime, physicalTCtx.TransformableEntity.Scaling);
            physicalTCtx.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Vector3D latestScaling = physicalTCtx.ScalingKeyFrameAnimation.KeyFrames[^1].value;
        KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
        physicalTCtx.ScalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return physicalTCtx;
    }

    #endregion

    #region Transform

    public static AnimationContext<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        transformableTCtx.TransformationAnimation ??= new InstantaneousAnimation<TTransformableEntity>(transformableTCtx.TransformableEntity, new List<KeyFrame<Action<TTransformableEntity>>>());
        KeyFrame<Action<TTransformableEntity>> newKeyFrame = new KeyFrame<Action<TTransformableEntity>>(time, transformation);
        transformableTCtx.TransformationAnimation.KeyFrames.Add(newKeyFrame);

        return transformableTCtx;
    }

    #endregion

    #region Translate

    public static AnimationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static AnimationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceY, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static AnimationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

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

    #region Orientate with predicate

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

    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Rotate(q, time);
        }
        return orientatedTCtx;
    }

    public static AnimationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Rotate(axis, angle, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region Scale with predicate

    public static AnimationContext<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContext<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorY, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContext<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContext<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        if (predicate(physicalTCtx.TransformableEntity))
        {
            physicalTCtx.Scale(scaleFactor, time);
        }
        return physicalTCtx;
    }

    #endregion

    #region Transform with predicate

    public static AnimationContext<TTransformableEntity> Transform<TTransformableEntity>(this AnimationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        if (predicate(transformableTCtx.TransformableEntity))
        {
            transformableTCtx.Transform(transformation, time);
        }
        return transformableTCtx;
    }

    #endregion

    #region Translate with predicate

    public static AnimationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceY, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContext<TTranslatableEntity> translatableTCtx, Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        if (predicate(translatableTCtx.TransformableEntity))
        {
            translatableTCtx.Translate(displacement, time);
        }
        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Orientate

    public static AnimationContextIEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            tCtx.Orientate(orientation, time);
        }

        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Rotate

    public static AnimationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Quaternion q, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            tCtx.Rotate(q, time);
        }

        return orientatedTCtx;
    }

    public static AnimationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, float time) where TOrientatedEntity : OrientatedEntity
    {
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region IEnumerable Scale

    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorY, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static AnimationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

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

    #region IEnumerable Transform

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

    #region IEnumerable Translate

    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceY, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    public static AnimationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

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

    #region IEnumerable Orientate with predicate

    public static AnimationContextIEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Orientation orientation, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Orientate(orientation, time);
            }
        }
        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Rotate with predicate

    public static AnimationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Rotate(q, time);
            }
        }
        return orientatedTCtx;
    }

    public static AnimationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Rotate(axis, angle, time);
            }
        }
        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Scale with predicate

    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorY, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContextIEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static AnimationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this AnimationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

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

    #region IEnumerable Transform with predicate

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

    #region IEnumerable Translate with predicate

    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceY, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContextIEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static AnimationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this AnimationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

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