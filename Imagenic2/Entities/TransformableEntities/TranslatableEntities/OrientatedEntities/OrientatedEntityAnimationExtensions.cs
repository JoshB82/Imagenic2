using Imagenic2.Core.Entities.Animation;
using Imagenic2.Core.Maths.Transformations;
using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities;

public static partial class TransformableEntityAnimationExtensions
{
    // TOrientatedEntity

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx"></param>
    extension<TOrientatedEntity>([DisallowNull] AnimationContext<TOrientatedEntity> orientatedTCtx) where TOrientatedEntity : OrientatedEntity
    {
        #region Orientate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="orientation"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> Orientate(Orientation orientation, float time)
        {
            ThrowIfNull(orientatedTCtx);

            if (orientatedTCtx.OrientationKeyFrameAnimation is null)
            {
                orientatedTCtx.OrientationKeyFrameAnimation = new KeyFrameAnimation<TOrientatedEntity, Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedTCtx.TransformableEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedTCtx.TransformableEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
                orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Quaternion newQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientation);
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

            return orientatedTCtx;
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="q"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> Rotate(Quaternion q, float time)
        {
            ThrowIfNull(orientatedTCtx);

            if (orientatedTCtx.OrientationKeyFrameAnimation is null)
            {
                orientatedTCtx.OrientationKeyFrameAnimation = new KeyFrameAnimation<TOrientatedEntity, Quaternion>(new List<KeyFrame<Quaternion>>(), v => orientatedTCtx.TransformableEntity.WorldOrientation.Rotate(v), MathsHelper.Lerp);
                Quaternion startingQuaternion = MathsHelper.QuaternionRotateBetweenOrientations(Orientation.ModelOrientation, orientatedTCtx.TransformableEntity.WorldOrientation);
                KeyFrame<Quaternion> startingKeyFrame = new KeyFrame<Quaternion>(orientatedTCtx.StartTime, startingQuaternion);
                orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(startingKeyFrame);
            }

            Quaternion latestQuaternion = orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames[^1].Value;
            Quaternion newQuaternion = q * latestQuaternion;
            KeyFrame<Quaternion> newKeyFrame = new KeyFrame<Quaternion>(time, newQuaternion);
            orientatedTCtx.OrientationKeyFrameAnimation.KeyFrames.Add(newKeyFrame);

            return orientatedTCtx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> Rotate(Vector3D axis, float angle, float time)
        {
            return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
        }

        #endregion

        #region Look At

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="translatableEntity"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> LookAt([DisallowNull] TranslatableEntity translatableEntity, float time)
        {
            Vector3D newDirectionForward = translatableEntity.WorldOrigin - orientatedTCtx.TransformableEntity.WorldOrigin;
            return orientatedTCtx.Rotate(MathsHelper.QuaternionRotateBetweenVectors(orientatedTCtx.TransformableEntity.WorldOrientation.DirectionForward, newDirectionForward), time);
        }

        #endregion

        #region Orientate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="orientation"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> Orientate(Orientation orientation, [DisallowNull] Func<TOrientatedEntity, bool> predicate, float time)
        {
            if (predicate(orientatedTCtx.TransformableEntity))
            {
                orientatedTCtx.Orientate(orientation, time);
            }
            return orientatedTCtx;
        }

        #endregion

        #region Rotate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="q"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> Rotate(Quaternion q, [DisallowNull] Func<TOrientatedEntity, bool> predicate, float time)
        {
            if (predicate(orientatedTCtx.TransformableEntity))
            {
                orientatedTCtx.Rotate(q, time);
            }
            return orientatedTCtx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> Rotate(Vector3D axis, float angle, [DisallowNull] Func<TOrientatedEntity, bool> predicate, float time)
        {
            if (predicate(orientatedTCtx.TransformableEntity))
            {
                orientatedTCtx.Rotate(axis, angle, time);
            }
            return orientatedTCtx;
        }

        #endregion

        #region Look At with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The context for this <see cref="Animation"/>.</param>
        /// <param name="translatableEntity"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TOrientatedEntity> LookAt(
            [DisallowNull] TranslatableEntity translatableEntity,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            float time)
        {
            if (predicate(orientatedTCtx.TransformableEntity))
            {
                orientatedTCtx.LookAt(translatableEntity, time);
            }
            return orientatedTCtx;
        }

        #endregion
    }

    // IEnumerable<TOrientatedEntity>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedTCtx"></param>
    extension<TOrientatedEntity>([DisallowNull] IEnumerable<AnimationContext<TOrientatedEntity>> orientatedTCtx) where TOrientatedEntity : OrientatedEntity
    {
        #region Orientate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="orientation"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> Orientate(Orientation orientation, float time)
        {
            ThrowIfNull(orientatedTCtx);

            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return tCtx.Orientate(orientation, time);
            }
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="q"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> Rotate(
            Quaternion q,
            float time)
        {
            ThrowIfNull(orientatedTCtx);

            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return tCtx.Rotate(q, time);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> Rotate(
            Vector3D axis,
            float angle,
            float time)
        {
            return orientatedTCtx.Rotate(MathsHelper.QuaternionRotate(axis, angle), time);
        }

        #endregion

        #region Look At

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="translatableEntity"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> LookAt(
            [DisallowNull] TranslatableEntity translatableEntity,
            float time)
        {
            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return tCtx.LookAt(translatableEntity, time);
            }
        }

        #endregion

        #region Orientate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="orientation"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> Orientate(
            Orientation orientation,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            float time)
        {
            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return predicate(tCtx.TransformableEntity) ? tCtx.Orientate(orientation, time) : tCtx;
            }
        }

        #endregion

        #region Rotate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="q"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> Rotate(
            Quaternion q, 
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            float time)
        {
            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return predicate(tCtx.TransformableEntity) ? tCtx.Rotate(q, time) : tCtx;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> Rotate(
            Vector3D axis,
            float angle,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            float time)
        {
            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return predicate(tCtx.TransformableEntity) ? tCtx.Rotate(axis, angle, time) : tCtx;
            }
        }

        #endregion

        #region Look At with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
        /// <param name="translatableEntity"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TOrientatedEntity>> LookAt(
            [DisallowNull] TranslatableEntity translatableEntity, 
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            float time)
        {
            foreach (AnimationContext<TOrientatedEntity> tCtx in orientatedTCtx)
            {
                yield return predicate(tCtx.TransformableEntity) ? tCtx.LookAt(translatableEntity, time) : tCtx;
            }
        }

        #endregion
    }
}