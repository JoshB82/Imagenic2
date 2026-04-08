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

    public static TransformationContextIAsyncEnumerable<TTransformableEntity> Start<TTransformableEntity>(this IAsyncEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContextIAsyncEnumerable<TTransformableEntity>(transformableEntities, time);
    }

    #endregion

    #region Orientate

    public static TransformationContext<TOrientatedEntity> Orientate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        if (orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation is null)
        {
            orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedTCtx.TransformableEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
            Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedTCtx.TransformableEntity.WorldOrientation);
            KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
            orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Quaternion newQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation);
        KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
        orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return orientatedTCtx;
    }

    #endregion

    #region Rotate

    public static TransformationContext<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContext<TOrientatedEntity> orientatedTCtx, Quaternion q, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        if (orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation is null)
        {
            orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedTCtx.TransformableEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
            Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedTCtx.TransformableEntity.WorldOrientation);
            KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
            orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Quaternion latestQuaternion = orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation.KeyFrames[^1].value;
        Quaternion newQuaternion = q * latestQuaternion;
        KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
        orientatedTCtx.TransformableEntity.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

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

        if (physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation is null)
        {
            physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, MathsHelper.Lerp);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(physicalTCtx.StartTime, physicalTCtx.TransformableEntity.Scaling);
            physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Vector3D latestScaling = physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation.KeyFrames[^1].value;
        KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
        physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

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

        if (translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation is null)
        {
            translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableTCtx.TransformableEntity.WorldOrigin = v, MathsHelper.Lerp);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(translatableTCtx.StartTime, translatableTCtx.TransformableEntity.WorldOrigin);
            translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Vector3D latestWorldOrigin = translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation.KeyFrames[^1].value;
        KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, latestWorldOrigin + displacement);
        translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Orientate

    public static TransformationContextIEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Orientation orientation, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (TOrientatedEntity orientatedEntity in orientatedTCtx.TransformableEntities)
        {
            if (orientatedEntity.OrientationKeyFrameAnimation is null)
            {
                orientatedEntity.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
                orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Quaternion newQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation);
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Rotate

    public static TransformationContextIEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this TransformationContextIEnumerable<TOrientatedEntity> orientatedTCtx, Quaternion q, float time) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedTCtx);

        foreach (TOrientatedEntity orientatedEntity in orientatedTCtx.TransformableEntities)
        {
            if (orientatedEntity.OrientationKeyFrameAnimation is null)
            {
                orientatedEntity.OrientationKeyFrameAnimation = new KeyFrameAnimation<Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
                orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Quaternion latestQuaternion = orientatedEntity.OrientationKeyFrameAnimation.KeyFrames[^1].value;
            Quaternion newQuaternion = q * latestQuaternion;
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientatedEntity.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
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

        foreach (TPhysicalEntity physicalEntity in physicalTCtx.TransformableEntities)
        {
            if (physicalEntity.ScalingKeyFrameAnimation is null)
            {
                physicalEntity.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalEntity.Scaling = v, MathsHelper.Lerp);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(physicalTCtx.StartTime, physicalEntity.Scaling);
                physicalEntity.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Vector3D latestScaling = physicalEntity.ScalingKeyFrameAnimation.KeyFrames[^1].value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, new Vector3D(latestScaling.x * scaleFactor.x, latestScaling.y * scaleFactor.y, latestScaling.z * scaleFactor.z));
            physicalEntity.ScalingKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return physicalTCtx;
    }

    #endregion

    #region IEnumerable Transform

    public static TransformationContextIEnumerable<TTransformableEntity> Transform<TTransformableEntity>(this TransformationContextIEnumerable<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        List<TTransformableEntity> transformableEntities = transformableTCtx.TransformableEntities.ToList();
        transformableTCtx.TransformationAnimations ??= new List<InstantaneousAnimation<TTransformableEntity>>(transformableEntities.Count);
        for (int i = 0; i < transformableEntities.Count; i++)
        {
            transformableTCtx.TransformationAnimations[i] ??= new InstantaneousAnimation<TTransformableEntity>(transformableEntities[i], new List<KeyFrame<Action<TTransformableEntity>>>());
            KeyFrame<Action<TTransformableEntity>> newKeyFrame = new KeyFrame<Action<TTransformableEntity>>(time, transformation);
            transformableTCtx.TransformationAnimations[i].KeyFrames.Add(newKeyFrame);
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

        foreach (TTranslatableEntity translatableEntity in translatableTCtx.TransformableEntities)
        {
            if (translatableEntity.TranslationKeyFrameAnimation is null)
            {
                translatableEntity.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableEntity.WorldOrigin = v, MathsHelper.Lerp);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(translatableTCtx.StartTime, translatableEntity.WorldOrigin);
                translatableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Vector3D latestWorldOrigin = translatableEntity.TranslationKeyFrameAnimation.KeyFrames[^1].value;
            KeyFrame<Vector3D> newKeyFrame = new KeyFrame<Vector3D>(time, latestWorldOrigin + displacement);
            translatableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);
        }

        return translatableTCtx;
    }

    #endregion

    #region IAsyncEnumerable Transform

    public static TransformationContextIAsyncEnumerable<TTransformableEntity> Transform<TTransformableEntity>(this TransformationContextIAsyncEnumerable<TTransformableEntity> transformableTCtx, Action<TTransformableEntity> transformation, float time) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableTCtx);

        List<TTransformableEntity> transformableEntities = transformableTCtx.TransformableEntities.ToList();
        transformableTCtx.TransformationAnimations ??= new List<InstantaneousAnimation<TTransformableEntity>>(transformableEntities.Count);
        for (int i = 0; i < transformableEntities.Count; i++)
        {
            transformableTCtx.TransformationAnimations[i] ??= new InstantaneousAnimation<TTransformableEntity>(transformableEntities[i], new List<KeyFrame<Action<TTransformableEntity>>>());
            KeyFrame<Action<TTransformableEntity>> newKeyFrame = new KeyFrame<Action<TTransformableEntity>>(time, transformation);
            transformableTCtx.TransformationAnimations[i].KeyFrames.Add(newKeyFrame);
        }

        return transformableTCtx;
    }

    #endregion
}