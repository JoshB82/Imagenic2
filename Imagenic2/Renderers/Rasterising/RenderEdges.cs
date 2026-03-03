using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    public void RenderEdges()
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
                        edge.TransformedP1 = new Vector4D(edge.Vertex1.WorldOrigin, 1);
                        edge.TransformedP2 = new Vector4D(edge.Vertex2.WorldOrigin, 1);

                        Matrix4x4 modelToView = RenderingOptions.RenderCamera.WorldToView * mesh.ModelToWorld;
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
            }
        }
    }
}