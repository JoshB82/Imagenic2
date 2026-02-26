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
            if (value.ApproxEquals(radiusX)) return;
            radiusX = value;
            Scaling = new Vector3D(radiusX, radiusZ, 1);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    public float RadiusZ
    {
        get => radiusZ;
        set
        {
            if (value.ApproxEquals(radiusZ)) return;
            radiusZ = value;
            Scaling = new Vector3D(radiusX, radiusZ, 1);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
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
            Structure = MeshStructure.GenerateCircleStructure(resolution);
            foreach (Vertex vertex in Structure.Vertices)
            {
                vertex.Scaling = new Vector3D(radiusX, radiusZ, 1);
            }
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
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