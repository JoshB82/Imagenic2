using Imagenic2.Core.Entities;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.RayTracing;
using System.Diagnostics;

namespace Imagenic2.RayTracingDemo;

public partial class Form : System.Windows.Forms.Form
{
    private readonly Camera renderCamera;
    private const float fieldOfView = MathF.PI / 3; // 60 degrees
    private RayTracer<Imagenic2.Core.Images.Bitmap> renderer;
    private HashSet<Keys> keysPressed = new();

    private Bitmap renderBuffer;

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

        Task.Run(Loop);
    }

    private async Task Loop()
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
                Imagenic2.Core.Images.Bitmap? renderedImage = await renderer.RenderAsync();
                this.Invoke((MethodInvoker)(() =>
                {
                    System.Drawing.Bitmap? image = renderedImage?.ToSystemDrawingBitmap(renderBuffer);
                    pictureBox.Image = image;
                }));

                noFrames++;
                frameTime -= frameMinimumTime;
            }

            if (updateTime >= updateMinimumTime)
            {
                //UpdateShapes();
                noUpdates++;
                updateTime -= updateMinimumTime;
            }

            if (nowTime >= 1000 * timer)
            {
                this.Invoke((MethodInvoker)(() => this.Text = $"Ray Tracing Demo - FPS: {noFrames}, UPS: {noUpdates}"));
                noFrames = 0; noUpdates = 0;
                timer += 1;
            }

            CheckKeyboard((float)deltaTime);

            await Task.Delay(1);
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
                    /* if (keyboardControl) */
                    renderCamera.RotateUp(tiltAngle * deltaTime);
                    break;
                case Keys.K:
                    // Rotate down
                    /* if (keyboardControl) */
                    renderCamera.RotateDown(tiltAngle * deltaTime);
                    break;
                case Keys.J:
                    // Rotate left
                    /* if (keyboardControl) */
                    renderCamera.RotateLeft(tiltAngle * deltaTime);
                    break;
                case Keys.L:
                    // Rotate right
                    /* if (keyboardControl) */
                    renderCamera.RotateRight(tiltAngle * deltaTime);
                    break;
                case Keys.U:
                    // Roll left
                    renderCamera.RollLeft(tiltAngle * deltaTime);
                    break;
                case Keys.O:
                    // Roll right
                    renderCamera.RollRight(tiltAngle * deltaTime);
                    break;
                case Keys.Escape:
                    // Switch from mouse to keyboard input
                    //this.Invoke(() => SwitchToKeyboardControl());
                    break;
            }
        }
    }

    private void Form_KeyDown(object sender, KeyEventArgs e) => keysPressed.Add(e.KeyCode);

    private void Form_KeyUp(object sender, KeyEventArgs e) => keysPressed.Remove(e.KeyCode);
}