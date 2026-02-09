using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;
using System.Drawing;

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

        var colourBuffer = new Buffer2D<Color>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);
        var zBuffer = new Buffer2D<float>(RenderingOptions.RenderWidth, RenderingOptions.RenderHeight);

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                foreach (Triangle triangle in mesh.Structure.Triangles)
                {
                    Interpolate(triangle, colourBuffer, zBuffer);
                }
            }
        }

        Bitmap bitmap = new Bitmap(colourBuffer);
        //return bitmap;

        //return new TImage();
        return null; // Temporary
    }

    private static void Interpolate(Triangle triangle, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer)
    {
        // Extract values
        int x1 = triangle.P1.WorldOrigin.x.RoundToInt();
        int y1 = triangle.P1.WorldOrigin.y.RoundToInt();
        float z1 = triangle.P1.WorldOrigin.z;
        int x2 = triangle.P2.WorldOrigin.x.RoundToInt();
        int y2 = triangle.P2.WorldOrigin.y.RoundToInt();
        float z2 = triangle.P2.WorldOrigin.z;
        int x3 = triangle.P3.WorldOrigin.x.RoundToInt();
        int y3 = triangle.P3.WorldOrigin.y.RoundToInt();
        float z3 = triangle.P3.WorldOrigin.z;

        // Create steps
        float dyStep1 = y1 - y2;
        float dyStep2 = y1 - y3;
        float dyStep3 = y2 - y3;

        float xStep1 = 0, zStep1 = 0;
        float xStep3 = 0, zStep3 = 0;

        if (dyStep1 != 0)
        {
            xStep1 = (x1 - x2) / dyStep1; // dx from point 1 to point 2
            zStep1 = (z1 - z2) / dyStep1; // dz from point 1 to point 2
        }
        float xStep2 = (x1 - x3) / dyStep2; // dx from point 1 to point 3
        float zStep2 = (z1 - z3) / dyStep2; // dz from point 1 to point 3
        if (dyStep3 != 0)
        {
            xStep3 = (x2 - x3) / dyStep3; // dx from point 2 to point 3
            zStep3 = (z2 - z3) / dyStep3; // dz from point 2 to point 3
        }

        // Draw a flat-bottom triangle
        if (dyStep1 != 0)
        {
            for (int y = y2; y <= y1; y++)
            {
                int sx = ((y - y2) * xStep1 + x2).RoundToInt();
                float sz = (y - y2) * zStep1 + z2;

                int ex = ((y - y3) * xStep2 + x3).RoundToInt();
                float ez = (y - y3) * zStep2 + z3;

                if (sx > ex)
                {
                    Extensions.Swap(ref sx, ref ex);
                    Extensions.Swap(ref sz, ref ez);
                }

                float t = 0, tStep = 1f / (ex - sx);
                for (int x = sx; x <= ex; x++)
                {
                    float z = sz + t * (ez - sz);
                    OnInterpolation(triangle, colourBuffer, zBuffer, x, y, z);

                    t += tStep;
                }
            }
        }

        // Draw a flat-top triangle
        if (dyStep3 != 0)
        {
            for (int y = y3; y <= y2; y++)
            {
                int sx = ((y - y3) * xStep3 + x3).RoundToInt();
                float sz = (y - y3) * zStep3 + z3;

                int ex = ((y - y3) * xStep2 + x3).RoundToInt();
                float ez = (y - y3) * zStep2 + z3;

                if (sx > ex)
                {
                    Extensions.Swap(ref sx, ref ex);
                    Extensions.Swap(ref sz, ref ez);
                }

                float t = 0, tStep = 1f / (ex - sx);
                for (int x = sx; x <= ex; x++)
                {
                    float z = sz + t * (ez - sz);
                    OnInterpolation(triangle, colourBuffer, zBuffer, x, y, z);

                    t += tStep;
                }
            }
        }
    }

    private static void OnInterpolation(Triangle triangle, Buffer2D<Color> colourBuffer, Buffer2D<float> zBuffer, int x, int y, float z)
    {
        if (z.ApproxLessThan(zBuffer.Values[x][y], 1E-4f))
        {
            zBuffer.Values[x][y] = z;
            Color colour = ((SolidStyle)(triangle.FrontStyle)).Colour;
            colourBuffer.Values[x][y] = colour;
        }
    }


    #endregion
}