using System.ComponentModel;

namespace Material.Components.Shapes;

/// <summary>
/// Specifies categories of shapes based on their corner styles.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public enum ShapeFamily
{
    /// <summary>
    /// Represents shapes with curved corners, such as circles or ovals.
    /// </summary>
    Rounded,

    /// <summary>
    /// Represents shapes with angular corners, such as squares with chamfered edges.
    /// </summary>
    Cut
}
