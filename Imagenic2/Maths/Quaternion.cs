using Imagenic2.Core.Utilities;
using System.Numerics;

namespace Imagenic2.Core.Maths;

/// <summary>
/// Encapsulates a quaternion.
/// </summary>
public struct Quaternion : IApproximatelyEquatable<Quaternion>,
                           IAdditionOperators<Quaternion, Quaternion, Quaternion>,
                           ISubtractionOperators<Quaternion, Quaternion, Quaternion>,
                           IMultiplyOperators<Quaternion, Quaternion, Quaternion>,
                           IMultiplyOperators<Quaternion, float, Quaternion>,
                           IUnaryPlusOperators<Quaternion, Quaternion>,
                           IUnaryNegationOperators<Quaternion, Quaternion>,
                           IAdditiveIdentity<Quaternion, Quaternion>,
                           IMultiplicativeIdentity<Quaternion, Quaternion>
{
    #region Fields and Properties

    // Common quaternions
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (0, 0, 0, 0).
    /// </summary>
    public static readonly Quaternion Zero = new();
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (0, 0, 0, 0).
    /// </summary>
    public static Quaternion AdditiveIdentity => Zero;
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (1, 0, 0, 0).
    /// </summary>
    public static readonly Quaternion Identity = new(1, 0, 0, 0);
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (1, 1, 1, 1).
    /// </summary>
    public static readonly Quaternion One = new Quaternion(1, 1, 1, 1);
    /// <summary>
    /// A <see cref="Quaternion"/> equal to (1, 1, 1, 1).
    /// </summary>
    public static Quaternion MultiplicativeIdentity => One;

    /// <summary>
    /// The w-component of the <see cref="Quaternion"/> (w, x, y, z).
    /// </summary>
    public float w;
    /// <summary>
    /// The x-component of the <see cref="Quaternion"/> (w, x, y, z).
    /// </summary>
    public float x;
    /// <summary>
    /// The y-component of the <see cref="Quaternion"/> (w, x, y, z).
    /// </summary>
    public float y;
    /// <summary>
    /// The z-component of the <see cref="Quaternion"/> (w, x, y, z).
    /// </summary>
    public float z;

    #endregion

    #region Constructors

    public Quaternion(float w, float x, float y, float z)
    {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Quaternion(float scalar, Vector3D v)
    {
        w = scalar;
        x = v.x;
        y = v.y;
        z = v.z;
    }

    #endregion

    #region Methods

    public static bool IsFinite(Quaternion q) => float.IsFinite(q.w) && float.IsFinite(q.x) && float.IsFinite(q.y) && float.IsFinite(q.z);
    public readonly bool IsZero(float epsilon = Settings.epsilon) => ApproxEquals(Zero, epsilon);
    
    public readonly float SquaredMagnitude() => w * w + x * x + y * y + z * z;
    public readonly float Magnitude() => Sqrt(SquaredMagnitude());

    public readonly Quaternion Normalise(float epsilon = Settings.epsilon)
    {
        ThrowIfApproxZero(this, epsilon);
        return this / Magnitude();
    }
    public readonly bool TryNormalise(out Quaternion q, float epsilon = Settings.epsilon)
    {
        q = Zero;
        if (IsZero(epsilon))
        {
            return false;
        }
        q = Normalise();
        return true;
    }

    public readonly bool Equals(Quaternion q) => this == q;
    public override readonly bool Equals(object obj) => obj is Quaternion q && this == q;
    public override readonly int GetHashCode() => (w, x, y, z).GetHashCode();

    public readonly bool ApproxEquals(Quaternion q, float epsilon = Settings.epsilon)
    {
        return w.ApproxEquals(q.w, epsilon) &&
               x.ApproxEquals(q.x, epsilon) &&
               y.ApproxEquals(q.y, epsilon) &&
               z.ApproxEquals(q.z, epsilon);
    }

    public override readonly string ToString() => $"(w: {w}, x: {x}, y: {y}, z: {z})";
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"(w: {w.ToString(format, formatProvider)}, x: {x.ToString(format, formatProvider)}, y: {y.ToString(format, formatProvider)}, z: {z.ToString(format, formatProvider)})";

    public static bool operator ==(Quaternion v1, Quaternion v2) => v1.w == v2.w && v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;

    public static bool operator !=(Quaternion v1, Quaternion v2) => !(v1 == v2);

    public static Quaternion operator +(Quaternion q) => q;
    public static Quaternion operator -(Quaternion q) => new Quaternion(-q.w, -q.x, -q.y, -q.z);

    public static Quaternion operator +(Quaternion q1, Quaternion q2) => new(q1.w + q2.w, q1.x + q2.x, q1.y + q2.y, q1.z + q2.z);

    public static Quaternion operator -(Quaternion q1, Quaternion q2) => new(q1.w - q2.w, q1.x - q2.x, q1.y - q2.y, q1.z - q2.z);

    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        return new
        (
            q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z,
            q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y,
            q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x,
            q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w
        );
    }

    public static Quaternion operator *(Quaternion q, float scalar) => new(q.w * scalar, q.x * scalar, q.y * scalar, q.z * scalar);

    public static Quaternion operator *(float scalar, Quaternion q) => q * scalar;

    public static Quaternion operator /(Quaternion q, float scalar) => new(q.w / scalar, q.x / scalar, q.y / scalar, q.z / scalar);

    #endregion

    public int ApproximatelyCompareTo(Quaternion other, float epsilon = Settings.epsilon)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(Quaternion other)
    {
        throw new NotImplementedException();
    }
}