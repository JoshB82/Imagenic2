using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Ellipse : Mesh
{
    #region Fields and Properties
    
    private float radiusX, radiusZ;
    
    public float RadiusX
    {
        get => radiusX;
        set
        {
            radiusX = value;
            Scaling = new Vector3D(radiusX, radiusZ, 1);
        }
    }
    public float RadiusZ
    {
        get => radiusZ;
        set
        {
            radiusZ = value;
            Scaling = new Vector3D(radiusX, radiusZ, 1);
        }
    }
    private int resolution;
    public int Resolution
    {
        get => resolution;
        set
        {
            resolution = value;
            Structure = MeshStructure.GenerateCircleStructure(resolution);
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Ellipse(Vector3D worldOrigin, Orientation worldOrientation, float radiusX, float radiusY, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateCircleStructure(resolution))
    {
        RadiusX = radiusX;
        RadiusZ = radiusZ;
        Resolution = resolution;
    }

    #endregion
}