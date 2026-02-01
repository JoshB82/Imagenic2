namespace Imagenic2.Core.Entities;

public class Cube : Mesh
{
    #region Fields and Properties
    
    private float sideLength;

    public float SideLength
    {
        get => sideLength;
        set
        {
            sideLength = value;
            Scaling = new Vector3D(sideLength, sideLength, sideLength);
        }
    }

    #endregion

    #region Constructors

    public Cube(Vector3D worldOrigin, Orientation worldOrientation, float sideLength) : base(worldOrigin, worldOrientation, MeshStructure.cubeStructure)
    {
        SideLength = sideLength;
    }

    #endregion

    #region Methods

    public override Cube ShallowCopy() => (Cube)MemberwiseClone();
    public override Cube DeepCopy()
    {
        var cube = (Cube)base.DeepCopy();
        cube.sideLength = sideLength;
        return cube;
    }

    /// <summary>
    /// Casts a <see cref="Cube"/> into a <see cref="Cuboid"/>.
    /// </summary>
    /// <param name="cube"><see cref="Cube"/> to cast.</param>
    public static implicit operator Cuboid(Cube cube)
    {
        Cuboid cuboid = new Cuboid(cube.WorldOrigin, cube.WorldOrientation, cube.sideLength, cube.sideLength, cube.sideLength);
        return cuboid;
    }

    #endregion
}