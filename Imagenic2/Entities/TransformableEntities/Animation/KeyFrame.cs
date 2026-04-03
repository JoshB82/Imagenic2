namespace Imagenic2.Core.Entities.TransformableEntities.Animation;

public struct KeyFrame<TValue>
{
    #region Fields and Properties

    public float time;
    public TValue value;

    #endregion

    #region Constructors

    public KeyFrame(float time, TValue value)
    {
        this.time = time;
        this.value = value;
    }

    #endregion
}