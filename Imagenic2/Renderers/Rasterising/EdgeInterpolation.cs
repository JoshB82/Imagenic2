using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private void InterpolateEdge(Edge edge)
    {
        // Extract values
        int x1 = edge.TransformedP1.x.RoundToInt();
        int y1 = edge.TransformedP1.y.RoundToInt();
        float z1 = edge.TransformedP1.z;
        int x2 = edge.TransformedP2.x.RoundToInt();
        int y2 = edge.TransformedP2.y.RoundToInt();
        float z2 = edge.TransformedP2.z;

        float tStep = 1 / Max(Abs(x2 - x1), Abs(y2 - y1));

        for (float t = 0; t <= 1; t += tStep)
        {
            int x = ((1 - t) * x1 + t * x2).RoundToInt();
            int y = ((1 - t) * y1 + t * y2).RoundToInt();
            float z = (1 - t) * z1 + t * z2;

            OnInterpolation(edge, x, y, z);
        }
    }
}