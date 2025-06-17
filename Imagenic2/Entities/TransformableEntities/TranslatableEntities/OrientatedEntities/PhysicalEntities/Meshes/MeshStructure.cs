namespace Imagenic2.Core.Entities;

internal class MeshStructure
{
    #region Fields and Properties

    public IList<Vertex> Vertices;
    public IList<Edge> Edges;
    public IList<Triangle> Triangles;
    public IList<Face> Faces;
    public IList<Texture> Textures;

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