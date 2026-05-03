using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;
using System.Drawing;

namespace Imagenic2.Core.Loaders;

public interface IOBJBuilder
{
    public IOBJBuilder WithMTLFile(string filePath);
    public IOBJBuilder WithRFLFile(string filePath);
    public bool Load();
}

public sealed partial class OBJLoader : MeshLoader<OBJLoaderOptions>, IOBJBuilder
{
    #region Fields and Properties

    public override OBJLoaderOptions LoaderOptions { get; set; }

    private LoadedFile<IEnumerable<string>> objData;

    private bool mtlSelected;
    private List<LoadedFile<IEnumerable<string>>> mtlData;

    private bool rflSelected;
    private List<LoadedFile<IEnumerable<string>>> rflData;

    private bool bmpSelected;
    private List<LoadedFile<Bitmap>> bitmaps;

    private class LoadedFile<TData>
    {
        public string FilePath { get; set; }
        public TData Data { get; set; }

        public LoadedFile(string filePath, TData data)
        {
            FilePath = filePath;
            Data = data;
        }
    }

    #endregion

    #region Constructors

    public OBJLoader(OBJLoaderOptions options) : base(options)
    {
        
    }

    #endregion

    #region Methods

    public IOBJBuilder LoadOBJFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".obj");

        objData = new LoadedFile<IEnumerable<string>>(filePath, File.ReadLines(filePath));
        return this;
    }

    
    public IOBJBuilder WithMTLFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".mtl");

        mtlData.Add(new LoadedFile<IEnumerable<string>>(filePath, File.ReadLines(filePath)));
        return this;
    }

    public IOBJBuilder WithRFLFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".rfl");

        // ...

        return this;
    }

    public IOBJBuilder WithBMPFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".bmp");

        // ...

        return this;
    }

    public IOBJBuilder WithPNGFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".png");

        // ...

        return this;
    }

    public override bool Load()
    {
        mtlSelected = mtlData.Count > 0;
        rflSelected = rflData.Count > 0;
        bmpSelected = bitmaps.Count > 0;
        return objData is not null;
    }

    public /*async */override MeshStructure ExtractMeshStructure(CancellationToken ct = default)
    {
        var materialDictionary =  ObtainMaterials();

        List<Vector3D> positions = new List<Vector3D>();
        List<Vector2D> textureCoordinates = new List<Vector2D>();
        List<Vector3D> vertexNormals = new List<Vector3D>();

        int ParsePositionsIndex(string input)
        {
            int intIndex = int.Parse(input) - 1;
            return (intIndex < 0) ? positions.Count + intIndex : intIndex;
        }
        int ParseTextureCoordinatesIndex(string input)
        {
            int intIndex = int.Parse(input) - 1;
            return (intIndex < 0) ? textureCoordinates.Count + intIndex : intIndex;
        }
        int ParseVertexNormalsIndex(string input)
        {
            int intIndex = int.Parse(input) - 1;
            return (intIndex < 0) ? vertexNormals.Count + intIndex : intIndex;
        }

        List<Vertex> vertices = new();
        List<Edge> edges = new();
        List<Triangle> triangles = new();
        List<Face> faces = new();

        Material? currentMaterial = null;

        foreach (string line in objData.Data)
        {
            if (line == "") continue;

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            switch (parts[0])
            {
                case "usemtl": // Use material
                    currentMaterial = materialDictionary[parts[1]];
                    break;
                case "v": // Position co-ordinates
                    float x = float.Parse(parts[1]);
                    float y = float.Parse(parts[2]);
                    float z = float.Parse(parts[3]);

                    positions.Add(new Vector3D(x, y, z));

                    break;
                case "vt": // Texture co-ordinates
                    x = float.Parse(parts[1]);
                    y = float.Parse(parts[2]);

                    textureCoordinates.Add(new Vector2D(x, y));

                    break;
                case "vn": // Normal
                    x = float.Parse(parts[1]);
                    y = float.Parse(parts[2]);
                    z = float.Parse(parts[3]);

                    Vector3D vertexNormal = new Vector3D(x, y, z);
                    vertexNormals.Add(LoaderOptions.NormaliseVertexNormals ? vertexNormal.Normalise() : vertexNormal);

                    break;
                case "l": // Line
                    List<Vertex> referencedVertices = new();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        Vertex referencedVertex = new Vertex(positions[ParsePositionsIndex(parts[i])]);
                        referencedVertices.Add(referencedVertex);
                        vertices.Add(referencedVertex);
                    }

                    for (int j = 0; j < referencedVertices.Count - 1; j++)
                    {
                        Edge edge = new Edge(referencedVertices[j], referencedVertices[j + 1]);
                        edges.Add(edge);
                    }

                    break;
                case "f": // Face
                    referencedVertices = new();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] indices = parts[i].Split('/');

                        int vertexIndex = ParsePositionsIndex(indices[0]);
                        int textureIndex = indices.Length > 1 ? ParseTextureCoordinatesIndex(indices[1]) : -1;
                        int normalIndex = indices.Length > 2 ? ParseVertexNormalsIndex(indices[2]) : -1;

                        Vector3D position = (vertexIndex == -1) ? Vector3D.Zero : positions[vertexIndex];
                        Vector2D texture = (textureIndex == -1) ? Vector2D.Zero : textureCoordinates[textureIndex];
                        Vector3D normal = (normalIndex == -1) ? Vector3D.Zero : vertexNormals[normalIndex];

                        Vertex referencedVertex = new Vertex(position, normal, texture);
                        referencedVertices.Add(referencedVertex);
                        vertices.Add(referencedVertex);
                    }

                    List<Triangle> referencedTriangles = new();
                    for (int j = 1; j < referencedVertices.Count - 1; j++)
                    {
                        Triangle triangle = new Triangle(referencedVertices[0], referencedVertices[j], referencedVertices[j + 1]);
                        triangle.FrontStyle = currentMaterial;
                        referencedTriangles.Add(triangle);
                        triangles.Add(triangle);
                    }
                    Face face = new Face(referencedTriangles);
                    faces.Add(face);

                    break;
            }
        }

        // Determine mesh dimension
        MeshDimension dimension;
        if (triangles.Count > 0)
        {
            dimension = MeshDimension._3D;
        }
        else if (edges.Count > 0)
        {
            dimension = MeshDimension._2D;
        }
        else
        {
            dimension = MeshDimension._1D;
        }

        DeduplicateVertices(vertices);
        DeduplicateEdges(edges);
        DeduplicateTriangles(triangles);
        DeduplicateFaces(faces);

        return new MeshStructure(dimension,
                                 vertices.ToArray(),
                                 edges.ToArray(),
                                 triangles.ToArray(),
                                 faces.ToArray()
        );
    }

    private struct VertexEqualityComparer : IEqualityComparer<Vertex>
    {
        public readonly bool Equals(Vertex v1, Vertex v2) => (v1.WorldOrigin, v1.TextureCoordinates, v1.Normal) == (v2.WorldOrigin, v2.TextureCoordinates, v2.Normal);
        public readonly int GetHashCode(Vertex v) => (v.WorldOrigin, v.TextureCoordinates, v.Normal).GetHashCode();
    }

    private void DeduplicateVertices(List<Vertex> vertices)
    {
        if (LoaderOptions.DeduplicateVertices)
        {
            vertices = vertices.Distinct(new VertexEqualityComparer()).ToList();
        }
    }

    private void DeduplicateEdges(List<Edge> edges)
    {
        if (LoaderOptions.DeduplicateEdges)
        {

        }
    }

    private void DeduplicateTriangles(List<Triangle> triangles)
    {
        if (LoaderOptions.DeduplicateTriangles)
        {

        }
    }

    private void DeduplicateFaces(List<Face> faces)
    {
        if (LoaderOptions.DeduplicateFaces)
        {

        }
    }

    private class RFL
    {

    }

    #endregion
}