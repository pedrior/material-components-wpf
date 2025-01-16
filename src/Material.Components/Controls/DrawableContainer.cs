using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Represents an abstract container control that supports additional custom visual rendering 
/// on its <see cref="VisualLayer"/> object. The <see cref="VisualLayer"/> can be drawn in a specified order 
/// relative to the child content and can be used to render custom graphics or effects.
/// </summary>
/// <remarks>
/// The <see cref="DrawableContainer"/> allows composing UI elements with additional visuals, such as overlays,
/// decorations, or background graphics. The rendering order of the visual layer and child content can be configured
/// using the <see cref="Order"/> property.
/// </remarks>
public abstract class DrawableContainer : Decorator
{
    private readonly ContainerVisual visualLayer = new();

    private Size? lastRenderSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableContainer"/> class.
    /// </summary>
    protected DrawableContainer() => AddVisualChild(visualLayer);

    /// <summary>
    /// Gets the visual layer of the container used for custom rendering.
    /// </summary>
    /// <remarks>
    /// The visual layer can be customized by overriding the <see cref="OnVisualLayerRender"/> 
    /// method to define specific graphics or effects.
    /// </remarks>
    protected IContainerVisual VisualLayer => visualLayer;

    /// <summary>
    /// Gets the rendering order of the container's visual layer relative to its child content.
    /// </summary>
    /// <remarks>
    /// By default, the visual layer is rendered before the child content (<see cref="DrawableContainerOrder.VisualThenChild"/>). 
    /// Override this property to change the rendering order if necessary.
    /// </remarks>
    protected virtual DrawableContainerOrder Order => DrawableContainerOrder.VisualThenChild;

    /// <summary>
    /// Gets a value indicating whether the container's render size has changed since the last render.
    /// </summary>
    /// <remarks>
    /// This property can be used in derived classes to optimize rendering operations 
    /// by detecting when the size of the container has changed.
    /// </remarks>
    protected bool RenderSizeHasChanged => lastRenderSize.HasValue && lastRenderSize.Value != RenderSize;

    protected override int VisualChildrenCount => Child is null ? 1 : 2;

    protected override Visual GetVisualChild(int index)
    {
        if (Child is not null)
        {
            return Order switch
            {
                DrawableContainerOrder.VisualThenChild => index is 0 ? visualLayer : Child,
                DrawableContainerOrder.ChildThenVisual => index is 0 ? Child : visualLayer,
                _ => throw new ArgumentOutOfRangeException(nameof(Order))
            };
        }

        return index is 0
            ? visualLayer
            : throw new ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>
    /// Handles the rendering process by invoking the visual layer rendering logic.
    /// </summary>
    /// <param name="baseContext">The drawing context used to render custom graphics on the container.</param>
    /// <remarks>
    /// This method opens the visual layer's drawing context and calls <see cref="OnVisualLayerRender"/> 
    /// to perform custom drawing operations. It also updates the last render size.
    /// </remarks>
    protected override void OnRender(DrawingContext baseContext)
    {
        base.OnRender(baseContext);

        using var context = visualLayer.RenderOpen();

        OnVisualLayerRender(context);

        lastRenderSize = RenderSize;
    }

    /// <summary>
    /// When overridden in a derived class, provides custom rendering logic for the container's visual layer.
    /// </summary>
    /// <param name="context">The drawing context used to render custom graphics on the visual layer.</param>
    /// <remarks>
    /// Override this method to define how the container's visual layer should be drawn. 
    /// Use the provided <paramref name="context"/> to issue drawing commands.
    /// </remarks>
    protected virtual void OnVisualLayerRender(DrawingContext context)
    {
    }
}