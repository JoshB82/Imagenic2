namespace Imagenic2.Core.Maths;

public struct Quaternion : IEquatable<Quaternion>
{
    #region Fields and Properties

    // Common quaternions
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (0, 0, 0, 0).
    /// </summary>
    public static readonly Quaternion Zero = new();
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (1, 0, 0, 0).
    /// </summary>
    public static readonly Quaternion Identity = new(1, 0, 0, 0);

    public float q1;
    public float q2;
    public float q3;
    public float q4;

    #endregion

    #region Constructors

    public Quaternion(float q1, float q2, float q3, float q4)
    {
        this.q1 = q1;
        this.q2 = q2;
        this.q3 = q3;
        this.q4 = q4;
    }

    public Quaternion(float scalar, Vector3D v)
    {
        q1 = scalar;
        q2 = v.x;
        q3 = v.y;
        q4 = v.z;
    }

    #endregion

    #region Methods

    public readonly float Magnitude() => Sqrt(SquaredMagnitude());

    public readonly float SquaredMagnitude() => q1 * q1 + q2 * q2 + q3 * q3 + q4 * q4;

    public readonly Quaternion Normalise() =>
        ApproxEquals(Zero, 1E-6f)
            ? Quaternion.Zero//throw Exceptions.QuaternionNormalise
            : this / Magnitude();
    public readonly bool ApproxEquals(Quaternion q, float epsilon = float.Epsilon) =>
        q1.ApproxEquals(q.q1, epsilon) &&
        q2.ApproxEquals(q.q2, epsilon) &&
        q3.ApproxEquals(q.q3, epsilon) &&
        q4.ApproxEquals(q.q4, epsilon);

    public static bool operator ==(Quaternion v1, Quaternion v2) => v1.q1 == v2.q1 && v1.q2 == v2.q2 && v1.q3 == v2.q3 && v1.q4 == v2.q4;

    public static bool operator !=(Quaternion v1, Quaternion v2) => !(v1 == v2);

    public readonly bool Equals(Quaternion q) => this == q;
    public override readonly bool Equals(object obj) => this == (Quaternion)obj;
    public override int GetHashCode() => (q1, q2, q3, q4).GetHashCode();

    public static Quaternion operator +(Quaternion q1, Quaternion q2) => new(q1.q1 + q2.q1, q1.q2 + q2.q2, q1.q3 + q2.q3, q1.q4 + q2.q4);

    public static Quaternion operator -(Quaternion q1, Quaternion q2) => new(q1.q1 - q2.q1, q1.q2 - q2.q2, q1.q3 - q2.q3, q1.q4 - q2.q4);

    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        return new
        (
            q1.q1 * q2.q1 - q1.q2 * q2.q2 - q1.q3 * q2.q3 - q1.q4 * q2.q4,
            q1.q1 * q2.q2 + q1.q2 * q2.q1 + q1.q3 * q2.q4 - q1.q4 * q2.q3,
            q1.q1 * q2.q3 - q1.q2 * q2.q4 + q1.q3 * q2.q1 + q1.q4 * q2.q2,
            q1.q1 * q2.q4 + q1.q2 * q2.q3 - q1.q3 * q2.q2 + q1.q4 * q2.q1
        );
    }

    public static Quaternion operator *(Quaternion q, float scalar) => new(q.q1 * scalar, q.q2 * scalar, q.q3 * scalar, q.q4 * scalar);

    public static Quaternion operator *(float scalar, Quaternion q) => q * scalar;

    public static Quaternion operator /(Quaternion q, float scalar) => new(q.q1 / scalar, q.q2 / scalar, q.q3 / scalar, q.q4 / scalar);

    #endregion

    public int ApproximatelyCompareTo(Quaternion other, float epsilon)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(Quaternion other)
    {
        throw new NotImplementedException();
    }
}