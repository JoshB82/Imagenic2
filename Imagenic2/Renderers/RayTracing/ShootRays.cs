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
                    }
                }
            }
        }

        // Cast rays
        float viewWidth = renderingEntity.ViewWidth;
        float viewHeight = renderingEntity.ViewHeight;
        int renderWidth = RenderingOptions.RenderWidth;
        int renderHeight = RenderingOptions.RenderHeight;

        Parallel.For(0, renderHeight, y =>
        {
            Parallel.For(0, renderWidth, x =>
            {
                float u = (x + 0.5f) / renderWidth;
                float v = (y + 0.5f) / renderHeight;
                Vector3D direction = (new Vector3D((2 * u - 1) * viewWidth, (2 * v - 1) * viewHeight, renderingEntity.ZNear)).Normalise();

                Ray ray = new Ray(Vector3D.Zero, direction);
                Color colour = TraceRay(ray);
                colourBuffer[x, y] = colour;
            });
        });
    }

    private Color TraceRay(Ray ray, int depth = 5)
    {
        if (depth == 0)
        {
            return Color.Black;
        }

        if (!ClosestHit(ray, out HitInfo hitInfo))
        {
            return RenderingOptions.BackgroundColour;
        }

        //return ((Material)(hitInfo.triangle.FrontStyle)).Shade(hitInfo, RenderingOptions.Lights);

        Color colour = Color.Black;

        foreach (Light light in RenderingOptions.Lights)
        {
            Vector3D lightVector = light.WorldOrigin - hitInfo.position;
            

            if (!IsInShadow(hitInfo.position, light))
            {
                colour = ((Material)(hitInfo.triangle.FrontStyle)).Shade(hitInfo, light);
                //colour = light.Colour;
            }
        }

        return colour;

        /*
        if (hitTriangle is null)
        {
            // No triangle hit
            return RenderingOptions.BackgroundColour;
        }

        return ((SolidStyle)(hitTriangle.FrontStyle)).Colour;
        */
    }

    private bool IsInShadow(Vector3D point, Light light)
    {
        Vector3D lightVector = light.WorldOrigin - point;
        float distance = lightVector.Magnitude();
        Ray shadowRay = new Ray(point, lightVector);

        if (ClosestHit(shadowRay, out HitInfo hitInfo))
        {
            return hitInfo.distance < distance;
        }

        return false;
    }

    private bool ClosestHit(Ray ray, out HitInfo hitInfo)
    {
        bool hit = false;

        float closestDistance = float.MaxValue;
        hitInfo = new HitInfo();

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
                            hit = true;
                            closestDistance = distance;

                            hitInfo.distance = closestDistance;
                            hitInfo.triangle = triangle;
                            hitInfo.position = ray.direction * distance;
                            hitInfo.normal = triangle.CalculateNormal();
                        }
                    }
                }
            }
        }

        return hit;
    }
}

public struct HitInfo
{
    public Triangle triangle;
    public float distance;
    public Vector3D position;
    public Vector3D normal;
}