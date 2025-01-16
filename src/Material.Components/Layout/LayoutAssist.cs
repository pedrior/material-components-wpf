using System.Windows;

namespace Material.Components.Layout;

/// <summary>
/// Provides attached properties to assist with layout configurations for UI components.
/// </summary>
public static class LayoutAssist
{
    /// <summary>
    /// Identifies the <see cref="SpacingProperty"/> attached property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.RegisterAttached(
        "Spacing",
        typeof(double),
        typeof(LayoutAssist),
        new FrameworkPropertyMetadata(0.0, propertyChangedCallback: null, CoerceSpacing));

    /// <summary>
    /// Sets the value of the <see cref="SpacingProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">A <see cref="double"/> value representing the spacing to be applied.</param>
    /// <remarks>
    /// The <see cref="SpacingProperty"/> allows you to define consistent spacing between elements in a layout. 
    /// The spacing value is constrained to non-negative values; any negative input is coerced to <c>0.0</c>.
    /// </remarks>
    public static void SetSpacing(DependencyObject element, double value) =>
        element.SetValue(SpacingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SpacingProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>
    /// A <see cref="double"/> value representing the spacing between elements. The default value is <c>0.0</c>.
    /// </returns>
    /// <remarks>
    /// Use the <see cref="SpacingProperty"/> to retrieve the spacing defined for a UI component. The value is always 
    /// non-negative due to coercion logic applied during assignment.
    /// </remarks>
    public static double GetSpacing(DependencyObject element) =>
        (double)element.GetValue(SpacingProperty);
    
    private static object CoerceSpacing(DependencyObject _, object value) => Math.Max(0.0, (double)value);
}
