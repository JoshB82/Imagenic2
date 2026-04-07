using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Encapsulates the mesh of a ring.
/// </summary>
public sealed class Ring : Mesh
{
    #region Fields and Properties
    
    private float innerRadius, outerRadius;
    
    public float InnerRadius
    {
        get => innerRadius;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(innerRadius)) return;
            innerRadius = value;
            Structure = MeshStructure.GenerateRingStructure(resolution, innerRadius, outerRadius);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    public float OuterRadius
    {
        get => outerRadius;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(outerRadius)) return;
            outerRadius = value;
            Structure = MeshStructure.GenerateRingStructure(resolution, innerRadius, outerRadius);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    private int resolution;
    public int Resolution
    {
        get => resolution;
        set
        {
            ThrowIfNonpositive(value);
            if (value == resolution) return;
            resolution = value;
            Structure = MeshStructure.GenerateRingStructure(resolution, innerRadius, outerRadius);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Ring(Vector3D worldOrigin, Orientation worldOrientation, float innerRadius, float outerRadius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateRingStructure(resolution, innerRadius, outerRadius))
    {
        this.innerRadius = innerRadius;
        this.outerRadius = outerRadius;
        this.resolution = resolution;
    }

    #endregion

    #region Methods

    public override Ring ShallowCopy() => (Ring)MemberwiseClone();
    public override Ring DeepCopy()
    {
        var ring = (Ring)base.DeepCopy();
        return ring;
    }

    #endregion
}