using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private Queue<Triangle> triangleQueue = new Queue<Triangle>();

    private void RenderTriangles(RenderingEntity renderingEntity,
                                 Buffer2D<float> buffer,
                                 Action<Triangle, Buffer2D<float>, int, int, float> onInterpolation)
    {
        // Reset values
        zBuffer.SetAllToValue(backgroundValue); // A number > 1

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                if (mesh.DrawFaces)
                {
                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        triangle.TransformedP1 = new Vector4D(triangle.P1.WorldOrigin, 1);
                        triangle.TransformedP2 = new Vector4D(triangle.P2.WorldOrigin, 1);
                        triangle.TransformedP3 = new Vector4D(triangle.P3.WorldOrigin, 1);

                        Matrix4x4 modelToView = renderingEntity.WorldToView * mesh.ModelToWorld;
                        TransformTriangleVertices(triangle, modelToView);

                        if (mesh.Structure.MeshDimension == MeshDimension._3D)
                        {
                            Vector3D normal = Vector3D.NormalFromPlane((Vector3D)(triangle.TransformedP1), (Vector3D)(triangle.TransformedP2), (Vector3D)(triangle.TransformedP3));
                            if (normal * (Vector3D)(triangle.TransformedP1) >= 0) continue;
                        }
                        triangleQueue.Enqueue(triangle);
                        ClipTriangles(triangleQueue, renderingEntity.ViewClippingPlanes);
                        if (triangleQueue.Count == 0) continue;

                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            TransformTriangleVertices(clippedTriangle, renderingEntity.viewToScreen);
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
                            InterpolateTriangle(clippedTriangle, buffer, onInterpolation);
                        }

                        triangleQueue.Clear();
                    }
                }
            }
        }
    }
}