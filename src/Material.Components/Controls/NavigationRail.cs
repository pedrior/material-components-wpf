using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a navigation rail control that organizes and displays destination items and additional actions.
/// </summary>
public class NavigationRail : ItemsControl
{
    /// <summary>
    /// Identifies the <see cref="Alignment"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
        nameof(Alignment),
        typeof(NavigationRailAlignment),
        typeof(NavigationRail),
        new PropertyMetadata(NavigationRailAlignment.Top));

    /// <summary>
    /// Identifies the <see cref="TopActions"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TopActionsProperty = DependencyProperty.Register(
        nameof(TopActions),
        typeof(object),
        typeof(NavigationRail),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="BottomActions"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BottomActionsProperty = DependencyProperty.Register(
        nameof(BottomActions),
        typeof(object),
        typeof(NavigationRail),
        new PropertyMetadata(null));

    /// <summary>
    /// Initializes static members of the <see cref="NavigationRail"/> class.
    /// </summary>
    static NavigationRail()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRail),
            new FrameworkPropertyMetadata(typeof(NavigationRail)));
    }

    /// <summary>
    /// Gets or sets the alignment of destination items within the navigation rail.
    /// </summary>
    /// <value>
    /// A <see cref="NavigationRailAlignment"/> value that specifies the vertical alignment of destination items. 
    /// The default value is <see cref="NavigationRailAlignment.Top"/>.
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    public NavigationRailAlignment Alignment
    {
        get => (NavigationRailAlignment)GetValue(AlignmentProperty);
        set => SetValue(AlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the actions positioned at the top of the navigation rail.
    /// </summary>
    /// <value>
    /// An <see cref="object"/> representing the top actions of the navigation rail. 
    /// The default value is <see langword="null"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public object? TopActions
    {
        get => (object?)GetValue(TopActionsProperty);
        set => SetValue(TopActionsProperty, value);
    }

    /// <summary>
    /// Gets or sets the actions positioned at the bottom of the navigation rail.
    /// </summary>
    /// <value>
    /// An <see cref="object"/> representing the bottom actions of the navigation rail. 
    /// The default value is <see langword="null"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public object? BottomActions
    {
        get => (object?)GetValue(BottomActionsProperty);
        set => SetValue(BottomActionsProperty, value);
    }
}