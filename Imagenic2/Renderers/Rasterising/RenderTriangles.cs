using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private Queue<Triangle> triangleQueue = new Queue<Triangle>();

    private void ShadowMapRenderTriangles(RenderingEntity renderingEntity,
                                 Buffer2D<float> buffer,
                                 Matrix4x4 screenToWindow,
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
                            if ((normal * (Vector3D)(triangle.TransformedP1)).ApproxMoreThanEquals(0)) continue;
                        }
                        triangleQueue.Enqueue(triangle);
                        ClipTriangles(triangleQueue, renderingEntity.ViewClippingPlanes);
                        if (triangleQueue.Count == 0) continue;

                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            TransformTriangleVertices(clippedTriangle, renderingEntity.viewToScreen);

                            clippedTriangle.invW1 = 1 / clippedTriangle.TransformedP1.w;
                            clippedTriangle.invW2 = 1 / clippedTriangle.TransformedP2.w;
                            clippedTriangle.invW3 = 1 / clippedTriangle.TransformedP3.w;

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
                            TransformTriangleVertices(clippedTriangle, screenToWindow);
                            ShadowMapInterpolateTriangle(clippedTriangle, buffer, onInterpolation, clippedTriangle.invW1, clippedTriangle.invW2, clippedTriangle.invW3);
                        }

                        triangleQueue.Clear();
                    }
                }
            }
        }
    }

    private void RenderTriangles(RenderingEntity renderingEntity,
                                 Buffer2D<float> buffer,
                                 Matrix4x4 screenToWindow,
                                 Action<Triangle, Buffer2D<float>, int, int, float, Vector3D, Vector2D> onInterpolation)
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
                            if ((normal * (Vector3D)(triangle.TransformedP1)).ApproxMoreThanEquals(0)) continue;
                        }
                        triangleQueue.Enqueue(triangle);
                        ClipTriangles(triangleQueue, renderingEntity.ViewClippingPlanes);
                        if (triangleQueue.Count == 0) continue;

                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            clippedTriangle.ViewSpaceP1 = clippedTriangle.TransformedP1;
                            clippedTriangle.ViewSpaceP2 = clippedTriangle.TransformedP2;
                            clippedTriangle.ViewSpaceP3 = clippedTriangle.TransformedP3;

                            TransformTriangleVertices(clippedTriangle, renderingEntity.viewToScreen);

                            clippedTriangle.invW1 = 1 / clippedTriangle.TransformedP1.w;
                            clippedTriangle.invW2 = 1 / clippedTriangle.TransformedP2.w;
                            clippedTriangle.invW3 = 1 / clippedTriangle.TransformedP3.w;

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
                            TransformTriangleVertices(clippedTriangle, screenToWindow);
                            switch (clippedTriangle.FrontStyle)
                            {
                                case SolidStyle:
                                    InterpolateTriangle(clippedTriangle, buffer, onInterpolation, clippedTriangle.invW1, clippedTriangle.invW2, clippedTriangle.invW3);
                                    break;
                                case TextureStyle:
                                    InterpolateTextureTriangle(clippedTriangle, buffer, OnTextureInterpolation, clippedTriangle.invW1, clippedTriangle.invW2, clippedTriangle.invW3);
                                    break;
                            }
                        }

                        triangleQueue.Clear();
                    }
                }
            }
        }
    }
}