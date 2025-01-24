using System.Windows;

namespace Material.Components.Controls;

/// <summary>
/// Represents a method that calculates the placement of a modal within its host container.
/// </summary>
/// <param name="constraint">The available size of the host container.</param>
/// <param name="size">The desired size of the modal.</param>
/// <returns>
/// A <see cref="Rect"/> representing the final position and dimensions of the modal within the host.
/// </returns>
/// <remarks>
/// This delegate allows for dynamic positioning strategies based on the host's available space 
/// and the modal's content size.
/// </remarks>
public delegate Rect ModalPlacementCallback(Size constraint, Size size);
