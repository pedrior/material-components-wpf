using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Material.Components.Controls;

/// <summary>
/// Defines a contract for visual elements that can be composed within a <see cref="DrawableContainer"/>. 
/// This interface abstracts key rendering properties such as opacity, visual effects, transformations, 
/// and hit testing to allow for custom rendering and interaction behavior.
/// </summary>
public interface IContainerVisual
{
    /// <summary>
    /// Gets or sets the opacity level of the visual element.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value between 0.0 (completely transparent) and 1.0 (fully opaque). 
    /// The default value is 1.0.
    /// </value>
    /// <remarks>
    /// Adjusting this property allows the visual element to appear partially transparent, 
    /// enabling layering and blending effects.
    /// </remarks>
    double Opacity { get; set; }

    /// <summary>
    /// Gets or sets the bitmap effect applied to the visual element.
    /// </summary>
    /// <value>
    /// An <see cref="Effect"/> object that defines visual effects such as blur, shadow, or glow. 
    /// A null value indicates that no effect is applied.
    /// </value>
    /// <remarks>
    /// Use this property to enhance the visual appearance of the element with custom effects.
    /// </remarks>
    Effect? Effect { get; set; }

    /// <summary>
    /// Gets or sets the transformation applied to the visual element.
    /// </summary>
    /// <value>
    /// A <see cref="Transform"/> object that defines transformations such as scaling, rotation, 
    /// or translation. A null value indicates no transformation is applied.
    /// </value>
    /// <remarks>
    /// Transformations can be used to modify the position, size, or orientation of the visual 
    /// without altering its original structure.
    /// </remarks>
    Transform? Transform { get; set; }
    
    /// <summary>
    /// Gets or sets the geometry used to clip the visual element.
    /// </summary>
    /// <value>
    /// A <see cref="Geometry"/> object that defines the clipping region for the visual element.
    /// </value>
    /// <remarks>
    /// Clipping restricts the visible area of the visual element to the specified geometry,
    /// effectively masking out any content outside the clipping region.
    /// </remarks>
    Geometry? Clip { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the visual element can respond to hit testing.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the element can participate in hit testing; otherwise, <see langword="false"/>. 
    /// The default value is <see langword="true"/>.
    /// </value>
    /// <remarks>
    /// When set to <see langword="false"/>, the visual will not respond to mouse or touch input, effectively 
    /// making it invisible to interaction while remaining visible on screen.
    /// </remarks>
    bool IsHitTestVisible { get; set; }
}
