using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private static bool ClipEdge(Edge edge, ClippingPlane[] clippingPlanes)
    {
        foreach (ClippingPlane clippingPlane in clippingPlanes)
        {
            Vector3D p1 = (Vector3D)(edge.TransformedP1);
            Vector3D p2 = (Vector3D)(edge.TransformedP2);
            bool p1Inside = Vector3D.PointDistanceFromPlane(p1, clippingPlane.Point, clippingPlane.Normal) >= 0;
            bool p2Inside = Vector3D.PointDistanceFromPlane(p2, clippingPlane.Point, clippingPlane.Normal) >= 0;

            if (p1Inside && p2Inside)
            {
                // Both points are inside; don't change edge
            }
            else if (p1Inside)
            {
                // One point is inside; change edge
                edge.TransformedP2 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, clippingPlane.Point, clippingPlane.Normal, out float _), 1);
            }
            else if (p2Inside)
            {
                // One point is inside; change edge
                edge.TransformedP1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, clippingPlane.Point, clippingPlane.Normal, out float _), 1);
            }
            else
            {
                // No points are inside; indicate that edge should not be displayed
                return false;
            }
        }

        return true;
    }

    /*
    private static void ClipTriangles(Queue<Triangle> triangleQueue, ClippingPlane[] clippingPlanes)
    {
        foreach (ClippingPlane clippingPlane in clippingPlanes)
        {
            int noTrianglesRemaining = triangleQueue.Count;

            while (noTrianglesRemaining-- > 0)
            {
                var triangle = triangleQueue.Dequeue();
                ClipTriangle(triangleQueue, triangle, clippingPlane.Point, clippingPlane.Normal);
            }
        }

        void ClipTriangle(Queue<Triangle> triangleQueue, Triangle triangle, Vector3D planePoint, Vector3D planeNormal)
        {
            Vector3D[] insidePoints = Vector3DArrayPool.Rent(3), outsidePoints = Vector3DArrayPool.Rent(3);
            int insidePointCount = 0, outsidePointCount = 0;

            Vector3D p1 = (Vector3D)(triangle.TransformedP1);
            Vector3D p2 = (Vector3D)(triangle.TransformedP2);
            Vector3D p3 = (Vector3D)(triangle.TransformedP3);

            // Determine what vertices of the Triangle are inside and outside.
            if (Vector3D.PointDistanceFromPlane(p1, planePoint, planeNormal) >= 0)
            {
                insidePoints[insidePointCount++] = p1;
            }
            else
            {
                outsidePoints[outsidePointCount++] = p1;
            }

            if (Vector3D.PointDistanceFromPlane(p2, planePoint, planeNormal) >= 0)
            {
                insidePoints[insidePointCount++] = p2;
            }
            else
            {
                outsidePoints[outsidePointCount++] = p2;
            }

            if (Vector3D.PointDistanceFromPlane(p3, planePoint, planeNormal) >= 0)
            {
                insidePoints[insidePointCount++] = p3;
            }
            else
            {
                outsidePoints[outsidePointCount++] = p3;
            }

            switch (insidePointCount)
            {
                case 0:
                    // All points are on the outside, so no valid triangles to enqueue
                    break;
                case 1:
                    // One point is on the inside, so only a smaller triangle is needed
                    var intersection1 = new Vector4D(Vector3D.LineIntersectPlane(insidePoints[0], outsidePoints[0], planePoint, planeNormal, out float d1), 1);
                    var intersection2 = new Vector4D(Vector3D.LineIntersectPlane(insidePoints[0], outsidePoints[1], planePoint, planeNormal, out float d2), 1);

                    triangle.TransformedP1 = new Vector4D(insidePoints[0], 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangleQueue.Enqueue(triangle);

                    break;
                case 2:
                    // Two points are on the inside, so a quadrilateral is formed and split into two triangles
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(insidePoints[0], outsidePoints[0], planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(insidePoints[1], outsidePoints[0], planePoint, planeNormal, out d2), 1);

                    triangle.TransformedP1 = new Vector4D(insidePoints[0], 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = new Vector4D(insidePoints[1], 1);

                    var triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(insidePoints[1], 1);
                    triangle2.TransformedP2 = intersection1;
                    triangle2.TransformedP3 = intersection2;

                    triangleQueue.Enqueue(triangle);
                    triangleQueue.Enqueue(triangle2);
                    break;
                case 3:
                    // All points are on the inside, so enqueue the triangle unchanged
                    triangleQueue.Enqueue(triangle);
                    break;
            }

            Vector3DArrayPool.Return(insidePoints, true);
            Vector3DArrayPool.Return(outsidePoints, true);
        }
    }
    */
}