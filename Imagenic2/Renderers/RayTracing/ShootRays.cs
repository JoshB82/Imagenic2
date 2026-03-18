using Imagenic2.Core.Entities;
using System.Drawing;

namespace Imagenic2.Core.Renderers.RayTracing;

public partial class RayTracer<TImage>
{
    private void MoveToViewSpace(RenderingEntity renderingEntity)
    {
        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                if (mesh.DrawFaces)
                {
                    Matrix4x4 modelToView = renderingEntity.WorldToView * mesh.ModelToWorld;

                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        triangle.TransformedP1 = new Vector4D(triangle.P1.WorldOrigin, 1);
                        triangle.TransformedP2 = new Vector4D(triangle.P2.WorldOrigin, 1);
                        triangle.TransformedP3 = new Vector4D(triangle.P3.WorldOrigin, 1);

                        triangle.TransformedTextureP1 = triangle.TextureP1;
                        triangle.TransformedTextureP2 = triangle.TextureP2;
                        triangle.TransformedTextureP3 = triangle.TextureP3;

                        triangle.TransformTriangleVertices(modelToView);

                        // View clipping?

                        triangle.TransformTriangleVertices(renderingEntity.viewToScreen);

                        // Cast rays
                        float viewWidth = renderingEntity.ViewWidth;
                        float viewHeight = renderingEntity.ViewHeight;
                        int renderWidth = RenderingOptions.RenderWidth;
                        int renderHeight = RenderingOptions.RenderHeight;

                        for (int y = 0; y < renderHeight; y++)
                        {
                            for (int x = 0; x < renderWidth; x++)
                            {
                                float u = (x + 0.5f) / renderWidth;
                                float v = (y + 0.5f) / renderHeight;
                                Vector3D direction = (new Vector3D(u * viewWidth, v * viewHeight, renderingEntity.ZNear)).Normalise();

                                Ray ray = new Ray(Vector3D.Zero, direction);
                                Color colour = TraceRay(ray);
                                colourBuffer[x, y] = colour;
                            }
                        }
                    }
                }
            }
        }
    }

    private Color TraceRay(Ray ray)
    {
        float closestDistance = float.MaxValue;
        Triangle? hitTriangle = null;
        Vector3D hitPoint = Vector3D.Zero;

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                foreach (Triangle triangle in mesh.Structure.Triangles)
                {
                    if (ray.IntersectTriangle(triangle, out float distance))
                    {
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            hitTriangle = triangle;
                            hitPoint = ray.direction * distance;
                        }
                    }
                }
            }
        }

        if (hitTriangle is null)
        {
            // No triangle hit
            return RenderingOptions.BackgroundColour;
        }

        return ((SolidStyle)(hitTriangle.FrontStyle)).Colour;
    }
}