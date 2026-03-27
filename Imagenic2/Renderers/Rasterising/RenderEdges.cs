using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    internal void RenderMeshEdges()
    {
        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                // Draw edges
                if (mesh.DrawEdges)
                {
                    foreach (Edge edge in mesh.Structure.Edges)
                    {
                        RenderEdge(edge, mesh);
                    }
                }
            }
        }
    }

    private void DrawViewVolumes()
    {
        foreach (Light light in RenderingOptions.Lights)
        {
            if (light.VolumeStyle == VolumeOutline.None)
            {
                continue;
            }
            else
            {
                Vertex nearBottomLeft = new Vertex(light.ViewClippingPlanes[0].Point);
                Vertex nearBottomRight = new Vertex(light.ViewClippingPlanes[0].Point + new Vector3D(light.ViewWidth, 0, 0));
                Vertex nearTopLeft = new Vertex(light.ViewClippingPlanes[0].Point + new Vector3D(0, light.ViewHeight, 0));
                Vertex nearTopRight = new Vertex(light.ViewClippingPlanes[0].Point + new Vector3D(light.ViewWidth, light.ViewHeight, 0));

                if (light.VolumeStyle.HasFlag(VolumeOutline.Near) || light.VolumeStyle.HasFlag(VolumeOutline.Far))
                {
                    // Near plane
                    RenderEdge(new Edge(nearBottomLeft, nearBottomRight), light);
                    RenderEdge(new Edge(nearBottomRight, nearTopRight), light);
                    RenderEdge(new Edge(nearTopRight, nearTopLeft), light);
                    RenderEdge(new Edge(nearTopLeft, nearBottomLeft), light);
                }
                
                if (light.VolumeStyle == VolumeOutline.Far)
                {
                    float scale = light is DistantLight ? 1 : light.ZFar / light.ZNear;
                    Vertex farTopRight = new Vertex(light.ViewClippingPlanes[3].Point);
                    Vertex farTopLeft = new Vertex(light.ViewClippingPlanes[3].Point - new Vector3D(light.ViewWidth * scale, 0, 0));
                    Vertex farBottomRight = new Vertex(light.ViewClippingPlanes[3].Point - new Vector3D(0, light.ViewHeight * scale, 0));
                    Vertex farBottomLeft = new Vertex(light.ViewClippingPlanes[3].Point - new Vector3D(light.ViewWidth * scale, light.ViewHeight * scale, 0));

                    // Between near plane and far plane
                    RenderEdge(new Edge(nearBottomLeft, farBottomLeft), light);
                    RenderEdge(new Edge(nearBottomRight, farBottomRight), light);
                    RenderEdge(new Edge(nearTopLeft, farTopLeft), light);
                    RenderEdge(new Edge(nearTopRight, farTopRight), light);

                    // Far plane
                    RenderEdge(new Edge(farBottomLeft, farBottomRight), light);
                    RenderEdge(new Edge(farBottomRight, farTopRight), light);
                    RenderEdge(new Edge(farTopRight, farTopLeft), light);
                    RenderEdge(new Edge(farTopLeft, farBottomLeft), light);
                }
            }
        }
    }

    private void RenderEdge(Edge edge, OrientatedEntity orientatedEntity)
    {
        edge.TransformedP1 = new Vector4D(edge.Vertex1.WorldOrigin, 1);
        edge.TransformedP2 = new Vector4D(edge.Vertex2.WorldOrigin, 1);

        Matrix4x4 modelToView = RenderingOptions.RenderCamera.WorldToView * orientatedEntity.ModelToWorld;
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
                InterpolateEdge(edge);
            }
        }
    }
}