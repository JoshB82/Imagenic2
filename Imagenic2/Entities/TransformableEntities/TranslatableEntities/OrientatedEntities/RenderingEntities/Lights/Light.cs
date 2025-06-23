namespace Imagenic2.Core.Entities;

public abstract class Light : RenderingEntity
{
    #region Fields and Properties

    #endregion

    #region Constructors

    protected Light(Vector3D worldOrigin,
                    Orientation worldOrientation,
                    float viewWidth,
                    float viewHeight,
                    float zNear,
                    float zFar)
        : base(worldOrigin, worldOrientation, viewWidth, viewHeight, zNear, zFar)
    {

    }

    #endregion

    #region Methods

    #endregion
}