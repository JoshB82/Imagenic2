namespace Imagenic2.Core.Maths.Vectors;

public struct Vector4D
{
    #region Fields and Methods

    public static readonly Vector4D Zero = new();
    public static readonly Vector4D One = new(1, 1, 1, 1);
    public static readonly Vector4D NegativeOne = new(-1, -1, -1, -1);
    public static readonly Vector4D UnitX = new(1, 0, 0, 0);
    public static readonly Vector4D UnitY = new(0, 1, 0, 0);
    public static readonly Vector4D UnitZ = new(0, 0, 1, 0);
    public static readonly Vector4D UnitW = new(0, 0, 0, 1);
    public static readonly Vector4D UnitNegativeX = new(-1, 0, 0, 0);
    public static readonly Vector4D UnitNegativeY = new(0, -1, 0, 0);
    public static readonly Vector4D UnitNegativeZ = new(0, 0, -1, 0);
    public static readonly Vector4D UnitNegativeW = new(0, 0, 0, -1);

    public float x;
    public float y;
    public float z;
    public float w;

    #endregion

    #region Constructors

    public Vector4D(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Vector4D(Vector3D v, float w = 0)
    {
        x = v.x;
        y = v.y;
        z = v.z;
        this.w = w;
    }

    public Vector4D(Vector2D v, float z = 0, float w = 0)
    {
        x = v.x;
        y = v.y;
        this.z = z;
        this.w = w;
    }

    #endregion

    #region Methods

    public readonly bool IsZero(float epsilon = float.Epsilon) => ApproxEquals(Zero, epsilon);

    public readonly float Angle(Vector4D v, float epsilon = float.Epsilon)
    {
        if (ApproxEquals(Zero, epsilon))
        {
            throw new InvalidOperationException();
        }
        if (v.ApproxEquals(Zero, epsilon))
        {
            throw new InvalidOperationException();
        }

        float quotient = this * v / Sqrt(SquaredMagnitude() * v.SquaredMagnitude());
        return Acos(quotient.Clamp(-1, 1));
    }
    public readonly bool TryGetAngle(Vector4D v, out float angle, float epsilon = float.Epsilon)
    {
        angle = 0;
        if (ApproxEquals(Zero, epsilon) || v.ApproxEquals(Zero, epsilon))
        {
            return false;
        }
        float quotient = this * v / Sqrt(SquaredMagnitude() * v.SquaredMagnitude());
        angle = Acos(quotient.Clamp(-1, 1));
        return true;
    }

    public readonly Vector4D Normalise(float epsilon = float.Epsilon)
    {
        if (ApproxEquals(Zero, epsilon))
        {
            throw new InvalidOperationException();
        }
        return this / Magnitude();
    }
    public readonly bool TryNormalise(out Vector4D v, float epsilon = float.Epsilon)
    {
        v = Zero;
        if (ApproxEquals(Zero, epsilon))
        {
            return false;
        }
        v = this / Magnitude();
        return true;
    }

    public readonly float Magnitude() => Sqrt(SquaredMagnitude());
    public readonly float SquaredMagnitude() => x * x + y * y + z * z + w * w;
    public override int GetHashCode() => (x, y, z, w).GetHashCode();
    public override readonly string ToString() => $"({x}, {y}, {z}, {w})";
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)}, {w.ToString(format, formatProvider)})";
    public static Vector4D operator checked +(Vector4D v1, Vector4D v2) => checked(new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w));
    public static Vector4D operator +(Vector4D v1, Vector4D v2) => new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);

    public static Vector4D operator checked +(Vector4D v1, Vector3D v2) => checked(new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w));
    public static Vector4D operator +(Vector4D v1, Vector3D v2) => new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w);

    public static Vector4D operator checked -(Vector4D v1, Vector4D v2) => checked(new(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w));
    public static Vector4D operator -(Vector4D v1, Vector4D v2) => new(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);

    public static float operator checked *(Vector4D v1, Vector4D v2) => checked(v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w);
    public static float operator *(Vector4D v1, Vector4D v2) => v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w;

    public static Vector4D operator checked *(Vector4D v, float scalar) => checked(new(v.x * scalar, v.y * scalar, v.z * scalar, v.w * scalar));
    public static Vector4D operator *(Vector4D v, float scalar) => new(v.x * scalar, v.y * scalar, v.z * scalar, v.w * scalar);

    public static Vector4D operator checked *(float scalar, Vector4D v) => checked(v * scalar);
    public static Vector4D operator *(float scalar, Vector4D v) => v * scalar;

    public static Vector4D operator checked /(Vector4D v, float scalar) => checked(new(v.x / scalar, v.y / scalar, v.z / scalar, v.w / scalar));
    public static Vector4D operator /(Vector4D v, float scalar) => new(v.x / scalar, v.y / scalar, v.z / scalar, v.w / scalar);

    public static Vector4D operator checked -(Vector4D v) => checked(new(-v.x, -v.y, -v.z, -v.w));
    public static Vector4D operator -(Vector4D v) => new(-v.x, -v.y, -v.z, -v.w);

    public static bool operator ==(Vector4D v1, Vector4D v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
    public static bool operator !=(Vector4D v1, Vector4D v2) => !(v1 == v2);
    public readonly bool Equals(Vector4D v) => this == v;
    public override readonly bool Equals(object obj) => this == (Vector4D)obj;
    public readonly bool ApproxEquals(Vector4D v, float epsilon = float.Epsilon) =>
        x.ApproxEquals(v.x, epsilon) && y.ApproxEquals(v.y, epsilon) &&
        z.ApproxEquals(v.z, epsilon) && w.ApproxEquals(v.w, epsilon);

    public static implicit operator Vector4D(Vector3D v) => new(v);
    public static explicit operator Vector3D(Vector4D v) => new(v.x, v.y, v.z);

    #endregion
}