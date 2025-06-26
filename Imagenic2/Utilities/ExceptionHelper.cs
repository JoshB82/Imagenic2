using System.Runtime.CompilerServices;

namespace Imagenic2.Core.Utilities;

internal static class ExceptionHelper
{
    internal static void ThrowIfNull(object? param,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param is null) throw new ArgumentNullException(paramName);
    }

    internal static void ThrowIfNull(object? param1, object? param2,
        [CallerArgumentExpression(nameof(param1))] string? param1Name = null,
        [CallerArgumentExpression(nameof(param2))] string? param2Name = null)
    {
        if (param1 is null) throw new ArgumentNullException(param1Name);
        if (param2 is null) throw new ArgumentNullException(param2Name);
    }

    internal static void ThrowIfNull(object? param1, object? param2, object? param3,
        [CallerArgumentExpression(nameof(param1))] string? param1Name = null,
        [CallerArgumentExpression(nameof(param2))] string? param2Name = null,
        [CallerArgumentExpression(nameof(param3))] string? param3Name = null)
    {
        if (param1 is null) throw new ArgumentNullException(param1Name);
        if (param2 is null) throw new ArgumentNullException(param2Name);
        if (param3 is null) throw new ArgumentNullException(param3Name);
    }

    internal static void ThrowIfNull(object? param1, object? param2, object? param3, object? param4,
        [CallerArgumentExpression(nameof(param1))] string? param1Name = null,
        [CallerArgumentExpression(nameof(param2))] string? param2Name = null,
        [CallerArgumentExpression(nameof(param3))] string? param3Name = null,
        [CallerArgumentExpression(nameof(param4))] string? param4Name = null)
    {
        if (param1 is null) throw new ArgumentNullException(param1Name);
        if (param2 is null) throw new ArgumentNullException(param2Name);
        if (param3 is null) throw new ArgumentNullException(param3Name);
        if (param4 is null) throw new ArgumentNullException(param4Name);
    }
}