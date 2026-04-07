using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Encapsulates the mesh of a cylinder.
/// </summary>
public sealed class Cylinder : Mesh
{
    #region Fields and Properties
    
    private float radius, height;
    
    public float Radius
    {
        get => radius;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(radius)) return;
            radius = value;
            Scaling = new Vector3D(radius, height, radius);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    public float Height
    {
        get => height;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(height)) return;
            height = value;
            Scaling = new Vector3D(radius, height, radius);
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
            Structure = MeshStructure.GenerateCylinderStructure(resolution);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Cylinder(Vector3D worldOrigin, Orientation worldOrientation, float height, float radius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateCylinderStructure(resolution))
    {
        Radius = radius;
        Height = height;
        this.resolution = resolution;
    }
    #endregion

    #region Methods

    public override Cylinder ShallowCopy() => (Cylinder)MemberwiseClone();
    public override Cylinder DeepCopy()
    {
        var cylinder = (Cylinder)base.DeepCopy();
        return cylinder;
    }

    #endregion
}