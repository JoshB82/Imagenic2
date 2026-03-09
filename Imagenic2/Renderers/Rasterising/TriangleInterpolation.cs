using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private static void InterpolateTriangle(Triangle triangle,
                                            Buffer2D<float> buffer,
                                            Action<Triangle, Buffer2D<float>, int, int, float, float, float, float> onInterpolation)
    {
        // Extract values
        float x1 = triangle.TransformedP1.x;
        float y1 = triangle.TransformedP1.y;
        float z1 = triangle.TransformedP1.z;
        float x2 = triangle.TransformedP2.x;
        float y2 = triangle.TransformedP2.y;
        float z2 = triangle.TransformedP2.z;
        float x3 = triangle.TransformedP3.x;
        float y3 = triangle.TransformedP3.y;
        float z3 = triangle.TransformedP3.z;

        float vx1 = triangle.ViewSpaceP1.x;
        float vy1 = triangle.ViewSpaceP1.y;
        float vz1 = triangle.ViewSpaceP1.z;
        float vx2 = triangle.ViewSpaceP2.x;
        float vy2 = triangle.ViewSpaceP2.y;
        float vz2 = triangle.ViewSpaceP2.z;
        float vx3 = triangle.ViewSpaceP3.x;
        float vy3 = triangle.ViewSpaceP3.y;
        float vz3 = triangle.ViewSpaceP3.z;

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

        float C = z1 - zStepX * x1 - zStepY * y1;
        float startZ = zStepX * px + zStepY * py + C;
        float startVx = vxStepX * px + vxStepY * py + (vx1 - vxStepX * x1 - vxStepY * y1);
        float startVy = vyStepX * px + vyStepY * py + (vy1 - vyStepX * x1 - vyStepY * y1);
        float startVz = vzStepX * px + vzStepY * py + (vz1 - vzStepX * x1 - vzStepY * y1);

        for (int y = bottomLeftY; y <= topRightY; y++)
        {
            float e1 = start1;
            float e2 = start2;
            float e3 = start3;
            float z = startZ;
            float vx = startVx, vy = startVy, vz = startVz;

            for (int x = bottomLeftX; x <= topRightX; x++)
            {
                if ((e1 < 0 || (e1 == 0 && ((y1 == y2 && x2 < x1) || (y2 < y1)))) &&
                    (e2 < 0 || (e2 == 0 && ((y2 == y3 && x3 < x2) || (y3 < y2)))) &&
                    (e3 < 0 || (e3 == 0 && ((y1 == y3 && x1 < x3) || (y1 < y3)))))
                {
                    // Point is inside triangle
                    onInterpolation(triangle, buffer, x, y, z, vx, vy, vz);
                }
                e1 += xStep1;
                e2 += xStep2;
                e3 += xStep3;
                z += zStepX;
                vx += vxStepX;
                vy += vyStepX;
                vz += vzStepX;
            }
            start1 += yStep1;
            start2 += yStep2;
            start3 += yStep3;
            startZ += zStepY;
            startVx += vxStepY;
            startVy += vyStepY;
            startVz += vzStepY;
        }
    }

    private static void InterpolateTriangle(Triangle triangle,
                                            Buffer2D<float> buffer,
                                            Action<Triangle, Buffer2D<float>, int, int, float> onInterpolation)
    {
        // Extract values
        float x1 = triangle.TransformedP1.x;
        float y1 = triangle.TransformedP1.y;
        float z1 = triangle.TransformedP1.z;
        float x2 = triangle.TransformedP2.x;
        float y2 = triangle.TransformedP2.y;
        float z2 = triangle.TransformedP2.z;
        float x3 = triangle.TransformedP3.x;
        float y3 = triangle.TransformedP3.y;
        float z3 = triangle.TransformedP3.z;

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
        float C = z1 - zStepX * x1 - zStepY * y1;
        float startZ = zStepX * px + zStepY * py + C;

        for (int y = bottomLeftY; y <= topRightY; y++)
        {
            float e1 = start1;
            float e2 = start2;
            float e3 = start3;
            float z = startZ;

            for (int x = bottomLeftX; x <= topRightX; x++)
            {
                if ((e1 < 0 || (e1 == 0 && ((y1 == y2 && x2 < x1) || (y2 < y1)))) &&
                    (e2 < 0 || (e2 == 0 && ((y2 == y3 && x3 < x2) || (y3 < y2)))) &&
                    (e3 < 0 || (e3 == 0 && ((y1 == y3 && x1 < x3) || (y1 < y3)))))
                {
                    // Point is inside triangle
                    onInterpolation(triangle, buffer, x, y, z);
                }
                e1 += xStep1;
                e2 += xStep2;
                e3 += xStep3;
                z += zStepX;
            }
            start1 += yStep1;
            start2 += yStep2;
            start3 += yStep3;
            startZ += zStepY;
        }
    }
}