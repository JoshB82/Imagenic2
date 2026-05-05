namespace Imagenic2.Core.Loaders;

public abstract class MeshLoaderOptions : LoaderOptions
{
    #region Fields and Properties

    public bool DeduplicateVertices { get; set; }
    public bool DeduplicateEdges { get; set; }
    public bool DeduplicateTriangles { get; set; }
    public bool DeduplicateFaces { get; set; }

    public bool NormaliseVertexNormals { get; set; }
    public bool RemoveUnusedVertices { get; set; }

    public Func<float, float> UnitTweaker { get; set; } = v => v;

    public bool LoadTextures { get; set; }
    public bool AddMissingTextures { get; set; }

    #endregion

    #region Constructors

    protected MeshLoaderOptions()
    {
        
    }

    #endregion
}