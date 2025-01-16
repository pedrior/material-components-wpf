namespace Material.Components.Controls;

/// <summary>
/// Specifies the type of input device to which the <see cref="FocusIndicator"/> responds.
/// </summary>
public enum FocusDevice
{
    /// <summary>
    /// Indicates that the focus indicator responds to input from a keyboard device.
    /// </summary>
    Keyboard,

    /// <summary>
    /// Indicates that the focus indicator responds to input from a mouse device.
    /// </summary>
    Mouse,

    /// <summary>
    /// Indicates that the focus indicator responds to input from any device.
    /// </summary>
    All
}
