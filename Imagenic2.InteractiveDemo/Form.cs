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
    private HashSet<Keys> keysPressed = new();

    public Form()
    {
        InitializeComponent();

        Cube cube = new Cube(
            worldOrigin: Vector3D.Zero,
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
            sideLength: 10
        );

        Cone cone = new Cone(
            worldOrigin: new Vector3D(30, 0, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
            height: 20,
            radius: 10,
            resolution: 10
        );

        renderCamera = new PerspectiveCamera(
            worldOrigin: new Vector3D(0, 0, 100),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
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

    private async void Loop()
    {
        bool running = true;

        const int maxFramePerSecond = 60, maxUpdatesPerSecond = 60;
        const double frameMinimumTime = 1000.0 / maxFramePerSecond;
        const double updateMinimumTime = 1000.0 / maxUpdatesPerSecond;

        int noFrames = 0, noUpdates = 0, timer = 1;

        double nowTime, deltaTime, frameTime = 0, updateTime = 0;

        Stopwatch sw = Stopwatch.StartNew();
        double startTime = sw.ElapsedMilliseconds;

        while (running)
        {
            nowTime = sw.ElapsedMilliseconds;
            deltaTime = nowTime - startTime;
            startTime = nowTime;

            frameTime += deltaTime;
            updateTime += deltaTime;

            if (frameTime >= frameMinimumTime)
            {
                this.Invoke((MethodInvoker)(async () =>
                {
                    var oldImage = pictureBox.Image;
                    Imagenic2.Core.Images.Bitmap renderedImage = await renderer.RenderAsync();
                    pictureBox.Image = renderedImage.ToSystemDrawingBitmap();
                    oldImage?.Dispose();
                }));
                noFrames++;
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

            CheckKeyboard((float)deltaTime);

            Thread.Sleep(1);
        }
    }

    private void CheckKeyboard(float deltaTime)
    {
        const float panDistance = 0.1f, tiltAngle = 0.001f;

        foreach (Keys key in keysPressed)
        {
            switch (key)
            {
                case Keys.W:
                    // Pan forward
                    renderCamera.PanForward(panDistance * deltaTime);
                    break;
                case Keys.A:
                    // Pan left
                    renderCamera.PanLeft(panDistance * deltaTime);
                    break;
                case Keys.D:
                    // Pan right
                    renderCamera.PanRight(panDistance * deltaTime);
                    break;
                case Keys.S:
                    // Pan backward
                    renderCamera.PanBackward(panDistance * deltaTime);
                    break;
                case Keys.Q:
                    // Pan up
                    renderCamera.PanUp(panDistance * deltaTime);
                    break;
                case Keys.E:
                    // Pan down
                    renderCamera.PanDown(panDistance * deltaTime);
                    break;
                case Keys.I:
                    // Rotate up
                    renderCamera.RotateUp(tiltAngle * deltaTime);
                    break;
                case Keys.J:
                    // Rotate left
                    renderCamera.RotateLeft(tiltAngle * deltaTime);
                    break;
                case Keys.L:
                    // Rotate right
                    renderCamera.RotateRight(tiltAngle * deltaTime);
                    break;
                case Keys.K:
                    // Rotate down
                    renderCamera.RotateDown(tiltAngle * deltaTime);
                    break;
                case Keys.U:
                    // Roll left
                    renderCamera.RollLeft(tiltAngle * deltaTime);
                    break;
                case Keys.O:
                    // Roll right
                    renderCamera.RollRight(tiltAngle * deltaTime);
                    break;
            }
        }
    }

    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        keysPressed.Add(e.KeyCode);
    }

    private void Form_KeyUp(object sender, KeyEventArgs e) => keysPressed.Remove(e.KeyCode);

    private void Form_Resize(object sender, System.EventArgs e)
    {
        renderCamera.ViewWidth = pictureBox.Width / 10f;
        renderCamera.ViewHeight = pictureBox.Height / 10f;
        renderer.RenderingOptions.RenderWidth = pictureBox.Width;
        renderer.RenderingOptions.RenderHeight = pictureBox.Height;
    }

    private void Form_FormClosed(object sender, FormClosedEventArgs e)
    {
        pictureBox.Image.Dispose();
    }
}