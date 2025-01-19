using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a collection of action items displayed in a <see cref="NavigationRail"/>.
/// </summary>
/// <remarks>
/// The <see cref="NavigationRailActions"/> class is an <see cref="ItemsControl"/> designed 
/// to host a collection of action items, such as buttons or other interactive elements, 
/// that are displayed in a navigation rail. These actions can be positioned at the top or 
/// bottom of the navigation rail based on the layout configuration.
/// </remarks>
public class NavigationRailActions : ItemsControl
{
    /// <summary>
    /// Initializes static members of the <see cref="NavigationRailActions"/> class.
    /// </summary>
    static NavigationRailActions()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRailActions),
            new FrameworkPropertyMetadata(typeof(NavigationRailActions)));
    }
}