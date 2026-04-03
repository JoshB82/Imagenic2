using Imagenic2.Core.Entities.TransformableEntities.Animation;
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

    public override async IAsyncEnumerable<TImage> RenderAsync(Animation animation, CancellationToken token = default)
    {
        int duration = animation.Duration;
        int FPS = animation.FPS;
        int numberOfFrames = duration * FPS;
        float invFPS = 1f / FPS;

        for (int i = 1; i <= numberOfFrames; i++)
        {
            float time = invFPS * i;
            animation.Apply(time);
            TImage render = await RenderAsync(token);
            yield return render;
        }
    }

    #endregion
}