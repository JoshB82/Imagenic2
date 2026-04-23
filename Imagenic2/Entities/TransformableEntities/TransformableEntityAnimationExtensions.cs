using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities.Animation;

public static partial class TransformableEntityAnimationExtensions
{
    // AnimationContext<TTransformableEntity>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx">The context for this <see cref="Animation"/>.</param>
    extension<TTransformableEntity>([DisallowNull] AnimationContext<TTransformableEntity> transformableTCtx) where TTransformableEntity : TransformableEntity
    {
        #region Transform

        /// <summary>
        /// Applies a transformation.
        /// </summary>
        /// <param name="transformation">The transformation to apply.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTransformableEntity> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            float time)
        {
            ThrowIfNull(transformableTCtx, transformation);
            ThrowIfNotFinite(time);

            transformableTCtx.TransformationAnimation ??= new InstantaneousAnimation<TTransformableEntity>(new List<KeyFrame<Action<TTransformableEntity>>>());
            KeyFrame<Action<TTransformableEntity>> newKeyFrame = new KeyFrame<Action<TTransformableEntity>>(time, transformation);
            transformableTCtx.TransformationAnimation.KeyFrames.Add(newKeyFrame);

            return transformableTCtx;
        }

        #endregion

        #region Transform with predicate

        /// <summary>
        /// Applies a transformation subject to a predicate.
        /// </summary>
        /// <param name="transformation">The transformation to apply.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTransformableEntity> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            [DisallowNull] Func<TTransformableEntity, bool> predicate,
            float time)
        {
            ThrowIfNull(transformableTCtx, transformation, predicate);
            ThrowIfNotFinite(time);

            return transformableTCtx.Transform(t => { if (predicate(t)) transformation(t); }, time);
        }

        #endregion
    }

    // IEnumerable<AnimationContext<TTransformableEntity>>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    extension<TTransformableEntity>([DisallowNull] IEnumerable<AnimationContext<TTransformableEntity>> transformableTCtx) where TTransformableEntity : TransformableEntity
    {
        #region Transform

        /// <summary>
        /// Transforms each element of a <typeparamref name="TTransformableEntity"/> sequence.
        /// </summary>
        /// <param name="transformation">The transformation to apply.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTransformableEntity>> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            float time)
        {
            ThrowIfNull(transformableTCtx, transformation);
            ThrowIfNotFinite(time);

            foreach (AnimationContext<TTransformableEntity> tCtx in transformableTCtx)
            {
                yield return tCtx.Transform(transformation, time);
            }
        }

        #endregion

        #region Transform with predicate

        /// <summary>
        /// Transforms each element of a <typeparamref name="TTransformableEntity"/> sequence subject to a specified predicate.
        /// </summary>
        /// <param name="transformation">The transformation to apply.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTransformableEntity>> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            [DisallowNull] Func<TTransformableEntity, bool> predicate,
            float time)
        {
            ThrowIfNull(transformableTCtx, transformation, predicate);
            ThrowIfNotFinite(time);

            foreach (AnimationContext<TTransformableEntity> tCtx in transformableTCtx)
            {
                yield return tCtx.Transform(transformation, predicate, time);
            }
        }

        #endregion
    }
}