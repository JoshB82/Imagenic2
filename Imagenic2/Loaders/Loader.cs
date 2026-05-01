using Imagenic2.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace Imagenic2.Core.Loaders;

public abstract class Loader
{
    #region Fields and Properties

    #if DEBUG

    public ILogger Logger { get; set; }

    #endif

    public bool IgnoreMalformedData { get; set; } = false;

    #endregion

    #region Constructors

    protected Loader()
    {
        #if DEBUG
        
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        Logger = loggerFactory.CreateLogger<Loader>();

        #endif
    }

    #endregion

    #region Methods

    protected void ThrowDueToMalformedData(int lineNumber, string filePath)
    {
        if (IgnoreMalformedData)
        {
            #if DEBUG

            Logger.LogInformation("Malformed data present on line {lineNumber} in {filePath}", lineNumber, filePath);

            #endif
        }
        else
        {
            throw new MalformedDataException($"Could not parse data on line {lineNumber} in {filePath}.");
        }
    }

    #endregion
}