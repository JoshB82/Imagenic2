using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Lights;
using System.Drawing;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private void ProduceShadowMaps(Triangle triangle, Buffer2D<float> buffer, int x, int y, float z)
    {
        if (z.ApproxLessThan(buffer[x, y], 1E-4f))
        {
            buffer[x, y] = z;
        }
    }

    private void OnInterpolation(Triangle triangle, Buffer2D<float> zBuffer, int x, int y, float z, Vector3D v, Vector2D t)
    {
        float vx = v.x, vy = v.y, vz = v.z;
        if (z.ApproxLessThan(zBuffer[x, y], 1E-4f))
        {
            zBuffer[x, y] = z;
            Color pixelColour = ((SolidStyle)(triangle.FrontStyle)).Colour;
            colourBuffer[x, y] = ShadowMapCheck(pixelColour, vx, vy, vz);
        }
    }

    private void OnTextureInterpolation(Triangle triangle, Buffer2D<float> zBuffer, int x, int y, float z, Vector3D v, Vector2D t)
    {
        float vx = v.x, vy = v.y, vz = v.z;
        float tu = t.x, tv = t.y;

        if (z.ApproxLessThan(zBuffer[x, y], 1E-4f))
        {
            zBuffer[x, y] = z;
            Color pixelColour;
            TextureStyle ts = (TextureStyle)(triangle.FrontStyle);
            Imagenic2.Core.Images.Image textureImage = ts.Image;
            if (tu.ApproxLessThan(0) || tu.ApproxMoreThan(1) ||
                tv.ApproxLessThan(0) || tv.ApproxMoreThan(1))
            {
                pixelColour = ts.OutsideColour;
            }
            else
            {
                int mappedTu = (tu * (textureImage.Width - 1)).RoundToInt(), mappedTv = (tv * (textureImage.Height - 1)).RoundToInt();
                pixelColour = textureImage.ColourBuffer[mappedTu, mappedTv];
            }
            colourBuffer[x, y] = ShadowMapCheck(pixelColour, vx, vy, vz);
        }
    }

    private Color ShadowMapCheck(Color pixelColour, float vx, float vy, float vz)
    {
        bool anyLights = false;

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

                if (shadowMap.Data[xLookUp, yLookUp].ApproxLessThan(point.z, 1e-3f))
                {
                    // Pixel is not affected by this light
                }
                else
                {
                    // Pixel is affected by this light
                    pixelColour = pixelColour.Mix(light.Colour);
                    anyLights = true;
                }
            }
        }

        return anyLights ? pixelColour : Color.Black;
    }

    private void OnInterpolation(Edge edge, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer[x, y], 1E-4f))
        {
            zBuffer[x, y] = z;
            Color colour = ((SolidEdgeStyle)(edge.EdgeStyle)).Colour;
            colourBuffer[x, y] = colour;
        }
    }
}