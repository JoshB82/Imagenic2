using Imagenic2.Core.Entities.Animation;

namespace Imagenic2.Core.Entities;

public static class CameraAnimationExtensions
{
    #region Pan

    public static AnimationContext<TCamera> PanForward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward * distance, time);
    }
    public static AnimationContext<TCamera> PanLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight * -distance, time);
    }
    public static AnimationContext<TCamera> PanRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight * distance, time);
    }
    public static AnimationContext<TCamera> PanBackward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward * -distance, time);
    }
    public static AnimationContext<TCamera> PanUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp * distance, time);
    }
    public static AnimationContext<TCamera> PanDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp * -distance, time);
    }

    #endregion

    #region Rotate, Roll

    public static AnimationContext<TCamera> RotateUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight, -angle, time);
    }
    public static AnimationContext<TCamera> RotateDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight, angle, time);
    }
    public static AnimationContext<TCamera> RotateLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp, -angle, time);
    }
    public static AnimationContext<TCamera> RotateRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp, angle, time);
    }
    public static AnimationContext<TCamera> RollLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward, angle, time);
    }
    public static AnimationContext<TCamera> RollRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward, -angle, time);
    }

    #endregion

    #region Pan with predicate

    public static AnimationContext<TCamera> PanForward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.PanForward(distance, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> PanLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.PanLeft(distance, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> PanRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.PanRight(distance, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> PanBackward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.PanBackward(distance, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> PanUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.PanUp(distance, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> PanDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.PanDown(distance, time);
        }
        return cameraTCtx;
    }

    #endregion

    #region Rotate, Roll with predicate

    public static AnimationContext<TCamera> RotateUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.RotateUp(angle, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> RotateDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.RotateDown(angle, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> RotateLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.RotateLeft(angle, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> RotateRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.RotateRight(angle, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> RollLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.RollLeft(angle, time);
        }
        return cameraTCtx;
    }
    public static AnimationContext<TCamera> RollRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        if (predicate(cameraTCtx.TransformableEntity))
        {
            cameraTCtx.RollRight(angle, time);
        }
        return cameraTCtx;
    }
    
    #endregion

    #region IEnumerable Pan

    public static AnimationContextIEnumerable<TCamera> PanForward<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.PanForward(distance, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanLeft<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.PanLeft(distance, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanRight<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.PanRight(distance, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanBackward<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.PanBackward(distance, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanUp<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.PanUp(distance, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanDown<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.PanDown(distance, time);
        }

        return cameraTCtx;
    }

    #endregion

    #region IEnumerable Rotate, Roll

    public static AnimationContextIEnumerable<TCamera> RotateUp<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.RotateUp(angle, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RotateDown<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.RotateDown(angle, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RotateLeft<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.RotateLeft(angle, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RotateRight<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.RotateRight(angle, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RollLeft<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.RollLeft(angle, time);
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RollRight<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            tCtx.RollRight(angle, time);
        }

        return cameraTCtx;
    }

    #endregion

    #region IEnumerable Pan with predicate

    public static AnimationContextIEnumerable<TCamera> PanForward<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.PanForward(distance, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanLeft<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.PanLeft(distance, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanRight<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.PanRight(distance, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanBackward<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.PanBackward(distance, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanUp<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.PanUp(distance, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> PanDown<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.PanDown(distance, time);
            }
        }

        return cameraTCtx;
    }

    #endregion

    #region IEnumerable Rotate, Roll with predicate

    public static AnimationContextIEnumerable<TCamera> RotateUp<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.RotateUp(angle, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RotateDown<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.RotateDown(angle, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RotateLeft<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.RotateLeft(angle, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RotateRight<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.RotateRight(angle, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RollLeft<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.RollLeft(angle, time);
            }
        }

        return cameraTCtx;
    }
    public static AnimationContextIEnumerable<TCamera> RollRight<TCamera>(this AnimationContextIEnumerable<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx.TransformationContexts)
        {
            if (predicate(tCtx.TransformableEntity))
            {
                tCtx.RollRight(angle, time);
            }
        }

        return cameraTCtx;
    }

    #endregion
}