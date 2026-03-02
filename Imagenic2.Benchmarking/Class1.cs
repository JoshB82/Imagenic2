using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;
using Imagenic2.Core.Maths;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.Rasterising;

namespace Imagenic2.Benchmarking;

public class Program
{
    public async static void Main()
    {
        Cube cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);

        OrthogonalCamera renderCamera = new OrthogonalCamera(new Vector3D(0, 0, -100), Orientation.OrientationZY, 100, 100, 1, 750);

        Rasteriser<Bitmap> renderer = new Rasteriser<Bitmap>(new RenderingOptions(renderCamera).AddToRender(cube));

        Bitmap bitmap = await renderer.RenderAsync();
    }
}