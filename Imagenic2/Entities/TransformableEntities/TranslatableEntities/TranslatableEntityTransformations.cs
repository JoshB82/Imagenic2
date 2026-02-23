namespace Imagenic2.Core.Entities;

public static class TranslatableEntityTransformations
{
    public static TTranslatableEntity TranslateX<TTranslatableEntity>(this TTranslatableEntity translatableEntity, float distanceX) where TTranslatableEntity : TranslatableEntity
    {
        translatableEntity.WorldOrigin += new Vector3D(distanceX, 0, 0);
        return translatableEntity;
    }

    public static TTranslatableEntity TranslateY<TTranslatableEntity>(this TTranslatableEntity translatableEntity, float distanceY) where TTranslatableEntity : TranslatableEntity
    {
        translatableEntity.WorldOrigin += new Vector3D(0, distanceY, 0);
        return translatableEntity;
    }

    public static TTranslatableEntity TranslateZ<TTranslatableEntity>(this TTranslatableEntity translatableEntity, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        translatableEntity.WorldOrigin += new Vector3D(0, 0, distanceZ);
        return translatableEntity;
    }

    public static TTranslatableEntity Translate<TTranslatableEntity>(this TTranslatableEntity translatableEntity, float distanceX, float distanceY, float distanceZ) where TTranslatableEntity : TranslatableEntity
    {
        translatableEntity.WorldOrigin += new Vector3D(distanceX, distanceY, distanceZ);
        return translatableEntity;
    }

    public static TTranslatableEntity Translate<TTranslatableEntity>(this TTranslatableEntity translatableEntity, Vector3D displacement) where TTranslatableEntity : TranslatableEntity
    {
        translatableEntity.WorldOrigin += displacement;
        return translatableEntity;
    }
}