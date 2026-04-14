namespace Imagenic2.Core.Entities.Animation;

public static partial class TransformableEntityAnimationExtensions
{
    #region Pan

    /// <summary>
    /// Pans the camera in the forward direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanForward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward * distance, time);
    }
    /// <summary>
    /// Pans the camera in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight * -distance, time);
    }
    /// <summary>
    /// Pans the camera in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight * distance, time);
    }
    /// <summary>
    /// Pans the camera in the backward direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanBackward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward * -distance, time);
    }
    /// <summary>
    /// Pans the camera in the up direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp * distance, time);
    }
    /// <summary>
    /// Pans the camera in the down direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        return cameraTCtx.Translate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp * -distance, time);
    }

    #endregion

    #region Rotate, Roll

    /// <summary>
    /// Rotates the camera in the up direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight, -angle, time);
    }
    /// <summary>
    /// Rotates the camera in the down direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionRight, angle, time);
    }
    /// <summary>
    /// Rotates the camera in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp, -angle, time);
    }
    /// <summary>
    /// Rotates the camera in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionUp, angle, time);
    }
    /// <summary>
    /// Rolls the camera in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RollLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward, angle, time);
    }
    /// <summary>
    /// Rolls the camera in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RollRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward, -angle, time);
    }

    #endregion

    #region Pan with predicate

    public static AnimationContext<TCamera> PanForward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanForward(distance, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> PanLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanLeft(distance, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> PanRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanRight(distance, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> PanBackward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanBackward(distance, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> PanUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return (predicate(cameraTCtx.TransformableEntity)) ? cameraTCtx.PanUp(distance, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> PanDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanDown(distance, time) : cameraTCtx;
    }

    #endregion

    #region Rotate, Roll with predicate

    public static AnimationContext<TCamera> RotateUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateUp(angle, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> RotateDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateDown(angle, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> RotateLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateLeft(angle, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> RotateRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateRight(angle, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> RollLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RollLeft(angle, time) : cameraTCtx;
    }
    public static AnimationContext<TCamera> RollRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RollRight(angle, time) : cameraTCtx;
    }
    
    #endregion

    #region IEnumerable Pan

    public static IEnumerable<AnimationContext<TCamera>> PanForward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanForward(distance, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanLeft(distance, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanRight(distance, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanBackward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanBackward(distance, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanUp(distance, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanDown(distance, time);
        }
    }

    #endregion

    #region IEnumerable Rotate, Roll

    public static IEnumerable<AnimationContext<TCamera>> RotateUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateUp(angle, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RotateDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateDown(angle, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RotateLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateLeft(angle, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RotateRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateRight(angle, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RollLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollLeft(angle, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RollRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollRight(angle, time);
        }
    }

    #endregion

    #region IEnumerable Pan with predicate

    public static IEnumerable<AnimationContext<TCamera>> PanForward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanForward(distance, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanLeft(distance, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanRight(distance, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanBackward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanBackward(distance, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanUp(distance, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> PanDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanDown(distance, predicate, time);
        }
    }

    #endregion

    #region IEnumerable Rotate, Roll with predicate

    public static IEnumerable<AnimationContext<TCamera>> RotateUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateUp(angle, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RotateDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateDown(angle, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RotateLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateLeft(angle, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RotateRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateRight(angle, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RollLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollLeft(angle, predicate, time);
        }
    }
    public static IEnumerable<AnimationContext<TCamera>> RollRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollRight(angle, predicate, time);
        }
    }

    #endregion
}