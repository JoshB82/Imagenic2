using System.Diagnostics.CodeAnalysis;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Provides extension methods for transforming physical entities.
/// </summary>
public static class PhysicalEntityTransformations
{
    #region ScaleX Method

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/> in the x-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var displacement = new Vector3D(10, 20, 30);
    /// var orientation = Orientation.OrientationYZ;
    /// var scaleFactor = 2;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.ScaleX(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity ScaleX<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity, float scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor, e.Scaling.y, e.Scaling.z); });
    }

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/> in the x-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling for the cube if it has side length 50.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.ScaleX(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate that determines if the <typeparamref name="TPhysicalEntity"/> is scaled.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity ScaleX<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity,
        float scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity, predicate);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor, e.Scaling.y, e.Scaling.z); }, predicate);
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence in the x-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = 2;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.ScaleX(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities, float scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor, e.Scaling.y, e.Scaling.z); });
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence that satisfies a specified predicate in the x-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling for cubes with side length 50.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = 2;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.ScaleX(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TPhysicalEntity"/> sequence are scaled.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> ScaleX<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities,
        float scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities, predicate);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor, e.Scaling.y, e.Scaling.z); }, predicate);
    }

    #endregion

    #region ScaleY Method

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/> in the y-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var displacement = new Vector3D(10, 20, 30);
    /// var orientation = Orientation.OrientationYZ;
    /// var scaleFactor = 2;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.ScaleY(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity ScaleY<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity, float scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y * scaleFactor, e.Scaling.z); });
    }

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/> in the y-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling for the cube if it has side length 50.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.ScaleY(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate that determines if the <typeparamref name="TPhysicalEntity"/> is scaled.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity ScaleY<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity,
        float scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity, predicate);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y * scaleFactor, e.Scaling.z); }, predicate);
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence in the y-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = 2;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.ScaleY(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities, float scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y * scaleFactor, e.Scaling.z); });
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence that satisfies a specified predicate in the y-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling for cubes with side length 50.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = 2;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.ScaleY(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TPhysicalEntity"/> sequence are scaled.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> ScaleY<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities,
        float scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities, predicate);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y * scaleFactor, e.Scaling.z); }, predicate);
    }

    #endregion

    #region ScaleZ Method

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/> in the z-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var displacement = new Vector3D(10, 20, 30);
    /// var orientation = Orientation.OrientationYZ;
    /// var scaleFactor = 2;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.ScaleZ(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity ScaleZ<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity, float scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y, e.Scaling.z * scaleFactor); });
    }

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/> in the z-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling for the cube if it has side length 50.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.ScaleZ(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate that determines if the <typeparamref name="TPhysicalEntity"/> is scaled.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity ScaleZ<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity,
        float scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity, predicate);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y, e.Scaling.z * scaleFactor); }, predicate);
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence in the z-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = 2;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.ScaleZ(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities, float scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y, e.Scaling.z * scaleFactor); });
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence that satisfies a specified predicate in the z-direction.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling for cubes with side length 50.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = 2;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.ScaleZ(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TPhysicalEntity"/> sequence are scaled.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> ScaleZ<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities,
        float scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities, predicate);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x, e.Scaling.y, e.Scaling.z * scaleFactor); }, predicate);
    }

    #endregion

    #region Scale Method

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/>.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var displacement = new Vector3D(10, 20, 30);
    /// var orientation = Orientation.OrientationYZ;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.Scale(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity Scale<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity, Vector3D scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor.x, e.Scaling.y * scaleFactor.y, e.Scaling.z * scaleFactor.z); });
    }

    /// <summary>
    /// Scales a <typeparamref name="TPhysicalEntity"/>.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a scaling for the cube if it has side length 50.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cube = cube.Translate(displacement)
    ///            .Orientate(orientation)
    ///            <strong>.Scale(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the object being scaled.</typeparam>
    /// <param name="physicalEntity">The <typeparamref name="TPhysicalEntity"/> to be scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate that determines if the <typeparamref name="TPhysicalEntity"/> is scaled.</param>
    /// <returns>The scaled <typeparamref name="TPhysicalEntity"/>.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static TPhysicalEntity Scale<TPhysicalEntity>(
        [DisallowNull] this TPhysicalEntity physicalEntity,
        Vector3D scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntity, predicate);
        return physicalEntity.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor.x, e.Scaling.y * scaleFactor.y, e.Scaling.z * scaleFactor.z); }, predicate);
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.Scale(scaleFactor);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities, Vector3D scaleFactor) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor.x, e.Scaling.y * scaleFactor.y, e.Scaling.z * scaleFactor.z); });
    }

    /// <summary>
    /// Scales each element of a <typeparamref name="TPhysicalEntity"/> sequence that satisfies a specified predicate.
    /// <remarks>The <typeparamref name="TPhysicalEntity"/> sequence and predicate cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a scaling for cubes with side length 50.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var displacement = new Vector3D(50, 60, 70);
    /// var orientation = Orientation.OrientationXY;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Translate(displacement)
    ///              .Orientate(orientation)
    ///              <strong>.Scale(scaleFactor, p => p.SideLength == 50);</strong>
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TPhysicalEntity">The type of the elements being scaled.</typeparam>
    /// <param name="physicalEntities">The <typeparamref name="TPhysicalEntity"/> sequence containing elements being scaled.</param>
    /// <param name="scaleFactor">The factor to scale by.</param>
    /// <param name="predicate">The predicate which determines which elements of the <typeparamref name="TPhysicalEntity"/> sequence are scaled.</param>
    /// <returns>The <typeparamref name="TPhysicalEntity"/> sequence with each element scaled.</returns>
    /// <exception cref="ArgumentNullException">None of this method's parameters can be null.</exception>
    public static IEnumerable<TPhysicalEntity> Scale<TPhysicalEntity>(
        [DisallowNull] this IEnumerable<TPhysicalEntity> physicalEntities,
        Vector3D scaleFactor,
        [DisallowNull] Func<TPhysicalEntity, bool> predicate) where TPhysicalEntity : PhysicalEntity
    {
        ThrowIfNull(physicalEntities, predicate);
        return physicalEntities.Transform(e => { e.Scaling = new Vector3D(e.Scaling.x * scaleFactor.x, e.Scaling.y * scaleFactor.y, e.Scaling.z * scaleFactor.z); }, predicate);
    }

    #endregion
}