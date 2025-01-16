using System.Windows;
using Material.Components.Controls;

namespace Material.Components.Elevations;

/// <summary>
/// Provides attached properties to assist with managing elevation levels for UI components.
/// </summary>
/// <remarks>
/// The <see cref="ElevationAssist"/> class allows applying elevation-related properties, such as level,
/// to any compatible <see cref="DependencyObject"/>. The <see cref="ElevationAssist"/> class allows applying
/// elevation-related properties, such as level, to any compatible <see cref="DependencyObject"/>. Although it
/// can be applied to any UI element, it is designed for use in conjunction with a <see cref="Container"/> element,
/// as the elevation is rendered by the container's underlying surface. If you want to apply elevation to any UI
/// element, you should use the elevations defined in <see cref="Elevations"/>.
/// </remarks>
public static class ElevationAssist
{
    /// <summary>
    /// Identifies the Level attached property.
    /// </summary>
    public static readonly DependencyProperty LevelProperty = DependencyProperty.RegisterAttached(
        "Level",
        typeof(ElevationLevel),
        typeof(ElevationAssist),
        new PropertyMetadata(ElevationLevel.None));

    /// <summary>
    /// Sets the value of the <see cref="LevelProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The <see cref="ElevationLevel"/> to apply to the element.</param>
    /// <remarks>
    /// Assigns a specific elevation level to a UI element, allowing it to display the appropriate shadow effect 
    /// and visual separation from other surfaces. 
    /// <para>The default value is <see cref="ElevationLevel.None"/>, meaning no elevation is applied.</para>
    /// </remarks>
    public static void SetLevel(DependencyObject element, ElevationLevel value) =>
        element.SetValue(LevelProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="LevelProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The <see cref="ElevationLevel"/> assigned to the element.</returns>
    /// <remarks>
    /// Retrieves the elevation level applied to a UI element.
    /// <para>The default value is <see cref="ElevationLevel.None"/>, meaning no elevation is applied.</para>
    /// </remarks>
    public static ElevationLevel GetLevel(DependencyObject element) => (ElevationLevel)element.GetValue(LevelProperty);
}