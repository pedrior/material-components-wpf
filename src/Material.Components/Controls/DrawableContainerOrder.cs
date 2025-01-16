namespace Material.Components.Controls;

/// <summary>
/// Specifies the rendering order of a container's visual layer and its child content within the visual tree.
/// </summary>
/// <remarks>
/// The rendering order affects the visual stacking of elements. Changing the order can influence 
/// both the appearance and hit testing behavior of the container and its child.
/// </remarks>
public enum DrawableContainerOrder
{
    /// <summary>
    /// Renders the child content of the container before the container's visual layer. 
    /// </summary>
    /// <remarks>
    /// In this mode, the child content appears beneath the container's visual layer. 
    /// This is useful when the container's visual layer is intended to overlay the child content, 
    /// such as for decoration or overlay effects.
    /// </remarks>
    ChildThenVisual,

    /// <summary>
    /// Renders the visual layer of the container before the container's child content.
    /// </summary>
    /// <remarks>
    /// In this mode, the container's visual layer appears beneath the child content. 
    /// This is ideal when the visual layer serves as a background or base layer for the child content, 
    /// providing base visuals without obscuring the child content.
    /// </remarks>
    VisualThenChild
}
