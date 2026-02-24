using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Circle : Mesh
{
    #region Fields and Properties

    private float radius;
    /// <summary>
    /// The radius of the <see cref="Circle"/>.
    /// </summary>
    public float Radius
    {
        get => radius;
        set
        {
            if (value.ApproxEquals(radius)) return;
            radius = value;
            Scaling = new Vector3D(radius, radius, 1);
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
            Structure = MeshStructure.GenerateCircleStructure(resolution);
            foreach (Vertex vertex in Structure.Vertices)
            {
                vertex.Scaling = new Vector3D(radius, radius, 1);
            }
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    public Circle(Vector3D worldOrigin, Orientation worldOrientation, float radius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateCircleStructure(resolution))
    {
        Radius = radius;
        Resolution = resolution;
    }

    #endregion
}