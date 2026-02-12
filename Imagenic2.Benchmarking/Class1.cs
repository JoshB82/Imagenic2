using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;
using Imagenic2.Core.Maths;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.Rasterising;

namespace Imagenic2.Benchmarking
{
    public class Program
    {
        public async static void Main()
        {
            Cube cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);

            Rasteriser<Bitmap> renderer = new Rasteriser<Bitmap>(new RenderingOptions()
            {
                PhysicalEntitiesToRender = new PhysicalEntity[] { cube },
                RenderCamera = new OrthogonalCamera(Vector3D.Zero, Orientation.OrientationXY, 10, 10, 1, 10)
            });

            Bitmap bitmap = await renderer.RenderAsync();
        }
    }
}