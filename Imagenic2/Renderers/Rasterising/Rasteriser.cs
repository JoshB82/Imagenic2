using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;
using System.Buffers;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public class Rasteriser<TImage> : Renderer<TImage> where TImage : Imagenic2.Core.Images.Image
{
    #region Fields and Properties

    private static ArrayPool<Vector3D> Vector3DArrayPool = ArrayPool<Vector3D>.Shared;
    internal Buffer2D<Color> colourBuffer;
    internal Buffer2D<float> zBuffer;

    #endregion

    #region Constructors

    public Rasteriser(RenderingOptions renderingOptions) : base(renderingOptions)
    {
        colourBuffer = new Buffer2D<Color>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);
        zBuffer = new Buffer2D<float>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);
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

        colourBuffer.SetAllToValue(RenderingOptions.BackgroundColour);
        zBuffer.SetAllToValue(1.1f); // A number > 1
        var triangleQueue = new Queue<Triangle>();

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                // Draw edges
                if (mesh.DrawEdges)
                {
                    foreach (Edge edge in mesh.Structure.Edges)
                    {
                        edge.TransformedP1 = new Vector4D(edge.Vertex1.WorldOrigin, 1);
                        edge.TransformedP2 = new Vector4D(edge.Vertex2.WorldOrigin, 1);

                        Matrix4x4 modelToView = RenderingOptions.RenderCamera.WorldToView * edge.Vertex1.ModelToWorld;
                        TransformEdgeVertices(edge, modelToView);
                        if (ClipEdge(edge, RenderingOptions.RenderCamera.ViewClippingPlanes))
                        {
                            TransformEdgeVertices(edge, RenderingOptions.RenderCamera.viewToScreen);
                            if (RenderingOptions.RenderCamera is PerspectiveCamera)
                            {
                                edge.TransformedP1 /= edge.TransformedP1.w;
                                edge.TransformedP2 /= edge.TransformedP2.w;
                            }
                            if (ClipEdge(edge, Renderer<TImage>.ScreenClippingPlanes))
                            {
                                TransformEdgeVertices(edge, RenderingOptions.ScreenToWindow);
                                InterpolateEdge(edge, colourBuffer, zBuffer);
                            }
                        }
                    }
                }

                // Draw triangles
                if (mesh.DrawFaces)
                {
                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        triangle.TransformedP1 = new Vector4D(triangle.P1.WorldOrigin, 1);
                        triangle.TransformedP2 = new Vector4D(triangle.P2.WorldOrigin, 1);
                        triangle.TransformedP3 = new Vector4D(triangle.P3.WorldOrigin, 1);

                        Matrix4x4 modelToView = RenderingOptions.RenderCamera.WorldToView * triangle.P1.ModelToWorld;
                        TransformTriangleVertices(triangle, modelToView);

                        if (mesh.Structure.MeshDimension == MeshDimension._3D)
                        {
                            Vector3D normal = Vector3D.NormalFromPlane((Vector3D)(triangle.TransformedP1), (Vector3D)(triangle.TransformedP2), (Vector3D)(triangle.TransformedP3));
                            if (normal * (Vector3D)(triangle.TransformedP1) >= 0) continue;
                        }
                        triangleQueue.Enqueue(triangle);
                        ClipTriangles(triangleQueue, RenderingOptions.RenderCamera.ViewClippingPlanes);
                        if (triangleQueue.Count == 0) continue;

                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            TransformTriangleVertices(clippedTriangle, RenderingOptions.RenderCamera.viewToScreen);
                            if (RenderingOptions.RenderCamera is PerspectiveCamera)
                            {
                                clippedTriangle.TransformedP1 /= clippedTriangle.TransformedP1.w;
                                clippedTriangle.TransformedP2 /= clippedTriangle.TransformedP2.w;
                                clippedTriangle.TransformedP3 /= clippedTriangle.TransformedP3.w;
                            }
                        }

                        ClipTriangles(triangleQueue, Renderer<TImage>.ScreenClippingPlanes);
                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            TransformTriangleVertices(clippedTriangle, RenderingOptions.ScreenToWindow);
                            InterpolateTriangle(clippedTriangle, colourBuffer, zBuffer);
                        }

                        triangleQueue.Clear();
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

    private static void TransformEdgeVertices(Edge edge, Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        edge.TransformedP1 = transformationMatrix * edge.TransformedP1;
        edge.TransformedP2 = transformationMatrix * edge.TransformedP2;
    }

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

    private static void InterpolateEdge(Edge edge, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer)
    {
        // Extract values
        int x1 = edge.TransformedP1.x.RoundToInt();
        int y1 = edge.TransformedP1.y.RoundToInt();
        float z1 = edge.TransformedP1.z;
        int x2 = edge.TransformedP2.x.RoundToInt();
        int y2 = edge.TransformedP2.y.RoundToInt();
        float z2 = edge.TransformedP2.z;

        float tStep = 1 / Max(Abs(x2 - x1), Abs(y2 - y1));

        for (float t = 0; t <= 1; t += tStep)
        {
            int x = ((1 - t) * x1 + t * x2).RoundToInt();
            int y = ((1 - t) * y1 + t * y2).RoundToInt();
            float z = (1 - t) * z1 + t * z2;

            OnInterpolation(edge, colourBuffer, zBuffer, x, y, z);
        }
    }

    private static void OnInterpolation(Edge edge, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color colour = ((SolidEdgeStyle)(edge.EdgeStyle)).Colour;
            colourBuffer.Values[x][y] = colour;
        }
    }

    private static void TransformTriangleVertices(Triangle triangle, Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        triangle.TransformedP1 = transformationMatrix * triangle.TransformedP1;
        triangle.TransformedP2 = transformationMatrix * triangle.TransformedP2;
        triangle.TransformedP3 = transformationMatrix * triangle.TransformedP3;
    }

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

    private static void InterpolateTriangle(Triangle triangle, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer)
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