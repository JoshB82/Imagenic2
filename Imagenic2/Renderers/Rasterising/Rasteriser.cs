using Imagenic2.Core.Entities;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public class Rasteriser<TImage> : Renderer<TImage> where TImage : Imagenic2.Core.Images.Image
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public Rasteriser(RenderingOptions renderingOptions) : base(renderingOptions)
    {

    }

    #endregion

    #region Methods

    public async override Task<TImage?> RenderAsync(CancellationToken token = default)
    {
        if (RenderingOptions.RenderCamera is null)
        {
            throw new InvalidOperationException("Render camera must be set in order to render an image.");
        }

        // Check if there is anything to render.
        if (RenderingOptions.PhysicalEntitiesToRender is null || !RenderingOptions.PhysicalEntitiesToRender.Any())
        {
            return null;
        }

        var colourBuffer = new Buffer2D<Color>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);
        colourBuffer.SetAllToValue(RenderingOptions.BackgroundColour);
        var zBuffer = new Buffer2D<float>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                foreach (Triangle triangle in mesh.Structure.Triangles)
                {
                    triangle.TransformedP1 = new Vector4D(triangle.P1.WorldOrigin, 1);
                    triangle.TransformedP2 = new Vector4D(triangle.P2.WorldOrigin, 1);
                    triangle.TransformedP3 = new Vector4D(triangle.P3.WorldOrigin, 1);

                    Matrix4x4 modelToView = RenderingOptions.RenderCamera.WorldToView * triangle.P1.ModelToWorld;
                    TransformTriangleVertices(triangle, modelToView);

                    var triangleQueue = new Queue<Triangle>();
                    triangleQueue.Enqueue(triangle);
                    var clippedTriangles = ClipTriangles(triangleQueue, RenderingOptions.RenderCamera.ViewClippingPlanes);
                    if (clippedTriangles.Count == 0) continue;

                    foreach (Triangle clippedTriangle in clippedTriangles)
                    {
                        TransformTriangleVertices(clippedTriangle, RenderingOptions.RenderCamera.viewToScreen);
                    }
                    clippedTriangles = ClipTriangles(triangleQueue, Renderer<TImage>.ScreenClippingPlanes);

                    foreach (Triangle clippedTriangle in clippedTriangles)
                    {
                        if (RenderingOptions.RenderCamera is PerspectiveCamera)
                        {
                            triangle.TransformedP1 /= triangle.TransformedP1.w;
                            triangle.TransformedP2 /= triangle.TransformedP2.w;
                            triangle.TransformedP3 /= triangle.TransformedP3.w;
                        }

                        TransformTriangleVertices(clippedTriangle, RenderingOptions.ScreenToWindow);
                        Interpolate(clippedTriangle, colourBuffer, zBuffer);
                    }
                }
            }
        }

        NewRenderNeeded = false;

        if (typeof(TImage) == typeof(Imagenic2.Core.Images.Bitmap))
        {
            return new Imagenic2.Core.Images.Bitmap(colourBuffer) as TImage;
        }

        return null;
    }

    private static void TransformTriangleVertices(Triangle triangle, Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        triangle.TransformedP1 = transformationMatrix * triangle.TransformedP1;
        triangle.TransformedP2 = transformationMatrix * triangle.TransformedP2;
        triangle.TransformedP3 = transformationMatrix * triangle.TransformedP3;
    }

    private static Queue<Triangle> ClipTriangles(Queue<Triangle> triangleQueue, ClippingPlane[] clippingPlanes)
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

        return triangleQueue;

        void ClipTriangle(Queue<Triangle> triangleQueue, Triangle triangle, Vector3D planePoint, Vector3D planeNormal)
        {
            Vector3D[] insidePoints = new Vector3D[3], outsidePoints = new Vector3D[3];
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
        }
    }

    private static void Interpolate(Triangle triangle, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer)
    {
        // Extract values
        int x1 = triangle.TransformedP1.x.RoundToInt();
        int y1 = triangle.TransformedP1.y.RoundToInt();
        float z1 = triangle.TransformedP1.z;
        int x2 = triangle.TransformedP2.x.RoundToInt();
        int y2 = triangle.TransformedP2.y.RoundToInt();
        float z2 = triangle.TransformedP2.z;
        int x3 = triangle.TransformedP3.x.RoundToInt();
        int y3 = triangle.TransformedP3.y.RoundToInt();
        float z3 = triangle.TransformedP3.z;

        Extensions.SortByY
        (
            ref x1, ref y1, ref z1,
            ref x2, ref y2, ref z2,
            ref x3, ref y3, ref z3
        );

        // Create steps
        float dyStep1 = y1 - y2;
        float dyStep2 = y1 - y3;
        float dyStep3 = y2 - y3;

        float xStep1 = 0, zStep1 = 0;
        float xStep3 = 0, zStep3 = 0;

        if (dyStep1 != 0)
        {
            xStep1 = (x1 - x2) / dyStep1; // dx from point 1 to point 2
            zStep1 = (z1 - z2) / dyStep1; // dz from point 1 to point 2
        }
        float xStep2 = (x1 - x3) / dyStep2; // dx from point 1 to point 3
        float zStep2 = (z1 - z3) / dyStep2; // dz from point 1 to point 3
        if (dyStep3 != 0)
        {
            xStep3 = (x2 - x3) / dyStep3; // dx from point 2 to point 3
            zStep3 = (z2 - z3) / dyStep3; // dz from point 2 to point 3
        }

        // Draw a flat-bottom triangle
        if (dyStep1 != 0)
        {
            for (int y = y2; y <= y1; y++)
            {
                int sx = ((y - y2) * xStep1 + x2).RoundToInt();
                float sz = (y - y2) * zStep1 + z2;

                int ex = ((y - y3) * xStep2 + x3).RoundToInt();
                float ez = (y - y3) * zStep2 + z3;

                if (sx > ex)
                {
                    Extensions.Swap(ref sx, ref ex);
                    Extensions.Swap(ref sz, ref ez);
                }

                float t = 0, tStep = 1f / (ex - sx);
                for (int x = sx; x <= ex; x++)
                {
                    float z = sz + t * (ez - sz);
                    OnInterpolation(triangle, colourBuffer, zBuffer, x, y, z);

                    t += tStep;
                }
            }
        }

        // Draw a flat-top triangle
        if (dyStep3 != 0)
        {
            for (int y = y3; y <= y2; y++)
            {
                int sx = ((y - y3) * xStep3 + x3).RoundToInt();
                float sz = (y - y3) * zStep3 + z3;

                int ex = ((y - y3) * xStep2 + x3).RoundToInt();
                float ez = (y - y3) * zStep2 + z3;

                if (sx > ex)
                {
                    Extensions.Swap(ref sx, ref ex);
                    Extensions.Swap(ref sz, ref ez);
                }

                float t = 0, tStep = 1f / (ex - sx);
                for (int x = sx; x <= ex; x++)
                {
                    float z = sz + t * (ez - sz);
                    OnInterpolation(triangle, colourBuffer, zBuffer, x, y, z);

                    t += tStep;
                }
            }
        }
    }

    private static void OnInterpolation(Triangle triangle, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color colour = ((SolidStyle)(triangle.FrontStyle)).Colour;
            colourBuffer.Values[x][y] = colour;
        }
    }

    #endregion
}