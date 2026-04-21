namespace Imagenic2.Core.Entities.Animation;

public static class AnimationExtensions
{
    #region Start

    public static AnimationContext<TTransformableEntity> Start<TTransformableEntity>(this TTransformableEntity transformableEntity, float time = 0) where TTransformableEntity : TransformableEntity
    {
        return new AnimationContext<TTransformableEntity>(transformableEntity, time);
    }

    public static IEnumerable<AnimationContext<TTransformableEntity>> Start<TTransformableEntity>(this IEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        foreach (TTransformableEntity transformableEntity in transformableEntities)
        {
            yield return new AnimationContext<TTransformableEntity>(transformableEntity, time);
        }
    }

    public static async IAsyncEnumerable<AnimationContext<TTransformableEntity>> Start<TTransformableEntity>(this IAsyncEnumerable<TTransformableEntity> transformableEntities, float time = 0) where TTransformableEntity : TransformableEntity
    {
        await foreach (TTransformableEntity transformableEntity in transformableEntities)
        {
            yield return new AnimationContext<TTransformableEntity>(transformableEntity, time);
        }
    }

    #endregion

    #region End

    public static Animation End<TTransformableEntity>(this IEnumerable<AnimationContext<TTransformableEntity>> tCtxs) where TTransformableEntity : TransformableEntity
    {
        foreach (AnimationContext<TTransformableEntity> transformationContext in tCtxs)
        {
            transformationContext.AssembleTransformation();
        }

        return new Animation(tCtxs.Select(t => t.Transformation));
    }

    #endregion
}