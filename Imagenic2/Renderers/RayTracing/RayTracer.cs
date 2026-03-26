using Imagenic2.Core.Images;

namespace Imagenic2.Core.Renderers.RayTracing;

public partial class RayTracer<TImage> : Renderer<TImage> where TImage : Image, IFactory<TImage>
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public RayTracer(RenderingOptions renderingOptions) : base(renderingOptions)
    {

    }

    #endregion

    #region Methods

    public async override Task<TImage?> RenderAsync(CancellationToken token = default)
    {
        if (!NewRenderNeeded) return LatestRender;
        colourBuffer.SetAllToValue(RenderingOptions.BackgroundColour);

        // Check if there is anything to render.
        if (RenderingOptions.PhysicalEntitiesToRender is null || !RenderingOptions.PhysicalEntitiesToRender.Any())
        {
            return null; // Temporary
        }

        CastRaysFromCamera(RenderingOptions.RenderCamera);

        NewRenderNeeded = false;

        return LatestRender = TImage.CreateFromBuffer(colourBuffer);
    }

    #endregion
}