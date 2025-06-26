using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities;

public static class OrientatedEntityTransformations
{
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
    public static TOrientatedEntity Orientate<TOrientatedEntity>([DisallowNull] this TOrientatedEntity orientatedEntity,
        [DisallowNull] Orientation orientation) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntity, orientation);
        orientatedEntity.WorldOrientation = orientation;
        return orientatedEntity;
    }

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
    public static IEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>([DisallowNull] this IEnumerable<TOrientatedEntity> orientatedEntities,
        [DisallowNull] Orientation orientation) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities, orientation);
        return orientatedEntities.Select(orientatedEntity =>
        {
            orientatedEntity.WorldOrientation = orientation;
            return orientatedEntity;
        });
    }

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
    public static IEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>([DisallowNull] this IEnumerable<TOrientatedEntity> orientatedEntities,
        [DisallowNull] Orientation orientation, [DisallowNull] Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities, orientation, predicate);
        return orientatedEntities.Select(orientatedEntity =>
        {
            if (predicate(orientatedEntity))
            {
                orientatedEntity.WorldOrientation = orientation;
            }
            return orientatedEntity;
        });
    }
}