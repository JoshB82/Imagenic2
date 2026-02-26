using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Plane : Mesh
{
    #region Fields and Properties
    
    private float length, width;
    
    /// <summary>
    /// The length of the <see cref="Plane"/>.
    /// </summary>
    public float Length
    {
        get => length;
        set
        {
            if (value.ApproxEquals(length)) return;
            length = value;
            Scaling = new Vector3D(length, width, 1);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    /// <summary>
    /// The width of the <see cref="Plane"/>.
    /// </summary>
    public float Width
    {
        get => width;
        set
        {
            if (value.ApproxEquals(width)) return;
            width = value;
            Scaling = new Vector3D(length, width, 1);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    
    #endregion
    
    #region Constructors
    
    public Plane(Vector3D worldOrigin, Orientation worldOrientation, float length, float width) : base(worldOrigin, worldOrientation, MeshStructure.planeStructure)
    {
        Length = length;
        Width = width;
    }

    #endregion
}