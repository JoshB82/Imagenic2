using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public class Rasteriser<TImage> : Renderer<TImage> where TImage : Image
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public Rasteriser(RenderingOptions renderingOptions, ImageOptions<TImage> imageOptions = null) : base(renderingOptions, imageOptions)
    {

    }

    #endregion

    #region Methods

    public async override Task<TImage?> RenderAsync(CancellationToken token = default)
    {
        // Check if there is anything to render.
        if (RenderingOptions.PhysicalEntitiesToRender is null || !RenderingOptions.PhysicalEntitiesToRender.Any())
        {
            return null;
        }

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {

        }
    }

    #endregion
}