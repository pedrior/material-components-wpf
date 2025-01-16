namespace Material.Components.Elevations;

/// <summary>
/// Represents predefined elevation levels, indicating the distance between two surfaces on the z-axis.
/// </summary>
/// <remarks>
/// Elevation levels are used to express the spatial relationship between UI components in a consistent way. 
/// Higher elevation levels visually indicate more separation from the background using larger and softer shadows, 
/// while lower levels use smaller and sharper shadows to indicate proximity. 
/// </remarks>
public enum ElevationLevel
{
    /// <summary>
    /// No elevation is applied. The surface is flush with the background.
    /// </summary>
    None,

    /// <summary>
    /// Represents a low elevation level. Typically used for small, minimally raised components.
    /// </summary>
    Level1,

    /// <summary>
    /// Represents a slightly higher elevation level. Suitable for moderately raised components.
    /// </summary>
    Level2,

    /// <summary>
    /// Represents a medium elevation level. Commonly used for interactive elements.
    /// </summary>
    Level3,

    /// <summary>
    /// Represents a high elevation level. Typically used for components that need more visual prominence.
    /// </summary>
    Level4,

    /// <summary>
    /// Represents the highest elevation level, used for highly prominent components.
    /// </summary>
    Level5
}