using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;
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
    private const float fieldOfView = MathF.PI / 3; // 60 degrees

    private bool mouseControl = false;
    private bool keyboardControl = true;

    public Form()
    {
        InitializeComponent();

        // Shapes
        Cube cube = new Cube(
            worldOrigin: new Vector3D(0, 20, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
            sideLength: 10
        );

        Cone cone = new Cone(
            worldOrigin: new Vector3D(85, 15, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZNegativeX,
            height: 20,
            radius: 10,
            resolution: 10
        );

        Circle circle = new Circle(
            worldOrigin: new Vector3D(-20, 70, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZNegativeY,
            radius: 15,
            resolution: 15
        );

        Plane plane = new Plane(
            worldOrigin: new Vector3D(-100, 0, -100),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
            length: 200,
            width: 200
        );

        // Textures
        string filePath = "C:\\Users\\joshd\\Downloads\\smiley.bmp";
        TextureStyle ts = new TextureStyle(new Imagenic2.Core.Images.Bitmap(new Bitmap(filePath)));
        cube.Structure.Triangles[2].FrontStyle = ts;
        cube.Structure.Triangles[3].FrontStyle = ts;

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

        // Lights
        DistantLight blueTopLight = new DistantLight(
            worldOrigin: new Vector3D(0, 100, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationNegativeYZ,
            viewWidth: 150,
            viewHeight: 150,
            zNear: 1,
            zFar: 200
        );
        blueTopLight.VolumeStyle = VolumeOutline.Far;

        Spotlight redRightLight = new Spotlight(
            worldOrigin: new Vector3D(150, 0, 0),
            worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationNegativeXY,
            viewWidth: 15,
            viewHeight: 15,
            zNear: 10,
            zFar: 300
        );
        redRightLight.VolumeStyle = VolumeOutline.Far;
        redRightLight.Colour = Color.Red;

        // Renderer
        RenderingOptions renderingOptions = new RenderingOptions(renderCamera)
        {
            RenderWidth = pictureBox.Width,
            RenderHeight = pictureBox.Height
        }
        .AddToRender(new List<PhysicalEntity>() { cube, cone, circle, plane })
        .AddToRender(blueTopLight, redRightLight);

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
                    if (keyboardControl) renderCamera.RotateUp(tiltAngle * deltaTime);
                    break;
                case Keys.K:
                    // Rotate down
                    if (keyboardControl) renderCamera.RotateDown(tiltAngle * deltaTime);
                    break;
                case Keys.J:
                    // Rotate left
                    if (keyboardControl) renderCamera.RotateLeft(tiltAngle * deltaTime);
                    break;
                case Keys.L:
                    // Rotate right
                    if (keyboardControl) renderCamera.RotateRight(tiltAngle * deltaTime);
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
                    this.Invoke(() => SwitchToKeyboardControl());
                    break;
            }
        }
    }

    private void Form_KeyDown(object sender, KeyEventArgs e) => keysPressed.Add(e.KeyCode);

    private void Form_KeyUp(object sender, KeyEventArgs e) => keysPressed.Remove(e.KeyCode);

    private void Form_Resize(object sender, System.EventArgs e)
    {
        /*
        float aspectRatio = pictureBox.Width / (float)(pictureBox.Height);
        float viewHeight = 2 * renderer.RenderingOptions.RenderCamera.ZNear * MathF.Tan(fieldOfView / 2);
        float viewWidth = viewHeight * aspectRatio;
        renderCamera.ViewWidth = viewWidth;
        renderCamera.ViewHeight = viewHeight;
        renderer.RenderingOptions.RenderWidth = pictureBox.Width;
        renderer.RenderingOptions.RenderHeight = pictureBox.Height;
        */
    }

    private void Form_FormClosed(object sender, FormClosedEventArgs e)
    {
        pictureBox.Image?.Dispose();
    }

    bool checkMouseMove = true;
    private void pictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        const float mouseDampener = 0.001f;
        if (mouseControl && checkMouseMove)
        {
            renderCamera.RotateLeft((e.X - pictureBox.Width / 2) * mouseDampener);
            renderCamera.RotateDown((e.Y - pictureBox.Height / 2) * mouseDampener);
            CentreCursor();
            checkMouseMove = false;
        }
        else
        {
            checkMouseMove = true;
        }
    }

    private void CentreCursor() => Cursor.Position = PointToScreen(new Point(pictureBox.Width / 2, pictureBox.Height / 2));

    private void SwitchToKeyboardControl()
    {
        keyboardToolStripMenuItem.Checked = true;
        mouseToolStripMenuItem.Checked = false;
        keyboardControl = true;
        mouseControl = false;
        Cursor.Show();
    }

    private void SwitchToMouseControl()
    {
        keyboardToolStripMenuItem.Checked = false;
        mouseToolStripMenuItem.Checked = true;
        keyboardControl = false;
        mouseControl = true;
        Cursor.Hide();
    }

    private void mouseToolStripMenuItem_Click(object sender, EventArgs e) => SwitchToMouseControl();

    private void keyboardToolStripMenuItem_Click(object sender, EventArgs e) => SwitchToKeyboardControl();
}