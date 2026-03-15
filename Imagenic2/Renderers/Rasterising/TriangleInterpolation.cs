using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private static void InterpolateTriangle(Triangle triangle,
                                            Buffer2D<float> buffer,
                                            Action<Triangle, Buffer2D<float>, int, int, float, Vector3D, Vector2D> onInterpolation,
                                            float invW1, float invW2, float invW3)
    {
        // Extract values
        float x1 = triangle.TransformedP1.x;
        float y1 = triangle.TransformedP1.y;
        float z1 = triangle.TransformedP1.z * invW1;
        float x2 = triangle.TransformedP2.x;
        float y2 = triangle.TransformedP2.y;
        float z2 = triangle.TransformedP2.z * invW2;
        float x3 = triangle.TransformedP3.x;
        float y3 = triangle.TransformedP3.y;
        float z3 = triangle.TransformedP3.z * invW3;

        float vx1 = triangle.ViewSpaceP1.x * invW1;
        float vy1 = triangle.ViewSpaceP1.y * invW1;
        float vz1 = triangle.ViewSpaceP1.z * invW1;
        float vx2 = triangle.ViewSpaceP2.x * invW2;
        float vy2 = triangle.ViewSpaceP2.y * invW2;
        float vz2 = triangle.ViewSpaceP2.z * invW2;
        float vx3 = triangle.ViewSpaceP3.x * invW3;
        float vy3 = triangle.ViewSpaceP3.y * invW3;
        float vz3 = triangle.ViewSpaceP3.z * invW3;

        // Calculate bounding rectangle
        int bottomLeftX = (int)(Floor(Min(Min(x1, x2), x3)));
        int bottomLeftY = (int)(Floor(Min(Min(y1, y2), y3)));
        int topRightX = (int)(Ceiling(Max(Max(x1, x2), x3)));
        int topRightY = (int)(Ceiling(Max(Max(y1, y2), y3)));
        float px = bottomLeftX + 0.5f;
        float py = bottomLeftY + 0.5f;
        
        float start1 = (px - x1) * (y2 - y1) - (py - y1) * (x2 - x1); // 1 - 2 edge
        float xStep1 = y2 - y1;
        float yStep1 = x1 - x2;
        float start2 = (px - x2) * (y3 - y2) - (py - y2) * (x3 - x2); // 2 - 3 edge
        float xStep2 = y3 - y2;
        float yStep2 = x2 - x3;
        float start3 = (px - x3) * (y1 - y3) - (py - y3) * (x1 - x3); // 3 - 1 edge
        float xStep3 = y1 - y3;
        float yStep3 = x3 - x1;

        float area = (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1);
        float zStepX = ((y2 - y3) * z1 + (y3 - y1) * z2 + (y1 - y2) * z3) / area;
        float zStepY = ((x3 - x2) * z1 + (x1 - x3) * z2 + (x2 - x1) * z3) / area;
        float vxStepX = ((y2 - y3) * vx1 + (y3 - y1) * vx2 + (y1 - y2) * vx3) / area;
        float vxStepY = ((x3 - x2) * vx1 + (x1 - x3) * vx2 + (x2 - x1) * vx3) / area;
        float vyStepX = ((y2 - y3) * vy1 + (y3 - y1) * vy2 + (y1 - y2) * vy3) / area;
        float vyStepY = ((x3 - x2) * vy1 + (x1 - x3) * vy2 + (x2 - x1) * vy3) / area;
        float vzStepX = ((y2 - y3) * vz1 + (y3 - y1) * vz2 + (y1 - y2) * vz3) / area;
        float vzStepY = ((x3 - x2) * vz1 + (x1 - x3) * vz2 + (x2 - x1) * vz3) / area;
        float invWStepX = ((y2 - y3) * invW1 + (y3 - y1) * invW2 + (y1 - y2) * invW3) / area;
        float invWStepY = ((x3 - x2) * invW1 + (x1 - x3) * invW2 + (x2 - x1) * invW3) / area;

        float C = z1 - zStepX * x1 - zStepY * y1;
        float startZ = zStepX * px + zStepY * py + C;
        float startVx = vxStepX * px + vxStepY * py + (vx1 - vxStepX * x1 - vxStepY * y1);
        float startVy = vyStepX * px + vyStepY * py + (vy1 - vyStepX * x1 - vyStepY * y1);
        float startVz = vzStepX * px + vzStepY * py + (vz1 - vzStepX * x1 - vzStepY * y1);
        float startInvW = invWStepX * px + invWStepY * py + (invW1 - invWStepX * x1 - invWStepY * y1);

        for (int y = bottomLeftY; y <= topRightY; y++)
        {
            float e1 = start1;
            float e2 = start2;
            float e3 = start3;
            float z = startZ;
            float vx = startVx, vy = startVy, vz = startVz;
            float invW = startInvW;

            for (int x = bottomLeftX; x <= topRightX; x++)
            {
                if ((e1 < 0 || (e1 == 0 && ((y1 == y2 && x2 < x1) || (y2 < y1)))) &&
                    (e2 < 0 || (e2 == 0 && ((y2 == y3 && x3 < x2) || (y3 < y2)))) &&
                    (e3 < 0 || (e3 == 0 && ((y1 == y3 && x1 < x3) || (y1 < y3)))))
                {
                    // Point is inside triangle
                    float w = 1 / invW;
                    onInterpolation(triangle, buffer, x, y, z * w, new Vector3D(vx * w, vy * w, vz * w), Vector2D.Zero);
                }
                e1 += xStep1;
                e2 += xStep2;
                e3 += xStep3;
                z += zStepX;
                vx += vxStepX;
                vy += vyStepX;
                vz += vzStepX;
                invW += invWStepX;
            }
            start1 += yStep1;
            start2 += yStep2;
            start3 += yStep3;
            startZ += zStepY;
            startVx += vxStepY;
            startVy += vyStepY;
            startVz += vzStepY;
            startInvW += invWStepY;
        }
    }

    private static void InterpolateTextureTriangle(Triangle triangle,
                                                   Buffer2D<float> buffer,
                                                   Action<Triangle, Buffer2D<float>, int, int, float, Vector3D, Vector2D> onInterpolation,
                                                   float invW1, float invW2, float invW3)
    {
        // Extract values
        float x1 = triangle.TransformedP1.x;
        float y1 = triangle.TransformedP1.y;
        float z1 = triangle.TransformedP1.z * invW1;
        float x2 = triangle.TransformedP2.x;
        float y2 = triangle.TransformedP2.y;
        float z2 = triangle.TransformedP2.z * invW2;
        float x3 = triangle.TransformedP3.x;
        float y3 = triangle.TransformedP3.y;
        float z3 = triangle.TransformedP3.z * invW3;

        float vx1 = triangle.ViewSpaceP1.x * invW1;
        float vy1 = triangle.ViewSpaceP1.y * invW1;
        float vz1 = triangle.ViewSpaceP1.z * invW1;
        float vx2 = triangle.ViewSpaceP2.x * invW2;
        float vy2 = triangle.ViewSpaceP2.y * invW2;
        float vz2 = triangle.ViewSpaceP2.z * invW2;
        float vx3 = triangle.ViewSpaceP3.x * invW3;
        float vy3 = triangle.ViewSpaceP3.y * invW3;
        float vz3 = triangle.ViewSpaceP3.z * invW3;

        float u1 = triangle.TransformedTextureP1.x * invW1;
        float v1 = triangle.TransformedTextureP1.y * invW1;
        float u2 = triangle.TransformedTextureP2.x * invW2;
        float v2 = triangle.TransformedTextureP2.y * invW2;
        float u3 = triangle.TransformedTextureP3.x * invW3;
        float v3 = triangle.TransformedTextureP3.y * invW3;

        // Calculate bounding rectangle
        int bottomLeftX = (int)(Floor(Min(Min(x1, x2), x3)));
        int bottomLeftY = (int)(Floor(Min(Min(y1, y2), y3)));
        int topRightX = (int)(Ceiling(Max(Max(x1, x2), x3)));
        int topRightY = (int)(Ceiling(Max(Max(y1, y2), y3)));
        float px = bottomLeftX + 0.5f;
        float py = bottomLeftY + 0.5f;

        // Calculate steps
        float start1 = (px - x1) * (y2 - y1) - (py - y1) * (x2 - x1); // 1 - 2 edge
        float xStep1 = y2 - y1;
        float yStep1 = x1 - x2;
        float start2 = (px - x2) * (y3 - y2) - (py - y2) * (x3 - x2); // 2 - 3 edge
        float xStep2 = y3 - y2;
        float yStep2 = x2 - x3;
        float start3 = (px - x3) * (y1 - y3) - (py - y3) * (x1 - x3); // 3 - 1 edge
        float xStep3 = y1 - y3;
        float yStep3 = x3 - x1;

        float area = (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1);
        float zStepX = ((y2 - y3) * z1 + (y3 - y1) * z2 + (y1 - y2) * z3) / area;
        float zStepY = ((x3 - x2) * z1 + (x1 - x3) * z2 + (x2 - x1) * z3) / area;
        float vxStepX = ((y2 - y3) * vx1 + (y3 - y1) * vx2 + (y1 - y2) * vx3) / area;
        float vxStepY = ((x3 - x2) * vx1 + (x1 - x3) * vx2 + (x2 - x1) * vx3) / area;
        float vyStepX = ((y2 - y3) * vy1 + (y3 - y1) * vy2 + (y1 - y2) * vy3) / area;
        float vyStepY = ((x3 - x2) * vy1 + (x1 - x3) * vy2 + (x2 - x1) * vy3) / area;
        float vzStepX = ((y2 - y3) * vz1 + (y3 - y1) * vz2 + (y1 - y2) * vz3) / area;
        float vzStepY = ((x3 - x2) * vz1 + (x1 - x3) * vz2 + (x2 - x1) * vz3) / area;
        float invWStepX = ((y2 - y3) * invW1 + (y3 - y1) * invW2 + (y1 - y2) * invW3) / area;
        float invWStepY = ((x3 - x2) * invW1 + (x1 - x3) * invW2 + (x2 - x1) * invW3) / area;
        float uStepX = ((y2 - y3) * u1 + (y3 - y1) * u2 + (y1 - y2) * u3) / area;
        float uStepY = ((x3 - x2) * u1 + (x1 - x3) * u2 + (x2 - x1) * u3) / area;
        float vStepX = ((y2 - y3) * v1 + (y3 - y1) * v2 + (y1 - y2) * v3) / area;
        float vStepY = ((x3 - x2) * v1 + (x1 - x3) * v2 + (x2 - x1) * v3) / area;

        // Calculate starting values
        float C = z1 - zStepX * x1 - zStepY * y1;
        float startZ = zStepX * px + zStepY * py + C;
        float startVx = vxStepX * px + vxStepY * py + (vx1 - vxStepX * x1 - vxStepY * y1);
        float startVy = vyStepX * px + vyStepY * py + (vy1 - vyStepX * x1 - vyStepY * y1);
        float startVz = vzStepX * px + vzStepY * py + (vz1 - vzStepX * x1 - vzStepY * y1);
        float startInvW = invWStepX * px + invWStepY * py + (invW1 - invWStepX * x1 - invWStepY * y1);
        float startU = uStepX * px + uStepY * py + (u1 - uStepX * x1 - uStepY * y1);
        float startV = vStepX * px + vStepY * py + (v1 - vStepX * x1 - vStepY * y1);

        for (int y = bottomLeftY; y <= topRightY; y++)
        {
            float e1 = start1;
            float e2 = start2;
            float e3 = start3;
            float z = startZ;
            float vx = startVx, vy = startVy, vz = startVz;
            float invW = startInvW;
            float u = startU;
            float v = startV;

            for (int x = bottomLeftX; x <= topRightX; x++)
            {
                if ((e1 < 0 || (e1 == 0 && ((y1 == y2 && x2 < x1) || (y2 < y1)))) &&
                    (e2 < 0 || (e2 == 0 && ((y2 == y3 && x3 < x2) || (y3 < y2)))) &&
                    (e3 < 0 || (e3 == 0 && ((y1 == y3 && x1 < x3) || (y1 < y3)))))
                {
                    // Point is inside triangle
                    float w = 1 / invW;
                    onInterpolation(triangle, buffer, x, y, z * w, new Vector3D(vx * w, vy * w, vz * w), new Vector2D(u * w, v * w));
                }
                e1 += xStep1;
                e2 += xStep2;
                e3 += xStep3;
                z += zStepX;
                vx += vxStepX;
                vy += vyStepX;
                vz += vzStepX;
                invW += invWStepX;
                u += uStepX;
                v += vStepX;
            }
            start1 += yStep1;
            start2 += yStep2;
            start3 += yStep3;
            startZ += zStepY;
            startVx += vxStepY;
            startVy += vyStepY;
            startVz += vzStepY;
            startInvW += invWStepY;
            startU += uStepY;
            startV += vStepY;
        }
    }

    private static void ShadowMapInterpolateTriangle(Triangle triangle,
                                            Buffer2D<float> buffer,
                                            Action<Triangle, Buffer2D<float>, int, int, float> onInterpolation,
                                            float invW1, float invW2, float invW3)
    {
        // Extract values
        float x1 = triangle.TransformedP1.x;
        float y1 = triangle.TransformedP1.y;
        float z1 = triangle.TransformedP1.z * invW1;
        float x2 = triangle.TransformedP2.x;
        float y2 = triangle.TransformedP2.y;
        float z2 = triangle.TransformedP2.z * invW2;
        float x3 = triangle.TransformedP3.x;
        float y3 = triangle.TransformedP3.y;
        float z3 = triangle.TransformedP3.z * invW3;

        // Calculate bounding rectangle
        int bottomLeftX = (int)(Floor(Min(Min(x1, x2), x3)));
        int bottomLeftY = (int)(Floor(Min(Min(y1, y2), y3)));
        int topRightX = (int)(Ceiling(Max(Max(x1, x2), x3)));
        int topRightY = (int)(Ceiling(Max(Max(y1, y2), y3)));
        float px = bottomLeftX + 0.5f;
        float py = bottomLeftY + 0.5f;

        float start1 = (px - x1) * (y2 - y1) - (py - y1) * (x2 - x1); // 1 - 2 edge
        float xStep1 = y2 - y1;
        float yStep1 = x1 - x2;
        float start2 = (px - x2) * (y3 - y2) - (py - y2) * (x3 - x2); // 2 - 3 edge
        float xStep2 = y3 - y2;
        float yStep2 = x2 - x3;
        float start3 = (px - x3) * (y1 - y3) - (py - y3) * (x1 - x3); // 3 - 1 edge
        float xStep3 = y1 - y3;
        float yStep3 = x3 - x1;

        float area = (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1);
        float zStepX = ((y2 - y3) * z1 + (y3 - y1) * z2 + (y1 - y2) * z3) / area;
        float zStepY = ((x3 - x2) * z1 + (x1 - x3) * z2 + (x2 - x1) * z3) / area;
        float invWStepX = ((y2 - y3) * invW1 + (y3 - y1) * invW2 + (y1 - y2) * invW3) / area;
        float invWStepY = ((x3 - x2) * invW1 + (x1 - x3) * invW2 + (x2 - x1) * invW3) / area;

        float C = z1 - zStepX * x1 - zStepY * y1;
        float startZ = zStepX * px + zStepY * py + C;
        float startInvW = invWStepX * px + invWStepY * py + (invW1 - invWStepX * x1 - invWStepY * y1);

        for (int y = bottomLeftY; y <= topRightY; y++)
        {
            float e1 = start1;
            float e2 = start2;
            float e3 = start3;
            float z = startZ;
            float invW = startInvW;

            for (int x = bottomLeftX; x <= topRightX; x++)
            {
                if ((e1 < 0 || (e1 == 0 && ((y1 == y2 && x2 < x1) || (y2 < y1)))) &&
                    (e2 < 0 || (e2 == 0 && ((y2 == y3 && x3 < x2) || (y3 < y2)))) &&
                    (e3 < 0 || (e3 == 0 && ((y1 == y3 && x1 < x3) || (y1 < y3)))))
                {
                    // Point is inside triangle
                    float w = 1 / invW;
                    onInterpolation(triangle, buffer, x, y, z * w);
                }
                e1 += xStep1;
                e2 += xStep2;
                e3 += xStep3;
                z += zStepX;
                invW += invWStepX;
            }
            start1 += yStep1;
            start2 += yStep2;
            start3 += yStep3;
            startZ += zStepY;
            startInvW += invWStepY;
        }
    }
}