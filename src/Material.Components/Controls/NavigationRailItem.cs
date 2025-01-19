using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Represents a destination item used within a <see cref="NavigationRail"/>.
/// </summary>
/// <remarks>
/// The <see cref="NavigationRailItem"/> class derives from <see cref="NavigationItem"/> and provides additional
/// features specific to <see cref="NavigationRail"/>'s destination items.
/// </remarks>
public class NavigationRailItem : NavigationItem
{
    /// <summary>
    /// Identifies the <see cref="InnerBackground"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InnerBackgroundProperty = DependencyProperty.Register(
        nameof(InnerBackground),
        typeof(Brush),
        typeof(NavigationRailItem),
        new PropertyMetadata(null));
    
    /// <summary>
    /// Initializes static members of the <see cref="NavigationRailItem"/> class.
    /// </summary>
    static NavigationRailItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRailItem),
            new FrameworkPropertyMetadata(typeof(NavigationRailItem)));
    }
    
    /// <summary>
    /// Gets or sets the brush used to paint the inner background of the navigation item.
    /// </summary>
    /// <value>
    /// A <see cref="Brush"/> that specifies the background color of the inner container. 
    /// The default value is <see langword="null"/>.
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? InnerBackground
    {
        get => (Brush?)GetValue(InnerBackgroundProperty);
        set => SetValue(InnerBackgroundProperty, value);
    }
}