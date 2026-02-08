using Imagenic2.Core.Images;

namespace Imagenic2.Core.Renderers.RayTracing;

public class RayTracer<TImage> : Renderer<TImage> where TImage : Image
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
        // Check if there is anything to render.
        if (RenderingOptions.PhysicalEntitiesToRender is null || !RenderingOptions.PhysicalEntitiesToRender.Any())
        {
            return null;
        }

        return null; // Temporary
    }

    #endregion
}