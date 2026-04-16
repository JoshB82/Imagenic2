using Imagenic2.Core.Maths.Transformations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities;

public static partial class TransformableEntityTransformations
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
    public static TOrientatedEntity Orientate<TOrientatedEntity>([DisallowNull] this TOrientatedEntity orientatedEntity,
        Orientation orientation) where TOrientatedEntity : OrientatedEntity
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
    public static TOrientatedEntity Rotate<TOrientatedEntity>(this TOrientatedEntity orientatedEntity, Quaternion q) where TOrientatedEntity : OrientatedEntity
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
    public static TOrientatedEntity Rotate<TOrientatedEntity>([DisallowNull] this TOrientatedEntity orientatedEntity, Vector3D axis, float angle) where TOrientatedEntity : OrientatedEntity
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
    public static TOrientatedEntity LookAt<TOrientatedEntity>(this TOrientatedEntity orientatedEntity, TranslatableEntity translatableEntity) where TOrientatedEntity : OrientatedEntity
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
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TOrientatedEntity Orientate<TOrientatedEntity>([DisallowNull] this TOrientatedEntity orientatedEntity,
        Orientation orientation, Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
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
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TOrientatedEntity Rotate<TOrientatedEntity>(this TOrientatedEntity orientatedEntity, Quaternion q, Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
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
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TOrientatedEntity Rotate<TOrientatedEntity>(this TOrientatedEntity orientatedEntity, Vector3D axis, float angle, Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
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
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TOrientatedEntity LookAt<TOrientatedEntity>(this TOrientatedEntity orientatedEntity, TranslatableEntity translatableEntity, Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
    {
        return predicate(orientatedEntity) ? orientatedEntity.LookAt(translatableEntity) : orientatedEntity;
    }

    #endregion

    #region IEnumerable Orientate

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
        Orientation orientation) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.Orientate(orientation);
        }
    }

    #endregion

    #region IEnumerable Rotate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    public static IEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>([DisallowNull] this IEnumerable<TOrientatedEntity> orientatedEntities, Quaternion q) where TOrientatedEntity : OrientatedEntity
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
    public static IEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>([DisallowNull] this IEnumerable<TOrientatedEntity> orientatedEntities, Vector3D axis, float angle) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.Rotate(axis, angle);
        }
    }

    #endregion

    #region IEnumerable Look At

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="translatableEntity"></param>
    /// <returns></returns>
    public static IEnumerable<TOrientatedEntity> LookAt<TOrientatedEntity>(this IEnumerable<TOrientatedEntity> orientatedEntities, TranslatableEntity translatableEntity) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.LookAt(translatableEntity);
        }
    }

    #endregion

    #region IEnumerable Orientate with predicate

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
        Orientation orientation, [DisallowNull] Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities, predicate);

        foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.Orientate(orientation, predicate);
        }
    }

    #endregion

    #region IEnumerable Rotate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    public static IEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>([DisallowNull] this IEnumerable<TOrientatedEntity> orientatedEntities, Quaternion q, [DisallowNull] Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
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
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>([DisallowNull] this IEnumerable<TOrientatedEntity> orientatedEntities, Vector3D axis, float angle, [DisallowNull] Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities, predicate);

        foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.Rotate(axis, angle, predicate);
        }
    }

    #endregion

    #region IEnumerable Look At with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="translatableEntity"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TOrientatedEntity> LookAt<TOrientatedEntity>(this IEnumerable<TOrientatedEntity> orientatedEntities, TranslatableEntity translatableEntity, [DisallowNull] Func<TOrientatedEntity, bool> predicate) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities, predicate);

        foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.LookAt(translatableEntity, predicate);
        }
    }

    #endregion

    #region IAsyncEnumerable Orientate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="orientation"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, Orientation orientation) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.Orientate(orientation);
        }
    }

    #endregion

    #region IAsyncEnumerable Rotate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, Quaternion q) where TOrientatedEntity : OrientatedEntity
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
    public static async IAsyncEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, Vector3D axis, float angle) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            yield return orientatedEntity.Rotate(axis, angle);
        }
    }

    #endregion

    #region IAsyncEnumerable Look At

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="translatableEntity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> LookAt<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, TranslatableEntity translatableEntity, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return orientatedEntity.LookAt(translatableEntity);
        }
    }

    #endregion

    #region IAsyncEnumerable Orientate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="orientation"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> Orientate<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, Orientation orientation, [DisallowNull] Func<TOrientatedEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return orientatedEntity.Orientate(orientation, predicate);
        }
    }

    #endregion

    #region IAsyncEnumerable Rotate with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="q"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, Quaternion q, [DisallowNull] Func<TOrientatedEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TOrientatedEntity : OrientatedEntity
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
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> Rotate<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, Vector3D axis, float angle, [DisallowNull] Func<TOrientatedEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TOrientatedEntity : OrientatedEntity
    {
        ThrowIfNull(orientatedEntities);

        await foreach (TOrientatedEntity orientatedEntity in orientatedEntities)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return orientatedEntity.Rotate(axis, angle, predicate);
        }
    }

    #endregion

    #region IAsyncEnumerable Look At with predicate

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOrientatedEntity"></typeparam>
    /// <param name="orientatedEntities"></param>
    /// <param name="translatableEntity"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TOrientatedEntity> LookAt<TOrientatedEntity>(this IAsyncEnumerable<TOrientatedEntity> orientatedEntities, TranslatableEntity translatableEntity, [DisallowNull] Func<TOrientatedEntity, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TOrientatedEntity : OrientatedEntity
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