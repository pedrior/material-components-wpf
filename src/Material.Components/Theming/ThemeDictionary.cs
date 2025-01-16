using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Material.Components.Theming;

/// <summary>
/// Represents a specialized <see cref="ResourceDictionary"/> for managing and applying application-wide theming
/// resources.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="ThemeDictionary"/> serves as the entry point for initializing the application's theme resources. 
/// It integrates with the <see cref="ThemeManager"/> to apply the selected <see cref="ThemeDefinition"/>
/// and <see cref="ThemeMode"/>. 
/// </para>
/// <para>
/// This class is designed to be instantiated only once during the application's lifecycle. Attempting to create
/// multiple instances will result in an exception to prevent resource conflicts.
/// </para>
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ThemeDictionary : ResourceDictionary
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// Upon creation, this instance automatically registers itself with the <see cref="ThemeManager"/>
    /// to initialize and manage theming resources throughout the application.
    /// </remarks>
    public ThemeDictionary() => Initialize();

    /// <summary>
    /// Gets or sets the application's current theme.
    /// </summary>
    /// <remarks>
    /// The theme defines the color schemes and typography styles applied to the application's UI.
    /// This property interacts with the <see cref="ThemeManager"/> to apply the selected <see cref="ThemeDefinition"/>.
    /// </remarks>
    /// <value>The active <see cref="ThemeDefinition"/> applied to the application.</value>
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public ThemeDefinition Theme
    {
        get => ThemeManager.Instance.Theme;
        set => ThemeManager.Instance.Theme = value;
    }

    /// <summary>
    /// Gets or sets the application's current theme mode (e.g., light or dark).
    /// </summary>
    /// <remarks>
    /// This property controls whether the application uses a light or dark theme and the contrast level.
    /// It may return <see cref="ThemeMode.Undefined"/> if no theme has been applied yet.
    /// </remarks>
    /// <value>The current <see cref="ThemeMode"/> applied to the application.</value>
    /// <exception cref="ArgumentException">
    /// Thrown when attempting to set the theme mode to <see cref="ThemeMode.Undefined"/>.
    /// </exception>
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public ThemeMode ThemeMode
    {
        get => ThemeManager.Instance.ThemeMode;
        set => ThemeManager.Instance.ThemeMode = value;
    }

    private void Initialize() => ThemeManager.Instance.Initialize(this);
}