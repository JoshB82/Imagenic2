using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Loaders;

public abstract class MeshLoader : Loader
{
    #region Fields and Properties

    #endregion

    #region Constructors

    protected MeshLoader()
    {
        
    }

    #endregion

    #region Methods

    public abstract bool Load();

    public abstract MeshStructure ExtractMeshStructure(CancellationToken ct);

    #endregion
}