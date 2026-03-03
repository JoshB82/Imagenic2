using Imagenic2.Core.Entities;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private void ProduceShadowMaps(Triangle triangle, int x, int y, float z)
    {
        if (z.ApproxLessThan(shadowMaps[shadowMapListIndexCount].Values[x][y], 1E-4f))
        {
            shadowMaps[shadowMapListIndexCount].Values[x][y] = z;
        }
    }

    private void OnInterpolation(Triangle triangle, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color colour = ((SolidStyle)(triangle.FrontStyle)).Colour;
            colourBuffer.Values[x][y] = colour;
        }
    }
}