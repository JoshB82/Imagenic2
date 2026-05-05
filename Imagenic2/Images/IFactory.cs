using Imagenic2.Core.Renderers;

namespace Imagenic2.Core.Images;

public interface IFactory<TImage> where TImage : Image
{
    static abstract TImage CreateFromBuffer(Buffer2D<Colour> colourBuffer);
}