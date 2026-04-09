using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities.Animation;

public static class TransformationExtensions
{
    #region Start

    public static TransformationContext<TTransformableEntity> Start<TTransformableEntity>(this TTransformableEntity transformableEntity, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContext<TTransformableEntity>(transformableEntity, time);
    }

    public static TransformationContextIEnumerable<TTransformableEntity> Start<TTransformableEntity>(this IEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContextIEnumerable<TTransformableEntity>(transformableEntities, time);
    }

    public static async Task<TransformationContextIEnumerable<TTransformableEntity>> Start<TTransformableEntity>(this IAsyncEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return await TransformationContextIEnumerable<TTransformableEntity>.Create(transformableEntities, time);
    }

    #endregion

    #region Orientate

    public static TransformationContext<TOrientatedEntity> Orientate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
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

    public static TransformationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Quaternion q, float time) where TOrientatedEntity : OrientatedEntity
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

    public static TransformationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, float time) where TOrientatedEntity : OrientatedEntity
    {
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region Scale

    public static TransformationContext<TPhysicalEntity> ScaleX<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContext<TPhysicalEntity> ScaleY<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorY, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContext<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, float time) where TPhysicalEntity : PhysicalEntity
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

    public static TransformationContext<TTransformableEntity> Transform<TTransformableEntity>(this TransformationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        transformableTCtx.TransformationAnimation ??= new InstantaneousAnimation<TTransformableEntity>(transformableTCtx.TransformableEntity, new List<KeyFrame<Action<TTransformableEntity>>>());
        KeyFrame<Action<TTransformableEntity>> newKeyFrame = new KeyFrame<Action<TTransformableEntity>>(time, transformation);
        transformableTCtx.TransformationAnimation.KeyFrames.Add(newKeyFrame);

        return transformableTCtx;
    }

    #endregion

    #region Translate

    public static TransformationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceX, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceY, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, Vector3D displacement, float time) where TTranslatableEntity : TranslatableEntity
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

    public static TransformationContext<TOrientatedEntity> Orientate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Orientation orientation, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Orientate(orientation, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region Rotate with predicate

    public static TransformationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Rotate(q, time);
        }
        return orientatedTCtx;
    }

    public static TransformationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.Rotate(axis, angle, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region Scale with predicate

    public static TransformationContext<TPhysicalEntity> ScaleX<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContext<TPhysicalEntity> ScaleY<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorY, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContext<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContext<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContext<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        if (predicate(physicalTCtx.TransformableEntity))
        {
            physicalTCtx.Scale(scaleFactor, time);
        }
        return physicalTCtx;
    }

    #endregion

    #region Transform with predicate

    public static TransformationContext<TTransformableEntity> Transform<TTransformableEntity>(this TransformationContext<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        if (predicate(transformableTCtx.TransformableEntity))
        {
            transformableTCtx.Transform(transformation, time);
        }
        return transformableTCtx;
    }

    #endregion

    #region Translate with predicate

    public static TransformationContext<TTranslatableEntity> TranslateX<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceX, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContext<TTranslatableEntity> TranslateY<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceY, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContext<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContext<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContext<TTranslatableEntity> translatableTCtx, Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        if (predicate(translatableTCtx.TransformableEntity))
        {
            translatableTCtx.Translate(displacement, time);
        }
        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Orientate

    public static TransformationContextIEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (TransformationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            tCtx.Orientate(orientation, time);
        }

        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Rotate

    public static TransformationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Quaternion q, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (TransformationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            tCtx.Rotate(q, time);
        }

        return orientatedTCtx;
    }

    public static TransformationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, float time) where TOrientatedEntity : OrientatedEntity
    {
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
    }

    #endregion

    #region IEnumerable Scale

    public static TransformationContextIEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorY, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, float time) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalTCtx);

        foreach (TransformationContext<TPhysicalEntity> tCtx in physicalTCtx.TransformationContexts)
        {
            tCtx.Scale(scaleFactor, time);
        }

        return physicalTCtx;
    }

    #endregion

    #region IEnumerable Transform

    public static TransformationContextIEnumerable<TTransformableEntity> Transform<TTransformableEntity>(this TransformationContextIEnumerable<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        foreach (TransformationContext<TTransformableEntity> tCtx in transformableTCtx.TransformationContexts)
        {
            tCtx.Transform(transformation, time);
        }

        return transformableTCtx;
    }

    #endregion

    #region IEnumerable Translate

    public static TransformationContextIEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceY, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, Vector3D displacement, float time) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableTCtx);

        foreach (TransformationContext<TTranslatableEntity> tCtx in translatableTCtx.TransformationContexts)
        {
            tCtx.Translate(displacement, time);
        }

        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Orientate with predicate

    public static TransformationContextIEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Orientation orientation, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (TransformationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
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

    public static TransformationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Quaternion q, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (TransformationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.Rotate(q, time);
            }
        }
        return orientatedTCtx;
    }

    public static TransformationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (TransformationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
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

    public static TransformationContextIEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, 1, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorY, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, scaleFactorY, 1);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(1, 1, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, float scaleFactorX, float scaleFactorY, float scaleFactorZ, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
        return physicalTCtx.Scale(scaleFactor, predicate, time);
    }

    public static TransformationContextIEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(this TransformationContextIEnumerable<TPhysicalEntity> physicalTCtx, Vector3D scaleFactor, Func<TPhysicalEntity, bool> predicate, float time) where TPhysicalEntity : PhysicalEntity
    {
        foreach (TransformationContext<TPhysicalEntity> tCtx in physicalTCtx.TransformationContexts)
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

    public static TransformationContextIEnumerable<TTransformableEntity> Transform<TTransformableEntity>(this TransformationContextIEnumerable<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, Func<TTransformableEntity, bool> predicate, float time) where TTransformableEntity : TransformableEntity
    {
        foreach (TransformationContext<TTransformableEntity> tCtx in transformableTCtx.TransformationContexts)
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

    public static TransformationContextIEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceY, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableTCtx.Translate(displacement, predicate, time);
    }

    public static TransformationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, Vector3D displacement, Func<TTranslatableEntity, bool> predicate, float time) where TTranslatableEntity : TranslatableEntity
    {
        foreach (TransformationContext<TTranslatableEntity> tCtx in translatableTCtx.TransformationContexts)
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