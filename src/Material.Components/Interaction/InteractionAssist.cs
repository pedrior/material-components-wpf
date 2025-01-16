using System.Windows;
using System.Windows.Media;
using Material.Components.Controls;

namespace Material.Components.Interaction;

/// <summary>
/// Provides attached properties to assist with interaction configurations for UI components.
/// </summary>
public static class InteractionAssist
{
    /// <summary>
    /// Identifies the FocusIndicatorBrush attached property.
    /// </summary>
    public static readonly DependencyProperty FocusIndicatorBrushProperty = DependencyProperty.RegisterAttached(
        "FocusIndicatorBrush",
        typeof(Brush),
        typeof(InteractionAssist),
        new FrameworkPropertyMetadata(FocusIndicator.BrushProperty.DefaultMetadata.DefaultValue));

    /// <summary>
    /// Identifies the FocusIndicatorPadding attached property.
    /// </summary>
    public static readonly DependencyProperty FocusIndicatorPaddingProperty = DependencyProperty.RegisterAttached(
        "FocusIndicatorPadding",
        typeof(double),
        typeof(InteractionAssist),
        new FrameworkPropertyMetadata(
            FocusIndicator.PaddingProperty.DefaultMetadata.DefaultValue,
            propertyChangedCallback: null,
            FocusIndicator.CoercePadding),
        FocusIndicator.ValidatePadding);

    /// <summary>
    /// Identifies the FocusIndicatorThickness attached property.
    /// </summary>
    public static readonly DependencyProperty FocusIndicatorThicknessProperty = DependencyProperty.RegisterAttached(
        "FocusIndicatorThickness",
        typeof(double),
        typeof(InteractionAssist),
        new FrameworkPropertyMetadata(
            FocusIndicator.ThicknessProperty.DefaultMetadata.DefaultValue,
            propertyChangedCallback: null,
            FocusIndicator.CoerceThickness),
        FocusIndicator.ValidateThickness);

    /// <summary>
    /// Identifies the FocusIndicatorDevice attached property.
    /// </summary>
    public static readonly DependencyProperty FocusIndicatorDeviceProperty = DependencyProperty.RegisterAttached(
        "FocusIndicatorDevice",
        typeof(FocusDevice),
        typeof(InteractionAssist),
        new FrameworkPropertyMetadata(FocusIndicator.DeviceProperty.DefaultMetadata.DefaultValue));

    /// <summary>
    /// Sets the value of the <see cref="FocusIndicatorBrushProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The <see cref="Brush"/> to be used for the focus indicator.</param>
    /// <remarks>
    /// Setting this property allows external controls or elements to define a custom brush for the focus indicator.
    /// This property can be used in conjunction with the <see cref="FocusIndicator"/> to customize focus visuals.
    /// <para>The default value is <see langword="Brushes.Black"/>.</para>
    /// </remarks>
    public static void SetFocusIndicatorBrush(DependencyObject element, Brush? value) =>
        element.SetValue(FocusIndicatorBrushProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FocusIndicatorBrushProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <see langword="Brushes.Black"/>.</remarks>
    /// <returns>The <see cref="Brush"/> used for the focus indicator.</returns>
    public static Brush? GetFocusIndicatorBrush(DependencyObject element) =>
        (Brush?)element.GetValue(FocusIndicatorBrushProperty);

    /// <summary>
    /// Sets the value of the <see cref="FocusIndicatorPaddingProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The padding value between the element and its focus indicator.</param>
    /// <remarks>
    /// Setting this property allows external controls or elements to specify the amount of space between the focus
    /// indicator and the element being focused. This can be used to fine-tune the visual appearance of the focus
    /// indicator.
    /// <para>The default value is <c>4.0</c>.</para>
    /// </remarks>
    public static void SetFocusIndicatorPadding(DependencyObject element, double value) =>
        element.SetValue(FocusIndicatorPaddingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FocusIndicatorPaddingProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <c>4.0</c>.</remarks>
    /// <returns>The padding value between the element and its focus indicator.</returns>
    public static double GetFocusIndicatorPadding(DependencyObject element) =>
        (double)element.GetValue(FocusIndicatorPaddingProperty);

    /// <summary>
    /// Sets the value of the <see cref="FocusIndicatorThicknessProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The thickness value for the focus indicator's outline.</param>
    /// <remarks>
    /// Setting this property allows external controls or elements to specify the thickness of the focus indicator.
    /// This property can be used to adjust the visual weight of the indicator based on design requirements.
    /// <para>The default value is <c>2.0</c>.</para>
    /// </remarks>
    public static void SetFocusIndicatorThickness(DependencyObject element, double value) =>
        element.SetValue(FocusIndicatorThicknessProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FocusIndicatorThicknessProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <c>2.0</c>.</remarks>
    /// <returns>The thickness value for the focus indicator's outline.</returns>
    public static double GetFocusIndicatorThickness(DependencyObject element) =>
        (double)element.GetValue(FocusIndicatorThicknessProperty);

    /// <summary>
    /// Sets the value of the <see cref="FocusIndicatorDeviceProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">
    /// A <see cref="FocusDevice"/> value indicating which input devices can trigger the focus indicator.
    /// </param>
    /// <remarks>
    /// Setting this property allows external controls or elements to define which input devices (e.g., keyboard,
    /// mouse, stylus) the focus indicator should respond to. This provides control over device-specific focus behavior.
    /// <para>The default value is <see cref="FocusDevice.Keyboard"/>.</para>
    /// </remarks>
    public static void SetFocusIndicatorDevice(DependencyObject element, FocusDevice value) =>
        element.SetValue(FocusIndicatorDeviceProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FocusIndicatorDeviceProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <see cref="FocusDevice.Keyboard"/>.</remarks>
    /// <returns>
    /// A <see cref="FocusDevice"/> value indicating which input devices can trigger the focus indicator.
    /// </returns>
    public static FocusDevice GetFocusIndicatorDevice(DependencyObject element) =>
        (FocusDevice)element.GetValue(FocusIndicatorDeviceProperty);
}