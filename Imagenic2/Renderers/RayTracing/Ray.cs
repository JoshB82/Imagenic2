using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.RayTracing;

public struct Ray
{
    #region Fields and Properties

    public Vector3D startPosition;
    public Vector3D direction;
    public Vector3D invDirection;

    private const float epsilon = 1e-6f;

    #endregion

    #region Constructors

    public Ray(Vector3D startPosition, Vector3D direction)
    {
        this.startPosition = startPosition;
        this.direction = direction;
        this.invDirection = new Vector3D(
            DivisionByZeroCheck(direction.x),
            DivisionByZeroCheck(direction.y),
            DivisionByZeroCheck(direction.z)
        );
    }

    private static float DivisionByZeroCheck(float num) => num.ApproxEquals(0, epsilon) ? float.PositiveInfinity : 1 / num;

    #endregion

    #region Methods

    internal readonly bool IntersectTriangle(Triangle triangle, out float distance)
    {
        Vector3D edge1 = triangle.P2.WorldOrigin - triangle.P1.WorldOrigin;
        Vector3D edge2 = triangle.P3.WorldOrigin - triangle.P1.WorldOrigin;

        Vector3D cross1 = direction.CrossProduct(edge2);
        float dot = edge1 * cross1;

        if (Abs(dot).ApproxLessThan(0, epsilon))
        {
            distance = 0;
            return false;
        }

        float i = 1 / dot;
        Vector3D s = startPosition - triangle.P1.WorldOrigin;
        float u = i * (s * cross1);
        if (u.ApproxLessThan(0, epsilon) || u.ApproxMoreThan(1, epsilon))
        {
            distance = 0;
            return false;
        }

        Vector3D cross2 = s.CrossProduct(edge1);

        float v = i * (direction * cross2);
        if (v.ApproxLessThan(0, epsilon) || (u + v).ApproxMoreThan(1, epsilon))
        {
            distance = 0;
            return false;
        }

        distance = edge2 * cross2 * i;

        return distance.ApproxMoreThan(0, epsilon);
    }

    internal readonly bool IntersectBoundingBox(BoundingBox boundingBox, out float distance)
    {
        float tMin = float.NegativeInfinity;
        float tMax = float.PositiveInfinity;

        float t1, t2;

        if (direction.x.ApproxEquals(0, epsilon))
        {
            if (startPosition.x < boundingBox.corner1.x || startPosition.x > boundingBox.corner2.x)
            {
                distance = 0;
                return false;
            }
        }
        else
        {
            t1 = (boundingBox.corner1.x - startPosition.x) * invDirection.x;
            t2 = (boundingBox.corner2.x - startPosition.x) * invDirection.x;

            float tNear = Min(t1, t2);
            float tFar = Max(t1, t2);

            tMin = Max(tMin, tNear);
            tMax = Min(tMax, tFar);
        }

        if (direction.y.ApproxEquals(0, epsilon))
        {
            if (startPosition.y < boundingBox.corner1.y || startPosition.y > boundingBox.corner2.y)
            {
                distance = 0;
                return false;
            }
        }
        else
        {
            t1 = (boundingBox.corner1.y - startPosition.y) * invDirection.y;
            t2 = (boundingBox.corner2.y - startPosition.y) * invDirection.y;

            float tNear = Min(t1, t2);
            float tFar = Max(t1, t2);

            tMin = Max(tMin, tNear);
            tMax = Min(tMax, tFar);
        }

        if (direction.z.ApproxEquals(0, epsilon))
        {
            if (startPosition.z < boundingBox.corner1.z || startPosition.z > boundingBox.corner2.z)
            {
                distance = 0;
                return false;
            }
        }
        else
        {
            t1 = (boundingBox.corner1.z - startPosition.z) * invDirection.z;
            t2 = (boundingBox.corner2.z - startPosition.z) * invDirection.z;

            float tNear = Min(t1, t2);
            float tFar = Max(t1, t2);

            tMin = Max(tMin, tNear);
            tMax = Min(tMax, tFar);
        }

        if (tMax.ApproxLessThan(0, epsilon) || tMin.ApproxMoreThan(tMax, epsilon))
        {
            distance = 0;
            return false;
        }

        distance = tMin > 0 ? tMin : tMax;
        return true;
    }

    #endregion
}