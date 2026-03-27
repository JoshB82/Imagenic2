using Imagenic2.Core.Entities;
using Imagenic2.Core.Renderers.Rasterising;

namespace Imagenic2.Core.Renderers.RayTracing;

public partial class RayTracer<TImage>
{
    private static readonly Random random = new Random();

    private const int numberOfRaysPerPixel = 5;

    private void CastRaysFromCamera(RenderingEntity renderingEntity)
    {
        // Cast rays
        float viewWidth = renderingEntity.ViewWidth;
        float viewHeight = renderingEntity.ViewHeight;
        int renderWidth = RenderingOptions.RenderWidth;
        int renderHeight = RenderingOptions.RenderHeight;

        Vector3D worldStartPosition = (Vector3D)(RenderingOptions.RenderCamera.ModelToWorld * Vector4D.UnitW);
        Parallel.For(0, renderWidth * renderHeight, i =>
        {
            int x = i % renderWidth;
            int y = i / renderWidth;

            Vector3D accumulatedColour = Vector3D.Zero;

            for (int j = 1; j <= numberOfRaysPerPixel; j++)
            {
                float u = (x + GenerateRandomOffset()) / renderWidth;
                float v = (y + GenerateRandomOffset()) / renderHeight;

                Vector3D worldDirection = ((Vector3D)(RenderingOptions.RenderCamera.ModelToWorld * new Vector4D((2 * u - 1) * viewWidth, (2 * v - 1) * viewHeight, renderingEntity.ZNear, 0))).Normalise();

                Ray ray = new Ray(worldStartPosition, worldDirection);
                accumulatedColour += TraceRay(ray);
            }

            Vector3D finalColour = accumulatedColour / numberOfRaysPerPixel;
            colourBuffer[x, y] = finalColour.ToSystemDrawingColor();
        });

        // Draw edges
        Rasteriser<TImage> r = new Rasteriser<TImage>(RenderingOptions); // Temporary
        r.RenderMeshEdges();
    }

    private static float GenerateRandomOffset()
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
                if (!IsInShadow(hitInfo.position, light, hitInfo.normal))
                {
                    colour = Shade(hitInfo, light, colour);
                }
            }
        }

        void Reflection(Material triangleMaterial)
        {
            if (triangleMaterial.Reflectivity > 0 || triangleMaterial.Transparency > 0)
            {
                Vector3D offset = hitInfo.normal * 1e-3f;

                float kr = Fresnel(ray.direction, hitInfo.normal, triangleMaterial.RefractiveIndex);

                Vector3D reflectionDirection = (ray.direction - 2 * (ray.direction * hitInfo.normal) * hitInfo.normal);
                Vector3D reflectedColour = TraceRay(new Ray(hitInfo.position + offset, reflectionDirection), depth - 1);

                Vector3D refractionColour = Vector3D.Zero;

                if (triangleMaterial.Transparency > 0)
                {
                    Vector3D normal = hitInfo.normal;
                    float etai = 1, etat = triangleMaterial.RefractiveIndex;

                    if ((ray.direction * normal).ApproxMoreThan(0))
                    {
                        normal = -normal;
                        (etai, etat) = (etat, etai);
                    }

                    float eta = etai / etat;

                    if (Refract(ray.direction, normal, eta, out Vector3D refractionDirection))
                    {
                        Ray refractedRay = new Ray(hitInfo.position - normal * 1e-3f, refractionDirection);
                        refractionColour = TraceRay(refractedRay, depth - 1);
                    }
                }

                colour = colour * (1 - triangleMaterial.Reflectivity)
                                + reflectedColour * kr
                                + refractionColour * (1 - kr) * triangleMaterial.Transparency;
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

    private bool IsInShadow(Vector3D point, Light light, Vector3D normal)
    {
        Vector3D offset = normal * 1e-3f;

        Vector3D lightVector = light.WorldOrigin - point;
        float distance = lightVector.Magnitude();
        Ray shadowRay = new Ray(point + offset, lightVector.Normalise());

        if (ClosestHit(shadowRay, out HitInfo hitInfo))
        {
            //return hitInfo.distance < distance;
            return hitInfo.distance.ApproxLessThan(distance, 1e-6f);
        }

        return false;
    }

    private static bool Refract(Vector3D incident, Vector3D normal, float eta, out Vector3D refracted)
    {
        float cosi = Math.Clamp(-(incident * normal), -1, 1);
        float sint2 = eta * eta * (1 - cosi * cosi);

        if (sint2.ApproxMoreThan(1))
        {
            // Total internal reflection
            refracted = Vector3D.Zero;
            return false;
        }

        float cost = MathF.Sqrt(1 - sint2);
        refracted = eta * incident + (eta * cosi - cost) * normal;
        return true;
    }

    private static float Fresnel(Vector3D incident, Vector3D normal, float ior)
    {
        float cosi = Math.Clamp(-(incident * normal), -1, 1);
        float etai = 1, etat = ior;

        if (cosi.ApproxMoreThan(0))
        {
            (etai, etat) = (etat, etai);
        }

        float r0 = (etai - etat) / (etai + etat);
        r0 = r0 * r0;

        return r0 + (1 - r0) * MathF.Pow(1 - cosi, 5);
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
                Vector3D modelStartPosition = (Vector3D)(mesh.WorldToModel * new Vector4D(ray.startPosition, 1));
                Vector3D modelDirection = ((Vector3D)(mesh.WorldToModel * new Vector4D(ray.direction, 0))).Normalise();
                Ray modelRay = new Ray(modelStartPosition, modelDirection);

                if (modelRay.IntersectBoundingBox(mesh.Structure.BoundingBox, out float boundingBoxDistance))
                {
                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        if (modelRay.IntersectTriangle(triangle, out float distance))
                        {
                            Vector3D hitPosition = (Vector3D)(mesh.ModelToWorld * new Vector4D(modelRay.startPosition + modelRay.direction * distance, 1));
                            float worldDistance = (hitPosition - ray.startPosition).Magnitude();

                            if (worldDistance < closestDistance)
                            {
                                hit = true;
                                closestDistance = worldDistance;

                                hitInfo.distance = closestDistance;
                                hitInfo.triangle = triangle;
                                hitInfo.position = hitPosition;
                                hitInfo.normal = ((Vector3D)(mesh.ModelToWorld * new Vector4D(triangle.CalculateNormal(), 0))).Normalise();
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