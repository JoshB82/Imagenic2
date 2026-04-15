namespace Imagenic2.Core.Entities.Animation;

/// <summary>
/// Encapsulates transformations for animations.
/// </summary>
public static partial class TransformableEntityAnimationExtensions
{
    #region Pan

    /// <summary>
    /// Pans the <see cref="Camera"/> in the forward direction (relative to the camera).
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
    /// Pans the <see cref="Camera"/> in the left direction (relative to the camera).
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
    /// Pans the <see cref="Camera"/> in the right direction (relative to the camera).
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
    /// Pans the <see cref="Camera"/> in the backward direction (relative to the camera).
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
    /// Pans the <see cref="Camera"/> in the up direction (relative to the camera).
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
    /// Pans the <see cref="Camera"/> in the down direction (relative to the camera).
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
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera).
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
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera).
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
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera).
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
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera).
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
    /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RollLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward, angle, time);
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="time">The time by which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RollRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        return cameraTCtx.Rotate(cameraTCtx.TransformableEntity.WorldOrientation.DirectionForward, -angle, time);
    }

    #endregion

    #region Pan with predicate

    /// <summary>
    /// Pans the <see cref="Camera"/> in the forward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanForward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanForward(distance, time) : cameraTCtx;
    }
    /// <summary>
    /// Pans the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanLeft(distance, time) : cameraTCtx;
    }
    /// <summary>
    /// Pans the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanRight(distance, time) : cameraTCtx;
    }
    /// <summary>
    /// Pans the <see cref="Camera"/> in the backward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanBackward<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanBackward(distance, time) : cameraTCtx;
    }
    /// <summary>
    /// Pans the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanUp(distance, time) : cameraTCtx;
    }
    /// <summary>
    /// Pans the <see cref="Camera"/> in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> PanDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.PanDown(distance, time) : cameraTCtx;
    }

    #endregion

    #region Rotate, Roll with predicate

    /// <summary>
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateUp<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateUp(angle, time) : cameraTCtx;
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateDown<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateDown(angle, time) : cameraTCtx;
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateLeft(angle, time) : cameraTCtx;
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RotateRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RotateRight(angle, time) : cameraTCtx;
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RollLeft<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RollLeft(angle, time) : cameraTCtx;
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The context for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The context for this <see cref="Animation"/>.</returns>
    public static AnimationContext<TCamera> RollRight<TCamera>(this AnimationContext<TCamera> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        return predicate(cameraTCtx.TransformableEntity) ? cameraTCtx.RollRight(angle, time) : cameraTCtx;
    }

    #endregion

    #region IEnumerable Pan

    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the forward direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanForward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanForward(distance, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanLeft(distance, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanRight(distance, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the backward direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanBackward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanBackward(distance, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the up direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanUp(distance, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the down direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanDown(distance, time);
        }
    }

    #endregion

    #region IEnumerable Rotate, Roll

    /// <summary>
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateUp(angle, time);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateDown(angle, time);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateLeft(angle, time);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateRight(angle, time);
        }
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RollLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollLeft(angle, time);
        }
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RollRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollRight(angle, time);
        }
    }

    #endregion

    #region IEnumerable Pan with predicate

    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the forward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanForward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanForward(distance, predicate, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanLeft(distance, predicate, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanRight(distance, predicate, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the backward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanBackward<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanBackward(distance, predicate, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanUp(distance, predicate, time);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> PanDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float distance, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.PanDown(distance, predicate, time);
        }
    }

    #endregion

    #region IEnumerable Rotate, Roll with predicate

    /// <summary>
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateUp<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateUp(angle, predicate, time);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateDown<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateDown(angle, predicate, time);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateLeft(angle, predicate, time);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RotateRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RotateRight(angle, predicate, time);
        }
    }
    /// <summary>
    /// Rolls each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RollLeft<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollLeft(angle, predicate, time);
        }
    }
    /// <summary>
    /// Rolls each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameraTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <param name="time">The time which this transformation should be completed by.</param>
    /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
    public static IEnumerable<AnimationContext<TCamera>> RollRight<TCamera>(this IEnumerable<AnimationContext<TCamera>> cameraTCtx, float angle, Func<TCamera, bool> predicate, float time) where TCamera : Camera
    {
        foreach (AnimationContext<TCamera> tCtx in cameraTCtx)
        {
            yield return tCtx.RollRight(angle, predicate, time);
        }
    }

    #endregion
}