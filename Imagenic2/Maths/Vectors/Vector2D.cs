namespace Imagenic2.Core.Maths.Vectors;

public struct Vector2D : IApproximatelyEquatable<Vector2D>
{
    #region Fields and Properties

    public static readonly Vector2D Zero = new(0, 0);
    public static readonly Vector2D One = new(1, 1);
    public static readonly Vector2D UnitX = new(1, 0);
    public static readonly Vector2D UnitY = new(0, 1);
    public static readonly Vector2D UnitNegativeX = new(-1, 0);
    public static readonly Vector2D UnitNegativeY = new(0, -1);

    public float x;
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

    public readonly bool IsZero(float epsilon = float.Epsilon) => ApproxEquals(Zero, epsilon);

    public readonly float Angle(Vector2D v, float epsilon = float.Epsilon)
    {
        if (IsZero(epsilon))
        {
            throw new InvalidOperationException();
        }
        if (v.IsZero(epsilon))
        {
            throw new InvalidOperationException();
        }
        float quotient = this * v / Sqrt(SquaredMagnitude() * v.SquaredMagnitude());
        return Acos(quotient.Clamp(-1, 1));
    }

    public readonly bool TryGetAngle(Vector2D v, out float angle, float epsilon = float.Epsilon)
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

    public readonly Vector2D Normalise(float epsilon = float.Epsilon)
    {
        if (IsZero(epsilon))
        {
            throw new InvalidOperationException();
        }
        return this / Magnitude();
    }

    public readonly bool TryNormalise(out Vector2D v, float epsilon = float.Epsilon)
    {
        v = Zero;
        if (IsZero(epsilon))
        {
            return false;
        }
        v = this / Magnitude();
        return true;
    }

    public readonly float Magnitude() => Sqrt(SquaredMagnitude());

    public readonly float SquaredMagnitude() => x * x + y * y;

    public readonly override string ToString() => $"({x}, {y})";
    public string ToString(string? format, IFormatProvider? formatProvider) => $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";

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
    public static Vector2D operator -(Vector2D v) => new(-v.x, -v.y);

    public static bool operator ==(Vector2D v1, Vector2D v2) => v1.x == v2.x && v1.y == v2.y;

    public static bool operator !=(Vector2D v1, Vector2D v2) => !(v1 == v2);

    public readonly bool Equals(Vector2D v) => this == v;

    public readonly bool ApproxEquals(Vector2D v, float epsilon = float.Epsilon) => x.ApproxEquals(v.x, epsilon) && y.ApproxEquals(v.y, epsilon);

    public override readonly bool Equals(object obj) => this == (Vector2D)obj;

    public override int GetHashCode() => (x, y).GetHashCode();

    public static implicit operator Vector3D(Vector2D v) => new(v);

    #endregion
}