using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Loaders;

public abstract class MeshLoader<TOptions> : Loader<TOptions> where TOptions : MeshLoaderOptions
{
    #region Fields and Properties

    public override TOptions LoaderOptions { get; set; }

    #endregion

    #region Constructors

    protected MeshLoader(TOptions options) : base(options)
    {
        
    }

    #endregion

    #region Methods

    public abstract bool Load();

    public abstract MeshStructure ExtractMeshStructure(CancellationToken ct);

    #endregion
}