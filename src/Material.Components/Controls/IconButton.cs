using System.ComponentModel;
using System.Windows;

namespace Material.Components.Controls;

/// <summary>
/// Represents an icon button control that adheres to the Material 3 guidelines.
/// </summary>
/// <remarks>
/// This class extends the functionality of the standard <see cref="System.Windows.Controls.Button"/> 
/// to provide additional properties and behaviors.
/// </remarks>
public class IconButton : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Kind"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind),
        typeof(IconButtonKind),
        typeof(IconButton),
        new PropertyMetadata(IconButtonKind.Standard));

    /// <summary>
    /// Initialize static members of the <see cref="IconButton"/> class.
    /// </summary>
    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(IconButton),
            new FrameworkPropertyMetadata(typeof(IconButton)));
    }

    /// <summary>
    /// Gets or sets the type of the icon button, determining its appearance and level of emphasis.
    /// </summary>  
    /// <value>
    /// <para>A <see cref="IconButtonKind"/> value that determines the icon button's appearance.</para>
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