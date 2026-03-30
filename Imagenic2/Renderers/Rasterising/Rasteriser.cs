using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Lights;
using Imagenic2.Core.Images;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage> : Renderer<TImage> where TImage : Imagenic2.Core.Images.Image, IFactory<TImage>
{
    #region Fields and Properties
    
    internal Buffer2D<float> zBuffer;

    internal const float backgroundValue = 1.1f;

    #endregion

    #region Constructors

    public Rasteriser(RenderingOptions renderingOptions) : base(renderingOptions)
    {
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
                    GenerateTileTriangles(shadowMap.Tiles, light, shadowMap.ScreenToWindow, shadowMap.Width, shadowMap.Height);
                    ShadowTileTriangles(shadowMap.Tiles, shadowMap.Data, ProduceShadowMaps);
                }
            }

            NewShadowMapNeeded = false;
        }

        //Imagenic2.Core.Images.Bitmap b = RenderingOptions.Lights[0].ShadowMaps.First().Data.ToImage<Imagenic2.Core.Images.Bitmap>();
        //b.Export("");

        if (RenderTriangles)
        {
            GenerateTileTriangles(Tiles, RenderingOptions.RenderCamera, RenderingOptions.ScreenToWindow, RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);
            RenderTileTriangles(Tiles, zBuffer, OnInterpolation);
        }
        if (RenderEdges) RenderMeshEdges();
        if (RenderViewVolumes) DrawViewVolumes();

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

        //if (typeof(TImage) == typeof(Imagenic2.Core.Images.Bitmap))
        //{
        //    return LatestRender = new Imagenic2.Core.Images.Bitmap(colourBuffer) as TImage;
        //}

        return LatestRender = TImage.CreateFromBuffer(colourBuffer);
    }

    public override IAsyncEnumerable<TImage> RenderAsync(Animation animation, CancellationToken token = default)
    {
        return null; // Temporary
    }
    
    // Transform vertices
    private static void TransformEdgeVertices(Edge edge, Matrix4x4 transformationMatrix)
    {
        // Transform vertices
        edge.TransformedP1 = transformationMatrix * edge.TransformedP1;
        edge.TransformedP2 = transformationMatrix * edge.TransformedP2;
    }

    

    #endregion
}