using Imagenic2.Core.Entities;
using static Imagenic2.Core.Maths.NumberHelpers;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private static void ClipTriangles(Queue<Triangle> triangleQueue, ClippingPlane[] clippingPlanes, Action<Triangle, Vector3D, Vector3D> clipTriangle)
    {
        foreach (ClippingPlane clippingPlane in clippingPlanes)
        {
            int noTrianglesRemaining = triangleQueue.Count;

            while (noTrianglesRemaining-- > 0)
            {
                var triangle = triangleQueue.Dequeue();
                clipTriangle(triangle, clippingPlane.Point, clippingPlane.Normal);
            }
        }
    }

    private void ShadowMapClipTriangle(Triangle triangle, Vector3D planePoint, Vector3D planeNormal)
    {
        Vector3D p1 = (Vector3D)(triangle.TransformedP1);
        Vector3D p2 = (Vector3D)(triangle.TransformedP2);
        Vector3D p3 = (Vector3D)(triangle.TransformedP3);

        bool isP1Inside = Vector3D.PointDistanceFromPlane(p1, planePoint, planeNormal) >= 0;
        bool isP2Inside = Vector3D.PointDistanceFromPlane(p2, planePoint, planeNormal) >= 0;
        bool isP3Inside = Vector3D.PointDistanceFromPlane(p3, planePoint, planeNormal) >= 0;

        int mask = (isP1Inside ? 1 : 0) |
                    (isP2Inside ? 2 : 0) |
                    (isP3Inside ? 4 : 0);

        Vector4D intersection1, intersection2;
        Triangle triangle1 = triangle.DeepCopy(), triangle2;

        switch (mask)
        {
            case 0:
                // Triangle is completely outside
                break;
                
            // One point inside
            case 1:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out _), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out _), 1);

                triangle1.TransformedP1 = new Vector4D(p1, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = intersection2;
                    
                triangleQueue.Enqueue(triangle1);
                break;
            case 2:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out _), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out _), 1);
                
                triangle1.TransformedP1 = new Vector4D(p2, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = intersection2;

                triangleQueue.Enqueue(triangle1);
                break;
            case 4:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out _), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out _), 1);
                    
                triangle1.TransformedP1 = new Vector4D(p3, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = intersection2;

                triangleQueue.Enqueue(triangle1);
                break;
                
            // Two points inside
            case 3:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out _), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out _), 1);

                triangle1.TransformedP1 = new Vector4D(p1, 1);
                triangle1.TransformedP2 = new Vector4D(p2, 1);
                triangle1.TransformedP3 = intersection1;

                triangle2 = triangle.DeepCopy();
                triangle2.TransformedP1 = new Vector4D(p2, 1);
                triangle2.TransformedP2 = intersection2;
                triangle2.TransformedP3 = intersection1;

                triangleQueue.Enqueue(triangle1);
                triangleQueue.Enqueue(triangle2);
                break;
            case 5:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out _), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out _), 1);

                triangle1.TransformedP1 = new Vector4D(p1, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = new Vector4D(p3, 1);

                triangle2 = triangle.DeepCopy();
                triangle2.TransformedP1 = new Vector4D(p3, 1);
                triangle2.TransformedP2 = intersection1;
                triangle2.TransformedP3 = intersection2;

                triangleQueue.Enqueue(triangle1);
                triangleQueue.Enqueue(triangle2);
                break;
            case 6:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out _), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out _), 1);

                triangle1.TransformedP1 = new Vector4D(p2, 1);
                triangle1.TransformedP2 = new Vector4D(p3, 1);
                triangle1.TransformedP3 = intersection1;

                triangle2 = triangle.DeepCopy();
                triangle2.TransformedP1 = new Vector4D(p3, 1);
                triangle2.TransformedP2 = intersection2;
                triangle2.TransformedP3 = intersection1;

                triangleQueue.Enqueue(triangle1);
                triangleQueue.Enqueue(triangle2);
                break;
            case 7:
                // Triangle is completely inside
                triangleQueue.Enqueue(triangle);
                break;
        }
    }

    private void ClipTriangle(Triangle triangle, Vector3D planePoint, Vector3D planeNormal)
    {
        Vector3D p1 = (Vector3D)(triangle.TransformedP1);
        Vector3D p2 = (Vector3D)(triangle.TransformedP2);
        Vector3D p3 = (Vector3D)(triangle.TransformedP3);

        Vector2D t1 = triangle.TransformedTextureP1;
        Vector2D t2 = triangle.TransformedTextureP2;
        Vector2D t3 = triangle.TransformedTextureP3;

        bool isP1Inside = Vector3D.PointDistanceFromPlane(p1, planePoint, planeNormal) >= 0;
        bool isP2Inside = Vector3D.PointDistanceFromPlane(p2, planePoint, planeNormal) >= 0;
        bool isP3Inside = Vector3D.PointDistanceFromPlane(p3, planePoint, planeNormal) >= 0;

        int mask = (isP1Inside ? 1 : 0) |
                    (isP2Inside ? 2 : 0) |
                    (isP3Inside ? 4 : 0);

        Vector4D intersection1, intersection2;
        Triangle triangle1 = triangle.DeepCopy(), triangle2;
        float d1, d2;

        switch (mask)
        {
            case 0:
                // Triangle is completely outside
                break;

            // One point inside
            case 1:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out d1), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out d2), 1);

                triangle1.TransformedP1 = new Vector4D(p1, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = intersection2;
                triangle1.TransformedTextureP1 = t1;
                triangle1.TransformedTextureP2 = Interpolate(t1, t2, d1);
                triangle1.TransformedTextureP3 = Interpolate(t1, t3, d2);

                triangleQueue.Enqueue(triangle1);
                break;
            case 2:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out d1), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out d2), 1);

                triangle1.TransformedP1 = new Vector4D(p2, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = intersection2;
                triangle1.TransformedTextureP1 = t2;
                triangle1.TransformedTextureP2 = Interpolate(t2, t3, d1);
                triangle1.TransformedTextureP3 = Interpolate(t2, t1, d2);

                triangleQueue.Enqueue(triangle1);
                break;
            case 4:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out d1), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out d2), 1);

                triangle1.TransformedP1 = new Vector4D(p3, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = intersection2;
                triangle1.TransformedTextureP1 = t3;
                triangle1.TransformedTextureP2 = Interpolate(t3, t1, d1);
                triangle1.TransformedTextureP3 = Interpolate(t3, t2, d2);

                triangleQueue.Enqueue(triangle1);
                break;

            // Two points inside
            case 3:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out d1), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out d2), 1);

                Vector2D t13 = Interpolate(t1, t3, d1);
                Vector2D t23 = Interpolate(t2, t3, d2);

                triangle1.TransformedP1 = new Vector4D(p1, 1);
                triangle1.TransformedP2 = new Vector4D(p2, 1);
                triangle1.TransformedP3 = intersection1;
                triangle1.TransformedTextureP1 = t1;
                triangle1.TransformedTextureP2 = t2;
                triangle1.TransformedTextureP3 = t13;

                triangle2 = triangle.DeepCopy();
                triangle2.TransformedP1 = new Vector4D(p2, 1);
                triangle2.TransformedP2 = intersection2;
                triangle2.TransformedP3 = intersection1;
                triangle2.TransformedTextureP1 = t2;
                triangle2.TransformedTextureP2 = t23;
                triangle2.TransformedTextureP3 = t13;

                triangleQueue.Enqueue(triangle1);
                triangleQueue.Enqueue(triangle2);
                break;
            case 5:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out d1), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out d2), 1);

                Vector2D t12 = Interpolate(t1, t2, d1);
                Vector2D t32 = Interpolate(t3, t2, d2);

                triangle1.TransformedP1 = new Vector4D(p1, 1);
                triangle1.TransformedP2 = intersection1;
                triangle1.TransformedP3 = new Vector4D(p3, 1);
                triangle1.TransformedTextureP1 = t1;
                triangle1.TransformedTextureP2 = t12;
                triangle1.TransformedTextureP3 = t3;

                triangle2 = triangle.DeepCopy();
                triangle2.TransformedP1 = new Vector4D(p3, 1);
                triangle2.TransformedP2 = intersection1;
                triangle2.TransformedP3 = intersection2;
                triangle2.TransformedTextureP1 = t3;
                triangle2.TransformedTextureP2 = t12;
                triangle2.TransformedTextureP3 = t32;

                triangleQueue.Enqueue(triangle1);
                triangleQueue.Enqueue(triangle2);
                break;
            case 6:
                intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out d1), 1);
                intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out d2), 1);

                Vector2D t21 = Interpolate(t2, t1, d1);
                Vector2D t31 = Interpolate(t3, t1, d2);

                triangle1.TransformedP1 = new Vector4D(p2, 1);
                triangle1.TransformedP2 = new Vector4D(p3, 1);
                triangle1.TransformedP3 = intersection1;
                triangle1.TransformedTextureP1 = t2;
                triangle1.TransformedTextureP2 = t3;
                triangle1.TransformedTextureP3 = t21;

                triangle2 = triangle.DeepCopy();
                triangle2.TransformedP1 = new Vector4D(p3, 1);
                triangle2.TransformedP2 = intersection2;
                triangle2.TransformedP3 = intersection1;
                triangle2.TransformedTextureP1 = t3;
                triangle2.TransformedTextureP2 = t31;
                triangle2.TransformedTextureP3 = t21;

                triangleQueue.Enqueue(triangle1);
                triangleQueue.Enqueue(triangle2);
                break;
            case 7:
                // Triangle is completely inside
                triangleQueue.Enqueue(triangle);
                break;
        }
    }
}