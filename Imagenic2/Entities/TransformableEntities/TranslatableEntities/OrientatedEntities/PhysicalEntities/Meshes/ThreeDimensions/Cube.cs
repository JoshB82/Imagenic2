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

    public Cube(Vector3D worldOrigin, Orientation worldOrientation, float sideLength)
    {
        SideLength = sideLength;
    }

    #endregion

    #region Methods

    #endregion
}