using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a collection of action items displayed in a <see cref="NavigationRail"/>.
/// </summary>
/// <remarks>
/// This class extends the functionality of the standard <see cref="System.Windows.Controls.ItemsControl"/>,
/// representing a collection of action that are displayed in a navigation rail.
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