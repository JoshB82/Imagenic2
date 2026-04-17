using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities;

public static partial class TransformableEntityTransformations
{
    #region Pan

    /// <summary>
    /// Pans the camera in the forward direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanForward<TCamera>([DisallowNull] this TCamera camera, float distance) where TCamera : Camera
    {
        return camera.Translate(camera.WorldOrientation.DirectionForward * distance);
        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the left direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanLeft<TCamera>([DisallowNull] this TCamera camera, float distance) where TCamera : Camera
    {
        return camera.Translate(camera.WorldOrientation.DirectionRight * -distance);
        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the right direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanRight<TCamera>([DisallowNull] this TCamera camera, float distance) where TCamera : Camera
    {
        return camera.Translate(camera.WorldOrientation.DirectionRight * distance);
        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the backward direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanBackward<TCamera>([DisallowNull] this TCamera camera, float distance) where TCamera : Camera
    {
        return camera.Translate(camera.WorldOrientation.DirectionForward * -distance);
        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the up direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanUp<TCamera>([DisallowNull] this TCamera camera, float distance) where TCamera : Camera
    {
        return camera.Translate(camera.WorldOrientation.DirectionUp * distance);
        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Pans the camera in the down direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanDown<TCamera>([DisallowNull] this TCamera camera, float distance) where TCamera : Camera
    {
        return camera.Translate(camera.WorldOrientation.DirectionUp * -distance);
        //InvokeRenderEvent(RenderUpdate.NewRender);
    }

    #endregion

    #region Rotate, Roll

    /// <summary>
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateUp<TCamera>([DisallowNull] this TCamera camera, float angle) where TCamera : Camera
    {
        return camera.Rotate(camera.WorldOrientation.DirectionRight, angle);

        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateDown<TCamera>([DisallowNull] this TCamera camera, float angle) where TCamera : Camera
    {
        return camera.Rotate(camera.WorldOrientation.DirectionRight, -angle);

        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateLeft<TCamera>([DisallowNull] this TCamera camera, float angle) where TCamera : Camera
    {
        return camera.Rotate(camera.WorldOrientation.DirectionUp, angle);

        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateRight<TCamera>([DisallowNull] this TCamera camera, float angle) where TCamera : Camera
    {
        return camera.Rotate(camera.WorldOrientation.DirectionUp, -angle);

        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RollLeft<TCamera>([DisallowNull] this TCamera camera, float angle) where TCamera : Camera
    {
        return camera.Rotate(camera.WorldOrientation.DirectionForward, -angle);

        //InvokeRenderEvent(RenderUpdate.NewRender);
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera).
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RollRight<TCamera>([DisallowNull] this TCamera camera, float angle) where TCamera : Camera
    {
        return camera.Rotate(camera.WorldOrientation.DirectionForward, angle);

        //InvokeRenderEvent(RenderUpdate.NewRender);
    }

    #endregion

    #region Pan with predicate

    /// <summary>
    /// Pans the camera in the forward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanForward<TCamera>([DisallowNull] this TCamera camera, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.PanForward(distance) : camera;
    }
    /// <summary>
    /// Pans the camera in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanLeft<TCamera>([DisallowNull] this TCamera camera, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.PanLeft(distance) : camera;
    }
    /// <summary>
    /// Pans the camera in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanRight<TCamera>([DisallowNull] this TCamera camera, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.PanRight(distance) : camera;
    }
    /// <summary>
    /// Pans the camera in the backward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanBackward<TCamera>([DisallowNull] this TCamera camera, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.PanBackward(distance) : camera;
    }
    /// <summary>
    /// Pans the camera in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanUp<TCamera>([DisallowNull] this TCamera camera, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.PanUp(distance) : camera;
    }
    /// <summary>
    /// Pans the camera in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera PanDown<TCamera>([DisallowNull] this TCamera camera, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.PanDown(distance) : camera;
    }

    #endregion

    #region Rotate, Roll with predicate

    /// <summary>
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateUp<TCamera>([DisallowNull] this TCamera camera, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.RotateUp(angle) : camera;
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateDown<TCamera>([DisallowNull] this TCamera camera, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.RotateDown(angle) : camera;
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateLeft<TCamera>([DisallowNull] this TCamera camera, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.RotateLeft(angle) : camera;
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RotateRight<TCamera>([DisallowNull] this TCamera camera, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.RotateRight(angle) : camera;
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RollLeft<TCamera>([DisallowNull] this TCamera camera, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.RollLeft(angle) : camera;
    }
    /// <summary>
    /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="camera">The <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/>.</returns>
    public static TCamera RollRight<TCamera>([DisallowNull] this TCamera camera, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        return predicate(camera) ? camera.RollRight(angle) : camera;
    }

    #endregion

    #region IEnumerable Pan

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanForward<TCamera>(this IEnumerable<TCamera> cameras, float distance) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanForward(distance);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanLeft<TCamera>(this IEnumerable<TCamera> cameras, float distance) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanLeft(distance);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanRight<TCamera>(this IEnumerable<TCamera> cameras, float distance) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanRight(distance);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanBackward<TCamera>(this IEnumerable<TCamera> cameras, float distance) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanBackward(distance);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanUp<TCamera>(this IEnumerable<TCamera> cameras, float distance) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanUp(distance);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanDown<TCamera>(this IEnumerable<TCamera> cameras, float distance) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanDown(distance);
        }
    }

    #endregion

    #region IEnumerable Rotate, Roll

    /// <summary>
    /// Rotates each element of the <see cref="Camera"/> sequence in the up direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras"></param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateUp<TCamera>(this IEnumerable<TCamera> cameras, float angle) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateUp(angle);
        }
    }
    /// <summary>
    /// Rotates each element of the <see cref="Camera"/> sequence in the down direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras"></param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateDown<TCamera>(this IEnumerable<TCamera> cameras, float angle) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateDown(angle);
        }
    }
    /// <summary>
    /// Rotates each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras"></param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateLeft<TCamera>(this IEnumerable<TCamera> cameras, float angle) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateLeft(angle);
        }
    }
    /// <summary>
    /// Rotates each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras"></param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateRight<TCamera>(this IEnumerable<TCamera> cameras, float angle) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateRight(angle);
        }
    }
    /// <summary>
    /// Rolls each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RollLeft<TCamera>(this IEnumerable<TCamera> cameras, float angle) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RollLeft(angle);
        }
    }
    /// <summary>
    /// Rolls each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera).
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RollRight<TCamera>(this IEnumerable<TCamera> cameras, float angle) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RollRight(angle);
        }
    }

    #endregion

    #region IEnumerable Pan with predicate

    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the forward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanForward<TCamera>(this IEnumerable<TCamera> cameras, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanForward(distance, predicate);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanLeft<TCamera>(this IEnumerable<TCamera> cameras, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanLeft(distance, predicate);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanRight<TCamera>(this IEnumerable<TCamera> cameras, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanRight(distance, predicate);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the backward direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanBackward<TCamera>(this IEnumerable<TCamera> cameras, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanBackward(distance, predicate);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanUp<TCamera>(this IEnumerable<TCamera> cameras, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanUp(distance, predicate);
        }
    }
    /// <summary>
    /// Pans each element of the <see cref="Camera"/> sequence in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
    /// <param name="distance">The distance to pan by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> PanDown<TCamera>(this IEnumerable<TCamera> cameras, float distance, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.PanDown(distance, predicate);
        }
    }

    #endregion

    #region IEnumerable Rotate, Roll with predicate

    /// <summary>
    /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateUp<TCamera>(this IEnumerable<TCamera> cameras, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateUp(angle, predicate);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateDown<TCamera>(this IEnumerable<TCamera> cameras, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateDown(angle, predicate);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateLeft<TCamera>(this IEnumerable<TCamera> cameras, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateLeft(angle, predicate);
        }
    }
    /// <summary>
    /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rotated.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RotateRight<TCamera>(this IEnumerable<TCamera> cameras, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RotateRight(angle, predicate);
        }
    }
    /// <summary>
    /// Rolls each element of the <see cref="Camera"/> sequence in the left direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RollLeft<TCamera>(this IEnumerable<TCamera> cameras, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RollLeft(angle, predicate);
        }
    }
    /// <summary>
    /// Rolls each element of the <see cref="Camera"/> sequence in the right direction (relative to the camera) subject to a specified predicate.
    /// </summary>
    /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
    /// <param name="cameras">The type of each <see cref="Camera"/> being rolled.</param>
    /// <param name="angle">The angle to roll by.</param>
    /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
    /// <returns>This <see cref="Camera"/> sequence.</returns>
    public static IEnumerable<TCamera> RollRight<TCamera>(this IEnumerable<TCamera> cameras, float angle, Func<TCamera, bool> predicate) where TCamera : Camera
    {
        foreach (TCamera camera in cameras)
        {
            yield return camera.RollRight(angle, predicate);
        }
    }

    #endregion
}