using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Cylinder : Mesh
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
            Structure = MeshStructure.GenerateCylinderStructure(resolution);
            foreach (Vertex vertex in Structure.Vertices)
            {
                vertex.Scaling = new Vector3D(radius, height, radius);
            }
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Cylinder(Vector3D worldOrigin, Orientation worldOrientation, float height, float radius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateCylinderStructure(resolution))
    {
        Radius = radius;
        Height = height;
        Resolution = resolution;
    }
    #endregion
    
    #region Methods
    
    #endregion
}