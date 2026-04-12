namespace Imagenic2.Core.Entities.Animation;

public sealed class Animation
{
    #region Fields and Properties

    private readonly List<Transformation> transformations;

    private int durationSeconds;
    public int DurationSeconds
    {
        get => durationSeconds;
        set
        {
            ThrowIfNonpositive(value);
            durationSeconds = value;
        }
    }
    private int fps;
    public int FPS
    {
        get => fps;
        set
        {
            ThrowIfNonpositive(value);
            fps = value;
        }
    }

    private int repeat;
    public int Repeat
    {
        get => repeat;
        set
        {
            ThrowIfNonpositive(value);
            repeat = value;
        }
    }

    public PlaybackDirection PlaybackDirection { get; set; } = PlaybackDirection.Forwards;

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