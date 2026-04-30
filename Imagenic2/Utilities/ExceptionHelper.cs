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

    internal static void ThrowIfApproxZero(Quaternion param, float epsilon = Settings.epsilon,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param.ApproxEquals(Quaternion.Zero, epsilon)) throw new CannotBeZeroException($"{paramName} cannot equal zero.");
    }

    internal static void ThrowIfApproxZero(Vector2D param, float epsilon = Settings.epsilon,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param.ApproxEquals(Vector2D.Zero, epsilon)) throw new CannotBeZeroException($"{paramName} cannot equal zero.");
    }

    internal static void ThrowIfApproxZero(Vector3D param, float epsilon = Settings.epsilon,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param.ApproxEquals(Vector3D.Zero, epsilon)) throw new CannotBeZeroException($"{paramName} cannot equal zero.");
    }

    internal static void ThrowIfApproxZero(Vector4D param, float epsilon = Settings.epsilon,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param.ApproxEquals(Vector4D.Zero, epsilon)) throw new CannotBeZeroException($"{paramName} cannot equal zero.");
    }

    internal static void ThrowIfNotOrthogonal(Vector3D param1, Vector3D param2, float epsilon = Settings.epsilon,
        [CallerArgumentExpression(nameof(param1))] string? param1Name = null,
        [CallerArgumentExpression(nameof(param2))] string? param2Name = null)
    {
        if (!(param1 * param2).ApproxEquals(0, epsilon)) throw new MustBeOrthogonalException($"{param1Name} and {param2Name} must be orthogonal.");
    }

    internal static void ThrowIfNotWithinRange(float param, float inclusiveStart, float inclusiveEnd,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param < inclusiveStart || param > inclusiveEnd) throw new ArgumentOutOfRangeException(paramName);
    }

    internal static void ThrowIfNonpositive(float param,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (param <= 0) throw new ArgumentOutOfRangeException(paramName);
    }

    internal static void ThrowIfNotFinite(float param,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (!float.IsFinite(param)) throw new MustBeFiniteException($"{paramName} must be finite.");
    }

    internal static void ThrowIfNotFinite(Vector3D param,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (!Vector3D.IsFinite(param)) throw new MustBeFiniteException($"{paramName} must be finite.");
    }

    internal static void ThrowIfNotFinite(Quaternion param,
        [CallerArgumentExpression(nameof(param))] string? paramName = null)
    {
        if (!Quaternion.IsFinite(param)) throw new MustBeFiniteException($"{paramName} must be finite.");
    }

    internal static void ThrowIfNotOfFileType(string filePath, string extension,
        [CallerArgumentExpression(nameof(filePath))] string? paramName = null)
    {
        if (!(Path.GetExtension(filePath) == extension)) throw new IncorrectFileTypeException($"{paramName} must be a file of type {extension}.");
    }
}

public class CannotBeZeroException : Exception
{
    #region Constructors

    public CannotBeZeroException() { }

    public CannotBeZeroException(string message) : base(message) { }

    public CannotBeZeroException(string message,  Exception innerException) : base(message, innerException) { }

    #endregion
}

public class MustBeOrthogonalException : Exception
{
    #region Constructors

    public MustBeOrthogonalException() { }

    public MustBeOrthogonalException(string message) : base(message) { }

    public MustBeOrthogonalException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

public class MustBeFiniteException : Exception
{
    #region Constructors

    public MustBeFiniteException() { }

    public MustBeFiniteException(string message) : base(message) { }

    public MustBeFiniteException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

public class IncorrectFileTypeException : Exception
{
    #region Constructors

    public IncorrectFileTypeException() { }

    public IncorrectFileTypeException(string message) : base(message) { }

    public IncorrectFileTypeException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}