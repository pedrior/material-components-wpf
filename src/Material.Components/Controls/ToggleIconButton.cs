using System.ComponentModel;
using System.Windows;

namespace Material.Components.Controls;

/// <summary>
/// Represents a toggle icon button control that adheres to the Material 3 guidelines.
/// </summary>
/// <remarks>
/// This class extends the functionality of the <see cref="ToggleButton"/> to provide
/// additional properties and behaviors.
/// </remarks>
public class ToggleIconButton : ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="Kind"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind),
        typeof(IconButtonKind),
        typeof(ToggleIconButton),
        new PropertyMetadata(IconButtonKind.Standard));

    /// <summary>
    /// Initialize static members of the <see cref="ToggleIconButton"/> class.
    /// </summary>
    static ToggleIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ToggleIconButton),
            new FrameworkPropertyMetadata(typeof(ToggleIconButton)));
    }

    /// <summary>
    /// Gets or sets the type of the toggle icon button, determining its appearance and level of emphasis.
    /// </summary>  
    /// <value>
    /// <para>A <see cref="IconButtonKind"/> value that determines the toggle icon button's appearance.</para>
    /// <para>The default value is <see cref="IconButtonKind.Standard"/>.</para>
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public IconButtonKind Kind
    {
        get => (IconButtonKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }
}