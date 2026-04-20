using Imagenic2.Core.Utilities;
using System.Drawing;
using System.Numerics;

namespace Imagenic2.Core.Maths.Vectors;

/// <summary>
/// Encapsulates a three-dimensional vector.
/// </summary>
public struct Vector3D : IApproximatelyEquatable<Vector3D>,
                         IAdditionOperators<Vector3D, Vector3D, Vector3D>,
                         ISubtractionOperators<Vector3D, Vector3D, Vector3D>,
                         IMultiplyOperators<Vector3D, float, Vector3D>,
                         IDivisionOperators<Vector3D, float, Vector3D>,
                         IUnaryPlusOperators<Vector3D, Vector3D>,
                         IUnaryNegationOperators<Vector3D, Vector3D>,
                         IAdditiveIdentity<Vector3D, Vector3D>,
                         IVector<Vector3D>
{
    #region Fields and Properties

    /// <summary>
    /// A <see cref="Vector3D"/> equal to (0, 0, 0).
    /// </summary>
    public static readonly Vector3D Zero = new(0, 0, 0);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (0, 0, 0).
    /// </summary>
    public static Vector3D AdditiveIdentity => Zero;
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (1, 1, 1).
    /// </summary>
    public static readonly Vector3D One = new(1, 1, 1);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (-1, -1, -1).
    /// </summary>
    public static readonly Vector3D NegativeOne = new(-1, -1, -1);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (1, 0, 0).
    /// </summary>
    public static readonly Vector3D UnitX = new(1, 0, 0);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (0, 1, 0).
    /// </summary>
    public static readonly Vector3D UnitY = new(0, 1, 0);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (0, 0, 1).
    /// </summary>
    public static readonly Vector3D UnitZ = new(0, 0, 1);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (-1, 0, 0).
    /// </summary>
    public static readonly Vector3D UnitNegativeX = new(-1, 0, 0);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (0, -1, 0).
    /// </summary>
    public static readonly Vector3D UnitNegativeY = new(0, -1, 0);
    /// <summary>
    /// A <see cref="Vector3D"/> equal to (0, 0, -1).
    /// </summary>
    public static readonly Vector3D UnitNegativeZ = new(0, 0, -1);

    /// <summary>
    /// The x-component of the <see cref="Vector3D"/> (x, y, z).
    /// </summary>
    public float x;
    /// <summary>
    /// The y-component of the <see cref="Vector3D"/> (x, y, z).
    /// </summary>
    public float y;
    /// <summary>
    /// The z-component of the <see cref="Vector3D"/> (x, y, z).
    /// </summary>
    public float z;

    #endregion

    #region Constructors

    public Vector3D(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3D(Vector2D v, float z = 0)
    {
        x = v.x;
        y = v.y;
        this.z = z;
    }

    #endregion

    #region Methods

    public static bool IsFinite(Vector3D v) => float.IsFinite(v.x) && float.IsFinite(v.y) && float.IsFinite(v.z);
    public readonly bool IsZero(float epsilon = Settings.epsilon) => ApproxEquals(Zero, epsilon);

    public readonly float SquaredMagnitude() => x * x + y * y + z * z;
    public readonly float Magnitude() => Sqrt(SquaredMagnitude());
    public static float Magnitude(Vector3D v) => v.Magnitude();

    public static float Dot(Vector3D v1, Vector3D v2) => v1 * v2;

    public static Vector3D LineIntersectPlane(Vector3D lineStart, Vector3D lineFinish, Vector3D planePoint, Vector3D planeNormal, out float d)
    {
        Vector3D line = lineFinish - lineStart;
        float denominator = line * planeNormal;
        //if (denominator == 0) throw new ArgumentException("Line does not intersect plane or exists entirely on plane.");

        d = (planePoint - lineStart) * planeNormal / denominator;
        // Round in direction of normal!?
        return line * d + lineStart;
    }

    public static float PointDistanceFromPlane(Vector3D point, Vector3D planePoint, Vector3D planeNormal) => point * planeNormal - planePoint * planeNormal;

    public static Vector3D NormalFromPlane(Vector3D p1, Vector3D p2, Vector3D p3) => (p3 - p1).CrossProduct(p2 - p1).Normalise();

    public readonly float Angle(Vector3D v, float epsilon = Settings.epsilon)
    {
        ThrowIfApproxZero(this, epsilon);
        ThrowIfApproxZero(v, epsilon);

        float quotient = this * v / Sqrt(SquaredMagnitude() * v.SquaredMagnitude());
        return Acos(quotient.Clamp(-1, 1));
    }
    public readonly bool TryGetAngle(Vector3D v, out float angle, float epsilon = Settings.epsilon)
    {
        angle = 0;
        if (IsZero(epsilon) || v.IsZero(epsilon))
        {
            return false;
        }
        float quotient = this * v / Sqrt(SquaredMagnitude() * v.SquaredMagnitude());
        angle = Acos(quotient.Clamp(-1, 1));
        return true;
    }

    public readonly Vector3D Normalise(float epsilon = Settings.epsilon)
    {
        ThrowIfApproxZero(this, epsilon);
        return this / Magnitude();
    }
    public readonly bool TryNormalise(out Vector3D v, float epsilon = Settings.epsilon)
    {
        v = Zero;
        if (IsZero(epsilon))
        {
            return false;
        }
        v = this / Magnitude();
        return true;
    }

    public readonly Vector3D CrossProduct(Vector3D v) => new Vector3D(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);
    
    

    public override readonly string ToString() => $"(x: {x}, y: {y}, z: {z})";
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"(x: {x.ToString(format, formatProvider)}, y: {y.ToString(format, formatProvider)}, z: {z.ToString(format, formatProvider)})";
    
    // Operators
    public static Vector3D operator checked +(Vector3D v1, Vector3D v2) => checked(new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z));
    public static Vector3D operator +(Vector3D v1, Vector3D v2) => new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);

    public static Vector3D operator checked *(Vector3D v, float scalar) => checked(new(v.x * scalar, v.y * scalar, v.z * scalar));
    public static Vector3D operator *(Vector3D v, float scalar) => new(v.x * scalar, v.y * scalar, v.z * scalar);

    public static Vector3D operator checked *(float scalar, Vector3D v) => checked(v * scalar);
    public static Vector3D operator *(float scalar, Vector3D v) => v * scalar;

    public static Vector3D operator checked -(Vector3D v1, Vector3D v2) => checked(new(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z));
    public static Vector3D operator -(Vector3D v1, Vector3D v2) => new(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);

    public static float operator checked *(Vector3D v1, Vector3D v2) => checked(v1.x * v2.x + v1.y * v2.y + v1.z * v2.z);
    public static float operator *(Vector3D v1, Vector3D v2) => v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;

    public static Vector3D operator checked /(Vector3D v, float scalar) => checked(new(v.x / scalar, v.y / scalar, v.z / scalar));
    public static Vector3D operator /(Vector3D v, float scalar) => new(v.x / scalar, v.y / scalar, v.z / scalar);

    public static Vector3D operator checked -(Vector3D v) => checked(new(-v.x, -v.y, -v.z));
    public static Vector3D operator +(Vector3D v) => new(v.x, v.y, v.z);
    public static Vector3D operator -(Vector3D v) => new(-v.x, -v.y, -v.z);

    public static bool operator ==(Vector3D v1, Vector3D v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;

    public static bool operator !=(Vector3D v1, Vector3D v2) => !(v1 == v2);

    public static implicit operator Vector4D(Vector3D v) => new Vector4D(v);
    public static explicit operator Vector2D(Vector3D v) => new Vector2D(v.x, v.y);

    // Equality
    public readonly bool Equals(Vector3D v) => this == v;
    public override readonly bool Equals(object obj) => obj is Vector3D v && this == v;

    public readonly bool ApproxEquals(Vector3D v, float epsilon = Settings.epsilon) => x.ApproxEquals(v.x, epsilon) && y.ApproxEquals(v.y, epsilon) && z.ApproxEquals(v.z, epsilon);

    public override readonly int GetHashCode() => (x, y, z).GetHashCode();

    public readonly Color ToSystemDrawingColor()
    {
        byte r = (byte)(Math.Clamp(x, 0, 1) * 255);
        byte g = (byte)(Math.Clamp(y, 0, 1) * 255);
        byte b = (byte)(Math.Clamp(z, 0, 1) * 255);

        return Color.FromArgb(r, g, b);
    }

    #endregion
}