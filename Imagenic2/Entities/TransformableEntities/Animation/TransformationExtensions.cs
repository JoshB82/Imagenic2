namespace Imagenic2.Core.Entities.TransformableEntities.Animation;

public static class TransformationExtensions
{
    public static TransformationContext<TTransformableEntity> Start<TTransformableEntity>(this TTransformableEntity transformableEntity, float time) where TTransformableEntity : TransformableEntity
    {
        return new TransformationContext<TTransformableEntity>(transformableEntity, time);
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
        if (physicalTCtx.ScalingKeyFrameAnimation is null)
        {
            physicalTCtx.ScalingKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, NumberHelpers.Interpolate);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(physicalTCtx.StartTime, physicalTCtx.TransformableEntity.Scaling);
            physicalTCtx.ScalingKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            physicalTCtx.scaling = physicalTCtx.TransformableEntity.Scaling;
        }

        physicalTCtx.scaling = new Vector3D(physicalTCtx.scaling.x * scaleFactor.x, physicalTCtx.scaling.y * scaleFactor.y, physicalTCtx.scaling.z * scaleFactor.z);
        KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(time, physicalTCtx.scaling);
        physicalTCtx.ScalingKeyFrameAnimation.KeyFrames.Add(keyFrame);

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
        if (translatableTCtx.TranslationKeyFrameAnimation is null)
        {
            translatableTCtx.TranslationKeyFrameAnimation = new KeyFrameAnimation<Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableTCtx.TransformableEntity.WorldOrigin = v, NumberHelpers.Interpolate);
            KeyFrame<Vector3D> startingKeyFrame = new KeyFrame<Vector3D>(translatableTCtx.StartTime, translatableTCtx.TransformableEntity.WorldOrigin);
            translatableTCtx.TranslationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            translatableTCtx.position = translatableTCtx.TransformableEntity.WorldOrigin;
        }

        translatableTCtx.position += displacement;
        KeyFrame<Vector3D> keyFrame = new KeyFrame<Vector3D>(time, translatableTCtx.position);
        translatableTCtx.TranslationKeyFrameAnimation.KeyFrames.Add(keyFrame);

        return translatableTCtx;
    }

    #endregion
}