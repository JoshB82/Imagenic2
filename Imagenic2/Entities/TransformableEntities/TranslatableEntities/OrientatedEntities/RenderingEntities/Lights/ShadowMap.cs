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
        }
    }
    public int Height
    {
        get => height;
        set
        {
            height = value;
        }
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