namespace Material.Components.Controls;

/// <summary>
/// Specifies the alignment of child elements within a panel.
/// </summary>
/// <remarks>
/// The <see cref="Alignment"/> enumeration is used to define how child elements 
/// are aligned within a parent panel. It provides options for aligning 
/// elements at the start, middle, or end of the available space.
/// </remarks>
public enum Alignment
{
    /// <summary>
    /// Aligns child elements to the start of the panel.
    /// </summary>
    /// <remarks>
    /// For horizontal alignment, this corresponds to aligning elements to the left.
    /// For vertical alignment, this corresponds to aligning elements to the top.
    /// </remarks>
    Start,

    /// <summary>
    /// Aligns child elements to the middle of the panel.
    /// </summary>
    /// <remarks>
    /// The middle alignment centers child elements within the available space, 
    /// both horizontally and vertically, depending on the layout direction.
    /// </remarks>
    Middle,

    /// <summary>
    /// Aligns child elements to the end of the panel.
    /// </summary>
    /// <remarks>
    /// For horizontal alignment, this corresponds to aligning elements to the right.
    /// For vertical alignment, this corresponds to aligning elements to the bottom.
    /// </remarks>
    End
}