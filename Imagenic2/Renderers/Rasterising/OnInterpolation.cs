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

    private void OnInterpolation(Triangle triangle, Buffer2D<float> zBuffer, int x, int y, float z, float vx, float vy, float vz)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color pixelColour = Color.Black;
            bool firstLight = true;

            // Check if pixel is in shadow
            foreach (Light light in RenderingOptions.Lights)
            {
                Matrix4x4 cameraViewToLightScreen = light.viewToScreen * light.WorldToView * RenderingOptions.RenderCamera.WorldToView.Inverse();

                foreach (ShadowMap shadowMap in light.ShadowMaps)
                {
                    Vector4D point = new Vector4D(vx, vy, vz, 1);
                    point = cameraViewToLightScreen * point;
                    
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

                    if (shadowMap.Data.Values[xLookUp][yLookUp].ApproxLessThan(point.z, 1e-4f))
                    {
                        // Pixel is not affected by this light
                    }
                    else
                    {
                        // Pixel is affected by this light
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