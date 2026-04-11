namespace Imagenic2.Core.Entities.Animation;

public static class AnimationExtensions
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
}