namespace Imagenic2.Core.Entities.TransformableEntities.Animation;

public class Animation
{
    #region Fields and Properties

    private readonly List<IAnimation> transformations;

    public int Duration { get; set; }
    public int FPS { get; set; }

    #endregion

    #region Constructors

    public Animation(params IEnumerable<IAnimation> transformations)
    {
        this.transformations = transformations.ToList();
    }

    #endregion

    #region Methods

    public void Apply(float time)
    {
        foreach (Transformation<TransformableEntity> t in transformations)
        {
            t.Apply(time);
        }
    }

    public static Animation operator +(Animation a1, Animation a2)
    {
        return new Animation(a1.transformations.Concat(a2.transformations));
    }

    #endregion
}