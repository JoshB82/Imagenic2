using Imagenic2.Core.Entities;
using System.Drawing.Text;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private static void ClipTriangles(Queue<Triangle> triangleQueue, ClippingPlane[] clippingPlanes)
    {
        foreach (ClippingPlane clippingPlane in clippingPlanes)
        {
            int noTrianglesRemaining = triangleQueue.Count;

            while (noTrianglesRemaining-- > 0)
            {
                var triangle = triangleQueue.Dequeue();
                ClipTriangle(triangle, clippingPlane.Point, clippingPlane.Normal);
            }
        }

        void ClipTriangle(Triangle triangle, Vector3D planePoint, Vector3D planeNormal)
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
            Triangle triangle2;

            switch (mask)
            {
                case 0:
                    // Triangle is completely outside
                    break;
                
                // One point inside
                case 1:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out float _), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out float _), 1);
                    triangle.TransformedP1 = new Vector4D(p1, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangleQueue.Enqueue(triangle);
                    break;
                case 2:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out float _), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out float _), 1);
                    triangle.TransformedP1 = new Vector4D(p2, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangleQueue.Enqueue(triangle);
                    break;
                case 4:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out _), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out _), 1);
                    triangle.TransformedP1 = new Vector4D(p3, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangleQueue.Enqueue(triangle);
                    break;
                
                // Two points inside
                case 3:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out _), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out _), 1);

                    triangle.TransformedP1 = new Vector4D(p1, 1);
                    triangle.TransformedP2 = new Vector4D(p2, 1);
                    triangle.TransformedP3 = intersection1;

                    triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(p2, 1);
                    triangle2.TransformedP2 = intersection2;
                    triangle2.TransformedP3 = intersection1;

                    triangleQueue.Enqueue(triangle);
                    triangleQueue.Enqueue(triangle2);
                    break;
                case 5:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out _), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out _), 1);

                    triangle.TransformedP1 = new Vector4D(p1, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = new Vector4D(p3, 1);

                    triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(p3, 1);
                    triangle2.TransformedP2 = intersection1;
                    triangle2.TransformedP3 = intersection2;

                    triangleQueue.Enqueue(triangle);
                    triangleQueue.Enqueue(triangle2);
                    break;
                case 6:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out _), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out _), 1);

                    triangle.TransformedP1 = new Vector4D(p2, 1);
                    triangle.TransformedP2 = new Vector4D(p3, 1);
                    triangle.TransformedP3 = intersection1;

                    triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(p3, 1);
                    triangle2.TransformedP2 = intersection2;
                    triangle2.TransformedP3 = intersection1;

                    triangleQueue.Enqueue(triangle);
                    triangleQueue.Enqueue(triangle2);
                    break;
                case 7:
                    // Triangle is completely inside
                    triangleQueue.Enqueue(triangle);
                    break;
            }
        }
    }
}