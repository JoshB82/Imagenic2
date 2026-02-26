namespace Imagenic2.Core.Entities;

public class MeshStructure
{
    #region Fields and Properties

    public IReadOnlyList<Vertex> Vertices { get; set; }
    public IReadOnlyList<Edge> Edges { get; set; }
    public IReadOnlyList<Triangle> Triangles { get; set; }
    public IReadOnlyList<Face> Faces { get; set; }
    public IList<Texture> Textures { get; set; }

    // Line
    private static readonly IReadOnlyList<Vertex> lineVertices = new Vertex[2]
    {
        new Vertex(Vector3D.Zero), // 0
        new Vertex(Vector3D.One) // 1
    };
    private static readonly IReadOnlyList<Edge> lineEdges = new Edge[1]
    {
        new Edge(lineVertices[0], lineVertices[1]) // 0
    };
    internal static readonly MeshStructure lineStructure = new MeshStructure(lineVertices, lineEdges);

    // Plane
    private static readonly IReadOnlyList<Vertex> planeVertices = new Vertex[4]
    {
        new Vertex(new Vector3D(0, 0, 0)), // 0 [Bottom-left]
        new Vertex(new Vector3D(1, 0, 0)), // 1 [Bottom-right]
        new Vertex(new Vector3D(1, 0, 1)), // 2 [Top-right]
        new Vertex(new Vector3D(0, 0, 1)) // 3 [Top-left]
    };
    private static readonly IReadOnlyList<Edge> planeEdges = new Edge[4]
    {
        new Edge(planeVertices[0], planeVertices[1]), // 0 []
        new Edge(planeVertices[1], planeVertices[2]), // 1 []
        new Edge(planeVertices[2], planeVertices[3]), // 2 []
        new Edge(planeVertices[0], planeVertices[3]) // 3 []
    };
    private static readonly IReadOnlyList<Triangle> planeTriangles = new Triangle[2]
    {
        new Triangle(planeVertices[0], planeVertices[1], planeVertices[2]), // 0 []
        new Triangle(planeVertices[0], planeVertices[2], planeVertices[3]) // 1 []
    };
    private static readonly IReadOnlyList<Face> planeFaces = new Face[1]
    {
        new Face(planeTriangles[0], planeTriangles[1]) // 0
    };
    internal static readonly MeshStructure planeStructure = new MeshStructure(planeVertices, planeEdges, planeTriangles, planeFaces);

    // Cube
    private static float radical = MathF.Sqrt(3) / 3;
    private static readonly IReadOnlyList<Vertex> cuboidVertices = new Vertex[8]
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
    private static readonly IReadOnlyList<Edge> cuboidEdges = new Edge[12]
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
    private static readonly IReadOnlyList<Triangle> cuboidTriangles = new Triangle[12]
    {
        new Triangle(cuboidVertices[0], cuboidVertices[1], cuboidVertices[2]), // 0 [Back-1]
        new Triangle(cuboidVertices[0], cuboidVertices[2], cuboidVertices[3]), // 1 [Back-2]
        new Triangle(cuboidVertices[1], cuboidVertices[5], cuboidVertices[6]), // 2 [Right-1]
        new Triangle(cuboidVertices[1], cuboidVertices[6], cuboidVertices[2]), // 3 [Right-2]
        new Triangle(cuboidVertices[5], cuboidVertices[4], cuboidVertices[7]), // 4 [Front-1]
        new Triangle(cuboidVertices[5], cuboidVertices[7], cuboidVertices[6]), // 5 [Front-2]
        new Triangle(cuboidVertices[4], cuboidVertices[0], cuboidVertices[3]), // 6 [Left-1]
        new Triangle(cuboidVertices[4], cuboidVertices[3], cuboidVertices[7]), // 7 [Left-2]
        new Triangle(cuboidVertices[3], cuboidVertices[2], cuboidVertices[6]), // 8 [Top-1]
        new Triangle(cuboidVertices[3], cuboidVertices[6], cuboidVertices[7]), // 9 [Top-2]
        new Triangle(cuboidVertices[1], cuboidVertices[0], cuboidVertices[4]), // 10 [Bottom-1]
        new Triangle(cuboidVertices[1], cuboidVertices[4], cuboidVertices[5])  // 11 [Bottom-2]
    };
    private static readonly IReadOnlyList<Face> cuboidFaces = new Face[6]
    {
        new Face(cuboidTriangles[0], cuboidTriangles[1]),// 0 [Back]
        new Face(cuboidTriangles[2], cuboidTriangles[3]), // 1 [Right]
        new Face(cuboidTriangles[4], cuboidTriangles[5]), // 2 [Front]
        new Face(cuboidTriangles[6], cuboidTriangles[7]), // 3 [Left]
        new Face(cuboidTriangles[8], cuboidTriangles[9]), // 4 [Top]
        new Face(cuboidTriangles[10], cuboidTriangles[11]) // 5 [Bottom]
    };
    internal static readonly MeshStructure cubeStructure = new MeshStructure(cuboidVertices, cuboidEdges, cuboidTriangles, cuboidFaces);

    #endregion

    #region Constructors

    public MeshStructure(IReadOnlyList<Vertex> vertices,
                         IReadOnlyList<Edge>? edges = null,
                         IReadOnlyList<Triangle>? triangles = null,
                         IReadOnlyList<Face>? faces = null,
                         IList<Texture>? textures = null)
    {
        Vertices = vertices;
        Edges = edges;
        Triangles = triangles;
        Faces = faces;
        Textures = textures;
    }

    #endregion

    #region Methods

    internal static IReadOnlyList<Vertex> GenerateCircleVertices(int resolution)
    {
        // Vertices are defined in anti-clockwise order.
        IList<Vertex> vertices = new Vertex[resolution + 1];
        vertices[0] = new Vertex(Vector3D.Zero);

        float angle = Tau / resolution;
        for (int i = 0; i < resolution; i++)
        {
            vertices[i + 1] = new Vertex(new Vector3D(Cos(angle * i), 0, Sin(angle * i)));
        }

        return vertices.AsReadOnly();
    }

    internal static IReadOnlyList<Edge> GenerateCircleEdges(IReadOnlyList<Vertex> vertices, int resolution)
    {
        IList<Edge> edges = new Edge[resolution];

        for (int i = 0; i < resolution - 1; i++)
        {
            edges[i] = new Edge(vertices[i + 1], vertices[i + 2]);
        }
        edges[resolution - 1] = new Edge(vertices[resolution], vertices[1]);

        return edges.AsReadOnly();
    }

    internal static IReadOnlyList<Triangle> GenerateCircleTriangles(IReadOnlyList<Vertex> vertices, int resolution)
    {
        IList<Triangle> triangles = new Triangle[resolution];
        
        for (int i = 0; i < resolution - 1; i++)
        {
            triangles[i] = new Triangle(vertices[i + 1], vertices[0], vertices[i + 2]);
        }
        triangles[resolution - 1] = new Triangle(vertices[resolution], vertices[0], vertices[1]);

        return triangles.AsReadOnly();
    }

    internal static IReadOnlyList<Face> GenerateCircleFaces(IReadOnlyList<Triangle> triangles)
    {
        return new Face[1]
        {
            new Face(triangles)
        };
    }

    internal static MeshStructure GenerateCircleStructure(int resolution)
    {
        IReadOnlyList<Vertex> vertices = GenerateCircleVertices(resolution);
        IReadOnlyList<Edge> edges = GenerateCircleEdges(vertices, resolution);
        IReadOnlyList<Triangle> triangles = GenerateCircleTriangles(vertices, resolution);
        IReadOnlyList<Face> faces = GenerateCircleFaces(triangles);

        return new MeshStructure(vertices, edges, triangles, faces);
    }

    internal static IReadOnlyList<Vertex> GenerateConeVertices(MeshStructure circleStructure)
    {
        IList<Vertex> coneVertices = new Vertex[circleStructure.Vertices.Count + 1];
        for (int i = 0; i < circleStructure.Vertices.Count; i++)
        {
            coneVertices[i] = circleStructure.Vertices[i];
        }
        coneVertices[circleStructure.Vertices.Count] = new Vertex(Vector3D.UnitZ);
        return coneVertices.AsReadOnly();
    }

    internal static IReadOnlyList<Edge> GenerateConeEdges(MeshStructure circleStructure, IReadOnlyList<Vertex> vertices, int resolution)
    {
        IList<Edge> topEdges = new Edge[resolution];

        for (int i = 0; i < resolution; i++)
        {
            topEdges[i] = new Edge(vertices[i + 1], vertices[resolution + 1]);
        }

        return topEdges.Concat(circleStructure.Edges).ToList();
    }

    internal static IReadOnlyList<Triangle> GenerateConeTriangles(MeshStructure circleStructure, IReadOnlyList<Vertex> vertices, int resolution)
    {
        IList<Triangle> topTriangles = new Triangle[resolution];

        for (int i = 0; i < resolution - 1; i++)
        {
            topTriangles[i] = new Triangle(vertices[i + 1], vertices[resolution + 1], vertices[i + 2]);
        }
        topTriangles[resolution - 1] = new Triangle(vertices[resolution], vertices[resolution + 1], vertices[1]);

        return topTriangles.Concat(circleStructure.Triangles).ToList();
    }

    internal static IReadOnlyList<Face> GenerateConeFaces(MeshStructure circleStructure, IReadOnlyList<Triangle> triangles, int resolution)
    {
        return new Face[2]
        {
            circleStructure.Faces[0],
            new Face(triangles.Skip(resolution).ToList())
        };
    }

    internal static MeshStructure GenerateConeStructure(int resolution)
    {
        MeshStructure circleStructure = GenerateCircleStructure(resolution);

        IReadOnlyList<Vertex> vertices = GenerateConeVertices(circleStructure);
        IReadOnlyList<Edge> edges = GenerateConeEdges(circleStructure, vertices, resolution);
        IReadOnlyList<Triangle> triangles = GenerateConeTriangles(circleStructure, vertices, resolution);
        IReadOnlyList<Face> faces = GenerateConeFaces(circleStructure, triangles, resolution);

        return new MeshStructure(vertices, edges, triangles, faces);
    }

    internal static IReadOnlyList<Vertex> GenerateRingVertices(int resolution, float innerRadius, float outerRadius)
    {
        // Vertices are defined in anti-clockwise order.
        IList<Vertex> vertices = new Vertex[2 * resolution + 1];
        vertices[0] = new Vertex(Vector3D.Zero);

        float angle = Tau / resolution;
        for (int i = 0; i < resolution; i++)
        {
            vertices[i + 1] = new Vertex(new Vector3D(Cos(angle * i) * innerRadius, 0, Sin(angle * i) * innerRadius));
            vertices[i + resolution + 1] = new Vertex(new Vector3D(Cos(angle * i) * outerRadius, 0, Sin(angle * i) * outerRadius));
        }

        return vertices.AsReadOnly();
    }

    internal static IReadOnlyList<Edge> GenerateRingEdges(IReadOnlyList<Vertex> vertices, int resolution)
    {
        IList<Edge> edges = new Edge[2 * resolution];

        for (int i = 0; i < resolution - 1; i++)
        {
            edges[i] = new Edge(vertices[i + 1], vertices[i + 2]);
            edges[i + resolution] = new Edge(vertices[i + resolution + 1], vertices[i + resolution + 2]);
        }
        edges[resolution - 1] = new Edge(vertices[resolution], vertices[1]);
        edges[2 * resolution - 1] = new Edge(vertices[2 * resolution], vertices[resolution + 1]);

        return edges.AsReadOnly();
    }

    internal static IReadOnlyList<Triangle> GenerateRingTriangles(IReadOnlyList<Vertex> vertices, int resolution)
    {
        IList<Triangle> triangles = new Triangle[2 * resolution];

        triangles[0] = new Triangle(vertices[1], vertices[resolution + 1], vertices[2 * resolution]);
        triangles[1] = new Triangle(vertices[1], vertices[2 * resolution], vertices[resolution]);
        for (int i = 1; i < resolution; i++)
        {
            triangles[i + 1] = new Triangle(vertices[i + 1], vertices[i + resolution + 1], vertices[i + resolution]);
            triangles[i + resolution + 1] = new Triangle(vertices[i + 1], vertices[i + resolution], vertices[i]);
        }

        return triangles.AsReadOnly();
    }

    internal static IReadOnlyList<Face> GenerateRingFaces(IReadOnlyList<Triangle> triangles)
    {
        return new Face[1]
        {
            new Face(triangles)
        };
    }

    internal static MeshStructure GenerateRingStructure(int resolution, float innerRadius, float outerRadius)
    {
        IReadOnlyList<Vertex> vertices = GenerateRingVertices(resolution, innerRadius, outerRadius);
        IReadOnlyList<Edge> edges = GenerateRingEdges(vertices, resolution);
        IReadOnlyList<Triangle> triangles = GenerateRingTriangles(vertices, resolution);
        IReadOnlyList<Face> faces = GenerateRingFaces(triangles);

        return new MeshStructure(vertices, edges, triangles, faces);
    }

    internal static IReadOnlyList<Vertex> GenerateCylinderVertices(MeshStructure topCircleStructure, MeshStructure bottomCircleStructure)
    {
        return topCircleStructure.Vertices.Concat(bottomCircleStructure.Vertices).ToList();
    }

    internal static IReadOnlyList<Edge> GenerateCylinderEdges(MeshStructure topCircleStructure, MeshStructure bottomCircleStructure, int resolution)
    {
        IList<Edge> sideEdges = new Edge[resolution];

        for (int i = 0; i < resolution; i++)
        {
            sideEdges[i] = new Edge(topCircleStructure.Vertices[i + 1], bottomCircleStructure.Vertices[i + resolution + 1]);
        }

        return topCircleStructure.Edges.Concat(bottomCircleStructure.Edges.Concat(sideEdges)).ToList();
    }

    internal static IReadOnlyList<Triangle> GenerateCylinderTriangles(MeshStructure topCircleStructure, MeshStructure bottomCircleStructure, int resolution)
    {
        IList<Triangle> sideTriangles = new Triangle[2 * resolution];

        for (int i = 1; i < resolution - 1; i++)
        {
            sideTriangles[i - 1] = new Triangle(topCircleStructure.Vertices[i], bottomCircleStructure.Vertices[i + 1], bottomCircleStructure.Vertices[i]);
            sideTriangles[i + resolution - 1] = new Triangle(topCircleStructure.Vertices[i], topCircleStructure.Vertices[i + 1], bottomCircleStructure.Vertices[i + 1]);
        }
        sideTriangles[resolution - 1] = new Triangle(topCircleStructure.Vertices[resolution], bottomCircleStructure.Vertices[1], bottomCircleStructure.Vertices[resolution]);
        sideTriangles[2 * resolution - 1] = new Triangle(topCircleStructure.Vertices[resolution], topCircleStructure.Vertices[1], bottomCircleStructure.Vertices[1]);

        return sideTriangles.Concat(topCircleStructure.Triangles.Concat(bottomCircleStructure.Triangles)).ToList();
    }

    internal static IReadOnlyList<Face> GenerateCylinderFaces(IReadOnlyList<Triangle> triangles, MeshStructure topCircleStructure, MeshStructure bottomCircleStructure, int resolution)
    {
        IList<Face> faces = new Face[resolution + 2];

        for (int i = 0; i < resolution; i++)
        {
            faces[i] = new Face(triangles[i], triangles[i + resolution]);
        }
        faces[resolution] = new Face(topCircleStructure.Triangles);
        faces[resolution + 1] = new Face(bottomCircleStructure.Triangles);

        return faces.AsReadOnly();
    }

    internal static MeshStructure GenerateCylinderStructure(int resolution)
    {
        MeshStructure topCircleStructure = GenerateCircleStructure(resolution);
        MeshStructure bottomCircleStructure = GenerateCircleStructure(resolution);

        IReadOnlyList<Vertex> vertices = GenerateCylinderVertices(topCircleStructure, bottomCircleStructure);
        IReadOnlyList<Edge> edges = GenerateCylinderEdges(topCircleStructure, bottomCircleStructure, resolution);
        IReadOnlyList<Triangle> triangles = GenerateCylinderTriangles(topCircleStructure, bottomCircleStructure, resolution);
        IReadOnlyList<Face> faces = GenerateCylinderFaces(triangles, topCircleStructure, bottomCircleStructure, resolution);

        return new MeshStructure(vertices, edges, triangles, faces);
    }

    #endregion
}