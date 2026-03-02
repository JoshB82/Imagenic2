namespace Imagenic2.Core.Entities;

public sealed class DistantLight : Light
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public DistantLight(Vector3D worldOrigin, Orientation worldOrientation, float viewWidth, float viewHeight, float zNear, float zFar) : base(worldOrigin, worldOrientation, viewWidth, viewHeight, zNear, zFar)
    {

    }

    #endregion

    #region Methods

    public override DistantLight ShallowCopy() => (DistantLight)MemberwiseClone();
    public override DistantLight DeepCopy()
    {
        var distantLight = (DistantLight)base.DeepCopy();
        return distantLight;
    }

    #endregion
}