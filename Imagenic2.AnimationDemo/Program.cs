using Imagenic2.Core.Entities;
using Imagenic2.Core.Entities.Animation;
using Imagenic2.Core.Maths.Vectors;
using Imagenic2.Core.Renderers;
using Imagenic2.Core.Renderers.RayTracing;

// Shapes
Cube cube = new Cube(
    worldOrigin: new Vector3D(0, 20, 0),
    worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
    sideLength: 10
);
cube.SetAllTrianglesToFaceStyle(new Material());

Cone cone = new Cone(
    worldOrigin: new Vector3D(40, 20, 0),
    worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
    height: 20,
    radius: 10,
    resolution: 10
);
cone.SetAllTrianglesToFaceStyle(new Material());

Plane plane = new Plane(
    worldOrigin: new Vector3D(-50, 0, -50),
    worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
    length: 100,
    width: 100
);
plane.SetAllTrianglesToFaceStyle(new Material() { Reflectivity = 1 });

// Cameras
Camera renderCamera = new PerspectiveCamera(
    worldOrigin: new Vector3D(0, 0, -100),
    worldOrientation: Imagenic2.Core.Maths.Orientation.OrientationZY,
    viewWidth: 1920,
    viewHeight: 1080,
    zNear: 10,
    zFar: 1000
);

RenderingOptions renderingOptions = new RenderingOptions(renderCamera)
{
    RenderWidth = 1920,
    RenderHeight = 1080
}
.AddToRender(new List<PhysicalEntity>() { cube, plane, cone });

Renderer<Imagenic2.Core.Images.Bitmap> renderer = new RayTracer<Imagenic2.Core.Images.Bitmap>(renderingOptions);

Animation animation = cube.Start(time: 0)
                          .Scale(1, 2, 3, time: 5)
                          .TranslateX(10, time: 7)
                          .Rotate(Vector3D.One, MathF.PI, time: 11.5f)
                          .End();

IAsyncEnumerable<Imagenic2.Core.Images.Bitmap?> render = renderer.RenderAsync(animation);

// render.ToVideo(); ??