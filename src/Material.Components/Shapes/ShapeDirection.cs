using System.ComponentModel;

namespace Material.Components.Shapes;

/// <summary>
/// Specifies the directional configuration of a shape's corner values.
/// </summary>
/// <remarks>
/// The direction determines how corner values are distributed across the shape,
/// allowing for symmetric or asymmetric configurations.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public enum ShapeDirection
{
    /// <summary>
    /// A symmetric shape configuration where all corners have the same value.
    /// </summary>
    Symmetric,

    /// <summary>
    /// An asymmetric shape configuration where only the top-left and top-right
    /// corners are assigned values.
    /// </summary>
    Top,

    /// <summary>
    /// An asymmetric shape configuration where only the bottom-left and bottom-right
    /// corners are assigned values.
    /// </summary>
    Bottom,

    /// <summary>
    /// An asymmetric shape configuration where only the top-left and bottom-left
    /// corners are assigned values.
    /// </summary>
    Start,

    /// <summary>
    /// An asymmetric shape configuration where only the top-right and bottom-right
    /// corners are assigned values.
    /// </summary>
    End
}