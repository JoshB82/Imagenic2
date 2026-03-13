using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Imagenic2.Core.Entities;
using Imagenic2.Core.Images;
using Imagenic2.Core.Maths;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.Rasterising;

namespace Imagenic2.Benchmarking;

public class Program
{
    public static void Main()
    {
        var summary = BenchmarkRunner.Run<RenderTest>();
    }
}

public class RenderTest
{
    private Cube cube;
    private OrthogonalCamera renderCamera;
    private Rasteriser<Bitmap> renderer;

    public RenderTest()
    {
        cube = new Cube(Vector3D.Zero, Orientation.OrientationXY, 10);
        renderCamera = new OrthogonalCamera(new Vector3D(0, 0, -100), Orientation.OrientationZY, 100, 100, 1, 750);
        renderer = new Rasteriser<Bitmap>(new RenderingOptions(renderCamera).AddToRender(cube));
    }

    [Benchmark]
    public void Render()
    {
        Bitmap bitmap = Task.Run(async () => await renderer.RenderAsync()).Result;
    }
}