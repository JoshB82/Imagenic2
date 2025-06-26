using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Provides extension methods for transforming transformable entities.
/// </summary>
public static class TransformableEntityTransformations
{
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
    public static TTransformableEntity Transform<TTransformableEntity>(
        [DisallowNull] this TTransformableEntity transformableEntity,
        Action<TTransformableEntity> transformation) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableEntity, transformation);
        transformation(transformableEntity);
        return transformableEntity;
    }

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
    public static TTransformableEntity Transform<TTransformableEntity>(
        [DisallowNull] this TTransformableEntity transformableEntity,
        [DisallowNull] Action<TTransformableEntity> transformation,
        [DisallowNull] Func<TTransformableEntity, bool> predicate) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableEntity, transformation, predicate);
        if (predicate(transformableEntity))
        {
            transformation(transformableEntity);
        }
        return transformableEntity;
    }

    public static TOutput? Transform<TTransformableEntity, TOutput>(
        [DisallowNull] this TTransformableEntity transformableEntity,
        [DisallowNull] Func<TTransformableEntity, TOutput?> transformation) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableEntity, transformation);
        return transformation(transformableEntity);
    }

    /// <summary>
    /// Transforms each element of a <typeparamref name="TTransformableEntity"/> sequence.
    /// <remarks>The <typeparamref name="TTransformableEntity"/> sequence and transformation cannot be <see langword="null"/>.</remarks>
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
    /// </summary>
    /// <typeparam name="TTransformableEntity">The type of the elements being transformed.</typeparam>
    /// <param name="transformableEntities">The <typeparamref name="TTransformableEntity"/> sequence containing elements being transformed.</param>
    /// <param name="transformation">The transformation to apply.</param>
    /// <returns>The <typeparamref name="TTransformableEntity"/> sequence with each element transformed.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TTransformableEntity> Transform<TTransformableEntity>(
        [DisallowNull] this IEnumerable<TTransformableEntity> transformableEntities,
        [DisallowNull] Action<TTransformableEntity> transformation) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableEntities, transformation);
        return transformableEntities.Select(transformableEntity =>
        {
            transformation(transformableEntity);
            return transformableEntity;
        });
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TTransformableEntity"/> sequence that satisfies a specified predicate.
    /// <remarks>The <typeparamref name="TTransformableEntity"/> sequence and predicate cannot be <see langword="null"/>.</remarks>
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
    /// </summary>
    /// <typeparam name="TTransformableEntity">The type of the elements being transformed.</typeparam>
    /// <param name="transformableEntities">The <typeparamref name="TTransformableEntity"/> sequence containing elements being transformed.</param>
    /// <param name="transformation">The transformation to apply.</param>
    /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TTransformableEntity"/> sequence are transformed.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TTransformableEntity> Transform<TTransformableEntity>(
        [DisallowNull] this IEnumerable<TTransformableEntity> transformableEntities,
        [DisallowNull] Action<TTransformableEntity> transformation,
        [DisallowNull] Func<TTransformableEntity, bool> predicate) where TTransformableEntity : TransformableEntity
    {
        ThrowIfNull(transformableEntities, transformation, predicate);
        return transformableEntities.Select(transformableEntity =>
        {
            if (predicate(transformableEntity))
            {
                transformation(transformableEntity);
            }
            return transformableEntity;
        });
    }
}