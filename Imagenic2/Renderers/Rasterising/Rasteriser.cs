using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Lights;
using System.Buffers;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage> : Renderer<TImage> where TImage : Imagenic2.Core.Images.Image
{
    #region Fields and Properties

    private static ArrayPool<Vector3D> Vector3DArrayPool = ArrayPool<Vector3D>.Shared;
    internal Buffer2D<Color> colourBuffer;
    internal Buffer2D<float> zBuffer;

    internal const float backgroundValue = 1.1f;

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
        if (!NewRenderNeeded) return LatestRender;
        colourBuffer.SetAllToValue(RenderingOptions.BackgroundColour);

        if (RenderingOptions.RenderCamera is null)
        {
            throw new InvalidOperationException("Render camera must be set in order to render an image.");
        }

        // Check if there is anything to render.
        if (RenderingOptions.PhysicalEntitiesToRender is null || !RenderingOptions.PhysicalEntitiesToRender.Any())
        {
            return null;
        }

        if (NewShadowMapNeeded)
        {
            foreach (Light light in RenderingOptions.Lights)
            {
                foreach (ShadowMap shadowMap in light.ShadowMaps)
                {
                    shadowMap.Data.SetAllToValue(backgroundValue); // A number > 1
                    RenderTriangles(light, shadowMap.Data, shadowMap.ScreenToWindow, ProduceShadowMaps);
                }
            }

            NewShadowMapNeeded = false;
        }

        //Imagenic2.Core.Images.Bitmap b = RenderingOptions.Lights[0].ShadowMaps.First().Data.ToImage<Imagenic2.Core.Images.Bitmap>();
        //b.Export("C:\\Users\\joshd\\Documents\\test.bmp");

        RenderTriangles(RenderingOptions.RenderCamera, zBuffer, RenderingOptions.ScreenToWindow, OnInterpolation);
        RenderEdges();

        //var triangleQueue = new Queue<Triangle>();
        /*
        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                

                // Draw triangles
                if (mesh.DrawFaces)
                {
                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        triangle.TransformedP1 = new Vector4D(triangle.P1.WorldOrigin, 1);
                        triangle.TransformedP2 = new Vector4D(triangle.P2.WorldOrigin, 1);
                        triangle.TransformedP3 = new Vector4D(triangle.P3.WorldOrigin, 1);

                        Matrix4x4 modelToView = RenderingOptions.RenderCamera.WorldToView * mesh.ModelToWorld;
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
                            //InterpolateTriangle(clippedTriangle, colourBuffer, zBuffer);
                            InterpolateTriangle(clippedTriangle);
                        }

                        triangleQueue.Clear();
                    }
                }
            }
        }
        */

        NewRenderNeeded = false;

        if (typeof(TImage) == typeof(Imagenic2.Core.Images.Bitmap))
        {
            return LatestRender = new Imagenic2.Core.Images.Bitmap(colourBuffer) as TImage;
        }

        return null;
    }

    // Transform vertices
    private static void TransformEdgeVertices(Edge edge, Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        edge.TransformedP1 = transformationMatrix * edge.TransformedP1;
        edge.TransformedP2 = transformationMatrix * edge.TransformedP2;
    }

    private static void TransformTriangleVertices(Triangle triangle, Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        triangle.TransformedP1 = transformationMatrix * triangle.TransformedP1;
        triangle.TransformedP2 = transformationMatrix * triangle.TransformedP2;
        triangle.TransformedP3 = transformationMatrix * triangle.TransformedP3;
    }

    /*

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

    */

    #endregion
}