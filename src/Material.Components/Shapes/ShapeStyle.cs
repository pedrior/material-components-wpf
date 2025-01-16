using System.ComponentModel;

namespace Material.Components.Shapes;

/// <summary>
/// Specifies predefined styles for the corner radius of a shape.
/// </summary>
/// <remarks>
/// These styles determine the radius applied to the corners of a shape. The styles are independent of the corner type,
/// whether rounded or cut.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public enum ShapeStyle
{
    /// <summary>
    /// No corner radius is applied, resulting in unmodified corners.
    /// </summary>
    None,

    /// <summary>
    /// A style with an extra-small corner radius of 4.0 units.
    /// </summary>
    ExtraSmall,

    /// <summary>
    /// A style with a small corner radius of 8.0 units.
    /// </summary>
    Small,

    /// <summary>
    /// A style with a medium corner radius of 12.0 units.
    /// </summary>
    Medium,

    /// <summary>
    /// A style with a large corner radius of 16.0 units.
    /// </summary>
    Large,

    /// <summary>
    /// A style with an extra-large corner radius of 28.0 units.
    /// </summary>
    ExtraLarge,

    /// <summary>
    /// A style where the corner radius is 100% of the corner dimensions.
    /// </summary>
    /// <remarks>
    /// The "Full" style applies a radius that completely modifies the corner, creating
    /// a fully rounded or completely cut appearance, depending on the shape type.
    /// </remarks>
    Full
}