using Imagenic2.Core.Entities.Lights;
using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public abstract class Light : RenderingEntity
{
    #region Fields and Properties

    private IEnumerable<ShadowMap> shadowMaps = new List<ShadowMap>();
    public IEnumerable<ShadowMap> ShadowMaps
    {
        get => shadowMaps;
        set
        {
            shadowMaps = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

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

    public override Light ShallowCopy() => (Light)MemberwiseClone();
    public override Light DeepCopy()
    {
        var light = (Light)base.DeepCopy();
        light.shadowMaps = new List<ShadowMap>(shadowMaps);
        return light;
    }

    #endregion
}