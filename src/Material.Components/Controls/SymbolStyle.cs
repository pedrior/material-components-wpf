namespace Material.Components.Controls;

/// <summary>
/// Defines the style of a symbol, specifying its overall shape, edge characteristics, 
/// and how it interacts with other design elements.
/// </summary>
/// <remarks>
/// The <see cref="SymbolStyle"/> enum provides three distinct styles for symbols: Rounded, Outlined, and Sharp. 
/// Each style is designed to fit different design contexts and brand identities:
/// <list type="bullet">
///   <item><see cref="Rounded"/>: Suitable for brands with softer, curved elements.</item>
///   <item><see cref="Outlined"/>: Ideal for dense UIs with adjustable stroke weights.</item>
///   <item><see cref="Sharp"/>: A crisp, angular style that remains clear at small sizes.</item>
/// </list>
/// By selecting an appropriate style, symbols can enhance visual consistency and legibility in the UI.
/// </remarks>
public enum SymbolStyle
{
    /// <summary>
    /// Rounded style, featuring smooth, circular edges for a softer and approachable appearance.
    /// </summary>
    /// <remarks>
    /// Rounded symbols use a corner radius that pairs well with brands emphasizing heavier typography, 
    /// curved logos, or circular design elements. This style conveys a friendly and modern feel.
    /// </remarks>
    Rounded,

    /// <summary>
    /// Outlined style, combining stroke and fill attributes for a clean, minimal appearance.
    /// </summary>
    /// <remarks>
    /// Outlined symbols are designed for dense UIs and work well when a light and airy look is needed. 
    /// The stroke weight of outlined symbols can be adjusted to complement or contrast with typography, 
    /// making them highly adaptable to various design systems.
    /// </remarks>
    Outlined,

    /// <summary>
    /// Sharp style, characterized by straight edges and clean, angular corners.
    /// </summary>
    /// <remarks>
    /// Sharp symbols provide a crisp and bold style that remains highly legible, even at smaller scales. 
    /// This style is well-suited for rectangular and angular shapes that align with brand aesthetics 
    /// focused on precision and modernity.
    /// </remarks>
    Sharp
}