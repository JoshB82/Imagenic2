using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;
using Imagenic2.Core.Maths.Transformations;
using System.Drawing;

namespace Imagenic2.Core.Renderers;

public sealed class RenderingOptions
{
    #region Fields and Properties

    internal event Action<RenderUpdate>? RenderAlteringPropertyChanged;

    // Render size
    private int renderWidth = 1920, renderHeight = 1080;
    public int RenderWidth
    {
        get => renderWidth;
        set
        {
            if (value == renderWidth) return;
            ThrowIfNonpositive(value);
            renderWidth = value;
            UpdateScreenToWindow();
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }
    public int RenderHeight
    {
        get => renderHeight;
        set
        {
            if (value == renderHeight) return;
            ThrowIfNonpositive(value);
            renderHeight = value;
            UpdateScreenToWindow();
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    // Render camera
    private Camera renderCamera;
    public Camera RenderCamera
    {
        get => renderCamera;
        set
        {
            renderCamera = value;
            renderCamera.RenderAlteringPropertyChanged += args => InvokeRenderEvent(args);
            InvokeRenderEvent(RenderUpdate.NewRender | RenderUpdate.NewShadowMap);
        }
    }

    // Background colour
    private Color backgroundColour = Color.White;
    public Color BackgroundColour
    {
        get => backgroundColour;
        set
        {
            backgroundColour = value;
            InvokeRenderEvent(RenderUpdate.NewRender);
        }
    }

    // Matrices
    public Matrix4x4 WindowToScreen { get; private set; }
    public Matrix4x4 ScreenToWindow { get; private set; }
    internal static readonly Matrix4x4 windowTranslate = MathsHelper.Translate(new Vector3D(1, 1, 0));
    public void UpdateScreenToWindow()
    {
        ScreenToWindow = MathsHelper.Scale(0.5f * (renderWidth - 1), 0.5f * (renderHeight - 1), 1) * windowTranslate;
    }
    
    public List<PhysicalEntity>? PhysicalEntitiesToRender { get; private set; } = new List<PhysicalEntity>();

    public List<Light> Lights { get; private set; } = new List<Light>();

    #endregion

    #region Constructors

    public RenderingOptions(Camera renderCamera)
    {
        RenderCamera = renderCamera;
    }

    #endregion

    #region Methods

    public RenderingOptions AddToRender(params IEnumerable<PhysicalEntity> physicalEntitiesToRender)
    {
        foreach (PhysicalEntity physicalEntity in physicalEntitiesToRender)
        {
            physicalEntity.RenderAlteringPropertyChanged += args => InvokeRenderEvent(args);
            PhysicalEntitiesToRender.Add(physicalEntity);
        }

        return this;
    }

    public RenderingOptions AddToRender(params IEnumerable<Light> lights)
    {
        foreach (Light light in lights)
        {
            light.RenderAlteringPropertyChanged += args => InvokeRenderEvent(args);
            Lights.Add(light);
        }
        return this;
    }

    private void InvokeRenderEvent(RenderUpdate args)
    {
        RenderAlteringPropertyChanged?.Invoke(args);
    }

    #endregion
}