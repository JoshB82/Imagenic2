using Imagenic2.Core.Renderers;
using System.Drawing;

namespace Imagenic2.Core.Images;

public interface IFactory<TImage> where TImage : Image
{
    static abstract TImage CreateFromBuffer(Buffer2D<Color> colourBuffer);
}