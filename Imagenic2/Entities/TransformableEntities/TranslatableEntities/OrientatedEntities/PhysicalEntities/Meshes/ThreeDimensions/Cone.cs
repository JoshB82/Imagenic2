using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Cone : Mesh
{
    #region Fields and Properties
    
    private float radius, height;
    
    public float Radius
    {
        get => radius;
        set
        {
            if (value.ApproxEquals(radius)) return;
            radius = value;
            Scaling = new Vector3D(radius, height, radius);
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    public float Height
    {
        get => height;
        set
        {
            if (value.ApproxEquals(height)) return;
            height = value;
            Scaling = new Vector3D(radius, height, radius);
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }

    private int resolution;
    public int Resolution
    {
        get => resolution;
        set
        {
            if (value == resolution) return;
            resolution = value;
            Structure = MeshStructure.GenerateConeStructure(resolution);
            foreach (Vertex vertex in Structure.Vertices)
            {
                vertex.Scaling = new Vector3D(radius, height, radius);
            }
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    public Cone(Vector3D worldOrigin, Orientation worldOrientation, float height, float radius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateConeStructure(resolution))
    {
        Radius = radius;
        Height = height;
        Resolution = resolution;
    }
    
    #endregion
    
    #region Methods
    
    public override Cone ShallowCopy() => (Cone)MemberwiseClone();
    public override Cone DeepCopy()
    {
        var cone = (Cone)base.DeepCopy();
        cone.radius = radius;
        cone.height = height;
        cone.resolution = resolution;
        return cone;
    }

    #endregion
}