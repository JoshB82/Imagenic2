using Imagenic2.Core.Entities;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.Rasterising;

namespace Imagenic.WinForms;

public partial class Form : System.Windows.Forms.Form
{
    public Form()
    {
        InitializeComponent();

        Cube cube = new Cube(
            worldOrigin: Vector3D.Zero,
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationYZ,
            sideLength: 10);

        Camera renderCamera = new OrthogonalCamera(
            worldOrigin: new Vector3D(0, 0, 50),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationNegativeZY,
            viewWidth: pictureBox.Width / 10f,
            viewHeight: pictureBox.Height / 10f,
            zNear: 50,
            zFar: 750
        );

        RenderingOptions renderingOptions = new RenderingOptions()
        {
            RenderWidth = pictureBox.Width,
            RenderHeight = pictureBox.Height,
            PhysicalEntitiesToRender = new List<PhysicalEntity>() { cube },
            RenderCamera = renderCamera
        };

        Rasteriser<Imagenic2.Core.Images.Bitmap> renderer = new Rasteriser<Imagenic2.Core.Images.Bitmap>(renderingOptions);

        Imagenic2.Core.Images.Bitmap renderedImage = renderer.RenderAsync().Result;

        pictureBox.Image = renderedImage;
    }
}