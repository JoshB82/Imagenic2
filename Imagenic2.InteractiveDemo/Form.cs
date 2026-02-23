using Imagenic2.Core.Entities;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.Rasterising;
using System.Diagnostics;

namespace Imagenic2.InteractiveDemo;

public partial class Form : System.Windows.Forms.Form
{
    private readonly Camera renderCamera;
    private Rasteriser<Imagenic2.Core.Images.Bitmap> renderer;
    private List<Keys> keysPressed = new();

    public Form()
    {
        InitializeComponent();

        Cube cube = new Cube(
            worldOrigin: Vector3D.Zero,
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationYZ,
            sideLength: 10
        );

        Cone cone = new Cone(
            worldOrigin: new Vector3D(30, 0, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationYZ,
            height: 20,
            radius: 10,
            resolution: 10
        );

        renderCamera = new OrthogonalCamera(
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
            PhysicalEntitiesToRender = new List<PhysicalEntity>() { cube, cone },
            RenderCamera = renderCamera
        };

        renderer = new Rasteriser<Imagenic2.Core.Images.Bitmap>(renderingOptions);

        Thread thread = new(Loop) { IsBackground = true };
        thread.Start();
    }

    private void Loop()
    {
        bool running = true;

        const int maxFramePerSecond = 60, maxUpdatesPerSecond = 60;
        const long frameMinimumTime = 1000 / maxFramePerSecond; //(?)
        const long updateMinimumTime = 1000 / maxUpdatesPerSecond; // ?

        int noFrames = 0, noUpdates = 0, timer = 1;

        long nowTime, deltaTime, frameTime = 0, updateTime = 0;

        Stopwatch sw = Stopwatch.StartNew();
        long startTime = sw.ElapsedMilliseconds;

        while (running)
        {
            nowTime = sw.ElapsedMilliseconds;
            deltaTime = nowTime - startTime;
            startTime = nowTime;

            frameTime += deltaTime;
            updateTime += deltaTime;

            if (frameTime >= frameMinimumTime)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    var oldImage = pictureBox.Image;
                    Imagenic2.Core.Images.Bitmap renderedImage = renderer.RenderAsync().Result;
                    pictureBox.Image = renderedImage.ToSystemDrawingBitmap();
                    oldImage?.Dispose();
                }));
                noFrames++; //?
                frameTime -= frameMinimumTime;
            }

            if (updateTime >= updateMinimumTime)
            {
                //Update_Position(); uncomment
                noUpdates++; //?
                updateTime -= updateMinimumTime;
            }

            if (nowTime >= 1000 * timer)
            {
                this.Invoke((MethodInvoker)(() => this.Text = $"Interactive Demo - FPS: {noFrames}, UPS: {noUpdates}"));
                noFrames = 0; noUpdates = 0;
                timer += 1;
            }

            CheckKeyboard(updateTime);
        }
    }

    private void CheckKeyboard(long updateTime)
    {
        const float cameraPanDampener = 0.0001f, cameraTiltDampener = 0.00001f;

        for (int i = 0; i < keysPressed.Count; i++)
        {
            switch (keysPressed[i])
            {
                case Keys.W:
                    // Pan forward
                    renderCamera.PanForward(cameraPanDampener * updateTime);
                    break;
                case Keys.A:
                    // Pan left
                    renderCamera.PanLeft(cameraPanDampener * updateTime);
                    break;
                case Keys.D:
                    // Pan right
                    renderCamera.PanRight(cameraPanDampener * updateTime);
                    break;
                case Keys.S:
                    // Pan backward
                    renderCamera.PanBackward(cameraPanDampener * updateTime);
                    break;
                case Keys.Q:
                    // Pan up
                    renderCamera.PanUp(cameraPanDampener * updateTime);
                    break;
                case Keys.E:
                    // Pan down
                    renderCamera.PanDown(cameraPanDampener * updateTime);
                    break;
                case Keys.I:
                    // Rotate up
                    renderCamera.RotateUp(cameraTiltDampener * updateTime);
                    break;
                case Keys.J:
                    // Rotate left
                    renderCamera.RotateLeft(cameraTiltDampener * updateTime);
                    break;
                case Keys.L:
                    // Rotate right
                    renderCamera.RotateRight(cameraTiltDampener * updateTime);
                    break;
                case Keys.K:
                    // Rotate down
                    renderCamera.RotateDown(cameraTiltDampener * updateTime);
                    break;
                case Keys.U:
                    // Roll left
                    renderCamera.RollLeft(cameraTiltDampener * updateTime);
                    break;
                case Keys.O:
                    // Roll right
                    renderCamera.RollRight(cameraTiltDampener * updateTime);
                    break;
            }
        }
    }

    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        keysPressed.Add(e.KeyCode);
        keysPressed = keysPressed.Distinct().ToList();
    }

    private void Form_KeyUp(object sender, KeyEventArgs e) => keysPressed.Remove(e.KeyCode);

    private void Form_Resize(object sender, System.EventArgs e)
    {
        renderCamera.ViewWidth = pictureBox.Width / 10f;
        renderCamera.ViewHeight = pictureBox.Height / 10f;
        renderer.RenderingOptions.RenderWidth = pictureBox.Width;
        renderer.RenderingOptions.RenderHeight = pictureBox.Height;
    }
}