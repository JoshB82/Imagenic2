namespace Imagenic2.Core.Maths;

public struct Orientation : IEquatable<Orientation>
{
    #region Fields and Properties

    internal static readonly Vector3D ModelDirectionForward = Vector3D.UnitZ;
    internal static readonly Vector3D ModelDirectionUp = Vector3D.UnitY;
    internal static readonly Vector3D ModelDirectionRight = Vector3D.UnitX;

    public Vector3D DirectionForward { get; private set; }
    public Vector3D DirectionUp { get; private set; }
    public Vector3D DirectionRight { get; private set; }

    #endregion

    #region Constructors

    private Orientation(Vector3D directionForward, Vector3D directionUp, Vector3D directionRight)
    {
        DirectionForward = directionForward;
        DirectionUp = directionUp;
        DirectionRight = directionRight;
    }

    public static Orientation CreateOrientationForwardUp(Vector3D directionForward, Vector3D directionUp)
    {
        Orientation newOrientation = new();
        newOrientation.SetDirectionForwardUp(directionForward, directionUp);
        return newOrientation;
    }

    public static Orientation CreateOrientationUpRight(Vector3D directionUp, Vector3D directionRight)
    {
        Orientation newOrientation = new();
        newOrientation.SetDirectionUpRight(directionUp, directionRight);
        return newOrientation;
    }

    public static Orientation CreateOrientationRightForward(Vector3D directionRight, Vector3D directionForward)
    {
        Orientation newOrientation = new();
        newOrientation.SetDirectionRightForward(directionRight, directionForward);
        return newOrientation;
    }

    public void SetDirectionForwardUp(Vector3D directionForward, Vector3D directionUp)
    {
        if (directionForward.ApproxEquals(Vector3D.Zero, epsilon))
        {
            throw GenerateException<VectorCannotBeZeroException>.WithParameters(nameof(directionForward));
        }
        if (directionUp.ApproxEquals(Vector3D.Zero, epsilon))
        {
            throw GenerateException<VectorCannotBeZeroException>.WithParameters(nameof(directionUp));
        }
        if (!(directionForward * directionUp).ApproxEquals(0, epsilon))
        {
            throw new MessageBuilder<VectorsAreNotOrthogonalMessage>()
                  .AddParameter(directionForward)
                  .AddParameter(directionUp)
                  .BuildIntoException<ArgumentException>();

            //throw GenerateException<VectorsAreNotOrthogonalException>.WithParameters(nameof(directionForward), nameof(directionUp));
        }

        DirectionForward = directionForward.Normalise();
        DirectionUp = directionUp.Normalise();
        DirectionRight = Transform.CalculateDirectionRight(directionForward, directionUp).Normalise();
    }

    public void SetDirectionUpRight(Vector3D directionUp, Vector3D directionRight)
    {
        if (directionUp.ApproxEquals(Vector3D.Zero, epsilon))
        {
            throw GenerateException<VectorCannotBeZeroException>.WithParameters(nameof(directionUp));
        }
        if (directionRight.ApproxEquals(Vector3D.Zero, epsilon))
        {
            throw GenerateException<VectorCannotBeZeroException>.WithParameters(nameof(directionRight));
        }
        if (!(directionUp * directionRight).ApproxEquals(0, epsilon))
        {
            throw new MessageBuilder<VectorsAreNotOrthogonalMessage>()
                  .AddParameter(directionUp)
                  .AddParameter(directionRight)
                  .BuildIntoException<ArgumentException>();

            //throw GenerateException<VectorsAreNotOrthogonalException>.WithParameters(nameof(directionUp), nameof(directionRight));
        }

        DirectionForward = Transform.CalculateDirectionForward(directionUp, directionRight).Normalise();
        DirectionUp = directionUp.Normalise();
        DirectionRight = directionRight.Normalise();
    }

    public void SetDirectionRightForward(Vector3D directionRight, Vector3D directionForward)
    {
        if (directionRight.ApproxEquals(Vector3D.Zero, epsilon))
        {
            throw GenerateException<VectorCannotBeZeroException>.WithParameters(nameof(directionRight));
        }
        if (directionForward.ApproxEquals(Vector3D.Zero, epsilon))
        {
            throw GenerateException<VectorCannotBeZeroException>.WithParameters(nameof(directionForward));
        }
        if (!(directionRight * directionForward).ApproxEquals(0, epsilon))
        {
            throw new MessageBuilder<VectorsAreNotOrthogonalMessage>()
                  .AddParameter(directionRight)
                  .AddParameter(directionForward)
                  .BuildIntoException<ArgumentException>();

            //throw GenerateException<VectorsAreNotOrthogonalException>.WithParameters(nameof(directionRight), nameof(directionForward));
        }

        DirectionForward = directionForward.Normalise();
        DirectionUp = Transform.CalculateDirectionUp(directionRight, directionForward).Normalise();
        DirectionRight = directionRight.Normalise();
    }

    #endregion

    #region Methods

    #endregion
}