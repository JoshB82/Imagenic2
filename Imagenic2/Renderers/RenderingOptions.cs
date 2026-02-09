using Imagenic2.Core.Entities;
using Imagenic2.Core.Maths.Transformations;

namespace Imagenic2.Core.Renderers;

public sealed class RenderingOptions
{
    #region Fields and Properties

    public Matrix4x4 WindowToScreen { get; private set; }

    private int renderWidth = 1920, renderHeight = 1080;

    public int RenderWidth
    {
        get => renderWidth;
        set
        {
            if (value == renderWidth) return;
            if (value < 0)
            {
                // error!
            }
            renderWidth = value;
            UpdateScreenToWindow();
        }
    }
    public int RenderHeight
    {
        get => renderHeight;
        set
        {
            if (value == renderHeight) return;
            if (value < 0)
            {
                // error!
            }
            UpdateScreenToWindow();
            renderHeight = value;
        }
    }

    private Camera renderCamera;
    public Camera RenderCamera
    {
        get => renderCamera;
        set
        {
            renderCamera = value;
            //NewRenderNeeded = true;
        }
    }

    public Matrix4x4 ScreenToWindow { get; private set; }
    private static readonly Matrix4x4 windowTranslate = Transform.Translate(new Vector3D(1, 1, 0));
    public void UpdateScreenToWindow()
    {

        ScreenToWindow = Transform.Scale(0.5f * (renderWidth - 1), 0.5f * (renderHeight - 1), 1) * windowTranslate;
    }
    
    public IEnumerable<PhysicalEntity>? PhysicalEntitiesToRender { get; set; } = new List<PhysicalEntity>();

    #endregion

    #region Constructors

    #endregion

    #region Methods

    #endregion
}