using Imagenic2.Core.Entities.Lights;
using Imagenic2.Core.Enums;
using System.Drawing;

namespace Imagenic2.Core.Entities;

public abstract class Light : RenderingEntity
{
    #region Fields and Properties

    private List<ShadowMap> shadowMaps = new List<ShadowMap>();
    public List<ShadowMap> ShadowMaps
    {
        get => shadowMaps;
        set
        {
            shadowMaps = value;
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    public Color Colour { get; set; } = Color.Blue;

    private float intensity = 1;
    public float Intensity
    {
        get => intensity;
        set
        {
            if (value == intensity) return;
            intensity = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
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