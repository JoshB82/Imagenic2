using Imagenic2.Core.Maths.Transformations;
using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities.Animation;

public static partial class TransformableEntityAnimationExtensions
{
    // AnimationContext<TPhysicalEntity>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx">The context for this <see cref="Animation"/>.</param>
    extension<TPhysicalEntity>([DisallowNull] AnimationContext<TPhysicalEntity> physicalTCtx) where TPhysicalEntity : PhysicalEntity
    {
        #region Scale

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> ScaleX(float scaleFactor, float time)
        {
            Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> ScaleY(float scaleFactor, float time)
        {
            Vector3D scaling = new Vector3D(1, scaleFactor, 1);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> ScaleZ(float scaleFactor, float time)
        {
            Vector3D scaling = new Vector3D(1, 1, scaleFactor);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactorX"></param>
        /// <param name="scaleFactorY"></param>
        /// <param name="scaleFactorZ"></param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time)
        {
            Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> Scale(Vector3D scaleFactor, float time)
        {
            ThrowIfNull(physicalTCtx);
            ThrowIfNotFinite(time);

            physicalTCtx.ScalingKeyFrameAnimation ??= new KeyFrameAnimation<TPhysicalEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, MathsHelper.Lerp);

            Instruction<TPhysicalEntity, Vector3D> instruction = new(
                time: time,
                func: t => new Vector3D(t.Scaling.x * scaleFactor.x, t.Scaling.y * scaleFactor.y, t.Scaling.z * scaleFactor.z),
                predicateFailValue: physicalTCtx.TransformableEntity.Scaling,
                predicate: null
            );

            physicalTCtx.ScalingKeyFrameAnimation.Instructions.Add(instruction);

            return physicalTCtx;
        }

        #endregion

        #region Scale with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> ScaleX(float scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> ScaleY(float scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(1, scaleFactor, 1);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> ScaleZ(float scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(1, 1, scaleFactor);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactorX">The factor to scale by in the x-direction.</param>
        /// <param name="scaleFactorY">The factor to scale by in the y-direction.</param>
        /// <param name="scaleFactorZ">The factor to scale by in the z-direction.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public AnimationContext<TPhysicalEntity> Scale(Vector3D scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            ThrowIfNull(physicalTCtx, predicate);
            ThrowIfNotFinite(time);

            physicalTCtx.ScalingKeyFrameAnimation ??= new KeyFrameAnimation<TPhysicalEntity, Vector3D>(new List<KeyFrame<Vector3D>>(), v => physicalTCtx.TransformableEntity.Scaling = v, MathsHelper.Lerp);

            Instruction<TPhysicalEntity, Vector3D> instruction = new(
                time: time,
                func: t => new Vector3D(t.Scaling.x * scaleFactor.x, t.Scaling.y * scaleFactor.y, t.Scaling.z * scaleFactor.z),
                predicateFailValue: physicalTCtx.TransformableEntity.Scaling,
                predicate: predicate
            );

            physicalTCtx.ScalingKeyFrameAnimation.Instructions.Add(instruction);

            return physicalTCtx;
        }

        #endregion
    }

    // IEnumerable<AnimationContext<TPhysicalEntity>>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPhysicalEntity"></typeparam>
    /// <param name="physicalTCtx">The sequence of contexts for this <see cref="Animation"/>.</param>
    extension<TPhysicalEntity>([DisallowNull] IEnumerable<AnimationContext<TPhysicalEntity>> physicalTCtx) where TPhysicalEntity : PhysicalEntity
    {
        #region Scale

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> ScaleX(float scaleFactor, float time)
        {
            Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> ScaleY(float scaleFactor, float time)
        {
            Vector3D scaling = new Vector3D(1, scaleFactor, 1);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> ScaleZ(float scaleFactor, float time)
        {
            Vector3D scaling = new Vector3D(1, 1, scaleFactor);
            return physicalTCtx.Scale(scaling, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactorX">The factor to scale by in the x-direction.</param>
        /// <param name="scaleFactorY">The factor to scale by in the y-direction.</param>
        /// <param name="scaleFactorZ">The factor to scale by in the z-direction.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ, float time)
        {
            Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
            return physicalTCtx.Scale(scaleFactor, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> Scale(Vector3D scaleFactor, float time)
        {
            ThrowIfNull(physicalTCtx);

            foreach (AnimationContext<TPhysicalEntity> tCtx in physicalTCtx)
            {
                yield return tCtx.Scale(scaleFactor, time);
            }
        }

        #endregion

        #region Scale with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> ScaleX(float scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(scaleFactor, 1, 1);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> ScaleY(float scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(1, scaleFactor, 1);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> ScaleZ(float scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaling = new Vector3D(1, 1, scaleFactor);
            return physicalTCtx.Scale(scaling, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactorX">The factor to scale by in the x-direction.</param>
        /// <param name="scaleFactorY">The factor to scale by in the y-direction.</param>
        /// <param name="scaleFactorZ">The factor to scale by in the z-direction.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> Scale(float scaleFactorX, float scaleFactorY, float scaleFactorZ, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            Vector3D scaleFactor = new Vector3D(scaleFactorX, scaleFactorY, scaleFactorZ);
            return physicalTCtx.Scale(scaleFactor, predicate, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleFactor">The factor to scale by.</param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="time">The time which this transformation should be completed by.</param>
        /// <returns></returns>
        public IEnumerable<AnimationContext<TPhysicalEntity>> Scale(Vector3D scaleFactor, [DisallowNull] Func<TPhysicalEntity, bool> predicate, float time)
        {
            ThrowIfNull(physicalTCtx);

            foreach (AnimationContext<TPhysicalEntity> tCtx in physicalTCtx)
            {
                yield return tCtx.Scale(scaleFactor, predicate, time);
            }
        }

        #endregion
    }
}