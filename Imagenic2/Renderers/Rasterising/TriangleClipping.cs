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

    private struct ClipVertex
    {
        public Vector4D position;
        public Vector4D viewPosition;
        public Vector2D texturePosition;
        public float invW;
    }

    private List<Triangle> ClipTriangle2D(Triangle triangle, float bottom, float left, float right, float top)
    {
        List<Triangle> clippedTriangles = new List<Triangle>();

        int pointCount = 3;
        ClipVertex[] polygonPoints1 = new ClipVertex[8];
        ClipVertex[] polygonPoints2 = new ClipVertex[8];
        polygonPoints1[0] = new ClipVertex() { position = triangle.TransformedP1, texturePosition = triangle.TransformedTextureP1, invW = triangle.invW1, viewPosition = triangle.ViewSpaceP1 };
        polygonPoints1[1] = new ClipVertex() { position = triangle.TransformedP2, texturePosition = triangle.TransformedTextureP2, invW = triangle.invW2, viewPosition = triangle.ViewSpaceP2 };
        polygonPoints1[2] = new ClipVertex() { position = triangle.TransformedP3, texturePosition = triangle.TransformedTextureP3, invW = triangle.invW3, viewPosition = triangle.ViewSpaceP3 };

        static ClipVertex Interpolate(ClipVertex v1, ClipVertex v2, float t)
        {
            float invW = (v2.invW - v1.invW) * t + v1.invW;
            Vector4D position = (v2.position - v1.position) * t + v1.position;
            //Vector2D texturePosition = (v2.texturePosition - v1.texturePosition) * t + v1.texturePosition;
            //Vector4D viewPosition = (v2.viewPosition - v1.viewPosition) * t + v1.viewPosition;

            //float invW = (v2.invW - v1.invW) * t + v1.invW;
            Vector2D texturePosition = (v1.texturePosition * v1.invW + t * ((v2.texturePosition * v2.invW) - (v1.texturePosition * v1.invW))) / invW;
            //Vector4D position = (v1.position * v1.invW + t * ((v2.position * v2.invW) - (v1.position * v1.invW))) / invW;
            Vector4D viewPosition = (v1.viewPosition * v1.invW + t * ((v2.viewPosition * v2.invW) - (v1.viewPosition * v1.invW))) / invW;

            return new ClipVertex()
            {
                position = position,
                viewPosition = viewPosition,
                texturePosition = texturePosition,
                invW = invW
            };
        }

        static int ClipBoundary(ClipVertex[] inputPoints, int numberOfInputPoints,
                         ClipVertex[] outputPoints,
                         Func<ClipVertex, bool> isInside,
                         Func<ClipVertex, ClipVertex, ClipVertex> calculateIntersection)
        {
            int numberOfOutputPoints = 0;
            ClipVertex previous = inputPoints[numberOfInputPoints - 1];
            bool previousInside = isInside(previous);

            for (int i = 0; i < numberOfInputPoints; i++)
            {
                ClipVertex current = inputPoints[i];
                bool currentInside = isInside(current);

                if (currentInside)
                {
                    if (!previousInside)
                    {
                        outputPoints[numberOfOutputPoints++] = calculateIntersection(previous, current);
                    }

                    outputPoints[numberOfOutputPoints++] = current;
                }
                else if (previousInside)
                {
                    outputPoints[numberOfOutputPoints++] = calculateIntersection(previous, current);
                }

                previous = current;
                previousInside = currentInside;
            }

            return numberOfOutputPoints;
        }

        // Clip against bottom edge
        pointCount = ClipBoundary(polygonPoints1, pointCount, polygonPoints2,
                                  v => v.position.y <= bottom,
                                  (v1,v2) =>
                                  {
                                      float t = (bottom - v1.position.y) / (v2.position.y - v1.position.y);
                                      return Interpolate(v1, v2, t);
                                  });

        if (pointCount == 0) return clippedTriangles;

        // Clip against left edge
        pointCount = ClipBoundary(polygonPoints2, pointCount, polygonPoints1,
                                  v => v.position.x >= left,
                                  (v1, v2) =>
                                  {
                                      float t = (left - v1.position.x) / (v2.position.x - v1.position.x);
                                      return Interpolate(v1, v2, t);
                                  });

        if (pointCount == 0) return clippedTriangles;

        // Clip against right edge
        pointCount = ClipBoundary(polygonPoints1, pointCount, polygonPoints2,
                                  v => v.position.x <= right,
                                  (v1, v2) =>
                                  {
                                      float t = (right - v1.position.x) / (v2.position.x - v1.position.x);
                                      return Interpolate(v1, v2, t);
                                  });

        if (pointCount == 0) return clippedTriangles;

        // Clip against top edge
        pointCount = ClipBoundary(polygonPoints2, pointCount, polygonPoints1,
                                  v => v.position.y >= top,
                                  (v1, v2) =>
                                  {
                                      float t = (top - v1.position.y) / (v2.position.y - v1.position.y);
                                      return Interpolate(v1, v2, t);
                                  });

        if (pointCount < 3) return clippedTriangles;

        for (int i = 1; i < pointCount - 1; i++)
        {
            Triangle t = new Triangle()
            {
                TransformedP1 = polygonPoints1[0].position,
                TransformedP2 = polygonPoints1[i].position,
                TransformedP3 = polygonPoints1[i + 1].position,
                TransformedTextureP1 = polygonPoints1[0].texturePosition,
                TransformedTextureP2 = polygonPoints1[i].texturePosition,
                TransformedTextureP3 = polygonPoints1[i + 1].texturePosition,
                invW1 = polygonPoints1[0].invW,
                invW2 = polygonPoints1[i].invW,
                invW3 = polygonPoints1[i + 1].invW,
                ViewSpaceP1 = polygonPoints1[0].viewPosition,
                ViewSpaceP2 = polygonPoints1[i].viewPosition,
                ViewSpaceP3 = polygonPoints1[i + 1].viewPosition
            };

            clippedTriangles.Add(t);
        }

        return clippedTriangles;
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