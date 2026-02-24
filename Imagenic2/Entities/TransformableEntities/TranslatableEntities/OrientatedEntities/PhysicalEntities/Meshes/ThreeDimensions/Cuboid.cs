using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public class Cuboid : Mesh
{
    #region Fields and Properties

    private float length, width, height;

    /// <summary>
    /// The length of the <see cref="Cuboid"/>.
    /// </summary>
    public float Length
    {
        get => length;
        set
        {
            if (value.ApproxEquals(length)) return;
            length = value;
            Scaling = new Vector3D(length, width, height);
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    /// <summary>
    /// The width of the <see cref="Cuboid"/>.
    /// </summary>
    public float Width
    {
        get => width;
        set
        {
            if (value.ApproxEquals(width)) return;
            width = value;
            Scaling = new Vector3D(length, width, height);
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }
    /// <summary>
    /// The height of the <see cref="Cuboid"/>.
    /// </summary>
    public float Height
    {
        get => height;
        set
        {
            if (value.ApproxEquals(height)) return;
            height = value;
            Scaling = new Vector3D(length, width, height);
            InvokeRenderEvent(RenderUpdate.NewRender & RenderUpdate.NewShadowMap);
        }
    }

    #endregion

    #region Constructors

    public Cuboid(Vector3D worldOrigin, Orientation worldOrientation, float length, float width, float height) : base(worldOrigin, worldOrientation, MeshStructure.cubeStructure)
    {
        Length = length;
        Width = width;
        Height = height;
    }

    #endregion

    #region Methods

    public override Cuboid ShallowCopy() => (Cuboid)MemberwiseClone();
    public override Cuboid DeepCopy()
    {
        var cuboid = (Cuboid)base.DeepCopy();
        cuboid.length = length;
        cuboid.height = height;
        cuboid.width = width;
        return cuboid;
    }

    /// <summary>
    /// Casts a <see cref="Cuboid"/> into a <see cref="Cube"/>.
    /// </summary>
    /// <param name="cuboid"><see cref="Cuboid"/> to cast.</param>
    public static explicit operator Cube(Cuboid cuboid)
    {
        Cube cube = new Cube(cuboid.WorldOrigin, cuboid.WorldOrientation, Math.Min(Math.Min(cuboid.Length, cuboid.Width), cuboid.Height));
        return cube;
    }

    #endregion
}