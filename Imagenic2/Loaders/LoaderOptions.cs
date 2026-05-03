namespace Imagenic2.Core.Loaders;

public abstract class LoaderOptions
{
    #region Fields and Properties

    public bool IgnoreMalformedData { get; set; } = false;

    #endregion

    #region Constructors

    protected LoaderOptions()
    {
        
    }

    #endregion
}