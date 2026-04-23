using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities;

public static partial class TransformableEntityTransformations
{
    // TCamera

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera"></typeparam>
    /// <param name="camera">The <see cref="Camera"/> being transformed.</param>
    extension<TCamera>([DisallowNull] TCamera camera) where TCamera : Camera
    {
        #region Pan

        /// <summary>
        /// Pans the camera in the forward direction (relative to the camera).
        /// </summary>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanForward(float distance)
        {
            return camera.Translate(camera.WorldOrientation.DirectionForward * distance);
            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Pans the camera in the left direction (relative to the camera).
        /// </summary>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanLeft(float distance)
        {
            return camera.Translate(camera.WorldOrientation.DirectionRight * -distance);
            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Pans the camera in the right direction (relative to the camera).
        /// </summary>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanRight(float distance)
        {
            return camera.Translate(camera.WorldOrientation.DirectionRight * distance);
            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Pans the camera in the backward direction (relative to the camera).
        /// </summary>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanBackward(float distance)
        {
            return camera.Translate(camera.WorldOrientation.DirectionForward * -distance);
            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Pans the camera in the up direction (relative to the camera).
        /// </summary>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanUp(float distance)
        {
            return camera.Translate(camera.WorldOrientation.DirectionUp * distance);
            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Pans the camera in the down direction (relative to the camera).
        /// </summary>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanDown(float distance)
        {
            return camera.Translate(camera.WorldOrientation.DirectionUp * -distance);
            //InvokeRenderEvent(RenderUpdate.NewRender);
        }

        #endregion

        #region Rotate, Roll

        /// <summary>
        /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera).
        /// </summary>
        /// <param name="angle">The angle to rotate by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateUp(float angle)
        {
            return camera.Rotate(camera.WorldOrientation.DirectionRight, angle);

            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera).
        /// </summary>
        /// <param name="angle">The angle to rotate by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateDown(float angle)
        {
            return camera.Rotate(camera.WorldOrientation.DirectionRight, -angle);

            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera).
        /// </summary>
        /// <param name="angle">The angle to rotate by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateLeft(float angle)
        {
            return camera.Rotate(camera.WorldOrientation.DirectionUp, angle);

            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera).
        /// </summary>
        /// <param name="angle">The angle to rotate by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateRight(float angle)
        {
            return camera.Rotate(camera.WorldOrientation.DirectionUp, -angle);

            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera).
        /// </summary>
        /// <param name="angle">The angle to roll by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RollLeft(float angle)
        {
            return camera.Rotate(camera.WorldOrientation.DirectionForward, -angle);

            //InvokeRenderEvent(RenderUpdate.NewRender);
        }
        /// <summary>
        /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera).
        /// </summary>
        /// <param name="angle">The angle to roll by.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RollRight(float angle)
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
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanForward(float distance, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.PanForward(distance) : camera;
        }
        /// <summary>
        /// Pans the camera in the left direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanLeft(float distance, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.PanLeft(distance) : camera;
        }
        /// <summary>
        /// Pans the camera in the right direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanRight(float distance, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.PanRight(distance) : camera;
        }
        /// <summary>
        /// Pans the camera in the backward direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanBackward(float distance, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.PanBackward(distance) : camera;
        }
        /// <summary>
        /// Pans the camera in the up direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanUp(float distance, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.PanUp(distance) : camera;
        }
        /// <summary>
        /// Pans the camera in the down direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera PanDown(float distance, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.PanDown(distance) : camera;
        }

        #endregion

        #region Rotate, Roll with predicate

        /// <summary>
        /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
        /// <param name="angle">The angle to rotate by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateUp(float angle, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.RotateUp(angle) : camera;
        }
        /// <summary>
        /// Rotates the <see cref="Camera"/> in the down direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
        /// <param name="angle">The angle to rotate by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateDown(float angle, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.RotateDown(angle) : camera;
        }
        /// <summary>
        /// Rotates the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
        /// <param name="angle">The angle to rotate by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateLeft(float angle, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.RotateLeft(angle) : camera;
        }
        /// <summary>
        /// Rotates the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
        /// <param name="angle">The angle to rotate by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RotateRight(float angle, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.RotateRight(angle) : camera;
        }
        /// <summary>
        /// Rolls the <see cref="Camera"/> in the left direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
        /// <param name="angle">The angle to roll by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RollLeft(float angle, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.RollLeft(angle) : camera;
        }
        /// <summary>
        /// Rolls the <see cref="Camera"/> in the right direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rolled.</typeparam>
        /// <param name="angle">The angle to roll by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/>.</returns>
        public TCamera RollRight(float angle, [DisallowNull] Func<TCamera, bool> predicate)
        {
            return predicate(camera) ? camera.RollRight(angle) : camera;
        }

        #endregion
    }

    // IEnumerable<TCamera>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera"></typeparam>
    /// <param name="cameras"></param>
    extension<TCamera>([DisallowNull] IEnumerable<TCamera> cameras) where TCamera : Camera
    {
        #region Pan

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
        /// <param name="distance">The distance to pan by.</param>
        /// <returns>This <see cref="Camera"/> sequence.</returns>
        public IEnumerable<TCamera> PanForward(float distance)
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
        public IEnumerable<TCamera> PanLeft(float distance)
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
        public IEnumerable<TCamera> PanRight(float distance)
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
        public IEnumerable<TCamera> PanBackward(float distance)
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
        public IEnumerable<TCamera> PanUp(float distance)
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
        public IEnumerable<TCamera> PanDown(float distance)
        {
            foreach (TCamera camera in cameras)
            {
                yield return camera.PanDown(distance);
            }
        }

        #endregion

        #region Rotate, Roll

        /// <summary>
        /// Rotates each element of the <see cref="Camera"/> sequence in the up direction (relative to the camera).
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
        /// <param name="cameras"></param>
        /// <param name="angle">The angle to rotate by.</param>
        /// <returns>This <see cref="Camera"/> sequence.</returns>
        public IEnumerable<TCamera> RotateUp(float angle)
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
        public IEnumerable<TCamera> RotateDown(float angle)
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
        public IEnumerable<TCamera> RotateLeft(float angle)
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
        public IEnumerable<TCamera> RotateRight(float angle)
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
        public IEnumerable<TCamera> RollLeft(float angle)
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
        public IEnumerable<TCamera> RollRight(float angle)
        {
            foreach (TCamera camera in cameras)
            {
                yield return camera.RollRight(angle);
            }
        }

        #endregion

        #region Pan with predicate

        /// <summary>
        /// Pans each element of the <see cref="Camera"/> sequence in the forward direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being panned.</typeparam>
        /// <param name="cameras">The type of each <see cref="Camera"/> being panned.</param>
        /// <param name="distance">The distance to pan by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/> sequence.</returns>
        public IEnumerable<TCamera> PanForward(float distance, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> PanLeft(float distance, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> PanRight(float distance, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> PanBackward(float distance, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> PanUp(float distance, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> PanDown(float distance, Func<TCamera, bool> predicate)
        {
            foreach (TCamera camera in cameras)
            {
                yield return camera.PanDown(distance, predicate);
            }
        }

        #endregion

        #region Rotate, Roll with predicate

        /// <summary>
        /// Rotates the <see cref="Camera"/> in the up direction (relative to the camera) subject to a specified predicate.
        /// </summary>
        /// <typeparam name="TCamera">The type of the <see cref="Camera"/> being rotated.</typeparam>
        /// <param name="cameras">The type of each <see cref="Camera"/> being rotated.</param>
        /// <param name="angle">The angle to rotate by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns>This <see cref="Camera"/> sequence.</returns>
        public IEnumerable<TCamera> RotateUp(float angle, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> RotateDown(float angle, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> RotateLeft(float angle, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> RotateRight(float angle, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> RollLeft(float angle, Func<TCamera, bool> predicate)
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
        public IEnumerable<TCamera> RollRight(float angle, Func<TCamera, bool> predicate)
        {
            foreach (TCamera camera in cameras)
            {
                yield return camera.RollRight(angle, predicate);
            }
        }

        #endregion
    }

    // IAsyncEnumerable<TCamera>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCamera"></typeparam>
    /// <param name="cameras"></param>
    extension<TCamera>([DisallowNull] IAsyncEnumerable<TCamera> cameras) where TCamera : Camera
    {
        #region Pan

        public async IAsyncEnumerable<TCamera> PanForward(
            float distance,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanForward(distance);
            }
        }

        public async IAsyncEnumerable<TCamera> PanLeft(
            float distance,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanLeft(distance);
            }
        }

        public async IAsyncEnumerable<TCamera> PanRight(
            float distance,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanRight(distance);
            }
        }

        public async IAsyncEnumerable<TCamera> PanBackward(
            float distance,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanBackward(distance);
            }
        }

        public async IAsyncEnumerable<TCamera> PanUp(
            float distance,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanUp(distance);
            }
        }

        public async IAsyncEnumerable<TCamera> PanDown(
            float distance,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanDown(distance);
            }
        }

        #endregion

        #region Rotate, Roll

        public async IAsyncEnumerable<TCamera> RotateUp(
            float angle,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach(TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateUp(angle);
            }
        }

        public async IAsyncEnumerable<TCamera> RotateDown(
            float angle,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateDown(angle);
            }
        }

        public async IAsyncEnumerable<TCamera> RotateLeft(
            float angle,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateLeft(angle);
            }
        }

        public async IAsyncEnumerable<TCamera> RotateRight(
            float angle,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateRight(angle);
            }
        }

        public async IAsyncEnumerable<TCamera> RollLeft(
            float angle,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RollLeft(angle);
            }
        }

        public async IAsyncEnumerable<TCamera> RollRight(
            float angle,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RollRight(angle);
            }
        }

        #endregion

        #region Pan with predicate

        public async IAsyncEnumerable<TCamera> PanForward(
            float distance,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanForward(distance, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> PanLeft(
            float distance,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanLeft(distance, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> PanRight(
            float distance,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanRight(distance, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> PanBackward(
            float distance,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanBackward(distance, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> PanUp(
            float distance,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanUp(distance, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> PanDown(
            float distance,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.PanDown(distance, predicate);
            }
        }

        #endregion

        #region Rotate, Roll with predicate

        public async IAsyncEnumerable<TCamera> RotateUp(
            float angle,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateUp(angle, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> RotateDown(
            float angle,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateDown(angle, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> RotateLeft(
            float angle,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateLeft(angle, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> RotateRight(
            float angle,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RotateRight(angle, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> RollLeft(
            float angle,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RollLeft(angle, predicate);
            }
        }

        public async IAsyncEnumerable<TCamera> RollRight(
            float angle,
            Func<TCamera, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(cameras);

            await foreach (TCamera camera in cameras)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return camera.RollRight(angle, predicate);
            }
        }

        #endregion
    }
}