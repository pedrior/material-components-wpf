using System.Windows;
using System.Windows.Media;
using Material.Components.Components;
using Material.Components.Controls;

namespace Material.Components.Icons;

/// <summary>
/// Provides attached properties for customizing icon appearance and behavior in UI elements.
/// </summary>
/// <remarks>
/// The <see cref="IconAssist"/> class allows applying icon-related settings, such as symbol, style, 
/// weight, fill, size, and position, to any compatible <see cref="DependencyObject"/>. 
/// This enables seamless integration of Material Symbols icons into various UI components.
/// </remarks>
public static class IconAssist
{
    /// <summary>
    /// Identifies the <c>Symbol</c> attached property.
    /// </summary>
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.RegisterAttached(
        "Symbol",
        typeof(Symbol),
        typeof(IconAssist),
        new FrameworkPropertyMetadata((Symbol)SymbolIcon.NotDefinedSymbol));

    /// <summary>
    /// Identifies the <c>Kind</c> attached property.
    /// </summary>
    public static readonly DependencyProperty KindProperty = DependencyProperty.RegisterAttached(
        "Kind",
        typeof(SymbolStyle),
        typeof(IconAssist),
        new FrameworkPropertyMetadata(SymbolStyle.Rounded));

    /// <summary>
    /// Identifies the <c>Fill</c> attached property.
    /// </summary>
    public static readonly DependencyProperty FillProperty = DependencyProperty.RegisterAttached(
        "Fill",
        typeof(bool),
        typeof(IconAssist),
        new FrameworkPropertyMetadata(false));

    /// <summary>
    /// Identifies the <c>Weight</c> attached property.
    /// </summary>
    public static readonly DependencyProperty WeightProperty = DependencyProperty.RegisterAttached(
        "Weight",
        typeof(SymbolWeight),
        typeof(IconAssist),
        new FrameworkPropertyMetadata(SymbolWeight.Regular));

    /// <summary>
    /// Identifies the <c>Size</c> attached property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached(
        "Size",
        typeof(double),
        typeof(IconAssist),
        new FrameworkPropertyMetadata(18.0));

    /// <summary>
    /// Identifies the <c>Position</c> attached property.
    /// </summary>
    public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached(
        "Position",
        typeof(IconPosition),
        typeof(IconAssist),
        new FrameworkPropertyMetadata(IconPosition.Leading));

    /// <summary>
    /// Identifies the <c>Foreground</c> attached property.
    /// </summary>
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
        "Foreground",
        typeof(Brush),
        typeof(IconAssist),
        new FrameworkPropertyMetadata(Brushes.Black));
    
    /// <summary>
    /// Sets the value of the <see cref="SymbolProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The <see cref="Symbol"/> to be displayed.</param>
    /// <remarks>
    /// Assigns a specific symbol to a UI element, enabling it to render a Material Symbol icon.
    /// <para>The default value is a <c>NotDefinedSymbol</c> symbol, an invalid symbol..</para>
    /// </remarks>
    public static void SetSymbol(DependencyObject element, Symbol value) => element.SetValue(SymbolProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SymbolProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The <see cref="Symbol"/> assigned to the element.</returns>
    /// <remarks>The default value is an undefined glyph.</remarks>
    public static Symbol GetSymbol(DependencyObject element) => (Symbol)element.GetValue(SymbolProperty);

    /// <summary>
    /// Sets the value of the <see cref="KindProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The <see cref="SymbolStyle"/> to define the symbol's visual style.</param>
    /// <remarks>
    /// Allows specifying the symbol's visual style, such as rounded, outlined, or sharp.
    /// <para>The default value is <see cref="SymbolStyle.Rounded"/>.</para>
    /// </remarks>
    public static void SetKind(DependencyObject element, SymbolStyle value) => element.SetValue(KindProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="KindProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The <see cref="SymbolStyle"/> assigned to the element.</returns>
    /// <remarks>The default value is <see cref="SymbolStyle.Rounded"/>.</remarks>
    public static SymbolStyle GetKind(DependencyObject element) => (SymbolStyle)element.GetValue(KindProperty);

    /// <summary>
    /// Sets the value of the <see cref="FillProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">A boolean value indicating whether the symbol should be filled.</param>
    /// <remarks>
    /// Enables switching between a filled or outlined version of the symbol.
    /// <para>The default value is <see langword="false"/> (outlined).</para>
    /// </remarks>
    public static void SetFill(DependencyObject element, bool value) => element.SetValue(FillProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FillProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns><see langword="true"/> if the symbol is filled; otherwise, <see langword="false"/>.</returns>
    /// <remarks>The default value is <see langword="false"/> (outlined).</remarks>
    public static bool GetFill(DependencyObject element) => (bool)element.GetValue(FillProperty);

    /// <summary>
    /// Sets the value of the <see cref="SizeProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The size of the symbol in device-independent pixels.</param>
    /// <remarks>
    /// Adjusts the size of the icon to match design requirements.
    /// <para>The default value is <c>18.0</c> pixels.</para>
    /// </remarks>
    public static void SetSize(DependencyObject element, double value) => element.SetValue(SizeProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SizeProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The size of the symbol in device-independent pixels.</returns>
    /// <remarks>The default value is <c>18.0</c> pixels.</remarks>
    public static double GetSize(DependencyObject element) => (double)element.GetValue(SizeProperty);

    /// <summary>
    /// Sets the value of the <see cref="WeightProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The <see cref="SymbolWeight"/> to define the symbol's weight.</param>
    /// <remarks>
    /// Allows specifying the symbol's weight, a from <see cref="SymbolWeight.Thin"/> to
    /// <see cref="SymbolWeight.Bold"/>.
    /// <para>The default value is <see cref="SymbolWeight.Regular"/>.</para>
    /// </remarks>
    public static void SetWeight(DependencyObject element, SymbolWeight value) =>
        element.SetValue(WeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="WeightProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The <see cref="SymbolWeight"/> assigned to the element.</returns>
    /// <remarks>The default value is <see cref="SymbolWeight.Regular"/>.</remarks>
    public static SymbolWeight GetWeight(DependencyObject element) => (SymbolWeight)element.GetValue(WeightProperty);

    /// <summary>
    /// Sets the value of the <see cref="PositionProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">The <see cref="IconPosition"/> to define the icon's position.</param>
    /// <remarks>
    /// Allows specifying the icon's position of the symbol within the UI element.
    /// <para>The default value is <see cref="IconPosition.Leading"/>.</para>
    /// </remarks>
    public static void SetPosition(DependencyObject element, IconPosition value) =>
        element.SetValue(PositionProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="PositionProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The <see cref="IconPosition"/> assigned to the element.</returns>
    /// <remarks>The default value is <see cref="IconPosition.Leading"/>.</remarks>
    public static IconPosition GetPosition(DependencyObject element) =>
        (IconPosition)element.GetValue(PositionProperty);
    
    /// <summary>
    /// Sets the value of the <see cref="ForegroundProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">A <see cref="Brush"/> value to define the icon's foreground color.</param>
    /// <remarks>
    /// Allows specifying the icon's foreground color.
    /// <para>The default value is <see cref="Brushes.Black"/>.</para>
    /// </remarks>
    public static void SetForeground(DependencyObject element, Brush? value) => 
        element.SetValue(ForegroundProperty, value);
    
    /// <summary>
    /// Gets the value of the <see cref="ForegroundProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <returns>The <see cref="Brush"/> assigned to the element.</returns>
    /// <remarks>The default value is <see cref="Brushes.Black"/>.</remarks>
    public static Brush? GetForeground(DependencyObject element) => (Brush?)element.GetValue(ForegroundProperty);
}