namespace Imagenic2.Core.Entities.Animation;

public interface IAnimation<TTransformableEntity> where TTransformableEntity : TransformableEntity
{
    public void Apply(TTransformableEntity transformableEntity, float time);
}