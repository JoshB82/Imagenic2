using Imagenic2.Core.Renderers.RayTracing;

namespace Imagenic2.Core.Entities;

public class Material : FaceStyle
{
    #region Fields and Properties

    public float Roughness { get; set; }
    public float Metallic { get; set; }
    public Imagenic2.Core.Images.Image Texture { get; set; }

    private float reflectivity;
    public float Reflectivity
    {
        get => reflectivity;
        set
        {
            ThrowIfNotWithinRange(value, 0, 1);
            reflectivity = value;
        }
    }

    private float transparency;
    public float Transparency
    {
        get => transparency;
        set
        {
            transparency = value;
        }
    }

    private float refractiveIndex;
    public float RefractiveIndex
    {
        get => refractiveIndex;
        set
        {
            refractiveIndex = value;
        }
    }

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

    #endregion
}