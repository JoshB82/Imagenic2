using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Entities;

public static class TranslatableEntityTransformations
{
    #region TTranslatableEntity

    public static TTranslatableEntity TranslateX<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceX) where TTranslatableEntity : TranslatableEntity
    {
        return translatableEntity.Translate(new Vector3D(distanceX, 0, 0));
    }

    public static TTranslatableEntity TranslateY<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceY) where TTranslatableEntity : TranslatableEntity
    {
        return translatableEntity.Translate(new Vector3D(0, distanceY, 0));
    }

    public static TTranslatableEntity TranslateZ<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        return translatableEntity.Translate(new Vector3D(0, 0, distanceZ));
    }

    public static TTranslatableEntity Translate<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, float distanceX, float distanceY, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        return translatableEntity.Translate(new Vector3D(distanceX, distanceY, distanceZ));
    }

    public static TTranslatableEntity Translate<TTranslatableEntity>([DisallowNull] this TTranslatableEntity translatableEntity, Vector3D displacement) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntity);
        translatableEntity.WorldOrigin += displacement;
        return translatableEntity;
    }

    #endregion

    #region IEnumerable<TTranslatableEntity>

    public static IEnumerable<TTranslatableEntity> TranslateX<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceX) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return translatableEntities.Translate(displacement);
    }

    public static IEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceY) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return translatableEntities.Translate(displacement);
    }

    public static IEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>([DisallowNull] this IEnumerable<TTranslatableEntity> translatableEntities, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return translatableEntities.Translate(displacement);
    }

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
            translatableEntity.Translate(displacement);
            yield return translatableEntity;
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateY<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceY, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            translatableEntity.Translate(displacement);
            yield return translatableEntity;
        }
    }

    public static async IAsyncEnumerable<TTranslatableEntity> TranslateZ<TTranslatableEntity>([DisallowNull] this IAsyncEnumerable<TTranslatableEntity> translatableEntities, float distanceZ, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TTranslatableEntity : TranslatableEntity
    {
        ThrowIfNull(translatableEntities);
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        await foreach (TTranslatableEntity translatableEntity in translatableEntities.WithCancellation(cancellationToken))
        {
            translatableEntity.Translate(displacement);
            yield return translatableEntity;
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