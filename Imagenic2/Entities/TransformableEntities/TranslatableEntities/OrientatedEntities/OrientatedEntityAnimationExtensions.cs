using Imagenic2.Core.Entities.Animation;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Entities;

public static class OrientatedEntityAnimationExtensions
{
    #region Look At

    public static AnimationContext<TOrientatedEntity> LookAt<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, TranslatableEntity translatableEntity, float time) where TOrientatedEntity : OrientatedEntity
    {
        Vector3D newDirectionForward = translatableEntity.WorldOrigin - orientatedTCtx.TransformableEntity.WorldOrigin;
        return orientatedTCtx.Rotate(MathsHelper.QuaternionRotateBetweenVectors(orientatedTCtx.TransformableEntity.WorldOrientation.DirectionForward, newDirectionForward), time);
    }

    #endregion

    #region Look At with predicate

    public static AnimationContext<TOrientatedEntity> LookAt<TOrientatedEntity>(this AnimationContext<TOrientatedEntity> orientatedTCtx, TranslatableEntity translatableEntity, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        if (predicate(orientatedTCtx.TransformableEntity))
        {
            orientatedTCtx.LookAt(translatableEntity, time);
        }
        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Look At

    public static AnimationContextIEnumerable<TOrientatedEntity> LookAt<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, TranslatableEntity translatableEntity, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            tCtx.LookAt(translatableEntity, time);
        }

        return orientatedTCtx;
    }

    #endregion

    #region IEnumerable Look At with predicate

    public static AnimationContextIEnumerable<TOrientatedEntity> LookAt<TOrientatedEntity>(this AnimationContextIEnumerable<TOrientatedEntity> orientatedTCtx, TranslatableEntity translatableEntity, Func<TOrientatedEntity, bool> predicate, float time) where TOrientatedEntity : OrientatedEntity
    {
        foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.LookAt(translatableEntity, time);
            }
        }
        return orientatedTCtx;
    }

    #endregion
}