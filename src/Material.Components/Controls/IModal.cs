using System.Windows;

namespace Material.Components.Controls;

/// <summary>
/// Defines the contract for modal UI elements, providing common properties and actions.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="IModal"/> instance is created by calling the <see cref="ModalHost.PushModal"/> method,
/// which is responsible for managing the modal stack and displaying the modals according to their priority.
/// </para>
/// </remarks>
public interface IModal
{
    /// <summary>
    /// Gets the content to be displayed inside the modal.
    /// </summary>
    /// <value>A <see cref="UIElement"/> representing the modal content.</value>
    UIElement Content { get; }

    /// <summary>
    /// Gets a value indicating whether the modal is currently open.
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// Gets a value indicating whether the modal is effectively open and visible, considering animations.
    /// </summary>
    bool IsEffectiveOpen { get; }

    /// <summary>
    /// Gets the priority level of the modal, which determines stacking order and focus behavior.
    /// </summary>
    ModalPriority Priority { get; }

    /// <summary>
    /// Gets the placement mode of the modal within its host container.
    /// </summary>
    ModalPlacementMode Placement { get; }

    /// <summary>
    /// Gets the option flags applied to the modal, which customize its appearance and behavior.
    /// </summary>
    ModalOptions Options { get; }

    /// <summary>
    /// Gets the final placement rectangle of the modal relative to its host container.
    /// </summary>
    Rect PlacementRectangle { get; }

    /// <summary>
    /// Closes the modal.
    /// </summary>
    /// <remarks>
    /// Once closed, the modal is removed from the stack and the visual tree.
    /// </remarks>
    void Close();
}