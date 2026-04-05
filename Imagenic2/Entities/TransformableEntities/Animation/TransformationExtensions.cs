namespace Imagenic2.Core.Entities.Animation;

public static class TransformationExtensions
{
    public static TransformationContext<TTransformableEntity> Start<TTransformableEntity>(this TTransformableEntity transformableEntity, float time) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContext<TTransformableEntity>(transformableEntity, time);
    }

    public static TransformationContextIEnumerable<TTransformableEntity> Start<TTransformableEntity>(this IEnumerable<TTransformableEntity> transformableEntities, float time) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContextIEnumerable<TTransformableEntity>(transformableEntities, time);
    }

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
        if (physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation is null)
        {
            physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, NumberHelpers.Interpolate);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(physicalTCtx.StartTime, physicalTCtx.TransformableEntity.Scaling);
            physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        Vector3D lastScaling = physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation.KeyFrames[^1].value;
        KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(time, new Vector3D(lastScaling.x * scaleFactor.x, lastScaling.y * scaleFactor.y, lastScaling.z * scaleFactor.z));
        physicalTCtx.TransformableEntity.ScalingKeyFrameAnimation.KeyFrames.Add(keyFrame);

        return physicalTCtx;
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
        if (translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation is null)
        {
            translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableTCtx.TransformableEntity.WorldOrigin = v, NumberHelpers.Interpolate);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(translatableTCtx.StartTime, translatableTCtx.TransformableEntity.WorldOrigin);
            translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
        }

        KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(time, translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation.KeyFrames[^1].value + displacement);
        translatableTCtx.TransformableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(keyFrame);

        return translatableTCtx;
    }

    #endregion

    #region IEnumerable Translate

    public static TransformationContextIEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>(this TransformationContextIEnumerable<TTranslatableEntity> translatableTCtx, Vector3D displacement, float time) where TTranslatableEntity : TranslatableEntity
    {
        foreach (TTranslatableEntity translatableEntity in translatableTCtx.TransformableEntities)
        {
            if (translatableEntity.TranslationKeyFrameAnimation is null)
            {
                translatableEntity.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableEntity.WorldOrigin = v, NumberHelpers.Interpolate);
                KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(translatableTCtx.StartTime, translatableEntity.WorldOrigin);
                translatableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(time, translatableEntity.TranslationKeyFrameAnimation.KeyFrames[^1].value + displacement);
            translatableEntity.TranslationKeyFrameAnimation.KeyFrames.Add(keyFrame);
        }

        return translatableTCtx;
    }

    #endregion
}