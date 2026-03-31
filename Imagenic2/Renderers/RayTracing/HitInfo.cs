using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Renderers.RayTracing;

internal struct HitInfo
{
    public Triangle triangle;
    public float distance;
    public Vector3D position;
    public Vector3D normal;
}