using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Sphere : Mesh
{
    #region Fields and Properties

    private float radius;
    public float Radius
    {
        get => radius;
        set
        {
            if (value.ApproxEquals(radius)) return;
            radius = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    private float latResolution;
    public float LatResolution
    {
        get => latResolution;
        set
        {
            if (value.ApproxEquals(latResolution)) return;
            latResolution = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    private float longResolution;
    public float LongResolution
    {
        get => longResolution;
        set
        {
            if (value.ApproxEquals(longResolution)) return;
            longResolution = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    public Sphere(Vector3D worldOrigin, Orientation worldOrientation, float radius, int latResolution, int longResolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateSphereStructure(latResolution, longResolution))
    {
        Radius = radius;
        LatResolution = latResolution;
        LongResolution = longResolution;
    }

    #endregion

    #region Methods

    #endregion
}