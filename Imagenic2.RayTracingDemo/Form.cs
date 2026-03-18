using Imagenic2.Core.Entities;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.RayTracing;

namespace Imagenic2.RayTracingDemo;

public partial class Form : System.Windows.Forms.Form
{
    private readonly Camera renderCamera;
    private const float fieldOfView = MathF.PI / 3; // 60 degrees
    private RayTracer<Imagenic2.Core.Images.Bitmap> renderer;

    public Form()
    {
        InitializeComponent();

        // Shapes
        Cube cube = new Cube(
            worldOrigin: new Vector3D(0, 20, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
            sideLength: 10
        );

        // Cameras
        float aspectRatio = pictureBox.Width / (float)(pictureBox.Height);
        float zNear = 1;
        float zFar = 750;
        float viewHeight = 2 * zNear * MathF.Tan(fieldOfView / 2);
        float viewWidth = viewHeight * aspectRatio;

        renderCamera = new PerspectiveCamera(
            worldOrigin: new Vector3D(0, 0, -100),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
            viewWidth: viewWidth,
            viewHeight: viewHeight,
            zNear: zNear,
            zFar: zFar
        );

        // Renderer
        RenderingOptions renderingOptions = new RenderingOptions(renderCamera)
        {
            RenderWidth = pictureBox.Width,
            RenderHeight = pictureBox.Height
        }
        .AddToRender(new List<PhysicalEntity>() { cube });

        renderer = new RayTracer<Core.Images.Bitmap>(renderingOptions);

        Task.Run(async () => pictureBox.Image = (await renderer.RenderAsync()).ToSystemDrawingBitmap());
    }
}