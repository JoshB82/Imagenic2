using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.RayTracing;

public struct Ray
{
    #region Fields and Properties

    public Vector3D startPosition;
    public Vector3D direction;

    #endregion

    #region Constructors

    public Ray(Vector3D startPosition, Vector3D direction)
    {
        this.startPosition = startPosition;
        this.direction = direction;
    }

    #endregion

    #region Methods

    internal readonly bool IntersectTriangle(Triangle triangle, out float distance)
    {
        Vector3D edge1 = (Vector3D)(triangle.TransformedP2 - triangle.TransformedP1);
        Vector3D edge2 = (Vector3D)(triangle.TransformedP3 - triangle.TransformedP1);

        Vector3D cross1 = direction.CrossProduct(edge2);
        float dot = edge1 * cross1;

        if (Abs(dot).ApproxLessThan(0))
        {
            distance = 0;
            return false;
        }

        float i = 1 / dot;
        Vector3D s = startPosition - (Vector3D)(triangle.TransformedP1);
        float u = i * (s * cross1);
        if (u.ApproxLessThan(0) || u.ApproxMoreThan(1))
        {
            distance = 0;
            return false;
        }

        Vector3D cross2 = s.CrossProduct(edge1);

        float v = i * (direction * cross2);
        if (v.ApproxLessThan(0) || (u + v).ApproxMoreThan(1))
        {
            distance = 0;
            return false;
        }

        distance = edge2 * cross2 * i;

        return distance.ApproxMoreThan(0);
    }

    internal readonly bool IntersectBoundingBox(BoundingBox boundingBox, out float distance)
    {
        float t1 = (boundingBox.corner1.x - startPosition.x) / direction.x;
        float t2 = (boundingBox.corner2.x - startPosition.x) / direction.x;

        float tMin = Min(t1, t2);
        float tMax = Max(t1, t2);

        t1 = (boundingBox.corner1.y - startPosition.y) / direction.y;
        t2 = (boundingBox.corner2.y - startPosition.y) / direction.y;

        tMin = Max(tMin, Min(t1, t2));
        tMax = Min(tMax, Max(t1, t2));

        t1 = (boundingBox.corner1.z - startPosition.z) / direction.z;
        t2 = (boundingBox.corner2.z - startPosition.z) / direction.z;

        tMin = Max(tMin, Min(t1, t2));
        tMax = Min(tMax, Max(t1, t2));

        if (tMax < 0 || tMin > tMax)
        {
            distance = 0;
            return false;
        }

        distance = tMin;
        return true;
    }

    #endregion
}