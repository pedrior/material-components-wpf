namespace Material.Components.Icons;

/// <summary>
/// Specifies the position of an icon within a control.
/// </summary>
/// <remarks>
/// The <see cref="IconPosition"/> enumeration is used to indicate whether an icon should appear 
/// at the leading (start) or trailing (end) position of a control, helping with visual layout
/// and alignment of controls with icons.
/// </remarks>
public enum IconPosition
{
    /// <summary>
    /// Indicates that the icon should be positioned at the leading (start) side of the control.
    /// </summary>
    /// <remarks>
    /// The "leading" position typically refers to the start of the control, 
    /// which may depend on the layout direction (e.g., left for left-to-right layouts 
    /// and right for right-to-left layouts).
    /// </remarks>
    Leading,

    /// <summary>
    /// Indicates that the icon should be positioned at the trailing (end) side of the control.
    /// </summary>
    /// <remarks>
    /// The "trailing" position typically refers to the end of the control, 
    /// which may depend on the layout direction (e.g., right for left-to-right layouts 
    /// and left for right-to-left layouts).
    /// </remarks>
    Trailing
}