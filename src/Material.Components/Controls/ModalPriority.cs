namespace Material.Components.Controls;

/// <summary>
/// Specifies the priority levels of a modal, which determine its stacking order and focus behavior within the host.
/// </summary>
/// <remarks>
/// A higher priority modal will always be placed above lower priority modals in the visual stacking order. 
/// When multiple modals with the same priority are added to the host, precedence is given to the 
/// last pushed modal. Additionally, the keyboard focus is always moved to the current topmost modal in the host.
/// </remarks>
public enum ModalPriority
{
    /// <summary>
    /// Indicates a modal with normal priority, which is suitable for standard user interactions.
    /// </summary>
    Normal = 1,

    /// <summary>
    /// Indicates a modal with high priority, which takes precedence over normal priority modals.
    /// </summary>
    High = 2,

    /// <summary>
    /// Indicates a modal with the highest priority, ensuring it appears above all other modals.
    /// </summary>
    Highest = 3
}
