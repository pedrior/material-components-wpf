using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a base class for destination items used within navigation control.
/// </summary>
/// <remarks>
/// The <see cref="NavigationItem"/> class is an abstract base class derived from <see cref="RadioButton"/> 
/// and provides additional features specific to navigation scenarios.
/// </remarks>
public abstract class NavigationItem : RadioButton
{
    /// <summary>
    /// Identifies the <see cref="FillIconOnChecked"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FillIconOnCheckedProperty = DependencyProperty.Register(
        nameof(FillIconOnChecked),
        typeof(bool),
        typeof(NavigationItem),
        new PropertyMetadata(true));
    
    /// <summary>
    /// Identifies the <see cref="FillIconOnPressed"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FillIconOnPressedProperty = DependencyProperty.Register(
        nameof(FillIconOnPressed),
        typeof(bool),
        typeof(NavigationItem),
        new PropertyMetadata(true));
    
    /// <summary>
    /// Initializes static members of the <see cref="NavigationItem"/> class.
    /// </summary>
    static NavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(typeof(NavigationItem)));
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the icon of the navigation item should be filled when checked.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the icon should be filled when the item is checked; otherwise, <see langword="false"/>. 
    /// The default value is <see langword="true"/>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool FillIconOnChecked
    {
        get => (bool)GetValue(FillIconOnCheckedProperty);
        set => SetValue(FillIconOnCheckedProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the icon of the navigation item should be filled when pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the icon should be filled when the item is pressed; otherwise, <see langword="false"/>. 
    /// The default value is <see langword="true"/>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool FillIconOnPressed
    {
        get => (bool)GetValue(FillIconOnPressedProperty);
        set => SetValue(FillIconOnPressedProperty, value);
    }
}