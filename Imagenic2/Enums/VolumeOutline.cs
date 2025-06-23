namespace Imagenic2.Core.Enums;

/// <summary>
/// Options describing how view volume outlines are drawn.
/// </summary>
[Flags]
public enum VolumeOutline : byte
{
    /// <summary>
    /// Indicates that no view volume outline should be drawn.
    /// </summary>
    None = 0,
    /// <summary>
    /// Indicates that a view volume outline should be drawn from the origin to the near plane.
    /// </summary>
    Near = 1,
    /// <summary>
    /// Indicates that a view volume outline should be drawn from the origin to the far plane.
    /// </summary>
    Far = 2
}