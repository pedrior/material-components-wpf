namespace Material.Components.Controls;

/// <summary>
/// Specifies the vertical alignment of the destination items within a <see cref="NavigationRail"/>.
/// </summary>
/// <remarks>
/// The <see cref="NavigationRailAlignment"/> determines how the destination items in a <see cref="NavigationRail"/>
/// are aligned vertically within the available space of the control.
/// </remarks>
public enum NavigationRailAlignment
{
    /// <summary>
    /// Aligns the destination items at the top of the navigation rail.
    /// </summary>
    Top,

    /// <summary>
    /// Aligns the destination items in the middle of the navigation rail.
    /// </summary>
    Middle,

    /// <summary>
    /// Aligns the destination items at the bottom of the navigation rail.
    /// </summary>
    Bottom
}