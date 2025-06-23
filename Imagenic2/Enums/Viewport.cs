namespace Imagenic2.Core.Enums;

/// <summary>
/// Options describing how the viewport is constructed.
/// </summary>
public enum Viewport
{
    /// <summary>
    /// Indicates that the viewport should consist of a single view covering all available space.
    /// </summary>
    Single,
    /// <summary>
    /// Indicates that the viewport should consist of two identical views but separated vertically down the middle of the available space and each taking up half of the available space.
    /// </summary>
    LeftAndRight,
    /// <summary>
    /// Indicates that the viewport should consist of two identical views but separated horizontally across the middle of the available space and each taking up half of the available space.
    /// </summary>
    TopAndBottom
}
