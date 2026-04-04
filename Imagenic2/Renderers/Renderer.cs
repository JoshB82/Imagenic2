using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Animation;
using Imagenic2.Core.Enums;
using Imagenic2.Core.Images;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Renderers;

public abstract class Renderer<TImage> where TImage : Images.Image, IFactory<TImage>
{
    #region Fields and Properties

    protected Buffer2D<Color> colourBuffer;

    internal static readonly ClippingPlane[] ScreenClippingPlanes = new ClippingPlane[]
    {
        new(-Vector3D.One, Vector3D.UnitX), // Left
        new(-Vector3D.One, Vector3D.UnitY), // Bottom
        new(-Vector3D.One, Vector3D.UnitZ), // Near
        new(Vector3D.One, Vector3D.UnitNegativeX), // Right
        new(Vector3D.One, Vector3D.UnitNegativeY), // Top
        new(Vector3D.One, Vector3D.UnitNegativeZ) // Far
    };
    
    internal bool NewRenderNeeded { get; set; } = true;
    internal bool NewShadowMapNeeded { get; set; } = true;

    private RenderingOptions renderingOptions;
    public RenderingOptions RenderingOptions
    {
        get => renderingOptions;
        set
        {
            if (value == renderingOptions) return;
            renderingOptions = value;
            renderingOptions.RenderAlteringPropertyChanged += OnRenderingAlteringPropertyChanged;
            ComputeTiles(renderingOptions.RenderWidth, renderingOptions.RenderHeight);
        }
    }

    public TImage LatestRender { get; protected set; }

    private bool renderEdges;
    public bool RenderEdges
    {
        get => renderEdges;
        set
        {
            if (value == renderEdges) return;
            renderEdges = value;
            NewRenderNeeded = true;
        }
    }

    private bool renderViewVolumes;
    public bool RenderViewVolumes
    {
        get => renderViewVolumes;
        set
        {
            if (value == renderViewVolumes) return;
            renderViewVolumes = value;
            NewRenderNeeded = true;
        }
    }

    private bool renderTriangles = true;
    public bool RenderTriangles
    {
        get => renderTriangles;
        set
        {
            if (value == renderTriangles) return;
            renderTriangles = value;
            NewRenderNeeded = true;
        }
    }

    internal Buffer2D<Tile> Tiles { get; set; }

    protected const int numberOfTilesHorizontal = 20;
    protected const int numberOfTilesVertical = 12;

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

    public Renderer(RenderingOptions renderingOptions)
    {
        RenderingOptions = renderingOptions;
        renderingOptions.RenderAlteringPropertyChanged += OnRenderingAlteringPropertyChanged;
        colourBuffer = new Buffer2D<Color>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);
    }

    #endregion

    #region Methods

    private void OnRenderingAlteringPropertyChanged(RenderUpdate args)
    {
        if (args.HasFlag(RenderUpdate.NewRender)) NewRenderNeeded = true;
        if (args.HasFlag(RenderUpdate.NewShadowMap)) NewShadowMapNeeded = true;
    }

    public abstract Task<TImage?> RenderAsync(CancellationToken token);

    public async IAsyncEnumerable<TImage?> RenderAsync(Animation animation, [EnumeratorCancellation] CancellationToken token = default)
    {
        int duration = animation.DurationSeconds;
        int fps = animation.FPS;
        int numberOfFrames = duration * fps;
        float invFPS = 1f / fps;

        for (int i = 0; i <= numberOfFrames; i++)
        {
            token.ThrowIfCancellationRequested();
            float time = invFPS * i;
            animation.Apply(time);
            TImage? render = await RenderAsync(token);
            yield return render;
        }
    }

    #endregion
}