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
            radius = value;
            Scaling = new Vector3D(radius, height, radius);
        }
    }
    public float Height
    {
        get => height;
        set
        {
            height = value;
            Scaling = new Vector3D(radius, height, radius);
        }
    }
    private int resolution;
    public int Resolution
    {
        get => resolution;
        set
        {
            resolution = value;
            Structure = MeshStructure.GenerateCylinderStructure(resolution);
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