namespace Imagenic2.Core.Entities.Animation;

public class Transformation
{
    #region Fields and Properties

    public List<IAnimation> KeyFrameAnimations { get; set; }

    #endregion

    #region Constructors

    public Transformation(List<IAnimation> keyFrameAnimations)
    {
        KeyFrameAnimations = keyFrameAnimations;
    }

    #endregion

    #region Methods

    public void Apply(float time)
    {
        foreach (IAnimation animation in KeyFrameAnimations)
        {
            animation.Apply(time);
        }
    }

    #endregion
}