namespace Imagenic2.Core.Entities.Animation;

public class Animation
{
    #region Fields and Properties

    private readonly List<Transformation> transformations;

    public int DurationSeconds { get; set; }
    public int FPS { get; set; }

    #endregion

    #region Constructors

    public Animation(params IEnumerable<Transformation> transformations)
    {
        this.transformations = transformations.ToList();
    }

    #endregion

    #region Methods

    public void Apply(float time)
    {
        foreach (Transformation transformation in transformations)
        {
            transformation.Apply(time);
        }
    }

    public static Animation operator +(Animation a1, Animation a2)
    {
        return new Animation(a1.transformations.Concat(a2.transformations));
    }

    #endregion
}