using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public sealed class Square : Mesh
{
    #region Fields and Properties
    
    private float sideLength;
    /// <summary>
    /// The length of the sides of the <see cref="Square"/>.
    /// </summary>
    public float SideLength
    {
        get => sideLength;
        set
        {
            if (value.ApproxEquals(sideLength)) return;
            sideLength = value;
            Scaling = new Vector3D(sideLength, sideLength, 1);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    public Square(Vector3D worldOrigin, Orientation worldOrientation, float sideLength) : base(worldOrigin, worldOrientation, MeshStructure.planeStructure.DeepCopy())
    {
        SideLength = sideLength;
    }

    #endregion

    #region Methods

    public override Square ShallowCopy() => (Square)MemberwiseClone();
    public override Square DeepCopy()
    {
        var square = (Square)base.DeepCopy();
        return square;
    }

    public static implicit operator Plane(Square square)
    {
        Plane plane = new Plane(square.WorldOrigin, square.WorldOrientation, square.sideLength, square.sideLength);
        return plane;
    }

    #endregion
}