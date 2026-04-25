using Imagenic2.Core.Maths.Transformations;
using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities.Animation;

public static partial class TransformableEntityAnimationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the <see cref="TranslatableEntity"/> being transformed.</typeparam>
    /// <param name="translatableTCtx">The context for this <see cref="Animation"/>.</param>
    extension<TTranslatableEntity>([DisallowNull] AnimationContext<TTranslatableEntity> translatableTCtx) where TTranslatableEntity : TranslatableEntity
    {
        #region Translate

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the x-direction.
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance">The distance to translate by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> TranslateX(float distance, float time)
        {
            Vector3D displacement = new Vector3D(distance, 0, 0);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the y-direction.
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance">The distance to translate by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> TranslateY(float distance, float time)
        {
            Vector3D displacement = new Vector3D(0, distance, 0);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the z-direction.
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance">The distance to translate by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> TranslateZ(float distance, float time)
        {
            Vector3D displacement = new Vector3D(0, 0, distance);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the x, y and z-directions.
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distanceX">The distance to translate by in the x-direction.</param>
        /// <param name="distanceY">The distance to translate by in the y-direction.</param>
        /// <param name="distanceZ">The distance to translate by in the z-direction.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> Translate(float distanceX, float distanceY, float distanceZ, float time)
        {
            Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/>.
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="displacement">The displacement to translate by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> Translate(Vector3D displacement, float time)
        {
            ThrowIfNull(translatableTCtx);
            ThrowIfNotFinite(time);

            translatableTCtx.TranslationKeyFrameAnimation ??= new KeyFrameAnimation<TTranslatableEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableTCtx.TransformableEntity.WorldOrigin = v, MathsHelper.Lerp);

            Instruction<TTranslatableEntity, Vector3D> instruction = new(
                time: time,
                func: t => t.WorldOrigin + displacement,
                predicateFailValue: translatableTCtx.TransformableEntity.WorldOrigin,
                predicate: null);

            translatableTCtx.TranslationKeyFrameAnimation.Instructions.Add(instruction);

            return translatableTCtx;
        }

        #endregion

        #region Translate with predicate

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the x-direction subject to a specified predicate.
        /// </summary>
        /// <param name="distance">The distance to translate by.</param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> TranslateX(float distance, [DisallowNull] Func<TTranslatableEntity, bool> predicate, float time)
        {
            Vector3D displacement = new Vector3D(distance, 0, 0);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the y-direction subject to a specified predicate.
        /// </summary>
        /// <param name="distance">The distance to translate by.</param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> TranslateY(float distance, [DisallowNull] Func<TTranslatableEntity, bool> predicate, float time)
        {
            Vector3D displacement = new Vector3D(0, distance, 0);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the z-direction subject to a specified predicate.
        /// </summary>
        /// <param name="distance">The distance to translate by.</param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> TranslateZ(float distance, [DisallowNull] Func<TTranslatableEntity, bool> predicate, float time)
        {
            Vector3D displacement = new Vector3D(0, 0, distance);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> in the x, y and z-directions subject to a specified predicate.
        /// </summary>
        /// <param name="distanceX">The distance to translate by in the x-direction.</param>
        /// <param name="distanceY">The distance to translate by in the y-direction.</param>
        /// <param name="distanceZ">The distance to translate by in the z-direction.</param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> Translate(float distanceX, float distanceY, float distanceZ, [DisallowNull] Func<TTranslatableEntity, bool> predicate, float time)
        {
            Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// Translates a <typeparamref name="TTranslatableEntity"/> subject to a specified predicate.
        /// </summary>
        /// <param name="displacement">The displacement to translate by.</param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The context for this <see cref="Animation"/>.</returns>
        public AnimationContext<TTranslatableEntity> Translate(Vector3D displacement, [DisallowNull] Func<TTranslatableEntity, bool> predicate, float time)
        {
            ThrowIfNull(translatableTCtx, predicate);

            translatableTCtx.TranslationKeyFrameAnimation ??= new KeyFrameAnimation<TTranslatableEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => translatableTCtx.TransformableEntity.WorldOrigin = v, MathsHelper.Lerp);

            Instruction<TTranslatableEntity, Vector3D> instruction = new(
                time: time,
                func: t => t.WorldOrigin + displacement,
                predicateFailValue: translatableTCtx.TransformableEntity.WorldOrigin,
                predicate: predicate);
                

            translatableTCtx.TranslationKeyFrameAnimation.Instructions.Add(instruction);

            return translatableTCtx;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of each <see cref="TranslatableEntity"/> being transformed.</typeparam>
    /// <param name="translatableTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    extension<TTranslatableEntity>([DisallowNull] IEnumerable<AnimationContext<TTranslatableEntity>> translatableTCtx) where TTranslatableEntity : TranslatableEntity
    {
        #region Translate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> TranslateX(
            float distance,
            float time)
        {
            Vector3D displacement = new Vector3D(distance, 0, 0);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> TranslateY(
            float distance,
            float time)
        {
            Vector3D displacement = new Vector3D(0, distance, 0);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> TranslateZ(
            float distance,
            float time)
        {
            Vector3D displacement = new Vector3D(0, 0, distance);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distanceX"></param>
        /// <param name="distanceY"></param>
        /// <param name="distanceZ"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> Translate(
            float distanceX,
            float distanceY,
            float distanceZ,
            float time)
        {
            Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
            return translatableTCtx.Translate(displacement, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="displacement"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> Translate(
            Vector3D displacement,
            float time)
        {
            ThrowIfNull(translatableTCtx);

            foreach (AnimationContext<TTranslatableEntity> tCtx in translatableTCtx)
            {
                yield return tCtx.Translate(displacement, time);
            }
        }

        #endregion

        #region Translate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> TranslateX(
            float distance,
            [DisallowNull] Func<TTranslatableEntity, bool> predicate,
            float time)
        {
            Vector3D displacement = new Vector3D(distance, 0, 0);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> TranslateY(
            float distance,
            [DisallowNull] Func<TTranslatableEntity, bool> predicate,
            float time)
        {
            Vector3D displacement = new Vector3D(0, distance, 0);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distance"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> TranslateZ(
            float distance,
            [DisallowNull] Func<TTranslatableEntity, bool> predicate,
            float time)
        {
            Vector3D displacement = new Vector3D(0, 0, distance);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="distanceX"></param>
        /// <param name="distanceY"></param>
        /// <param name="distanceZ"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> Translate(
            float distanceX,
            float distanceY,
            float distanceZ,
            [DisallowNull] Func<TTranslatableEntity, bool> predicate,
            float time)
        {
            Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
            return translatableTCtx.Translate(displacement, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatableEntity"></typeparam>
        /// <param name="displacement"></param>
        /// <param name="predicate"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns>The sequence of contexts for this <see cref="Animation"/>.</returns>
        public IEnumerable<AnimationContext<TTranslatableEntity>> Translate(
            Vector3D displacement,
            [DisallowNull] Func<TTranslatableEntity, bool> predicate,
            float time)
        {
            foreach (AnimationContext<TTranslatableEntity> tCtx in translatableTCtx)
            {
                yield return predicate(tCtx.TransformableEntity) ? tCtx.Translate(displacement, predicate, time) : tCtx;
            }
        }

        #endregion
    }
}