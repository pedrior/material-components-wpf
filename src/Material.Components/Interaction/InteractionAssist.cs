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
    /// Identifies the RippleAnimate attached property.
    /// </summary>
    public static readonly DependencyProperty RippleAnimateProperty = DependencyProperty.RegisterAttached(
        "RippleAnimate",
        typeof(bool),
        typeof(InteractionAssist),
        new PropertyMetadata(Ripple.AnimateProperty.DefaultMetadata.DefaultValue));

    /// <summary>
    /// Identifies the RippleTint attached property.
    /// </summary>
    public static readonly DependencyProperty RippleTintProperty = DependencyProperty.RegisterAttached(
        "RippleTint",
        typeof(Brush),
        typeof(InteractionAssist),
        new PropertyMetadata(Ripple.TintProperty.DefaultMetadata.DefaultValue));

    /// <summary>
    /// Identifies the RippleIsCentered attached property.
    /// </summary>
    public static readonly DependencyProperty RippleIsCenteredProperty = DependencyProperty.RegisterAttached(
        "RippleIsCentered",
        typeof(bool),
        typeof(InteractionAssist),
        new PropertyMetadata(Ripple.IsCenteredProperty.DefaultMetadata.DefaultValue));

    /// <summary>
    /// Identifies the RippleIsUnbounded attached property.
    /// </summary>
    public static readonly DependencyProperty RippleIsUnboundedProperty = DependencyProperty.RegisterAttached(
        "RippleIsUnbounded",
        typeof(bool),
        typeof(InteractionAssist),
        new PropertyMetadata(Ripple.IsUnboundedProperty.DefaultMetadata.DefaultValue));

    /// <summary>
    /// Identifies the RippleEnableRightClick attached property.
    /// </summary>
    public static readonly DependencyProperty RippleEnableRightClickProperty =
        DependencyProperty.RegisterAttached(
            "RippleEnableRightClick",
            typeof(bool),
            typeof(InteractionAssist),
            new PropertyMetadata(Ripple.EnableRightClickProperty.DefaultMetadata.DefaultValue));
   
    /// <summary>
    /// Sets the value of the <see cref="RippleAnimateProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">A <see cref="bool"/> value indicating whether the ripple should animate.</param>
    /// <remarks>
    /// Setting this property allows external controls or elements to enable or disable the ripple animation effect.
    /// <para>The default value is <see langword="true"/>.</para>
    /// </remarks>
    public static void SetRippleAnimate(DependencyObject element, bool value) =>
        element.SetValue(RippleAnimateProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RippleAnimateProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    /// <returns>A <see cref="bool"/> value indicating whether the ripple should animate.</returns>
    public static bool GetRippleAnimate(DependencyObject element) => (bool)element.GetValue(RippleAnimateProperty);

    /// <summary>
    /// Sets the value of the <see cref="RippleTintProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">A <see cref="Brush"/> that defines the tint color for the ripple effect.</param>
    /// <remarks>
    /// Setting this property allows external controls or elements to define a tint brush for the ripple effect.
    /// The default value is <see langword="null"/>.
    /// </remarks>
    public static void SetRippleTint(DependencyObject element, Brush? value) =>
        element.SetValue(RippleTintProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RippleTintProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <see langword="null"/>.</remarks>
    /// <returns>A <see cref="Brush"/> that defines the tint color for the ripple effect.</returns>
    public static Brush? GetRippleTint(DependencyObject element) => (Brush?)element.GetValue(RippleTintProperty);

    /// <summary>
    /// Sets the value of the <see cref="RippleIsCenteredProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">
    /// A <see cref="bool"/> value indicating whether the ripple effect should always originate from the center 
    /// of the element.
    /// </param>
    /// <remarks>
    /// When this property is set to <see langword="true"/>, the ripple effect will always start from the center 
    /// of the element, regardless of the interaction point. The default value is <see langword="false"/>.
    /// </remarks>
    public static void SetRippleIsCentered(DependencyObject element, bool value) =>
        element.SetValue(RippleIsCenteredProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RippleIsCenteredProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>
    /// A <see cref="bool"/> value indicating whether the ripple effect starts from the center of the element.
    /// </returns>
    /// <remarks>
    /// The default value is <see langword="false"/>.
    /// </remarks>
    public static bool GetRippleIsCentered(DependencyObject element) =>
        (bool)element.GetValue(RippleIsCenteredProperty);

    /// <summary>
    /// Sets the value of the <see cref="RippleIsUnboundedProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">
    /// A <see cref="bool"/> value indicating whether the ripple effect is unbounded, 
    /// allowing it to extend beyond the bounds of the element.
    /// </param>
    /// <remarks>
    /// When this property is set to <see langword="true"/>, the ripple effect will not be clipped to the bounds of 
    /// the <see cref="Ripple.DefiningGeometry"/>. To constrain the ripple within the bounds, set this property to
    /// <see langword="false"/>. 
    /// The default value is <see langword="false"/>.
    /// </remarks>
    public static void SetRippleIsUnbounded(DependencyObject element, bool value) =>
        element.SetValue(RippleIsUnboundedProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RippleIsUnboundedProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>
    /// A <see cref="bool"/> value indicating whether the ripple effect is unbounded.
    /// </returns>
    /// <remarks>
    /// The default value is <see langword="false"/>.
    /// </remarks>
    public static bool GetRippleIsUnbounded(DependencyObject element) =>
        (bool)element.GetValue(RippleIsUnboundedProperty);

    /// <summary>
    /// Sets the value of the <see cref="RippleEnableRightClickProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">
    /// A <see cref="bool"/> value indicating whether right-click interactions should trigger the ripple effect.
    /// </param>
    /// <remarks>
    /// When this property is set to <see langword="true"/>, the ripple effect will be triggered by right-click events. 
    /// The default value is <see langword="false"/>.
    /// </remarks>
    public static void SetRippleEnableRightClick(DependencyObject element, bool value) =>
        element.SetValue(RippleEnableRightClickProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RippleEnableRightClickProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>
    /// A <see cref="bool"/> value indicating whether right-click interactions trigger the ripple effect.
    /// </returns>
    /// <remarks>
    /// The default value is <see langword="false"/>.
    /// </remarks>
    public static bool GetRippleEnableRightClick(DependencyObject element) =>
        (bool)element.GetValue(RippleEnableRightClickProperty);
}