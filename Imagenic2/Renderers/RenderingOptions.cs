using Imagenic2.Core.Entities;
using Imagenic2.Core.Maths.Transformations;
using Imagenic2.Core.Renderers.Rasterising;
using System.Drawing;

namespace Imagenic2.Core.Renderers;

public sealed class RenderingOptions
{
    #region Fields and Properties

    //internal Rasteriser<Imagenic2.Core.Images.Bitmap> Renderer { get; set; }

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
            UpdateBuffers();
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
            renderHeight = value;
            UpdateScreenToWindow();
            UpdateBuffers();
        }
    }

    private void UpdateBuffers()
    {
        //Renderer.colourBuffer = new Buffer2D<Color>(RenderWidth, RenderHeight);
        //Renderer.zBuffer = new Buffer2D<float>(RenderWidth, RenderHeight);
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

    private Color backgroundColour = Color.White;
    public Color BackgroundColour
    {
        get => backgroundColour;
        set
        {
            backgroundColour = value;
            //InvokeRenderEvent(RenderUpdate.NewRender);
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