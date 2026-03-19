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

    internal bool IntersectTriangle(Triangle triangle, out float distance)
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

    #endregion
}