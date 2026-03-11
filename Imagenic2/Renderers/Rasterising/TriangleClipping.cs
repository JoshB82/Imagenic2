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

            Vector2D t1 = triangle.TextureP1;
            Vector2D t2 = triangle.TextureP2;
            Vector2D t3 = triangle.TextureP3;

            float o1 = triangle.invW1;
            float o2 = triangle.invW2;
            float o3 = triangle.invW3;

            bool isP1Inside = Vector3D.PointDistanceFromPlane(p1, planePoint, planeNormal) >= 0;
            bool isP2Inside = Vector3D.PointDistanceFromPlane(p2, planePoint, planeNormal) >= 0;
            bool isP3Inside = Vector3D.PointDistanceFromPlane(p3, planePoint, planeNormal) >= 0;

            int mask = (isP1Inside ? 1 : 0) |
                       (isP2Inside ? 2 : 0) |
                       (isP3Inside ? 4 : 0);

            Vector4D intersection1, intersection2;
            Triangle triangle2;
            float d1, d2, i1, i2;

            static Vector2D Interpolate(Vector2D v1, Vector2D v2, float o1, float o2, float d, out float inverseInterpolated)
            {
                inverseInterpolated = (o2 - o1) * d + o1;
                Vector2D vInterpolated = (v2 * o2 - v1 * o1) * d + v1 * o1;
                return vInterpolated / inverseInterpolated;
            }

            switch (mask)
            {
                case 0:
                    // Triangle is completely outside
                    break;
                
                // One point inside
                case 1:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out d2), 1);

                    triangle.TransformedP1 = new Vector4D(p1, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangle.TextureP1 = t1;
                    triangle.TextureP2 = Interpolate(t1, t2, o1, o2, d1, out i1);
                    triangle.TextureP3 = Interpolate(t1, t3, o1, o3, d2, out i2);
                    triangle.invW1 = o1;
                    triangle.invW2 = i1;
                    triangle.invW3 = i2;
                    
                    triangleQueue.Enqueue(triangle);
                    break;
                case 2:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out d2), 1);
                    
                    triangle.TransformedP1 = new Vector4D(p2, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangle.TextureP1 = t2;
                    triangle.TextureP2 = Interpolate(t2, t3, o2, o3, d1, out i1);
                    triangle.TextureP3 = Interpolate(t2, t1, o2, o1, d2, out i2);
                    triangle.invW1 = o2;
                    triangle.invW2 = i1;
                    triangle.invW3 = i2;

                    triangleQueue.Enqueue(triangle);
                    break;
                case 4:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out d2), 1);
                    
                    triangle.TransformedP1 = new Vector4D(p3, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = intersection2;
                    triangle.TextureP1 = t3;
                    triangle.TextureP2 = Interpolate(t3, t1, o3, o1, d1, out i1);
                    triangle.TextureP3 = Interpolate(t3, t2, o3, o2, d2, out i2);
                    triangle.invW1 = o3;
                    triangle.invW2 = i1;
                    triangle.invW3 = i2;

                    triangleQueue.Enqueue(triangle);
                    break;
                
                // Two points inside
                case 3:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p3, planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p2, p3, planePoint, planeNormal, out d2), 1);

                    Vector2D t13 = Interpolate(t1, t3, o1, o3, d1, out i1);
                    Vector2D t23 = Interpolate(t2, t3, o2, o3, d2, out i2);

                    triangle.TransformedP1 = new Vector4D(p1, 1);
                    triangle.TransformedP2 = new Vector4D(p2, 1);
                    triangle.TransformedP3 = intersection1;
                    triangle.TextureP1 = t1;
                    triangle.TextureP2 = t2;
                    triangle.TextureP3 = t13;
                    triangle.invW1 = o1;
                    triangle.invW2 = o2;
                    triangle.invW3 = i1;

                    triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(p2, 1);
                    triangle2.TransformedP2 = intersection2;
                    triangle2.TransformedP3 = intersection1;
                    triangle2.TextureP1 = t2;
                    triangle2.TextureP2 = t23;
                    triangle2.TextureP3 = t13;
                    triangle2.invW1 = o2;
                    triangle2.invW2 = i2;
                    triangle2.invW3 = i1;

                    triangleQueue.Enqueue(triangle);
                    triangleQueue.Enqueue(triangle2);
                    break;
                case 5:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p1, p2, planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p2, planePoint, planeNormal, out d2), 1);

                    Vector2D t12 = Interpolate(t1, t2, o1, o2, d1, out i1);
                    Vector2D t32 = Interpolate(t3, t2, o3, o2, d2, out i2);

                    triangle.TransformedP1 = new Vector4D(p1, 1);
                    triangle.TransformedP2 = intersection1;
                    triangle.TransformedP3 = new Vector4D(p3, 1);
                    triangle.TextureP1 = t1;
                    triangle.TextureP2 = t12;
                    triangle.TextureP3 = t3;
                    triangle.invW1 = o1;
                    triangle.invW2 = i1;
                    triangle.invW3 = o3;

                    triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(p3, 1);
                    triangle2.TransformedP2 = intersection1;
                    triangle2.TransformedP3 = intersection2;
                    triangle2.TextureP1 = t3;
                    triangle2.TextureP2 = t12;
                    triangle2.TextureP3 = t32;
                    triangle2.invW1 = o3;
                    triangle2.invW2 = i1;
                    triangle2.invW3 = i2;

                    triangleQueue.Enqueue(triangle);
                    triangleQueue.Enqueue(triangle2);
                    break;
                case 6:
                    intersection1 = new Vector4D(Vector3D.LineIntersectPlane(p2, p1, planePoint, planeNormal, out d1), 1);
                    intersection2 = new Vector4D(Vector3D.LineIntersectPlane(p3, p1, planePoint, planeNormal, out d2), 1);

                    Vector2D t21 = Interpolate(t2, t1, o2, o1, d1, out i1);
                    Vector2D t31 = Interpolate(t3, t1, o3, o1, d2, out i2);

                    triangle.TransformedP1 = new Vector4D(p2, 1);
                    triangle.TransformedP2 = new Vector4D(p3, 1);
                    triangle.TransformedP3 = intersection1;
                    triangle.TextureP1 = t2;
                    triangle.TextureP2 = t3;
                    triangle.TextureP3 = t21;
                    triangle.invW1 = o2;
                    triangle.invW2 = o3;
                    triangle.invW3 = i1;

                    triangle2 = triangle.DeepCopy();
                    triangle2.TransformedP1 = new Vector4D(p3, 1);
                    triangle2.TransformedP2 = intersection2;
                    triangle2.TransformedP3 = intersection1;
                    triangle2.TextureP1 = t3;
                    triangle2.TextureP2 = t31;
                    triangle2.TextureP3 = t21;
                    triangle2.invW1 = o3;
                    triangle2.invW2 = i2;
                    triangle2.invW3 = i1;

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