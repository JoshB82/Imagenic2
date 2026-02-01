namespace Imagenic2.Core.Entities;

public class MeshStructure
{
    #region Fields and Properties

    public IList<Vertex> Vertices;
    public IList<Edge> Edges;
    public IList<Triangle> Triangles;
    public IList<Face> Faces;
    public IList<Texture> Textures;

    // Line
    internal static readonly MeshStructure lineStructure = new MeshStructure(lineVertices, lineEdges);
    private static readonly IList<Vertex> lineVertices = new Vertex[2]
    {
        new Vertex(Vector3D.Zero), // 0
        new Vertex(Vector3D.One) // 1
    };
    private static readonly IList<Edge> lineEdges = new Edge[1]
    {
        new Edge(lineVertices[0], lineVertices[1]) // 0
    };
    

    // Plane
    internal static readonly MeshStructure planeStructure = new MeshStructure(planeVertices);
    private static readonly IList<Vertex> planeVertices = new Vertex[4]
    {
        new(new Vector3D(0, 0, 0)), // 0 [Bottom-left]
        new(new Vector3D(1, 0, 0)), // 1 [Bottom-right]
        new(new Vector3D(1, 0, 1)), // 2 [Top-right]
        new(new Vector3D(0, 0, 1)) // 3 [Top-left]
    };
    private static readonly IList<Edge> planeEdges = new Edge[4]
    {
        new Edge(planeVertices[0], planeVertices[1]), // 0 []
        new Edge(planeVertices[1], planeVertices[2]), // 1 []
        new Edge(planeVertices[2], planeVertices[3]), // 2 []
        new Edge(planeVertices[0], planeVertices[3]) // 3 []
    };


    private static readonly IList<Triangle> planeTriangles = new Triangle[2]
    {
        new Triangle(planeVertices[0], planeVertices[1], planeVertices[2]), // 0 []
        new Triangle(planeVertices[0], planeVertices[2], planeVertices[3]) // 1 []
    };


    private static readonly IList<Face> planeFaces = new Face[1]
    {
        new Face(planeTriangles[0], planeTriangles[1]) // 0
    };
    

    // Cube
    internal static readonly MeshStructure cubeStructure = new MeshStructure(cuboidVertices, cuboidEdges, cuboidTriangles, cuboidFaces);
    private static float radical = MathF.Sqrt(3) / 3;
    private static readonly IList<Vertex> cuboidVertices = new Vertex[8]
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
    private static readonly IList<Edge> cuboidEdges = new Edge[12]
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
    private static readonly IList<Triangle> cuboidTriangles = new Triangle[12]
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
    private static readonly IList<Face> cuboidFaces = new Face[6]
    {
        new Face(cuboidTriangles[0], cuboidTriangles[1]),// 0 [Back]
        new Face(cuboidTriangles[2], cuboidTriangles[3]), // 1 [Right]
        new Face(cuboidTriangles[4], cuboidTriangles[5]), // 2 [Front]
        new Face(cuboidTriangles[6], cuboidTriangles[7]), // 3 [Left]
        new Face(cuboidTriangles[8], cuboidTriangles[9]), // 4 [Top]
        new Face(cuboidTriangles[10], cuboidTriangles[11]) // 5 [Bottom]
    };

    #endregion

    #region Constructors

    public MeshStructure(IList<Vertex> vertices,
                         IList<Edge>? edges = null,
                         IList<Triangle>? triangles = null,
                         IList<Face>? faces = null,
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

    #endregion
}