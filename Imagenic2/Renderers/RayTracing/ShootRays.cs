using Imagenic2.Core.Entities;
using System.Drawing;

namespace Imagenic2.Core.Renderers.RayTracing;

public partial class RayTracer<TImage>
{
    private static readonly Random random = new Random();

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

                    mesh.Structure.BoundingBox.TransformVertices(modelToView);
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
                float u = (x + GenerateRandomOffset()) / renderWidth;
                float v = (y + GenerateRandomOffset()) / renderHeight;
                Vector3D direction = (new Vector3D((2 * u - 1) * viewWidth, (2 * v - 1) * viewHeight, renderingEntity.ZNear)).Normalise();

                Ray ray = new Ray(Vector3D.Zero, direction);
                Color colour = TraceRay(ray).ToSystemDrawingColor();
                colourBuffer[x, y] = colour;
            });
        });
    }

    private float GenerateRandomOffset()
    {
        return (float)random.NextDouble();
    }

    private Vector3D TraceRay(Ray ray, int depth = 5)
    {
        if (depth == 0)
        {
            return Vector3D.Zero;
        }

        if (!ClosestHit(ray, out HitInfo hitInfo))
        {
            return RenderingOptions.BackgroundColour.ToVector3D();
        }

        Vector3D colour;
        switch (hitInfo.triangle.FrontStyle)
        {
            case Material triangleMaterial:
                colour = Vector3D.Zero;
                Lighting();
                Reflection(triangleMaterial);
                if (triangleMaterial is EmissiveMaterial em)
                {
                    Emission(em);
                }
                break;
            case SolidStyle solidStyle:
                colour = solidStyle.Colour.ToVector3D();
                Lighting();
                break;
            default:
                colour = Vector3D.Zero;
                break;
        }

        void Lighting()
        {
            foreach (Light light in RenderingOptions.Lights)
            {
                if (!IsInShadow(hitInfo.position, light))
                {
                    colour = Shade(hitInfo, light, colour);
                }
            }
        }

        void Reflection(Material triangleMaterial)
        {
            if (triangleMaterial.Reflectivity > 0)
            {
                Vector3D reflectionDirection = (ray.direction - 2 * (ray.direction * hitInfo.normal) * hitInfo.normal);
                Vector3D reflectedColour = TraceRay(new Ray(hitInfo.position, reflectionDirection), depth - 1);
                colour = colour * (1 - triangleMaterial.Reflectivity) + reflectedColour * triangleMaterial.Reflectivity;
            }
        }

        void Emission(EmissiveMaterial emissiveMaterial)
        {
            Vector3D emission = emissiveMaterial.EmissionColour.ToVector3D() * emissiveMaterial.EmissionIntensity;
            colour += emission;
        }

        return colour;
    }

    internal static Vector3D Shade(HitInfo hitInfo, Light light, Vector3D colour)
    {
        Vector3D lightColour = new Vector3D(light.Colour.R, light.Colour.G, light.Colour.B);
        Vector3D lightVector = (light.WorldOrigin - hitInfo.position).Normalise();
        float dot = Max(hitInfo.normal * lightVector, 0);
        colour += lightColour * dot * light.Intensity;

        return colour;
    }

    private bool IsInShadow(Vector3D point, Light light)
    {
        Vector3D lightVector = light.WorldOrigin - point;
        float distance = lightVector.Magnitude();
        Ray shadowRay = new Ray(point, lightVector);

        if (ClosestHit(shadowRay, out HitInfo hitInfo))
        {
            //return hitInfo.distance < distance;
            return hitInfo.distance.ApproxLessThan(distance, 1e-6f);
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
                if (ray.IntersectBoundingBox(mesh.Structure.BoundingBox, out float boundingBoxDistance))
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
        }

        return hit;
    }
}

internal struct HitInfo
{
    public Triangle triangle;
    public float distance;
    public Vector3D position;
    public Vector3D normal;
}