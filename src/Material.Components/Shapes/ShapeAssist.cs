using System.Windows;

namespace Material.Components.Shapes;

/// <summary>
/// Provides attached properties to assist with shape configurations for UI components.
/// </summary>
public static class ShapeAssist
{
    /// <summary>
    /// Identifies the Family attached property.
    /// </summary>
    public static readonly DependencyProperty FamilyProperty = DependencyProperty.RegisterAttached(
        "Family",
        typeof(ShapeFamily),
        typeof(ShapeAssist),
        new FrameworkPropertyMetadata(ShapeFamily.Rounded, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the Style attached property.
    /// </summary>
    public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
        "Style",
        typeof(ShapeStyle),
        typeof(ShapeAssist),
        new FrameworkPropertyMetadata(ShapeStyle.None));

    /// <summary>
    /// Identifies the Direction attached property.
    /// </summary>
    public static readonly DependencyProperty DirectionProperty = DependencyProperty.RegisterAttached(
        "Direction",
        typeof(ShapeDirection),
        typeof(ShapeAssist),
        new FrameworkPropertyMetadata(ShapeDirection.Symmetric));

    /// <summary>
    /// Identifies the ShapeRadius attached property.
    /// </summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.RegisterAttached(
        "Radius",
        typeof(CornerRadius?),
        typeof(ShapeAssist),
        new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Sets the value of the <see cref="FamilyProperty"/> attached property for a specified dependency object.
    /// Changes the family of the shape, determining whether its corners are rounded or cut.
    /// </summary>
    /// <remarks>
    /// The value of this property is inherited by child elements. The default value is <see cref="ShapeFamily.Rounded"/>.
    /// </remarks>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FamilyProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFamily(DependencyObject element, ShapeFamily value) =>
        element.SetValue(FamilyProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FamilyProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FamilyProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FamilyProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ShapeFamily GetFamily(DependencyObject element) => (ShapeFamily)element.GetValue(FamilyProperty);

    /// <summary>
    /// Sets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// Changes the style of the shape, defining its corner radius according to predefined styles.
    /// </summary>
    /// <remarks>The default value is <see cref="ShapeStyle.None"/>.</remarks>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetStyle(DependencyObject element, ShapeStyle value) => element.SetValue(StyleProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="StyleProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ShapeStyle GetStyle(DependencyObject element) => (ShapeStyle)element.GetValue(StyleProperty);

    /// <summary>
    /// Sets the value of the <see cref="DirectionProperty"/> attached property for a specified dependency object.
    /// Changes the direction of the shape, determining which corners are assigned specific values.
    /// </summary>
    /// <remarks>The default value is <see cref="ShapeDirection.Symmetric"/>.</remarks>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="DirectionProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetDirection(DependencyObject element, ShapeDirection value) =>
        element.SetValue(DirectionProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="DirectionProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="DirectionProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="DirectionProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ShapeDirection GetDirection(DependencyObject element) =>
        (ShapeDirection)element.GetValue(DirectionProperty);

    /// <summary>
    /// Sets the value of the <see cref="RadiusProperty"/> attached property for a specified dependency object.
    /// Overrides the corner radius of the shape with a custom value, defining the roundness or cut size of its corners.
    /// </summary>
    /// <remarks>The default value is <see langword="null"/>.</remarks>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="RadiusProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetRadius(DependencyObject element, CornerRadius? value) =>
        element.SetValue(RadiusProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RadiusProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="RadiusProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="RadiusProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static CornerRadius? GetRadius(DependencyObject element) => (CornerRadius?)element.GetValue(RadiusProperty);
}