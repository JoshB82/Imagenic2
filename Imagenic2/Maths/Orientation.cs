using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Maths;

public struct Orientation : IEquatable<Orientation>
{
    #region Fields and Properties

    internal static readonly Vector3D ModelDirectionForward = Vector3D.UnitZ;
    internal static readonly Vector3D ModelDirectionUp = Vector3D.UnitY;
    internal static readonly Vector3D ModelDirectionRight = Vector3D.UnitX;

    // Orientations
    internal static readonly Orientation ModelOrientation = Orientation.CreateOrientationForwardUp(ModelDirectionForward, ModelDirectionUp);
    public static readonly Orientation OrientationXY = new(Vector3D.UnitX, Vector3D.UnitY, Vector3D.UnitNegativeZ);
    public static readonly Orientation OrientationXZ = new(Vector3D.UnitX, Vector3D.UnitZ, Vector3D.UnitNegativeY);
    public static readonly Orientation OrientationYX = new(Vector3D.UnitY, Vector3D.UnitX, Vector3D.UnitZ);
    public static readonly Orientation OrientationYZ = new(Vector3D.UnitY, Vector3D.UnitZ, Vector3D.UnitNegativeX);
    public static readonly Orientation OrientationZX = new(Vector3D.UnitZ, Vector3D.UnitX, Vector3D.UnitNegativeY);
    public static readonly Orientation OrientationZY = new(Vector3D.UnitZ, Vector3D.UnitY, Vector3D.UnitX);
    public static readonly Orientation OrientationXNegativeY = new(Vector3D.UnitX, Vector3D.UnitNegativeY, Vector3D.UnitZ);
    public static readonly Orientation OrientationXNegativeZ = new(Vector3D.UnitX, Vector3D.UnitNegativeZ, Vector3D.UnitY);
    public static readonly Orientation OrientationYNegativeX = new(Vector3D.UnitY, Vector3D.UnitNegativeX, Vector3D.UnitNegativeZ);
    public static readonly Orientation OrientationYNegativeZ = new(Vector3D.UnitY, Vector3D.UnitNegativeZ, Vector3D.UnitX);
    public static readonly Orientation OrientationZNegativeX = new(Vector3D.UnitZ, Vector3D.UnitNegativeX, Vector3D.UnitY);
    public static readonly Orientation OrientationZNegativeY = new(Vector3D.UnitZ, Vector3D.UnitNegativeY, Vector3D.UnitNegativeX);
    public static readonly Orientation OrientationNegativeXY = new(Vector3D.UnitNegativeX, Vector3D.UnitY, Vector3D.UnitZ);
    public static readonly Orientation OrientationNegativeXZ = new(Vector3D.UnitNegativeX, Vector3D.UnitZ, Vector3D.UnitNegativeY);
    public static readonly Orientation OrientationNegativeYX = new(Vector3D.UnitNegativeY, Vector3D.UnitX, Vector3D.UnitNegativeZ);
    public static readonly Orientation OrientationNegativeYZ = new(Vector3D.UnitNegativeY, Vector3D.UnitZ, Vector3D.UnitX);
    public static readonly Orientation OrientationNegativeZX = new(Vector3D.UnitNegativeZ, Vector3D.UnitX, Vector3D.UnitY);
    public static readonly Orientation OrientationNegativeZY = new(Vector3D.UnitNegativeZ, Vector3D.UnitY, Vector3D.UnitNegativeX);
    public static readonly Orientation OrientationNegativeXNegativeY = new(Vector3D.UnitNegativeX, Vector3D.UnitNegativeY, Vector3D.UnitNegativeZ);
    public static readonly Orientation OrientationNegativeXNegativeZ = new(Vector3D.UnitNegativeX, Vector3D.UnitNegativeZ, Vector3D.UnitY);
    public static readonly Orientation OrientationNegativeYNegativeX = new(Vector3D.UnitNegativeY, Vector3D.UnitNegativeX, Vector3D.UnitZ);
    public static readonly Orientation OrientationNegativeYNegativeZ = new(Vector3D.UnitNegativeY, Vector3D.UnitNegativeZ, Vector3D.UnitNegativeX);
    public static readonly Orientation OrientationNegativeZNegativeX = new(Vector3D.UnitNegativeZ, Vector3D.UnitNegativeX, Vector3D.UnitNegativeY);
    public static readonly Orientation OrientationNegativeZNegativeY = new(Vector3D.UnitNegativeZ, Vector3D.UnitNegativeY, Vector3D.UnitX);

    public Quaternion rotation = Quaternion.Identity;

    public Vector3D DirectionForward { get; private set; }
    public Vector3D DirectionUp { get; private set; }
    public Vector3D DirectionRight { get; private set; }

    private const float epsilon = 1e-6f;

    #endregion

    #region Constructors

    private Orientation(Vector3D directionForward, Vector3D directionUp, Vector3D directionRight)
    {
        DirectionForward = directionForward;
        DirectionUp = directionUp;
        DirectionRight = directionRight;
    }

    public Orientation(Quaternion rotation)
    {
        this.rotation = rotation;
        SetDirectionForwardUp(ExtractForward(rotation), ExtractUp(rotation));
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

    public static Orientation CreateOrientationQuaternion(Quaternion rotation)
    {
        Orientation newOrientation = new();
        newOrientation.SetRotation(rotation);
        return newOrientation;
    }

    #endregion

    #region Methods

    public void SetDirectionForwardUp(Vector3D directionForward, Vector3D directionUp)
    {
        ThrowIfApproxZero(directionForward, float.Epsilon);
        ThrowIfApproxZero(directionUp, float.Epsilon);
        ThrowIfNotOrthogonal(directionForward, directionUp, epsilon);

        DirectionForward = directionForward.Normalise();
        DirectionUp = directionUp.Normalise();
        DirectionRight = Transform.CalculateDirectionRight(directionForward, directionUp).Normalise();

        //DirectionForward = new Vector3D(0,0,1).Normalise();
        //DirectionUp = new Vector3D(0, 1, 0).Normalise();
        //DirectionRight = Transform.CalculateDirectionRight(DirectionForward, DirectionUp).Normalise();

        //rotation = ExtractRotation(DirectionForward, DirectionUp, DirectionRight);

        //var forward = ExtractForward(rotation).Normalise();
        //var up = ExtractUp(rotation).Normalise();
        //var right = ExtractRight(rotation).Normalise();
    }

    public void SetDirectionUpRight(Vector3D directionUp, Vector3D directionRight)
    {
        ThrowIfApproxZero(directionUp, float.Epsilon);
        ThrowIfApproxZero(directionRight, float.Epsilon);
        ThrowIfNotOrthogonal(directionUp, directionRight, epsilon);

        DirectionForward = Transform.CalculateDirectionForward(directionUp, directionRight).Normalise();
        DirectionUp = directionUp.Normalise();
        DirectionRight = directionRight.Normalise();
        //rotation = ExtractRotation(DirectionForward, DirectionUp, DirectionRight);
    }

    public void SetDirectionRightForward(Vector3D directionRight, Vector3D directionForward)
    {
        ThrowIfApproxZero(directionRight, float.Epsilon);
        ThrowIfApproxZero(directionForward, float.Epsilon);
        ThrowIfNotOrthogonal(directionRight, directionForward, epsilon);

        DirectionForward = directionForward.Normalise();
        DirectionUp = Transform.CalculateDirectionUp(directionRight, directionForward).Normalise();
        DirectionRight = directionRight.Normalise();
        //rotation = ExtractRotation(DirectionForward, DirectionUp, DirectionRight);
    }

    public void SetRotation(Quaternion rotation)
    {
        DirectionForward = ExtractForward(rotation);
        DirectionUp = ExtractUp(rotation);
        DirectionRight = Transform.CalculateDirectionRight(DirectionForward, DirectionUp).Normalise();
        this.rotation = rotation;
    }

    public static Vector3D ExtractForward(Quaternion q) => new Vector3D(2 * (q.q2 * q.q4 - q.q3 * q.q1), 2 * (q.q3 * q.q4 + q.q2 * q.q1), 1 - 2 * (q.q2 * q.q2 + q.q3 * q.q3));

    public static Vector3D ExtractUp(Quaternion q) => new Vector3D(2 * (q.q2 * q.q3 + q.q4 * q.q1), 1 - 2 * (q.q2 * q.q2 + q.q4 * q.q4), 2 * (q.q3 * q.q4 - q.q2 * q.q1));

    public static Vector3D ExtractRight(Quaternion q) => new Vector3D(1 - 2 * (q.q3 * q.q3 + q.q4 * q.q4), 2 * (q.q2 * q.q3 - q.q4 * q.q1), 2 * (q.q2 * q.q4 + q.q3 * q.q1));

    public static Quaternion ExtractRotation(Vector3D directionForward, Vector3D directionUp, Vector3D directionRight)
    {
        float m00 = directionRight.x, m01 = directionRight.y, m02 = directionRight.z;
        float m10 = directionUp.x, m11 = directionUp.y, m12 = directionUp.z;
        float m20 = directionForward.x, m21 = directionForward.y, m22 = directionForward.z;

        float trace = m00 + m11 + m22;
        Quaternion resultQuaternion;

        if (trace > 0)
        {
            float s = Sqrt(trace + 1) * 2;
            resultQuaternion = new(0.25f * s, (m21 - m12) / s, (m02 - m20) / s, (m10 - m01) / s);
        }
        else if (m00 > m11 && m00 > m22)
        {
            float s = Sqrt(m00 - m11 - m22 + 1) * 2;
            resultQuaternion = new((m21 - m12) / s, 0.25f * s, (m01 + m10) / s, (m02 + m20) / s);
        }
        else if (m11 > m22)
        {
            float s = Sqrt(m11 - m00 - m22 + 1) * 2;
            resultQuaternion = new((m02 - m20) / s, (m01 + m10) / s, 0.25f * s, (m12 + m21) / s);
        }
        else
        {
            float s = Sqrt(m22 - m00 - m11 + 1) * 2;
            resultQuaternion = new((m10 - m01) / s, (m02 + m20) / s, (m12 + m21) / s, 0.25f * s);
        }

        return resultQuaternion.Normalise();
    }

    public bool Equals(Orientation other) => (DirectionForward, DirectionUp, DirectionRight) == (other.DirectionForward, other.DirectionUp, other.DirectionRight);

    public override int GetHashCode() => (DirectionForward, DirectionUp, DirectionRight).GetHashCode();

    public static bool operator ==(Orientation lhs, Orientation rhs) => lhs.Equals(rhs);

    public static bool operator !=(Orientation lhs, Orientation rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => obj is Orientation orientation && Equals(orientation);

    public readonly override string ToString() => $"[Forward: {DirectionForward}, Up: {DirectionUp}, Right: {DirectionRight}]";

    #endregion
}