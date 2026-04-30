using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

namespace Imagenic2.Core.Loaders;

public sealed class OBJLoader : MeshLoader
{
    #region Fields and Properties

    private IEnumerable<string> objData;

    private bool mtlSelected;
    private List<IEnumerable<string>> mtlData;

    #endregion

    #region Constructors

    public OBJLoader()
    {
        
    }

    #endregion

    #region Methods

    public override OBJLoader LoadFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".obj");

        objData = File.ReadLines(filePath);
        return this;
    }

    public OBJLoader WithMTLFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".mtl");

        mtlData.Add(File.ReadLines(filePath));
        return this;
    }

    public OBJLoader WithRFLFile(string filePath)
    {
        ThrowIfNotOfFileType(filePath, ".rfl");



        return this;
    }

    public bool Load()
    {
        mtlSelected = mtlData.Any();
        return objData.Any();
    }

    public async override MeshStructure ExtractMeshStructure(CancellationToken ct = default)
    {
        var mtlDictionary =  ProcessMaterials();


        List<Vector3D> positions = new List<Vector3D>();
        List<Vector2D> textureCoordinates = new List<Vector2D>();
        List<Vector3D> vertexNormals = new List<Vector3D>();

        
        List<(Vertex, int, int, int)> addedVertices;
        Dictionary<string, Vertex> vertexDictionary = new Dictionary<string, Vertex>();
        Dictionary<(int, int), Edge> edgeDictionary = new Dictionary<(int, int), Edge>();
        Dictionary<(Vector3D, Vector3D, Vector3D), Triangle> triangleDictionary = new Dictionary<(Vector3D, Vector3D, Vector3D), Triangle>();
        Dictionary<string, Face> faceDictionary = new Dictionary<string, Face>();

        
        
        float x, y, z;

        foreach (string line in objData)
        {
            string[] parts = line.Split(' ');

            switch (parts[0])
            {
                // Position co-ordinates
                case "v":
                    x = float.Parse(parts[1]);
                    y = float.Parse(parts[2]);
                    z = float.Parse(parts[3]);

                    positions.Add(new Vector3D(x, y, z));
                    break;
                // Texture co-ordinates
                case "vt":
                    x = float.Parse(parts[1]);
                    y = float.Parse(parts[2]);

                    textureCoordinates.Add(new Vector2D(x, y));
                    break;
                // Normal
                case "vn":
                    x = float.Parse(parts[1]);
                    y = float.Parse(parts[2]);
                    z = float.Parse(parts[3]);

                    vertexNormals.Add(new Vector3D(x, y, z));
                    break;
                // Line
                case "l":
                    addedVertices = new List<(Vertex, int, int, int)>();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        int vertexIndex = ProcessIndex(parts[i]);
                        Vector3D position = vertexIndex == -1 ? Vector3D.Zero : positions[vertexIndex];

                        AddToVertexDictionary($"{vertexIndex}/-1/-1", position, Vector3D.Zero, Vector2D.Zero, addedVertices, vertexIndex, -1, -1);
                    }

                    for (int j = 0; j < addedVertices.Count - 1; j++)
                    {
                        (int, int) edgeKey = (addedVertices[j].Item2, addedVertices[j + 1].Item2);
                        Edge edge = new Edge(addedVertices[j].Item1, addedVertices[j + 1].Item1);
                        AddToEdgeDictionary(edgeKey, edge);
                    }

                    break;
                // Face
                case "f":
                    addedVertices = new List<(Vertex, int, int, int)>();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] indices = parts[i].Split('/');

                        int vertexIndex = ProcessIndex(indices[0]);
                        int textureIndex = indices.Length > 1 ? ProcessIndex(indices[1]) : -1;
                        int normalIndex = indices.Length > 2 ? ProcessIndex(indices[2]) : -1;

                        Vector3D position = (vertexIndex == -1) ? Vector3D.Zero : positions[vertexIndex];
                        Vector3D normal = (normalIndex == -1) ? Vector3D.Zero : vertexNormals[normalIndex];
                        Vector2D texture = (textureIndex == -1) ? Vector2D.Zero : textureCoordinates[textureIndex];

                        AddToVertexDictionary($"{vertexIndex}/{normalIndex}/{textureIndex}", position, normal, texture, addedVertices, vertexIndex, normalIndex, textureIndex);
                    }

                    List<(Triangle, Vector3D, Vector3D, Vector3D)> triangleFan = new List<(Triangle, Vector3D, Vector3D, Vector3D)>();
                    for (int j = 0; j < addedVertices.Count - 1; j++)
                    {
                        Vector3D v1 = new Vector3D(addedVertices[0].Item2, addedVertices[0].Item3, addedVertices[0].Item4);
                        Vector3D v2 = new Vector3D(addedVertices[j].Item2, addedVertices[j].Item3, addedVertices[j].Item4);
                        Vector3D v3 = new Vector3D(addedVertices[j + 1].Item2, addedVertices[j + 1].Item3, addedVertices[j + 1].Item4);
                        (Vector3D, Vector3D, Vector3D) key = (v1, v2, v3);
                        Vector3D[] v = new Vector3D[] { key.Item1, key.Item2, key.Item3 };
                        Array.Sort(v, (a, b) =>
                        {
                            int cmp = a.x.CompareTo(b.x);
                            if (cmp != 0) return cmp;
                            cmp = a.y.CompareTo(b.y);
                            if (cmp != 0) return cmp;
                            return a.z.CompareTo(b.z);
                        });
                        key = (v[0], v[1], v[2]);

                        Triangle t = new Triangle(addedVertices[0].Item1, addedVertices[j].Item1, addedVertices[j + 1].Item1);
                        triangleFan.Add((t, v1, v2, v3));

                        AddToTriangleDictionary(key, t);
                    }


                    triangleFan = triangleFan.Select(t =>
                    {
                        Vector3D[] v = new Vector3D[] { t.Item2, t.Item3, t.Item4 };
                        Array.Sort(v, (a, b) =>
                        {
                            int cmp = a.x.CompareTo(b.x);
                            if (cmp != 0) return cmp;
                            cmp = a.y.CompareTo(b.y);
                            if (cmp != 0) return cmp;
                            return a.z.CompareTo(b.z);
                        });
                        return (t.Item1, v[0], v[1], v[2]);
                    })
                    .OrderBy(t => t.Item2.x).ThenBy(t => t.Item2.y).ThenBy(t => t.Item2.z)
                    .ThenBy(t => t.Item3.x).ThenBy(t => t.Item3.y).ThenBy(t => t.Item3.z)
                    .ThenBy(t => t.Item4.x).ThenBy(t => t.Item4.y).ThenBy(t => t.Item4.z).ToList();
                    string faceKey = string.Join('-', triangleFan.Select(t => $"{t.Item2.x}/{t.Item2.y}/{t.Item2.z}|{t.Item3.x}/{t.Item3.y}/{t.Item3.z}|{t.Item4.x}/{t.Item4.y}/{t.Item4.z}"));
                    AddToFaceDictionary(faceKey, new Face(triangleFan.Select(t => t.Item1).ToList()));
                    break;
            }
        }

        int ProcessIndex(string stringIndex)
        {
            if (stringIndex == string.Empty) return -1;
            int intIndex = int.Parse(stringIndex) - 1;
            return (intIndex < 0) ? positions.Count + intIndex : intIndex;
        }

        void AddToVertexDictionary(string key, Vector3D position, Vector3D normal, Vector2D textureCoordinates, List<(Vertex, int, int, int)> addedVertices, int vertexIndex, int normalIndex, int textureIndex)
        {
            if (vertexDictionary.TryGetValue(key, out Vertex vertex))
            {
                addedVertices.Add((vertex, vertexIndex, normalIndex, textureIndex));
            }
            else
            {
                Vertex newVertex = new Vertex(position, normal, textureCoordinates);
                vertexDictionary[key] = newVertex;
                addedVertices.Add((newVertex, vertexIndex, normalIndex, textureIndex));
            }
        }

        void AddToEdgeDictionary((int, int) key, Edge edge)
        {
            key = key.Item1 < key.Item2 ? (key.Item1, key.Item2) : (key.Item2, key.Item1);
            if (!edgeDictionary.TryGetValue(key, out edge))
            {
                edgeDictionary[key] = edge;
            }
        }

        void AddToTriangleDictionary((Vector3D, Vector3D, Vector3D) key, Triangle triangle)
        {
            Vector3D[] v = new Vector3D[] { key.Item1, key.Item2, key.Item3 };
            Array.Sort(v, (a, b) =>
            {
                int cmp = a.x.CompareTo(b.x);
                if (cmp != 0) return cmp;
                cmp = a.y.CompareTo(b.y);
                if (cmp != 0) return cmp;
                return a.z.CompareTo(b.z);
            });
            key = (v[0], v[1], v[2]);
            if (!triangleDictionary.TryGetValue(key, out triangle))
            {
                triangleDictionary[key] = triangle;
            }
        }

        void AddToFaceDictionary(string key, Face face)
        {
            if (!faceDictionary.TryGetValue(key, out face))
            {
                faceDictionary[key] = face;
            }
        }

        // Determine mesh dimension
        MeshDimension dimension;
        if (triangleDictionary.Count > 0)
        {
            dimension = MeshDimension._3D;
        }
        else if (edgeDictionary.Count > 0)
        {
            dimension = MeshDimension._2D;
        }
        else
        {
            dimension = MeshDimension._1D;
        }

        

        return new MeshStructure(dimension,
                                 vertexDictionary.Select(kvp => kvp.Value).ToArray(),
                                 edgeDictionary.Select(kvp => kvp.Value).ToArray(),
                                 triangleDictionary.Select(kvp => kvp.Value).ToArray(),
                                 faceDictionary.Select(kvp => kvp.Value).ToArray()
        );
    }

    private Dictionary<string, MTL> ProcessMaterials()
    {
        var mtlDictionary = new Dictionary<string, MTL>();

        foreach (IEnumerable<string> fileData in mtlData)
        {
            string name = null;
            MTL mtl = new MTL();

            foreach (string line in fileData)
            {
                string[] parts = line.Split(' ');

                switch (parts[0])
                {
                    // Comment
                    case "#":
                        break;
                    // New material
                    case "newmtl":
                        name = parts[1];
                        break;
                    // Ambient colour
                    case "Ka":
                        float r = float.Parse(parts[1]);
                        float g = float.Parse(parts[2]);
                        float b = float.Parse(parts[3]);

                        ThrowIfNotWithinRange(r, 0, 1);
                        ThrowIfNotWithinRange(g, 0, 1);
                        ThrowIfNotWithinRange(b, 0, 1);

                        mtl.AmbientColour = new Vector3D(r, g, b);
                        break;
                    // Diffuse colour
                    case "Kd":
                        r = float.Parse(parts[1]);
                        g = float.Parse(parts[2]);
                        b = float.Parse(parts[3]);

                        ThrowIfNotWithinRange(r, 0, 1);
                        ThrowIfNotWithinRange(g, 0, 1);
                        ThrowIfNotWithinRange(b, 0, 1);

                        mtl.DiffuseColour = new Vector3D(r, g, b);
                        break;
                    // Specular colour
                    case "Ks":
                        r = float.Parse(parts[1]);
                        g = float.Parse(parts[2]);
                        b = float.Parse(parts[3]);

                        ThrowIfNotWithinRange(r, 0, 1);
                        ThrowIfNotWithinRange(g, 0, 1);
                        ThrowIfNotWithinRange(b, 0, 1);

                        mtl.SpecularColour = new Vector3D(r, g, b);
                        break;
                    // Specular exponent
                    case "Ns":
                        float specularExponent = float.Parse(parts[1]);

                        ThrowIfNotWithinRange(specularExponent, 0, 1000);

                        mtl.SpecularExponent = specularExponent;
                        break;
                    // Dissolve
                    case "d":
                        float dissolve = float.Parse(parts[1]);

                        mtl.Dissolve = dissolve;
                        break;
                    case "Tr":
                        dissolve = 1 - float.Parse(parts[1]);

                        mtl.Dissolve = dissolve;
                        break;
                    // Transmission filter colour
                    case "Tf":
                        switch (parts[1])
                        {
                            case "xyz":
                                float x = float.Parse(parts[1]);
                                float y = float.Parse(parts[2]);
                                float z = float.Parse(parts[3]);
                                // ...
                                break;
                            case "spectral":
                                break;
                            default:
                                r = float.Parse(parts[1]);
                                g = float.Parse(parts[2]);
                                b = float.Parse(parts[3]);

                                ThrowIfNotWithinRange(r, 0, 1);
                                ThrowIfNotWithinRange(g, 0, 1);
                                ThrowIfNotWithinRange(b, 0, 1);

                                mtl.TransmissionFilterColour = new Vector3D(r, g, b);
                                break;
                        }
                        break;
                    case "Ni":
                        float iof = float.Parse(parts[1]);

                        ThrowIfNotWithinRange(iof, 0.001f, 10);

                        mtl.IndexOfRefraction = iof;
                        break;
                    case "illum":
                        int illuminationModel = int.Parse(parts[1]);

                        ThrowIfNotWithinRange(illuminationModel, 0, 10);

                        mtl.IlluminationModel = illuminationModel;
                        break;
                }
            }

            name ??= Guid.NewGuid().ToString();
            mtlDictionary.Add(name, mtl);
        }

        return mtlDictionary;
    }

    private class MTL
    {
        public Vector3D AmbientColour { get; set; }
        public Vector3D DiffuseColour { get; set; }
        public Vector3D SpecularColour { get; set; }
        public float SpecularExponent { get; set; }
        public float Dissolve { get; set; }
        public Vector3D TransmissionFilterColour { get; set; }
        public float IndexOfRefraction { get; set; }
        public int IlluminationModel { get; set; }
    }

    private class RFL
    {

    }

    #endregion
}