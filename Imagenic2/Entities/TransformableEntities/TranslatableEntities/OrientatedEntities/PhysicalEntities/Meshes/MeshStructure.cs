using Imagenic2.Core.Enums;
using Imagenic2.Core.Renderers.RayTracing;

namespace Imagenic2.Core.Entities;

public sealed class MeshStructure
{
    #region Fields and Properties

    public Vertex[] Vertices { get; set; }
    public Edge[]? Edges { get; set; }
    public Triangle[]? Triangles { get; set; }
    public Face[]? Faces { get; set; }
    public TextureStyle[]? Textures { get; set; }

    public MeshDimension MeshDimension { get; set; }
    internal BoundingBox BoundingBox { get; set; }

    // Line
    private static readonly Vertex[] lineVertices = new Vertex[2]
    {
        new Vertex(Vector3D.Zero), // 0
        new Vertex(Vector3D.One) // 1
    };
    private static readonly Edge[] lineEdges = new Edge[1]
    {
        new Edge(lineVertices[0], lineVertices[1]) // 0
    };
    internal static readonly MeshStructure lineStructure = new MeshStructure(MeshDimension._1D, lineVertices, lineEdges);

    // Plane
    private static readonly Vertex[] planeVertices = new Vertex[4]
    {
        new Vertex(new Vector3D(0, 0, 0)), // 0 [Bottom-left]
        new Vertex(new Vector3D(1, 0, 0)), // 1 [Bottom-right]
        new Vertex(new Vector3D(1, 0, 1)), // 2 [Top-right]
        new Vertex(new Vector3D(0, 0, 1)) // 3 [Top-left]
    };
    private static readonly Edge[] planeEdges = new Edge[4]
    {
        new Edge(planeVertices[0], planeVertices[1]), // 0 []
        new Edge(planeVertices[1], planeVertices[2]), // 1 []
        new Edge(planeVertices[2], planeVertices[3]), // 2 []
        new Edge(planeVertices[0], planeVertices[3]) // 3 []
    };
    private static readonly Triangle[] planeTriangles = new Triangle[2]
    {
        new Triangle(planeVertices[0], planeVertices[1], planeVertices[2]), // 0 []
        new Triangle(planeVertices[0], planeVertices[2], planeVertices[3]) // 1 []
    };
    private static readonly Face[] planeFaces = new Face[1]
    {
        new Face(planeTriangles[0], planeTriangles[1]) // 0
    };
    private static readonly BoundingBox planeBoundingBox = new BoundingBox(planeVertices[0].WorldOrigin, planeVertices[2].WorldOrigin);
    internal static readonly MeshStructure planeStructure = new MeshStructure(MeshDimension._2D, planeVertices, planeEdges, planeTriangles, planeFaces) { BoundingBox = planeBoundingBox };

    // Cube
    private static float radical = MathF.Sqrt(3) / 3;
    private static readonly Vertex[] cuboidVertices = new Vertex[8]
    {
        new Vertex(Vector3D.Zero, new Vector3D(-radical, -radical, -radical)), // 0 [Back-bottom-left]
        new Vertex(Vector3D.UnitX, new Vector3D(radical, -radical, -radical)), // 1 [Back-bottom-right]
        new Vertex(new Vector3D(1, 1, 0), new Vector3D(radical, radical, -radical)), // 2 [Back-top-right]
        new Vertex(Vector3D.UnitY, new Vector3D(-radical, radical, -radical)), // 3 [Back-top-left]
        new Vertex(Vector3D.UnitZ, new Vector3D(-radical, -radical, radical)), // 4 [Front-bottom-left]
        new Vertex(new Vector3D(1, 0, 1), new Vector3D(radical, -radical, radical)), // 5 [Front-bottom-right]
        new Vertex(Vector3D.One, new Vector3D(radical, radical, radical)), // 6 [Front-top-right]
        new Vertex(new Vector3D(0, 1, 1), new Vector3D(-radical, radical, radical)) // 7 [Front-top-left]
    };
    private static readonly Edge[] cuboidEdges = new Edge[12]
    {
        new Edge(cuboidVertices[0], cuboidVertices[1]), // 0 [Back-bottom]
        new Edge(cuboidVertices[1], cuboidVertices[2]), // 1 [Back-right]
        new Edge(cuboidVertices[2], cuboidVertices[3]), // 2 [Back-top]
        new Edge(cuboidVertices[3], cuboidVertices[0]), // 3 [Back-left]
        new Edge(cuboidVertices[4], cuboidVertices[5]), // 4 [Front-bottom]
        new Edge(cuboidVertices[5], cuboidVertices[6]), // 5 [Front-right]
        new Edge(cuboidVertices[6], cuboidVertices[7]), // 6 [Front-top]
        new Edge(cuboidVertices[7], cuboidVertices[4]), // 7 [Front-left]
        new Edge(cuboidVertices[0], cuboidVertices[4]), // 8 [Middle-bottom-left]
        new Edge(cuboidVertices[1], cuboidVertices[5]), // 9 [Middle-bottom-right]
        new Edge(cuboidVertices[2], cuboidVertices[6]), // 10 [Middle-top-right]
        new Edge(cuboidVertices[3], cuboidVertices[7]) // 11 [Middle-top-left]
    };
    private static readonly Triangle[] cuboidTriangles = new Triangle[12]
    {
        new Triangle(cuboidVertices[0], cuboidVertices[1], cuboidVertices[2], Vector2D.Zero, Vector2D.UnitX, Vector2D.One), // 0 [Back-1]
        new Triangle(cuboidVertices[0], cuboidVertices[2], cuboidVertices[3], Vector2D.Zero, Vector2D.One, Vector2D.UnitY), // 1 [Back-2]
        new Triangle(cuboidVertices[1], cuboidVertices[5], cuboidVertices[6], Vector2D.Zero, Vector2D.UnitY, Vector2D.One), // 2 [Right-1]
        new Triangle(cuboidVertices[1], cuboidVertices[6], cuboidVertices[2], Vector2D.Zero, Vector2D.One, Vector2D.UnitX), // 3 [Right-2]
        new Triangle(cuboidVertices[5], cuboidVertices[4], cuboidVertices[7], Vector2D.UnitX, Vector2D.Zero, Vector2D.UnitY), // 4 [Front-1]
        new Triangle(cuboidVertices[5], cuboidVertices[7], cuboidVertices[6], Vector2D.UnitX, Vector2D.UnitY, Vector2D.One), // 5 [Front-2]
        new Triangle(cuboidVertices[4], cuboidVertices[0], cuboidVertices[3], Vector2D.UnitY, Vector2D.Zero, Vector2D.UnitX), // 6 [Left-1]
        new Triangle(cuboidVertices[4], cuboidVertices[3], cuboidVertices[7], Vector2D.UnitY, Vector2D.UnitX, Vector2D.One), // 7 [Left-2]
        new Triangle(cuboidVertices[3], cuboidVertices[2], cuboidVertices[6], Vector2D.Zero, Vector2D.UnitX, Vector2D.One), // 8 [Top-1]
        new Triangle(cuboidVertices[3], cuboidVertices[6], cuboidVertices[7], Vector2D.Zero, Vector2D.One, Vector2D.UnitY), // 9 [Top-2]
        new Triangle(cuboidVertices[1], cuboidVertices[0], cuboidVertices[4], Vector2D.UnitX, Vector2D.Zero, Vector2D.UnitY), // 10 [Bottom-1]
        new Triangle(cuboidVertices[1], cuboidVertices[4], cuboidVertices[5], Vector2D.UnitX, Vector2D.UnitY, Vector2D.One)  // 11 [Bottom-2]
    };
    private static readonly Face[] cuboidFaces = new Face[6]
    {
        new Face(cuboidTriangles[0], cuboidTriangles[1]),// 0 [Back]
        new Face(cuboidTriangles[2], cuboidTriangles[3]), // 1 [Right]
        new Face(cuboidTriangles[4], cuboidTriangles[5]), // 2 [Front]
        new Face(cuboidTriangles[6], cuboidTriangles[7]), // 3 [Left]
        new Face(cuboidTriangles[8], cuboidTriangles[9]), // 4 [Top]
        new Face(cuboidTriangles[10], cuboidTriangles[11]) // 5 [Bottom]
    };
    private static readonly BoundingBox cuboidBoundingBox = new BoundingBox(cuboidVertices[0].WorldOrigin, cuboidVertices[6].WorldOrigin);
    internal static readonly MeshStructure cuboidStructure = new MeshStructure(MeshDimension._3D, cuboidVertices, cuboidEdges, cuboidTriangles, cuboidFaces) { BoundingBox = cuboidBoundingBox };

    #endregion

    #region Constructors

    public MeshStructure(MeshDimension meshDimension,
                         Vertex[] vertices,
                         Edge[]? edges = null,
                         Triangle[]? triangles = null,
                         Face[]? faces = null,
                         TextureStyle[]? textures = null)
    {
        MeshDimension = meshDimension;
        Vertices = vertices;
        Edges = edges;
        Triangles = triangles;
        Faces = faces;
        Textures = textures;
    }

    #endregion

    #region Methods

    public MeshStructure DeepCopy()
    {
        /*
        MeshStructure copy = new MeshStructure(
            this.MeshDimension,
            this.Vertices.ToArray(),
            this.Edges?.Select(e =>
            {
                Edge newEdge = new Edge(e.Vertex1, e.Vertex2);
                newEdge.EdgeStyle = e.EdgeStyle?.DeepCopy();
                return newEdge;
            }).ToArray(),
            this.Triangles?.Select(t =>
            {
                Triangle newTriangle = new Triangle(t.P1, t.P2, t.P3, t.TextureP1, t.TextureP2, t.TextureP3);
                newTriangle.FrontStyle = t.FrontStyle?.DeepCopy();
                newTriangle.BackStyle = t.BackStyle?.DeepCopy();
                return newTriangle;
            }).ToArray(),
            this.Faces?.Select(f =>
            {
                Face newFace = new Face(f.Triangles);
                newFace.FrontStyle = f.FrontStyle?.DeepCopy();
                newFace.BackStyle = f.BackStyle?.DeepCopy();
                return newFace;
            }).ToArray(),
            this.Textures?.Select(t => new TextureStyle(t.Image)).ToArray()
        );
        copy.BoundingBox = BoundingBox;
        return copy;
        */
        return null;
    }

    #region Circle

    internal static Vertex[] GenerateCircleVertices(int resolution)
    {
        // Vertices are defined in anti-clockwise order.
        Vertex[] vertices = new Vertex[resolution + 1];

        vertices[0] = new Vertex(Vector3D.Zero);

        float angle = Tau / resolution;
        for (int i = 0; i < resolution; i++)
        {
            vertices[i + 1] = new Vertex(new Vector3D(Cos(angle * i), 0, Sin(angle * i)));
        }

        return vertices;
    }

    internal static Edge[] GenerateCircleEdges(Vertex[] vertices, int resolution)
    {
        Edge[] edges = new Edge[resolution];

        for (int i = 0; i < resolution - 1; i++)
        {
            edges[i] = new Edge(vertices[i + 1], vertices[i + 2]);
        }
        edges[resolution - 1] = new Edge(vertices[resolution], vertices[1]);

        return edges;
    }

    internal static Triangle[] GenerateCircleTriangles(Vertex[] vertices, int resolution)
    {
        Triangle[] triangles = new Triangle[resolution];
        
        for (int i = 0; i < resolution - 1; i++)
        {
            triangles[i] = new Triangle(vertices[i + 1], vertices[0], vertices[i + 2]);
        }
        triangles[resolution - 1] = new Triangle(vertices[resolution], vertices[0], vertices[1]);

        return triangles;
    }

    internal static Face[] GenerateCircleFaces(Triangle[] triangles)
    {
        return new Face[1]
        {
            new Face(triangles)
        };
    }

    internal static BoundingBox GenerateCircleBoundingBox(IReadOnlyList<Vertex> vertices)
    {
        float radius = 1;
        Vector3D centre = vertices[0].WorldOrigin;
        Vector3D corner1 = new Vector3D(centre.x - radius, 0, centre.z - radius);
        Vector3D corner2 = new Vector3D(centre.x + radius, 0, centre.z + radius);

        return new BoundingBox(corner1, corner2);
    }

    internal static MeshStructure GenerateCircleStructure(int resolution)
    {
        Vertex[] vertices = GenerateCircleVertices(resolution);
        Edge[] edges = GenerateCircleEdges(vertices, resolution);
        Triangle[] triangles = GenerateCircleTriangles(vertices, resolution);
        Face[] faces = GenerateCircleFaces(triangles);
        BoundingBox boundingBox = GenerateCircleBoundingBox(vertices);

        return new MeshStructure(MeshDimension._2D, vertices, edges, triangles, faces) { BoundingBox = boundingBox };
    }

    #endregion

    #region Cone

    internal static Vertex[] GenerateConeVertices(MeshStructure circleStructure)
    {
        Vertex[] coneVertices = new Vertex[circleStructure.Vertices.Length + 1];

        for (int i = 0; i < circleStructure.Vertices.Length; i++)
        {
            coneVertices[i] = circleStructure.Vertices[i];
        }
        coneVertices[circleStructure.Vertices.Length] = new Vertex(Vector3D.UnitY);

        return coneVertices;
    }

    internal static Edge[] GenerateConeEdges(MeshStructure circleStructure, Vertex[] vertices, int resolution)
    {
        Edge[] topEdges = new Edge[resolution];

        for (int i = 0; i < resolution; i++)
        {
            topEdges[i] = new Edge(vertices[i + 1], vertices[resolution + 1]);
        }

        return topEdges.Concat(circleStructure.Edges).ToArray();
    }

    internal static Triangle[] GenerateConeTriangles(MeshStructure circleStructure, Vertex[] vertices, int resolution)
    {
        Triangle[] topTriangles = new Triangle[resolution];

        for (int i = 0; i < resolution - 1; i++)
        {
            topTriangles[i] = new Triangle(vertices[i + 1], vertices[i + 2], vertices[resolution + 1]);
        }
        topTriangles[resolution - 1] = new Triangle(vertices[resolution], vertices[1], vertices[resolution + 1]);

        return topTriangles.Concat(circleStructure.Triangles).ToArray();
    }

    internal static Face[] GenerateConeFaces(MeshStructure circleStructure, Triangle[] triangles, int resolution)
    {
        return new Face[2]
        {
            circleStructure.Faces[0],
            new Face(triangles.Skip(resolution).ToList())
        };
    }

    internal static BoundingBox GenerateConeBoundingBox(Vertex[] vertices)
    {
        float radius = 1;
        Vector3D centre = vertices[0].WorldOrigin;
        Vector3D corner1 = new Vector3D(centre.x - radius, 0, centre.z - radius);
        Vector3D corner2 = new Vector3D(centre.x + radius, 1, centre.z + radius);

        return new BoundingBox(corner1, corner2);
    }

    internal static MeshStructure GenerateConeStructure(int resolution)
    {
        MeshStructure circleStructure = GenerateCircleStructure(resolution);

        Vertex[] vertices = GenerateConeVertices(circleStructure);
        Edge[] edges = GenerateConeEdges(circleStructure, vertices, resolution);
        Triangle[] triangles = GenerateConeTriangles(circleStructure, vertices, resolution);
        Face[] faces = GenerateConeFaces(circleStructure, triangles, resolution);
        BoundingBox boundingBox = GenerateConeBoundingBox(vertices);

        return new MeshStructure(MeshDimension._3D, vertices, edges, triangles, faces) { BoundingBox = boundingBox };
    }

    #endregion

    #region Ring

    internal static Vertex[] GenerateRingVertices(int resolution, float innerRadius, float outerRadius)
    {
        // Vertices are defined in anti-clockwise order.
        Vertex[] vertices = new Vertex[2 * resolution + 1];
        vertices[0] = new Vertex(Vector3D.Zero);

        float angle = Tau / resolution;
        for (int i = 0; i < resolution; i++)
        {
            vertices[i + 1] = new Vertex(new Vector3D(Cos(angle * i) * innerRadius, 0, Sin(angle * i) * innerRadius));
            vertices[i + resolution + 1] = new Vertex(new Vector3D(Cos(angle * i) * outerRadius, 0, Sin(angle * i) * outerRadius));
        }

        return vertices;
    }

    internal static Edge[] GenerateRingEdges(Vertex[] vertices, int resolution)
    {
        Edge[] edges = new Edge[2 * resolution];

        for (int i = 0; i < resolution - 1; i++)
        {
            edges[i] = new Edge(vertices[i + 1], vertices[i + 2]);
            edges[i + resolution] = new Edge(vertices[i + resolution + 1], vertices[i + resolution + 2]);
        }
        edges[resolution - 1] = new Edge(vertices[resolution], vertices[1]);
        edges[2 * resolution - 1] = new Edge(vertices[2 * resolution], vertices[resolution + 1]);

        return edges;
    }

    internal static Triangle[] GenerateRingTriangles(Vertex[] vertices, int resolution)
    {
        Triangle[] triangles = new Triangle[2 * resolution];

        triangles[0] = new Triangle(vertices[1], vertices[resolution + 1], vertices[2 * resolution]);
        triangles[1] = new Triangle(vertices[1], vertices[2 * resolution], vertices[resolution]);
        for (int i = 1; i < resolution; i++)
        {
            triangles[i + 1] = new Triangle(vertices[i + 1], vertices[i + resolution + 1], vertices[i + resolution]);
            triangles[i + resolution + 1] = new Triangle(vertices[i + 1], vertices[i + resolution], vertices[i]);
        }

        return triangles;
    }

    internal static Face[] GenerateRingFaces(Triangle[] triangles)
    {
        return new Face[1]
        {
            new Face(triangles)
        };
    }

    internal static BoundingBox GenerateRingBoundingBox(Vertex[] vertices, float outerRadius)
    {
        Vector3D centre = vertices[0].WorldOrigin;
        Vector3D corner1 = new Vector3D(centre.x - outerRadius, 0, centre.z - outerRadius);
        Vector3D corner2 = new Vector3D(centre.x + outerRadius, 0, centre.z + outerRadius);

        return new BoundingBox(corner1, corner2);
    }

    internal static MeshStructure GenerateRingStructure(int resolution, float innerRadius, float outerRadius)
    {
        Vertex[] vertices = GenerateRingVertices(resolution, innerRadius, outerRadius);
        Edge[] edges = GenerateRingEdges(vertices, resolution);
        Triangle[] triangles = GenerateRingTriangles(vertices, resolution);
        Face[] faces = GenerateRingFaces(triangles);
        BoundingBox boundingBox = GenerateRingBoundingBox(vertices, outerRadius);

        return new MeshStructure(MeshDimension._2D, vertices, edges, triangles, faces) { BoundingBox = boundingBox };
    }

    #endregion

    #region Cylinder

    internal static Vertex[] GenerateCylinderVertices(MeshStructure topCircleStructure, MeshStructure bottomCircleStructure)
    {
        return topCircleStructure.Vertices.Concat(bottomCircleStructure.Vertices).ToArray();
    }

    internal static Edge[] GenerateCylinderEdges(MeshStructure topCircleStructure, MeshStructure bottomCircleStructure, int resolution)
    {
        Edge[] sideEdges = new Edge[resolution];

        for (int i = 0; i < resolution; i++)
        {
            sideEdges[i] = new Edge(topCircleStructure.Vertices[i + 1], bottomCircleStructure.Vertices[i + resolution + 1]);
        }

        return topCircleStructure.Edges.Concat(bottomCircleStructure.Edges.Concat(sideEdges)).ToArray();
    }

    internal static Triangle[] GenerateCylinderTriangles(MeshStructure topCircleStructure, MeshStructure bottomCircleStructure, int resolution)
    {
        Triangle[] sideTriangles = new Triangle[2 * resolution];

        for (int i = 1; i < resolution - 1; i++)
        {
            sideTriangles[i - 1] = new Triangle(topCircleStructure.Vertices[i], bottomCircleStructure.Vertices[i + 1], bottomCircleStructure.Vertices[i]);
            sideTriangles[i + resolution - 1] = new Triangle(topCircleStructure.Vertices[i], topCircleStructure.Vertices[i + 1], bottomCircleStructure.Vertices[i + 1]);
        }
        sideTriangles[resolution - 1] = new Triangle(topCircleStructure.Vertices[resolution], bottomCircleStructure.Vertices[1], bottomCircleStructure.Vertices[resolution]);
        sideTriangles[2 * resolution - 1] = new Triangle(topCircleStructure.Vertices[resolution], topCircleStructure.Vertices[1], bottomCircleStructure.Vertices[1]);

        return sideTriangles.Concat(topCircleStructure.Triangles.Concat(bottomCircleStructure.Triangles)).ToArray();
    }

    internal static Face[] GenerateCylinderFaces(Triangle[] triangles, MeshStructure topCircleStructure, MeshStructure bottomCircleStructure, int resolution)
    {
        Face[] faces = new Face[resolution + 2];

        for (int i = 0; i < resolution; i++)
        {
            faces[i] = new Face(triangles[i], triangles[i + resolution]);
        }
        faces[resolution] = new Face(topCircleStructure.Triangles);
        faces[resolution + 1] = new Face(bottomCircleStructure.Triangles);

        return faces;
    }

    internal static BoundingBox GenerateCylinderBoundingBox(MeshStructure bottomCircleStructure)
    {
        float radius = 1;
        Vector3D centre = bottomCircleStructure.Vertices[0].WorldOrigin;
        Vector3D corner1 = new Vector3D(centre.x - radius, 0, centre.z - radius);
        Vector3D corner2 = new Vector3D(centre.x + radius, 1, centre.z + radius);

        return new BoundingBox(corner1, corner2);
    }

    internal static MeshStructure GenerateCylinderStructure(int resolution)
    {
        MeshStructure topCircleStructure = GenerateCircleStructure(resolution);
        MeshStructure bottomCircleStructure = GenerateCircleStructure(resolution);

        Vertex[] vertices = GenerateCylinderVertices(topCircleStructure, bottomCircleStructure);
        Edge[] edges = GenerateCylinderEdges(topCircleStructure, bottomCircleStructure, resolution);
        Triangle[] triangles = GenerateCylinderTriangles(topCircleStructure, bottomCircleStructure, resolution);
        Face[] faces = GenerateCylinderFaces(triangles, topCircleStructure, bottomCircleStructure, resolution);
        BoundingBox boundingBox = GenerateCylinderBoundingBox(bottomCircleStructure);

        return new MeshStructure(MeshDimension._3D, vertices, edges, triangles, faces) { BoundingBox = boundingBox };
    }

    #endregion

    #region Sphere

    internal static Vertex[] GenerateSphereVertices(int latRes, int longRes)
    {
        Vertex[] vertices = new Vertex[(latRes + 1) * (longRes + 1) + 1];
        vertices[0] = new Vertex(Vector3D.Zero);

        for (int i = 0; i <= latRes; i++)
        {
            float phi = PI * i / latRes;

            for (int j = 0; j <= longRes; j++)
            {
                float theta = 2 * PI * j / longRes;
                float x = Sin(phi) * Cos(theta);
                float y = Sin(phi) * Sin(theta);
                float z = Cos(phi);

                vertices[i * (latRes + 1) + j + 1] = new Vertex(new Vector3D(x, y, z));
            }
        }

        return vertices;
    }

    internal static Edge[] GenerateSphereEdges(Vertex[] vertices, int latRes, int longRes)
    {
        return null;
    }

    internal static Triangle[] GenerateSphereTriangles(Vertex[] vertices, int latRes, int longRes)
    {
        return null;
    }

    internal static Face[] GenerateSphereFaces(Triangle[] triangles, int latRes, int longRes)
    {
        return null;
    }

    internal static BoundingBox GenerateSphereBoundingBox()
    {
        float radius = 1;
        Vector3D corner1 = new Vector3D(-radius, -radius, -radius);
        Vector3D corner2 = new Vector3D(radius, radius, radius);

        return new BoundingBox(corner1, corner2);
    }

    internal static MeshStructure GenerateSphereStructure(int latRes, int longRes)
    {
        Vertex[] vertices = GenerateSphereVertices(latRes, longRes);
        Edge[] edges = GenerateSphereEdges(vertices, latRes, longRes);
        Triangle[] triangles = GenerateSphereTriangles(vertices, latRes, longRes);
        Face[] faces = GenerateSphereFaces(triangles, latRes, longRes);
        BoundingBox boundingBox = GenerateSphereBoundingBox();

        return new MeshStructure(MeshDimension._3D, vertices, edges, triangles, faces) { BoundingBox = boundingBox };
    }

    #endregion

    #endregion
}