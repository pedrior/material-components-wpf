namespace Material.Components.Controls;

/// <summary>
/// Specifies the alignment of child elements within a panel.
/// </summary>
/// <remarks>
/// Use this enum to control how child elements are positioned within the panel.
/// </remarks>
public enum Alignment
{
    /// <summary>
    /// Child elements are aligned to the start of the panel.
    /// </summary>
    /// <example>
    /// For horizontal orientation, children are aligned to the left.
    /// For vertical orientation, children are aligned to the top.
    /// </example>
    Start,

    /// <summary>
    /// Child elements are centered within the panel.
    /// </summary>
    /// <example>
    /// For horizontal orientation, children are centered horizontally.
    /// For vertical orientation, children are centered vertically.
    /// </example>
    Center,

    /// <summary>
    /// Child elements are aligned to the end of the panel.
    /// </summary>
    /// <example>
    /// For horizontal orientation, children are aligned to the right.
    /// For vertical orientation, children are aligned to the bottom.
    /// </example>
    End
}