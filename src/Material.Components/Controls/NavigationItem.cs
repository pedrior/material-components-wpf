using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a destination item base class for navigation controls.
/// </summary>
/// <remarks>
/// This class extends the functionality of the standard <see cref="System.Windows.Controls.RadioButton"/> 
/// to provide additional properties and behaviors.
/// </remarks>
public abstract class NavigationItem : RadioButton
{
    /// <summary>
    /// Initializes static members of the <see cref="NavigationItem"/> class.
    /// </summary>
    static NavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(typeof(NavigationItem)));
    }
}