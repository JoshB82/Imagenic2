namespace Imagenic2.Core.Loaders;

public abstract class FontLoader<TOptions> : Loader<TOptions> where TOptions : FontLoaderOptions
{
    #region Fields and Properties

    public override TOptions LoaderOptions { get; set; }

    #endregion

    #region Constructors

    protected FontLoader(TOptions options) : base(options)
    {
        
    }

    #endregion
}