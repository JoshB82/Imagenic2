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
        this.invDirection = new Vector3D(1 / direction.x, 1 / direction.y, 1 / direction.z);
    }

    #endregion

    #region Methods

    internal readonly bool IntersectTriangle(Triangle triangle, out float distance)
    {
        Vector3D edge1 = (Vector3D)(triangle.TransformedP2 - triangle.TransformedP1);
        Vector3D edge2 = (Vector3D)(triangle.TransformedP3 - triangle.TransformedP1);

        Vector3D cross1 = direction.CrossProduct(edge2);
        float dot = edge1 * cross1;

        if (Abs(dot).ApproxLessThan(0, epsilon))
        {
            distance = 0;
            return false;
        }

        float i = 1 / dot;
        Vector3D s = startPosition - (Vector3D)(triangle.TransformedP1);
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
        float t1 = (boundingBox.corner1.x - startPosition.x) * invDirection.x;
        float t2 = (boundingBox.corner2.x - startPosition.x) * invDirection.x;

        float tMin = Min(t1, t2);
        float tMax = Max(t1, t2);

        t1 = (boundingBox.corner1.y - startPosition.y) * invDirection.y;
        t2 = (boundingBox.corner2.y - startPosition.y) * invDirection.y;

        tMin = Max(tMin, Min(t1, t2));
        tMax = Min(tMax, Max(t1, t2));

        t1 = (boundingBox.corner1.z - startPosition.z) * invDirection.z;
        t2 = (boundingBox.corner2.z - startPosition.z) * invDirection.z;

        tMin = Max(tMin, Min(t1, t2));
        tMax = Min(tMax, Max(t1, t2));

        if (tMax.ApproxLessThan(0, epsilon) || tMin.ApproxMoreThan(tMax, epsilon))
        {
            distance = 0;
            return false;
        }

        distance = tMin;
        return true;
    }

    #endregion
}