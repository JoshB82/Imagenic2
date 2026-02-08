using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Renderers;

public abstract class Renderer<TImage> where TImage : Image
{
    #region Fields and Properties

    internal static readonly ClippingPlane[] ScreenClippingPlanes = new ClippingPlane[]
    {
        new(-Vector3D.One, Vector3D.UnitX), // Left
        new(-Vector3D.One, Vector3D.UnitY), // Bottom
        new(-Vector3D.One, Vector3D.UnitZ), // Near
        new(Vector3D.One, Vector3D.UnitNegativeX), // Right
        new(Vector3D.One, Vector3D.UnitNegativeY), // Top
        new(Vector3D.One, Vector3D.UnitNegativeZ) // Far
    };
    
    internal Buffer2D<float> zBuffer;
    internal bool NewRenderNeeded { get; set; }

    private Camera renderCamera;
    public virtual Camera RenderCamera
    {
        get => renderCamera;
        set
        {
            renderCamera = value;
            NewRenderNeeded = true;

        }
    }

    public Matrix4x4 ScreenToWindow { get; private set; }
    protected static readonly Matrix4x4 windowTranslate = Transform.Translate(new Vector3D(1, 1, 0));

    private RenderingOptions renderingOptions;
    public RenderingOptions RenderingOptions
    {
        get => renderingOptions;
        set
        {
            renderingOptions = value;
        }
    }

    #endregion

    #region Constructors

    public Renderer(RenderingOptions renderingOptions)
    {
        RenderingOptions = renderingOptions;
    }

    #endregion

    #region Methods

    public abstract Task<TImage?> RenderAsync(CancellationToken token);

    #endregion
}