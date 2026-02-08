using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;

namespace Imagenic2.Core.Renderers.Rasterising;

public class Rasteriser<TImage> : Renderer<TImage> where TImage : Image
{
    #region Fields and Properties

    #endregion

    #region Constructors

    public Rasteriser(RenderingOptions renderingOptions) : base(renderingOptions)
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

        return null; // Temporary
    }

    #endregion
}