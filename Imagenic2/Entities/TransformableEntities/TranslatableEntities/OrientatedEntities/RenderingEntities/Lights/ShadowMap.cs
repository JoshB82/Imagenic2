using Imagenic2.Core.Maths.Transformations;
using Imagenic2.Core.Renderers;

namespace Imagenic2.Core.Entities.Lights;

public sealed class ShadowMap
{
    #region Fields and Properties

    private int width = 1000, height = 1000;
    public int Width
    {
        get => width;
        set
        {
            width = value;
            UpdateScreenToWindow();
        }
    }
    public int Height
    {
        get => height;
        set
        {
            height = value;
            UpdateScreenToWindow();
        }
    }

    public Matrix4x4 ScreenToWindow { get; private set; }
    public void UpdateScreenToWindow()
    {
        ScreenToWindow = Transform.Scale(0.5f * (width - 1), 0.5f * (height - 1), 1) * RenderingOptions.windowTranslate;
    }

    internal Buffer2D<float> Data { get; set; }

    #endregion

    #region Constructors

    public ShadowMap(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new Buffer2D<float>(width, height);
    }

    #endregion

    #region Methods

    #endregion
}