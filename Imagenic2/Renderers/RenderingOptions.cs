using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;

namespace Imagenic2.Core.Renderers;

public sealed class RenderingOptions
{
    #region Fields and Properties

    private int renderWidth = 1920, renderHeight = 1080;

    public int RenderWidth
    {
        get => renderWidth;
        set
        {
            if (value == renderWidth) return;
            if (value < 0)
            {
                // error!
            }
            renderWidth = value;
        }
    }
    public int RenderHeight
    {
        get => renderHeight;
        set
        {
            if (value == renderHeight) return;
            if (value < 0)
            {
                // error!
            }
            renderHeight = value;
        }
    }

    public IEnumerable<PhysicalEntity>? PhysicalEntitiesToRender { get; set; } = new List<PhysicalEntity>();

    #endregion

    #region Constructors

    #endregion

    #region Methods

    #endregion
}