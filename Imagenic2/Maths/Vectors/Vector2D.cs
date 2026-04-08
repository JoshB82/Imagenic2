using Imagenic2.Core.Utilities;
using System.Numerics;

namespace Imagenic2.Core.Maths.Vectors;

public struct Vector2D : IApproximatelyEquatable<Vector2D>,
                         IEqualityOperators<Vector2D, Vector2D, bool>,
                         IAdditionOperators<Vector2D, Vector2D, Vector2D>,
                         ISubtractionOperators<Vector2D, Vector2D, Vector2D>,
                         IMultiplyOperators<Vector2D, float, Vector2D>,
                         IDivisionOperators<Vector2D, float, Vector2D>,
                         IUnaryPlusOperators<Vector2D, Vector2D>,
                         IUnaryNegationOperators<Vector2D, Vector2D>,
                         IAdditiveIdentity<Vector2D, Vector2D>,
                         IVector<Vector2D>
{
    #region Fields and Properties

    /// <summary>
    /// A <see cref="Vector2D"/> equal to (0, 0).
    /// </summary>
    public static readonly Vector2D Zero = new(0, 0);
    /// <summary>
    /// A <see cref="Vector2D"/> equal to (0, 0).
    /// </summary>
    public static Vector2D AdditiveIdentity => Zero;
    /// <summary>
    /// A <see cref="Vector2D"/> equal to (1, 1).
    /// </summary>
    public static readonly Vector2D One = new(1, 1);
    /// <summary>
    /// A <see cref="Vector2D"/> equal to (1, 0).
    /// </summary>
    public static readonly Vector2D UnitX = new(1, 0);
    /// <summary>
    /// A <see cref="Vector2D"/> equal to (0, 1).
    /// </summary>
    public static readonly Vector2D UnitY = new(0, 1);
    /// <summary>
    /// A <see cref="Vector2D"/> equal to (-1, 0).
    /// </summary>
    public static readonly Vector2D UnitNegativeX = new(-1, 0);
    /// <summary>
    /// A <see cref="Vector2D"/> equal to (0, -1).
    /// </summary>
    public static readonly Vector2D UnitNegativeY = new(0, -1);

    /// <summary>
    /// The x-component of the <see cref="Vector2D"/> (x, y).
    /// </summary>
    public float x;
    /// <summary>
    /// The y-component of the <see cref="Vector2D"/> (x, y).
    /// </summary>
    public float y;

    #endregion

    #region Constructors

    public Vector2D(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    #endregion

    #region Methods

    public static bool IsFinite(Vector2D v) => float.IsFinite(v.x) && float.IsFinite(v.y);
    public readonly bool IsZero(float epsilon = Settings.epsilon) => ApproxEquals(Zero, epsilon);

    public readonly float SquaredMagnitude() => x * x + y * y;
    public readonly float Magnitude() => Sqrt(SquaredMagnitude());
    public static float Magnitude(Vector2D v) => v.Magnitude();

    public static float Dot(Vector2D v1, Vector2D v2) => v1 * v2;

    public readonly float Angle(Vector2D v, float epsilon = Settings.epsilon)
    {
        ThrowIfApproxZero(this, epsilon);
        ThrowIfApproxZero(v, epsilon);
        
        float quotient = this * v / Sqrt(SquaredMagnitude() * v.SquaredMagnitude());
        return Acos(quotient.Clamp(-1, 1));
    }
    public readonly bool TryGetAngle(Vector2D v, out float angle, float epsilon = Settings.epsilon)
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

    public readonly Vector2D Normalise(float epsilon = Settings.epsilon)
    {
        ThrowIfApproxZero(this, epsilon);
        return this / Magnitude();
    }

    public readonly bool TryNormalise(out Vector2D v, float epsilon = Settings.epsilon)
    {
        v = Zero;
        if (IsZero(epsilon))
        {
            return false;
        }
        v = this / Magnitude();
        return true;
    }

    public readonly override string ToString() => $"(x: {x}, y: {y})";
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"(x: {x.ToString(format, formatProvider)}, y: {y.ToString(format, formatProvider)})";

    // Operators
    public static Vector2D operator checked *(Vector2D v, float scalar) => checked(new(v.x * scalar, v.y * scalar));
    public static Vector2D operator *(Vector2D v, float scalar) => new(v.x * scalar, v.y * scalar);

    public static Vector2D operator checked *(float scalar, Vector2D v) => checked(v * scalar);
    public static Vector2D operator *(float scalar, Vector2D v) => v * scalar;

    public static Vector2D operator checked +(Vector2D v1, Vector2D v2) => checked(new(v1.x + v2.x, v1.y + v2.y));
    public static Vector2D operator +(Vector2D v1, Vector2D v2) => new(v1.x + v2.x, v1.y + v2.y);

    public static Vector2D operator checked -(Vector2D v1, Vector2D v2) => checked(new(v1.x - v2.x, v1.y - v2.y));
    public static Vector2D operator -(Vector2D v1, Vector2D v2) => new(v1.x - v2.x, v1.y - v2.y);

    public static float operator checked *(Vector2D v1, Vector2D v2) => checked(v1.x * v2.x + v1.y * v2.y);
    public static float operator *(Vector2D v1, Vector2D v2) => v1.x * v2.x + v1.y * v2.y;

    public static Vector2D operator checked /(Vector2D v, float scalar) => checked(new(v.x / scalar, v.y / scalar));
    public static Vector2D operator /(Vector2D v, float scalar) => new(v.x / scalar, v.y / scalar);

    public static Vector2D operator checked -(Vector2D v) => checked(new(-v.x, -v.y));
    public static Vector2D operator +(Vector2D v) => new(v.x, v.y);
    public static Vector2D operator -(Vector2D v) => new(-v.x, -v.y);

    public static implicit operator Vector3D(Vector2D v) => new Vector3D(v);

    // Equality
    public static bool operator ==(Vector2D v1, Vector2D v2) => v1.x == v2.x && v1.y == v2.y;
    public static bool operator !=(Vector2D v1, Vector2D v2) => !(v1 == v2);

    public readonly bool Equals(Vector2D v) => this == v;
    public override readonly bool Equals(object obj) => obj is Vector2D v && this == v;

    public override readonly int GetHashCode() => (x, y).GetHashCode();

    public readonly bool ApproxEquals(Vector2D v, float epsilon = Settings.epsilon) => x.ApproxEquals(v.x, epsilon) && y.ApproxEquals(v.y, epsilon);

    #endregion
}