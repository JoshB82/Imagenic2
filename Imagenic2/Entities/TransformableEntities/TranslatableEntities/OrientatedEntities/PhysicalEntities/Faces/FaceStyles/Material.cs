using Imagenic2.Core.Renderers.RayTracing;
using System.Drawing;

namespace Imagenic2.Core.Entities;

public class Material : FaceStyle
{
    #region Fields and Properties

    public float Roughness { get; set; }
    public float Metallic { get; set; }
    public Imagenic2.Core.Images.Image Texture { get; set; }

    #endregion

    #region Constructors

    public Material() : base()
    {
        
    }

    #endregion

    #region Methods

    public override Material DeepCopy()
    {
        Material newMaterial = new Material();
        newMaterial.Texture = Texture;
        return newMaterial;
    }

    public Color Shade(HitInfo hitInfo, Light light)
    {
        Color baseColour = Color.Black;
        Vector3D colour = new Vector3D(baseColour.R, baseColour.G, baseColour.B);
        Vector3D lightColour = new Vector3D(light.Colour.R, light.Colour.G, light.Colour.B);
        Vector3D lightVector = (light.WorldOrigin - hitInfo.position).Normalise();
        float dot = Max(hitInfo.normal * lightVector, 0);
        colour += lightColour * dot * light.Intensity;

        return colour.ToSystemDrawingColor();
    }

    public Func<HitInfo, Color> shade;

    

    #endregion
}