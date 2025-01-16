using System.ComponentModel;
using System.Windows;

namespace Material.Components.Controls;

/// <summary>
/// Represents a customizable button control that adheres to Material Design 3 guidelines.
/// </summary>
/// <remarks>
/// This class extends the functionality of the standard <see cref="System.Windows.Controls.Button"/> 
/// to provide additional properties and behaviors that align with design principles.
/// </remarks>
public class Button : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Kind"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind),
        typeof(ButtonKind),
        typeof(Button),
        new PropertyMetadata(ButtonKind.Filled));
    
    /// <summary>
    /// Initializes static members of the <see cref="Button"/> class.
    /// </summary>
    static Button()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Button),
            new FrameworkPropertyMetadata(typeof(Button)));
    }

    /// <summary>
    /// Gets or sets the type of the button, determining its appearance and level of emphasis.
    /// </summary>  
    /// <value>
    /// One of the <see cref="ButtonKind"/> enumeration values that determines the button's appearance.
    /// The default value is <see cref="ButtonKind.Filled"/>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public ButtonKind Kind
    {
        get => (ButtonKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }
}