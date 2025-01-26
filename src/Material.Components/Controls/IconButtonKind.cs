namespace Material.Components.Controls;

/// <summary>
/// Specifies the types of icon buttons, determining their appearance and level of emphasis in the UI.
/// </summary>
public enum IconButtonKind
{
    /// <summary>
    /// Identifies an icon button with a transparent background and no border.
    /// </summary>
    Standard,

    /// <summary>
    /// Identifies an icon button with a primary background that contrasts with the surface color.
    /// </summary>
    Filled,

    /// <summary>
    /// Identifies an icon button with a lighter, tonal background and a dark label.
    /// </summary>
    Tonal,

    /// <summary>
    /// Identifies an icon button with a transparent background and a border.
    /// </summary>
    Outlined
}
