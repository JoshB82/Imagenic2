using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

/// <summary>
/// Encapsulates the mesh of a circle.
/// </summary>
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
            ThrowIfNotFinite(value);
            if (value.ApproxEquals(radius)) return;
            radius = value;
            Scaling = new Vector3D(radius, 1, radius);
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

    public Circle(Vector3D worldOrigin, Orientation worldOrientation, float radius, int resolution) : base(worldOrigin, worldOrientation, MeshStructure.GenerateCircleStructure(resolution))
    {
        Radius = radius;
        this.resolution = resolution;
    }

    #endregion

    #region Methods

    public override Circle ShallowCopy() => (Circle)MemberwiseClone();
    public override Circle DeepCopy()
    {
        var circle = (Circle)base.DeepCopy();
        return circle;
    }

    public static implicit operator Ellipse(Circle circle)
    {
        Ellipse ellipse = new Ellipse(circle.WorldOrigin, circle.WorldOrientation, circle.Radius, circle.Radius, circle.Resolution);
        return ellipse;
    }

    #endregion
}