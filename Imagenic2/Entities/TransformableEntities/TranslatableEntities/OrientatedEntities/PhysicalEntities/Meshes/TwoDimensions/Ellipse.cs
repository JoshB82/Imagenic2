using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Encapsulates the mesh of an ellipse.
/// </summary>
public sealed class Ellipse : Mesh
{
    #region Fields and Properties
    
    private float radiusX, radiusZ;
    
    public float RadiusX
    {
        get => radiusX;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(radiusX)) return;
            radiusX = value;
            Scaling = new Vector3D(radiusX, 1, radiusZ);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    public float RadiusZ
    {
        get => radiusZ;
        set
        {
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(radiusZ)) return;
            radiusZ = value;
            Scaling = new Vector3D(radiusX, 1, radiusZ);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    private int resolution;
    public int Resolution
    {
        get => resolution;
        set
        {
            ThrowIfNotFinite(value);
            ThrowIfNonpositive(value);
            if (value == resolution) return;
            resolution = value;
            Structure = MeshStructure.GenerateCircleStructure(resolution);
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Ellipse(Vector3D worldOrigin, Orientation worldOrientation, float radiusX, float radiusZ, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateCircleStructure(resolution))
    {
        RadiusX = radiusX;
        RadiusZ = radiusZ;
        Resolution = resolution;
    }

    #endregion

    #region Methods

    public override Ellipse ShallowCopy() => (Ellipse)MemberwiseClone();
    public override Ellipse DeepCopy()
    {
        var ellipse = (Ellipse)base.DeepCopy();
        return ellipse;
    }

    #endregion
}