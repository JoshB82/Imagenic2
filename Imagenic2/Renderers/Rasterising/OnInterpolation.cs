using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Lights;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private void ProduceShadowMaps(Triangle triangle, Buffer2D<float> buffer, int x, int y, float z)
    {
        if (z.ApproxLessThan(buffer.Values[x][y], 1E-4f))
        {
            buffer.Values[x][y] = z;
        }
    }

    private void OnInterpolation(Triangle triangle, Buffer2D<float> zBuffer, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color pixelColour = Color.Black;
            bool firstLight = true;

            // Check if pixel is in shadow
            foreach (Light light in RenderingOptions.Lights)
            {
                foreach (ShadowMap shadowMap in light.ShadowMaps)
                {
                    Vector4D point = new Vector4D(x, y, z, 1);

                    point = light.viewToScreen * light.WorldToView * RenderingOptions.RenderCamera.WorldToView.Inverse() * RenderingOptions.RenderCamera.viewToScreen.Inverse() * point;

                    if (light is Spotlight)
                    {
                        point /= point.w;
                    }

                    if (point.x.ApproxLessThan(-1) || point.x.ApproxMoreThan(1) ||
                        point.y.ApproxLessThan(-1) || point.y.ApproxMoreThan(1))
                    {
                        continue;
                    }

                    point = shadowMap.ScreenToWindow * point;
                    int xLookUp = point.x.RoundToInt();
                    int yLookUp = point.y.RoundToInt();

                    if (shadowMap.Data.Values[xLookUp][yLookUp].ApproxLessThan(point.z))
                    {
                        // Pixel is in shadow
                    }
                    else
                    {
                        if (firstLight)
                        {
                            pixelColour = light.Colour;
                            firstLight = false;
                        }
                        else
                        {
                            pixelColour = pixelColour.Mix(light.Colour);
                        }
                    }
                }
            }
            
            colourBuffer.Values[x][y] = pixelColour;
        }
    }

    private void OnInterpolation(Edge edge, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color colour = ((SolidEdgeStyle)(edge.EdgeStyle)).Colour;
            colourBuffer.Values[x][y] = colour;
        }
    }
}