using Imagenic2.Core.Maths.Transformations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities;

public static partial class TransformableEntityTransformations
{
    // TOrientatedEntity

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity">The type of the <see cref="OrientatedEntity"/> being transformed.</typeparam>
    /// <param name="orientatedEntity"></param>
    extension<TOrientatedEntity>([DisallowNull] TOrientatedEntity orientatedEntity) where TOrientatedEntity : OrientatedEntity
    {
        #region Orientate

        /// <summary>
        /// Orientates a <typeparamref name="TOrientatedEntity"/> to the specified <see cref="Orientation"/>.
        /// <remarks>The specified orientation and the <typeparamref name="TOrientatedEntity"/> cannot be <see langword="null"/>.</remarks>
        /// <para><example>
        /// Example: Using call chaining, a cube is subjected to multiple transformations, including an orientation change.
        /// <code>
        /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
        /// 
        /// var displacement = new Vector3D(10, 20, 30);
        /// var orientation = Orientation.OrientationYZ;
        /// var scaleFactor = new Vector3D(2, 2, 2);
        /// 
        /// cube = cube.Translate(displacement)
        ///            <strong>.Orientate(orientation)</strong><br/>
        ///            .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// </summary>
        /// <typeparam name="TOrientatedEntity">The type of the object being orientated.</typeparam>
        /// <param name="orientatedEntity">The <typeparamref name="TOrientatedEntity"/> being orientated.</param>
        /// <param name="orientation">The new <see cref="Orientation"/> of the <typeparamref name="TOrientatedEntity"/>.</param>
        /// <returns>The <typeparamref name="TOrientatedEntity"/> with the new <see cref="Orientation"/>.</returns>
        /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
        [return: NotNull]
        public TOrientatedEntity Orientate(Orientation orientation)
        {
            ThrowIfNull(orientatedEntity);

            orientatedEntity.WorldOrientation = orientation;
            return orientatedEntity;
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public TOrientatedEntity Rotate(Quaternion q)
        {
            ThrowIfNull(orientatedEntity);

            Orientation newOrientation = orientatedEntity.WorldOrientation;
            newOrientation.Rotate(q);
            orientatedEntity.WorldOrientation = newOrientation;
            return orientatedEntity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public TOrientatedEntity Rotate(Vector3D axis, float angle)
        {
            ThrowIfNull(orientatedEntity);

            return orientatedEntity.Rotate(MathsHelper.QuaternionRotate(axis, angle));
        }

        #endregion

        #region Look At

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="translatableEntity"></param>
        /// <returns></returns>
        public TOrientatedEntity LookAt([DisallowNull] TranslatableEntity translatableEntity)
        {
            Vector3D newDirectionForward = translatableEntity.WorldOrigin - orientatedEntity.WorldOrigin;
            return orientatedEntity.Rotate(MathsHelper.QuaternionRotateBetweenVectors(orientatedEntity.WorldOrientation.DirectionForward, newDirectionForward));
        }

        #endregion

        #region Orientate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="orientation"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns></returns>
        public TOrientatedEntity Orientate(
            Orientation orientation,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            return predicate(orientatedEntity) ? orientatedEntity.Orientate(orientation) : orientatedEntity;
        }

        #endregion

        #region Rotate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="q"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns></returns>
        public TOrientatedEntity Rotate(
            Quaternion q,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            return predicate(orientatedEntity) ? orientatedEntity.Rotate(q) : orientatedEntity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns></returns>
        public TOrientatedEntity Rotate(
            Vector3D axis,
            float angle,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            return predicate(orientatedEntity) ? orientatedEntity.Rotate(axis, angle) : orientatedEntity;
        }

        #endregion

        #region Look At with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntity"></param>
        /// <param name="translatableEntity"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns></returns>
        public TOrientatedEntity LookAt(
            [DisallowNull] TranslatableEntity translatableEntity,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            return predicate(orientatedEntity) ? orientatedEntity.LookAt(translatableEntity) : orientatedEntity;
        }

        #endregion
    }

    // IEnumerable<TOrientatedEntity>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity">The type of each <see cref="OrientatedEntity"/> being transformed.</typeparam>
    /// <param name="orientatedEntities"></param>
    extension<TOrientatedEntity>([DisallowNull] IEnumerable<TOrientatedEntity> orientatedEntities) where TOrientatedEntity : OrientatedEntity
    {
        #region Orientate

        /// <summary>
        /// Orientates each element of a <typeparamref name="TOrientatedEntity"/> sequence to the specified <see cref="Orientation"/>.
        /// <remarks>The specified orientation and the <typeparamref name="TOrientatedEntity"/> cannot be <see langword="null"/>.</remarks>
        /// <para><example>
        /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including an orientation change.
        /// <code>
        /// var cubes = new Cube[] { cube1, cube2, cube3 };
        /// var displacement = new Vector3D(50, 60, 70);
        /// var orientation = Orientation.OrientationXY;
        /// var scaleFactor = Vector3D.NegativeOne;
        /// 
        /// cubes = cubes.Translate(displacement)
        ///              <strong>.Orientate(orientation)</strong><br/>
        ///              .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// </summary>
        /// <typeparam name="TOrientatedEntity">The type of the elements being orientated.</typeparam>
        /// <param name="orientatedEntities">The <typeparamref name="TOrientatedEntity"/> sequence containing elements being orientated.</param>
        /// <param name="orientation">The new <see cref="Orientation"/> of each element of the <typeparamref name="TOrientatedEntity"/> sequence.</param>
        /// <returns>The <typeparamref name="TOrientatedEntity"/> sequence with each element having the new <see cref="Orientation"/>.</returns>
        [return: NotNull]
        public IEnumerable<TOrientatedEntity> Orientate(Orientation orientation)
        {
            ThrowIfNull(orientatedEntities);

            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Orientate(orientation);
            }
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public IEnumerable<TOrientatedEntity> Rotate(Quaternion q)
        {
            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Rotate(q);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public IEnumerable<TOrientatedEntity> Rotate(Vector3D axis, float angle)
        {
            ThrowIfNull(orientatedEntities);

            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Rotate(axis, angle);
            }
        }

        #endregion

        #region Look At

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="translatableEntity"></param>
        /// <returns></returns>
        public IEnumerable<TOrientatedEntity> LookAt([DisallowNull] TranslatableEntity translatableEntity)
        {
            ThrowIfNull(orientatedEntities);

            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.LookAt(translatableEntity);
            }
        }

        #endregion

        #region Orientate with predicate

        /// <summary>
        /// Orientates each element of a <typeparamref name="TOrientatedEntity"/> sequence that satisfies a specified predicate to the specified <see cref="Orientation"/>.
        /// <para><example>
        /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including an orientation change for cubes with side length 50.
        /// <code>
        /// var cubes = new Cube[] { cube1, cube2, cube3 };
        /// var displacement = new Vector3D(50, 60, 70);
        /// var orientation = Orientation.OrientationXY;
        /// var scaleFactor = Vector3D.NegativeOne;
        /// 
        /// cubes = cubes.Translate(displacement)
        ///              <strong>.Orientate(orientation, o => o.SideLength == 50)</strong><br/>
        ///              .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// </summary>
        /// <typeparam name="TOrientatedEntity">The type of the elements being orientated.</typeparam>
        /// <param name="orientatedEntities">The <typeparamref name="TOrientatedEntity"/> sequence containing elements being orientated.</param>
        /// <param name="orientation">The new <see cref="Orientation"/> of each element of the <typeparamref name="TOrientatedEntity"/> sequence.</param>
        /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TOrientatedEntity"/> sequence are orientated.</param>
        /// <returns>The <typeparamref name="TOrientatedEntity"/> sequence with each element that satisfied the predicate having the new <see cref="Orientation"/>.</returns>
        [return: NotNull]
        public IEnumerable<TOrientatedEntity> Orientate(
            Orientation orientation,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            ThrowIfNull(orientatedEntities, predicate);

            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Orientate(orientation, predicate);
            }
        }

        #endregion

        #region Rotate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public IEnumerable<TOrientatedEntity> Rotate(
            Quaternion q,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Rotate(q, predicate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns></returns>
        public IEnumerable<TOrientatedEntity> Rotate(
            Vector3D axis,
            float angle,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            ThrowIfNull(orientatedEntities, predicate);

            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Rotate(axis, angle, predicate);
            }
        }

        #endregion

        #region Look At with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="translatableEntity"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <returns></returns>
        public IEnumerable<TOrientatedEntity> LookAt(
            [DisallowNull] TranslatableEntity translatableEntity,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate)
        {
            ThrowIfNull(orientatedEntities, predicate);

            foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.LookAt(translatableEntity, predicate);
            }
        }

        #endregion
    }

    // IAsyncEnumerable<TOrientatedEntity>

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity">The type of each <see cref="OrientatedEntity"/> being transformed.</typeparam>
    /// <param name="orientatedEntities"></param>
    extension<TOrientatedEntity>([DisallowNull] IAsyncEnumerable<TOrientatedEntity> orientatedEntities) where TOrientatedEntity : OrientatedEntity
    {
        #region Orientate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> Orientate(Orientation orientation)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Orientate(orientation);
            }
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> Rotate(Quaternion q)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Rotate(q);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> Rotate(
            Vector3D axis, 
            float angle)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                yield return orientatedEntity.Rotate(axis, angle);
            }
        }

        #endregion

        #region Look At

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="translatableEntity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> LookAt(
            [DisallowNull] TranslatableEntity translatableEntity,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return orientatedEntity.LookAt(translatableEntity);
            }
        }

        #endregion

        #region Orientate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="orientation"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> Orientate(
            Orientation orientation,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return orientatedEntity.Orientate(orientation, predicate);
            }
        }

        #endregion

        #region Rotate with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="q"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> Rotate(
            Quaternion q, 
            [DisallowNull] Func<TOrientatedEntity, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return orientatedEntity.Rotate(q, predicate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> Rotate(
            Vector3D axis,
            float angle, 
            [DisallowNull] Func<TOrientatedEntity, bool> predicate, 
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return orientatedEntity.Rotate(axis, angle, predicate);
            }
        }

        #endregion

        #region Look At with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrientatedEntity"></typeparam>
        /// <param name="orientatedEntities"></param>
        /// <param name="translatableEntity"></param>
        /// <param name="predicate">The predicate that needs to be satisfied in order for this transformation to occur.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOrientatedEntity> LookAt(
            [DisallowNull] TranslatableEntity translatableEntity,
            [DisallowNull] Func<TOrientatedEntity, bool> predicate, 
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(orientatedEntities);

            await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return orientatedEntity.LookAt(translatableEntity, predicate);
            }
        }

        #endregion
    }
}