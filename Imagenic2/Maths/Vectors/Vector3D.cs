namespace Imagenic2.Core.Maths.Vectors;

public struct Vector3D : IEquatable<Vector3D>
{
    #region Fields and Properties

    public static readonly Vector3D Zero = new(0, 0, 0);
    public static readonly Vector3D One = new(1, 1, 1);
    public static readonly Vector3D NegativeOne = new(-1, -1, -1);
    public static readonly Vector3D UnitX = new(1, 0, 0);
    public static readonly Vector3D UnitY = new(0, 1, 0);
    public static readonly Vector3D UnitZ = new(0, 0, 1);
    public static readonly Vector3D UnitNegativeX = new(-1, 0, 0);
    public static readonly Vector3D UnitNegativeY = new(0, -1, 0);
    public static readonly Vector3D UnitNegativeZ = new(0, 0, -1);

    public float x;
    public float y;
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

    public readonly Vector3D CrossProduct(Vector3D v) => new Vector3D(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);

    public readonly float Magnitude() => Sqrt(SquaredMagnitude());
    public readonly float SquaredMagnitude() => x * x + y * y + z * z;

    public override readonly string ToString() => $"({x}, {y}, {z})";
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)})";
    
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
    public static Vector3D operator -(Vector3D v) => new(-v.x, -v.y, -v.z);

    public static bool operator ==(Vector3D v1, Vector3D v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;

    public static bool operator !=(Vector3D v1, Vector3D v2) => !(v1 == v2);

    public readonly bool Equals(Vector3D v) => this == v;

    public readonly bool ApproxEquals(Vector3D v, float epsilon = float.Epsilon) => x.ApproxEquals(v.x, epsilon) && y.ApproxEquals(v.y, epsilon) && z.ApproxEquals(v.z, epsilon);

    public override readonly bool Equals(object obj) => this == (Vector3D)obj;

    public override int GetHashCode() => (x, y, z).GetHashCode();

    #endregion
}