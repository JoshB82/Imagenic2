using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Provides extension methods for transforming transformable entities.
/// </summary>
public static partial class TransformableEntityTransformations
{
    // TTransformableEntity

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTransformableEntity"></typeparam>
    /// <param name="transformableEntity"></param>
    extension<TTransformableEntity>([DisallowNull] TTransformableEntity transformableEntity) where TTransformableEntity : TransformableEntity
    {
        #region Transform

        /// <summary>
        /// Applies a custom transformation.
        /// <remarks>The <typeparamref name="TTransformableEntity"/> and transformation cannot be <see langword="null"/>.</remarks>
        /// <para><example>
        /// Using call chaining, a cube is subjected to multiple transformations, including a custom transformation.
        /// <code>
        /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
        /// 
        /// var displacement = new Vector3D(5, 10, 15);
        /// var scaleFactor = Vector3D.One * 4.5f;
        /// 
        /// cube = cube.Translate(displacement)
        ///            <strong>.Transform(e => { e.SideLength = 10; })</strong><br/>
        ///            .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// </summary>
        /// <typeparam name="TTransformableEntity">The type of the object being transformed.</typeparam>
        /// <param name="transformableEntity">The <typeparamref name="TTransformableEntity"/> being transformed.</param>
        /// <param name="transformation">The transformation to apply.</param>
        /// <returns>The transformed <typeparamref name="TTransformableEntity"/>.</returns>
        /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
        public TTransformableEntity Transform(
            [DisallowNull] Action<TTransformableEntity> transformation)
        {
            ThrowIfNull(transformableEntity, transformation);

            transformation(transformableEntity);
            return transformableEntity;
        }

        #endregion

        #region Transform with predicate

        /// <summary>
        /// Applies a custom transformation to a <typeparamref name="TTransformableEntity"/> that satisfies a specified predicate.
        /// <remarks>The <typeparamref name="TTransformableEntity"/>, transformation and predicate cannot be <see langword="null"/>.</remarks>
        /// <para><example>
        /// Using call chaining, a cube is subjected to multiple transformations, including a custom transformation if it has side length 50.
        /// <code>
        /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
        /// 
        /// var displacement = new Vector3D(5, 10, 15);
        /// var scaleFactor = Vector3D.One * 4.5f;
        /// 
        /// cube = cube.Translate(displacement)
        ///            <strong>.Transform(e => { e.SideLength = 10; }, t => t.SideLength == 50)</strong><br/>
        ///            .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// </summary>
        /// <typeparam name="TTransformableEntity">The type of the object being transformed.</typeparam>
        /// <param name="transformableEntity">The <typeparamref name="TTransformableEntity"/> being transformed.</param>
        /// <param name="transformation">The transformation to apply.</param>
        /// <param name="predicate">The predicate that determines if the <typeparamref name="TTransformableEntity"/> is transformed.</param>
        /// <returns>The transformed <typeparamref name="TTransformableEntity"/>.</returns>
        /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
        public TTransformableEntity Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            [DisallowNull] Func<TTransformableEntity, bool> predicate)
        {
            ThrowIfNull(transformableEntity, transformation, predicate);

            return predicate(transformableEntity) ? transformableEntity.Transform(transformation) : transformableEntity;
        }

        #endregion
    }

    // IEnumerable<TTransformableEntity>

    extension<TTransformableEntity>([DisallowNull] IEnumerable<TTransformableEntity> transformableEntities) where TTransformableEntity : TransformableEntity
    {
        #region IEnumerable Transform

        /// <summary>
        /// Transforms each element of a <typeparamref name="TTransformableEntity"/> sequence.
        /// <remarks>The <typeparamref name="TTransformableEntity"/> sequence and transformation cannot be <see langword="null"/>.</remarks>
        /// </summary>
        /// <para><example>
        /// Using call chaining, a sequence of cubes are subjected to multiple transformations, including a custom transformation.
        /// <code>
        /// var cubes = new Cube[] { cube1, cube2, cube3 };
        /// 
        /// var displacement = new Vector3D(5, 10, 15);
        /// var scaleFactor = Vector3D.One * 4.5f;
        /// 
        /// cubes = cubes.Translate(displacement)
        ///             <strong>.Transform(e => { e.SideLength = 10; })</strong><br/>
        ///             .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// <typeparam name="TTransformableEntity">The type of the elements being transformed.</typeparam>
        /// <param name="transformableEntities">The <typeparamref name="TTransformableEntity"/> sequence containing elements being transformed.</param>
        /// <param name="transformation">The transformation to apply.</param>
        /// <returns>The <typeparamref name="TTransformableEntity"/> sequence with each element transformed.</returns>
        /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
        public IEnumerable<TTransformableEntity> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation)
        {
            ThrowIfNull(transformableEntities, transformation);

            foreach (TTransformableEntity transformableEntity in transformableEntities)
            {
                yield return transformableEntity.Transform(transformation);
            }
        }

        #endregion

        #region IEnumerable Transform with predicate

        /// <summary>
        /// Transforms each element of a <typeparamref name="TTransformableEntity"/> sequence that satisfies a specified predicate.
        /// <remarks>The <typeparamref name="TTransformableEntity"/> sequence and predicate cannot be <see langword="null"/>.</remarks>
        /// </summary>
        /// <para><example>
        /// Using call chaining, a sequence of cubes are subjected to multiple transformations, including a custom transformation for cubes with side length 50.
        /// <code>
        /// var cubes = new Cube[] { cube1, cube2, cube3 };
        /// 
        /// var displacement = new Vector3D(5, 10, 15);
        /// var scaleFactor = Vector3D.One * 4.5f;
        /// 
        /// cubes = cubes.Translate(displacement)
        ///             <strong>.Transform(e => { e.SideLength = 10; }, t => t.SideLength == 50)</strong><br/>
        ///             .Scale(scaleFactor);
        /// </code>
        /// </example></para>
        /// <typeparam name="TTransformableEntity">The type of the elements being transformed.</typeparam>
        /// <param name="transformableEntities">The <typeparamref name="TTransformableEntity"/> sequence containing elements being transformed.</param>
        /// <param name="transformation">The transformation to apply.</param>
        /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TTransformableEntity"/> sequence are transformed.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
        public IEnumerable<TTransformableEntity> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            [DisallowNull] Func<TTransformableEntity, bool> predicate)
        {
            ThrowIfNull(transformableEntities, transformation, predicate);

            foreach (TTransformableEntity transformableEntity in transformableEntities)
            {
                yield return transformableEntity.Transform(transformation, predicate);
            }
        }

        #endregion
    }

    // IAsyncEnumerable<TTransformableEntity>

    extension<TTransformableEntity>(IAsyncEnumerable<TTransformableEntity> transformableEntities) where TTransformableEntity : TransformableEntity
    {
        #region IAsyncEnumerable Transform

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transformation"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TTransformableEntity> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(transformableEntities, transformation);

            await foreach (TTransformableEntity transformableEntity in transformableEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return transformableEntity.Transform(transformation);
            }
        }

        #endregion

        #region IAsyncEnumerable Transform with predicate

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transformation"></param>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TTransformableEntity> Transform(
            [DisallowNull] Action<TTransformableEntity> transformation,
            [DisallowNull] Func<TTransformableEntity, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfNull(transformableEntities, transformation, predicate);

            await foreach (TTransformableEntity transformableEntity in transformableEntities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return transformableEntity.Transform(transformation, predicate);
            }
        }

        #endregion
    }
}