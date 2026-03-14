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

    internal Buffer2D<Tile> Tiles { get; set; }

    private const int numberOfTilesHorizontal = 20;
    private const int numberOfTilesVertical = 12;

    private void ComputeTiles(int width, int height)
    {
        int sizeX = (int)(Ceiling((float)width / numberOfTilesHorizontal));
        int sizeY = (int)(Ceiling((float)height / numberOfTilesVertical));

        Tiles = new Buffer2D<Tile>(numberOfTilesHorizontal, numberOfTilesVertical);
        for (int y = 0; y < numberOfTilesVertical; y++)
        {
            for (int x = 0; x < numberOfTilesHorizontal; x++)
            {
                int startX = x * sizeX, startY = y * sizeY;
                int tileWidth = (int)(Min(sizeX, width - startX));
                int tileHeight = (int)(Min(sizeY, height - startY));

                Tiles.Values[x][y] = new Tile(startX, startY, startX + tileWidth, startY + tileHeight);
            }
        }
    }

    #endregion

    #region Constructors

    public ShadowMap(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new Buffer2D<float>(width, height);
        ComputeTiles(width, height);
    }

    #endregion

    #region Methods

    #endregion
}