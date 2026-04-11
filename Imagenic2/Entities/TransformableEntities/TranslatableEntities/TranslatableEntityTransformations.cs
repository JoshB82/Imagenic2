using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities;

public static class TranslatableEntityTransformations
{
    #region TTranslatableEntity

    /// <summary>
    /// Translates a <typeparamref name="TTranslatableEntity"/> by the specified distance in the x-direction.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a translation.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var orientation = Orientation.OrientationYZ;
    /// var distanceX = 10;
    /// var scaleFactor = new Vector3D(2, 2, 2);
    /// 
    /// cube = cube.Orientate(orientation)
    ///            <strong>.TranslateX(distanceX)</strong><br/>
    ///            .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity">The <typeparamref name="TTranslatableEntity"/> to be translated.</param>
    /// <param name="distanceX">The distance in the x-direction the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// <returns>The translated <typeparamref name="TTranslatableEntity"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TTranslatableEntity TranslateX<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceX) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableEntity.Translate(displacement);
    }

    /// <summary>
    /// Translates a <typeparamref name="TTranslatableEntity"/> by the specified distance in the y-direction.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a translation.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var orientation = Orientation.OrientationYZ;
    /// var distanceY = 20;
    /// var scaleFactor = new Vector3D(2, 2, 2);
    /// 
    /// cube = cube.Orientate(orientation)
    ///            <strong>.TranslateY(distanceY)</strong><br/>
    ///            .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity">The <typeparamref name="TTranslatableEntity"/> to be translated.</param>
    /// <param name="distanceY">The distance in the y-direction the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// <returns>The translated <typeparamref name="TTranslatableEntity"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TTranslatableEntity TranslateY<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceY) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableEntity.Translate(displacement);
    }

    /// <summary>
    /// Translates a <typeparamref name="TTranslatableEntity"/> by the specified distance in the z-direction.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a translation.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var orientation = Orientation.OrientationYZ;
    /// var distanceZ = 30;
    /// var scaleFactor = new Vector3D(2, 2, 2);
    /// 
    /// cube = cube.Orientate(orientation)
    ///            <strong>.TranslateZ(distanceZ)</strong><br/>
    ///            .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity">The <typeparamref name="TTranslatableEntity"/> to be translated.</param>
    /// <param name="distanceZ">The distance in the z-direction the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// <returns>The translated <typeparamref name="TTranslatableEntity"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TTranslatableEntity TranslateZ<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableEntity.Translate(displacement);
    }

    /// <summary>
    /// Translates a <typeparamref name="TTranslatableEntity"/> by the specified distances in the x, y and z-directions.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a translation.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var orientation = Orientation.OrientationYZ;
    /// int distanceX = 10, distanceY = 20, distanceZ = 30;
    /// var scaleFactor = new Vector3D(2, 2, 2);
    /// 
    /// cube = cube.Orientate(orientation)
    ///            <strong>.Translate(distanceX, distanceY, distanceZ)</strong><br/>
    ///            .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity">The <typeparamref name="TTranslatableEntity"/> to be translated.</param>
    /// <param name="distanceX">The distance in the x-direction the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// <param name="distanceY">The distance in the y-direction the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// <param name="distanceZ">The distance in the z-direction the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// 
    /// <returns>The translated <typeparamref name="TTranslatableEntity"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TTranslatableEntity Translate<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceX, float distanceY, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableEntity.Translate(displacement);
    }

    /// <summary>
    /// Translates a <typeparamref name="TTranslatableEntity"/> by the specified vector.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a cube is subjected to multiple transformations, including a translation.
    /// <code>
    /// var cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
    /// 
    /// var orientation = Orientation.OrientationYZ;
    /// var displacement = new Vector3D(10, 20, 30);
    /// var scaleFactor = new Vector3D(2, 2, 2);
    /// 
    /// cube = cube.Orientate(orientation)
    ///            <strong>.Translate(displacement)</strong><br/>
    ///            .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity">The <typeparamref name="TTranslatableEntity"/> to be translated.</param>
    /// <param name="displacement">The displacement the <typeparamref name="TTranslatableEntity"/> will be translated.</param>
    /// 
    /// <returns>The translated <typeparamref name="TTranslatableEntity"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TTranslatableEntity Translate<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, Vector3D displacement) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntity);
        translatableEntity.WorldOrigin += displacement;
        return translatableEntity;
    }

    #endregion

    #region Translate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity"></param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TTranslatableEntity TranslateX<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distance, Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distance, 0, 0);
        return translatableEntity.Translate(displacement, predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity"></param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TTranslatableEntity TranslateY<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distance, Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, distance, 0);
        return translatableEntity.Translate(displacement, predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity"></param>
    /// <param name="distance"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TTranslatableEntity TranslateZ<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distance, Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(0, 0, distance);
        return translatableEntity.Translate(displacement, predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity"></param>
    /// <param name="distanceX"></param>
    /// <param name="distanceY"></param>
    /// <param name="distanceZ"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TTranslatableEntity Translate<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceX, float distanceY, float distanceZ, Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableEntity.Translate(displacement, predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the object being translated.</typeparam>
    /// <param name="translatableEntity"></param>
    /// <param name="displacement"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TTranslatableEntity Translate<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, Vector3D displacement, Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntity);

        if (predicate(translatableEntity))
        {
            translatableEntity.WorldOrigin += displacement;
        }

        return translatableEntity;
    }

    #endregion

    #region IEnumerable<TTranslatableEntity>

    /// <summary>
    /// Translates each element of a <typeparamref name="TTranslatableEntity"/> sequence by the specified distance in the x-direction.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a translation.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var orientation = Orientation.OrientationXY;
    /// var distanceX = 40;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Orientate(orientation)
    ///              <strong>.TranslateX(distanceX)</strong><br/>
    ///              .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the elements being translated.</typeparam>
    /// <param name="translatableEntities">The <typeparamref name="TTranslatableEntity"/> sequence containing elements to be translated.</param>
    /// <param name="distanceX">The distance in the x-direction each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <returns>The <typeparamref name="TTranslatableEntity"/> sequence with each element translated.</returns>
    public static IEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceX) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableEntities.Translate(displacement);
    }

    /// <summary>
    /// Translates each element of a <typeparamref name="TTranslatableEntity"/> sequence by the specified distance in the y-direction.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a translation.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var orientation = Orientation.OrientationXY;
    /// var distanceY = 50;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Orientate(orientation)
    ///              <strong>.TranslateY(distanceY)</strong><br/>
    ///              .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the elements being translated.</typeparam>
    /// <param name="translatableEntities">The <typeparamref name="TTranslatableEntity"/> sequence containing elements to be translated.</param>
    /// <param name="distanceY">The distance in the y-direction each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <returns>The <typeparamref name="TTranslatableEntity"/> sequence with each element translated.</returns>
    public static IEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceY) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableEntities.Translate(displacement);
    }

    /// <summary>
    /// Translates each element of a <typeparamref name="TTranslatableEntity"/> sequence by the specified distance in the z-direction.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a translation.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var orientation = Orientation.OrientationXY;
    /// var distanceZ = 60;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Orientate(orientation)
    ///              <strong>.TranslateZ(distanceZ)</strong><br/>
    ///              .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the elements being translated.</typeparam>
    /// <param name="translatableEntities">The <typeparamref name="TTranslatableEntity"/> sequence containing elements to be translated.</param>
    /// <param name="distanceZ">The distance in the z-direction each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <returns>The <typeparamref name="TTranslatableEntity"/> sequence with each element translated.</returns>
    public static IEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableEntities.Translate(displacement);
    }

    /// <summary>
    /// Translates each element of a <typeparamref name="TTranslatableEntity"/> sequence by the specified distances in the x, y and z-directions.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a translation.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var orientation = Orientation.OrientationXY;
    /// int distanceX = 40, distanceY = 50, distanceZ = 60;
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Orientate(orientation)
    ///              <strong>.Translate(distanceX, distanceY, distanceZ)</strong><br/>
    ///              .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the elements being translated.</typeparam>
    /// <param name="translatableEntities">The <typeparamref name="TTranslatableEntity"/> sequence containing elements to be translated.</param>
    /// <param name="distanceX">The distance in the x-direction each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <param name="distanceY">The distance in the y-direction each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <param name="distanceZ">The distance in the z-direction each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <returns>The <typeparamref name="TTranslatableEntity"/> sequence with each element translated.</returns>
    public static IEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceX, float distanceY, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableEntities.Translate(displacement);
    }

    /// <summary>
    /// Translates each element of a <typeparamref name="TTranslatableEntity"/> sequence by the specified vector.
    /// <remarks>The <typeparamref name="TTranslatableEntity"/> sequence cannot be <see langword="null"/>.</remarks>
    /// <para><example>
    /// Example: Using call chaining, a sequence of cubes are subjected to multiple transformations, including a translation.
    /// <code>
    /// var cubes = new Cube[] { cube1, cube2, cube3 };
    /// var orientation = Orientation.OrientationXY;
    /// var displacement = new Vector3D(40, 50, 60);
    /// var scaleFactor = Vector3D.NegativeOne;
    /// 
    /// cubes = cubes.Orientate(orientation)
    ///              <strong>.Translate(displacement)</strong><br/>
    ///              .Scale(scaleFactor);
    /// </code>
    /// </example></para>
    /// </summary>
    /// <typeparam name="TTranslatableEntity">The type of the elements being translated.</typeparam>
    /// <param name="translatableEntities">The <typeparamref name="TTranslatableEntity"/> sequence containing elements to be translated.</param>
    /// <param name="displacement">The displacement each element of the <typeparamref name="TTranslatableEntity"/> sequence will be translated.</param>
    /// <returns>The <typeparamref name="TTranslatableEntity"/> sequence with each element translated.</returns>
    public static IEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, Vector3D displacement) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        foreach (TTranslatableEntity translatableEntity in translatableEntities)
        {
            yield return translatableEntity.Translate(displacement);
        }
    }

    #endregion

    #region IEnumerable<TTranslatableEntity> with predicate

    public static IEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceX, [DisallowNull] Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableEntities.Translate(displacement, predicate);
    }

    public static IEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceY, [DisallowNull] Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableEntities.Translate(displacement, predicate);
    }

    public static IEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceZ, [DisallowNull] Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableEntities.Translate(displacement, predicate);
    }

    public static IEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceX, float distanceY, float distanceZ, [DisallowNull] Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return translatableEntities.Translate(displacement, predicate);
    }

    public static IEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, Vector3D displacement, [DisallowNull] Func<TTranslatableEntity, bool> predicate) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        foreach (TTranslatableEntity translatableEntity in translatableEntities)
        {
            if (predicate(translatableEntity))
            {
                translatableEntity.Translate(displacement);
            }
            yield return translatableEntity;
        }
    }

    #endregion

    #region IAsyncEnumerable<TTranslatableEntity>

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceX, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            yield return translatableEntity.Translate(displacement);
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceY, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            yield return translatableEntity.Translate(displacement);
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceZ, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            yield return translatableEntity.Translate(displacement);
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceX, float distanceY, float distanceZ, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            yield return translatableEntity.Translate(displacement);
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, Vector3D displacement, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            translatableEntity.Translate(displacement);
            yield return translatableEntity;
        }
    }

    #endregion

    #region IAsyncEnumerable<TTranslatableEntity> with predicate

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceX, [DisallowNull] Func<TTranslatableEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            if (predicate(translatableEntity))
            {
                translatableEntity.Translate(displacement);
            }
            yield return translatableEntity;
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceY, [DisallowNull] Func<TTranslatableEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            if (predicate(translatableEntity))
            {
                translatableEntity.Translate(displacement);
            }
            yield return translatableEntity;
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceZ, [DisallowNull] Func<TTranslatableEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            if (predicate(translatableEntity))
            {
                translatableEntity.Translate(displacement);
            }
            yield return translatableEntity;
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceX, float distanceY, float distanceZ, [DisallowNull] Func<TTranslatableEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            if (predicate(translatableEntity))
            {
                translatableEntity.Translate(displacement);
            }
            yield return translatableEntity;
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> Translate<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, Vector3D displacement, [DisallowNull] Func<TTranslatableEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities, predicate);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            if (predicate(translatableEntity))
            {
                translatableEntity.Translate(displacement);
            }
            yield return translatableEntity;
        }
    }

    #endregion
}