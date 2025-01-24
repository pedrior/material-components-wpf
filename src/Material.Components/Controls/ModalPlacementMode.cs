namespace Material.Components.Controls;

/// <summary>
/// Specifies the available placement modes for a modal within its host container.
/// </summary>
/// <remarks>
/// Modal placement is always relative to the host container's dimensions, and 
/// the modal will automatically adjust its position based on the available space.
/// </remarks>
public enum ModalPlacementMode
{
    /// <summary>
    /// The modal occupies the entire available space of the host container.
    /// </summary>
    FullScreen,

    /// <summary>
    /// The modal is centered within the host container.
    /// </summary>
    Center,

    /// <summary>
    /// The modal is positioned at the top of the host container.
    /// </summary>
    Top,

    /// <summary>
    /// The modal is positioned at the bottom of the host container.
    /// </summary>
    Bottom,

    /// <summary>
    /// The modal is positioned to the left side of the host container.
    /// </summary>
    Left,

    /// <summary>
    /// The modal is positioned to the right side of the host container.
    /// </summary>
    Right
}