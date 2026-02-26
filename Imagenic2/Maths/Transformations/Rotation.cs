namespace Imagenic2.Core.Maths.Transformations;

public static partial class Transform
{
    /// <summary>
    /// Creates a <see cref="Matrix4x4"/> for rotation about the x-axis.
    /// </summary>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>A rotation <see cref="Matrix4x4"/>.</returns>
    public static Matrix4x4 RotateX(float angle)
    {
        Matrix4x4 rotation = Matrix4x4.Identity;
        if (angle == 0) return rotation;
        float sinAngle = Sin(angle), cosAngle = Cos(angle);
        rotation.m11 = cosAngle;
        rotation.m12 = sinAngle;
        rotation.m21 = -sinAngle;
        rotation.m22 = cosAngle;
        return rotation;
    }

    /// <summary>
    /// Creates a <see cref="Matrix4x4"/> for rotation about the y-axis.
    /// </summary>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>A rotation <see cref="Matrix4x4"/>.</returns>
    public static Matrix4x4 RotateY(float angle)
    {
        Matrix4x4 rotation = Matrix4x4.Identity;
        if (angle == 0) return rotation;
        float sinAngle = Sin(angle), cosAngle = Cos(angle);
        rotation.m00 = cosAngle;
        rotation.m02 = -sinAngle;
        rotation.m20 = sinAngle;
        rotation.m22 = cosAngle;
        return rotation;
    }

    /// <summary>
    /// Creates a <see cref="Matrix4x4"/> for rotation about the z-axis.
    /// </summary>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>A rotation <see cref="Matrix4x4"/>.</returns>
    public static Matrix4x4 RotateZ(float angle)
    {
        Matrix4x4 rotation = Matrix4x4.Identity;
        if (angle == 0) return rotation;
        float sinAngle = Sin(angle), cosAngle = Cos(angle);
        rotation.m00 = cosAngle;
        rotation.m01 = sinAngle;
        rotation.m10 = -sinAngle;
        rotation.m11 = cosAngle;
        return rotation;
    }

    /// <summary>
    /// Creates a<see cref= "Matrix4x4" /> for rotation about any axis.
    /// </summary>
    /// <param name="axis">Axis that will be rotated around.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>A rotation <see cref="Matrix4x4"/>.</returns>
    public static Matrix4x4 Rotate(Vector3D axis, float angle)
    {
        if (angle == 0) return Matrix4x4.Identity;
        axis = axis.Normalise();
        float sinAngle = Sin(angle), cosAngle = Cos(angle);
        return new
        (
            cosAngle + axis.x * axis.x * (1 - cosAngle), axis.x * axis.y * (1 - cosAngle) - axis.z * sinAngle, axis.x * axis.z * (1 - cosAngle) + axis.y * sinAngle, 0,
            axis.y * axis.x * (1 - cosAngle) + axis.z * sinAngle, cosAngle + axis.y * axis.y * (1 - cosAngle), axis.y * axis.z * (1 - cosAngle) - axis.x * sinAngle, 0,
            axis.z * axis.x * (1 - cosAngle) - axis.y * sinAngle, axis.z * axis.y * (1 - cosAngle) + axis.x * sinAngle, cosAngle + axis.z * axis.z * (1 - cosAngle), 0,
            0, 0, 0, 1
        );
    }

    /// <summary>
    /// Creates a rotation <see cref="Matrix4x4"/> that rotates one <see cref="Vector3D" /> onto another. A rotation axis must be supplied if <see cref="Vector3D" > Vector3Ds </ see > are antiparallel.
    /// </summary>
    /// <param name="v1">The first <see cref="Vector3D"/>.</param>
    /// <param name="v2">The second <see cref="Vector3D"/>.</param>
    /// <param name="axis">Axis that will be rotated around if <see cref="Vector3D">Vector3Ds</see> are antiparallel.</param>
    /// <returns>A rotation <see cref="Matrix4x4"/>.</returns>
    public static Matrix4x4 RotateBetweenVectors(Vector3D v1, Vector3D v2, Vector3D? axis = null)
    {
        if (v1.ApproxEquals(v2, 1E-6f)) return Matrix4x4.Identity;
        axis ??= Vector3D.UnitY;
        Vector3D rotationAxis = v1.ApproxEquals(-v2, 1E-6F) ? (Vector3D)axis : v1.CrossProduct(v2).Normalise();
        float angle = v1.Angle(v2);
        return Rotate(rotationAxis, angle);
    }
}