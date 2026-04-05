using Imagenic2.Core.Renderers.RayTracing;

namespace Imagenic2.Core.Entities;

public class Material : FaceStyle
{
    #region Fields and Properties

    private float roughness;
    public float Roughness
    {
        get => roughness;
        set
        {
            ThrowIfNotFinite(value);
            roughness = value;
        }
    }
    public float Metallic { get; set; }
    public Imagenic2.Core.Images.Image Texture { get; set; }

    private float reflectivity;
    public float Reflectivity
    {
        get => reflectivity;
        set
        {
            ThrowIfNotFinite(value);
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
            ThrowIfNotFinite(value);
            transparency = value;
        }
    }

    private float refractiveIndex;
    public float RefractiveIndex
    {
        get => refractiveIndex;
        set
        {
            ThrowIfNotFinite(value);
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