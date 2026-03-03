using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private void InterpolateTriangle(Triangle triangle, Action<Triangle, int, int, float> onInterpolation)
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
                    onInterpolation(triangle, x, y, z);
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