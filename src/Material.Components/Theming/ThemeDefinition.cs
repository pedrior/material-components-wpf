using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using Material.Components.Assets;
using Material.Components.Theming.Colors;

namespace Material.Components.Theming;

/// <summary>
/// Serves as the base class for defining application themes in a Material Design 3 context.
/// </summary>
/// <remarks>
/// This class provides foundational properties and methods to define and retrieve color schemes and font families
/// for a specific theme. Derived classes can override methods to provide specific implementations of color schemes
/// for different theme modes and contrast levels.
/// </remarks>
[DebuggerDisplay("{ToString(),nq}")]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class ThemeDefinition
{
    /// <summary>
    /// Identifies the Roboto font family, used as the default typography for Material Design 3.
    /// </summary>
    /// <remarks>
    /// The Roboto Flex font family is a flexible, variable font available from Google Fonts.
    /// For more details, visit 
    /// <see href="https://fonts.google.com/specimen/Roboto+Flex">Roboto Flex on Google Fonts</see>.
    /// </remarks>
    public static readonly FontFamily RobotoFontFamily = new(
        AssetsHelper.MakeUri("Fonts/RobotoFlex/"),
        "./#Roboto Flex");

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeDefinition"/> class.
    /// </summary>
    /// <param name="name">The name of the theme.</param>
    protected ThemeDefinition(string name) => Name = name;

    internal ThemeDefinition(string name, bool isUndefined) : this(name) => IsUndefined = isUndefined;

    /// <summary>
    /// Gets the name of the theme.
    /// </summary>
    /// <value>
    /// A string representing the name of the theme.
    /// </value>
    public string Name { get; }

    /// <summary>
    /// Gets the <see cref="FontFamily"/> used for large text, such as display text, titles, and headings.
    /// </summary>
    /// <value>
    /// The font family used for large text. Defaults to <see cref="RobotoFontFamily"/>.
    /// </value>
    public virtual FontFamily LargeTextFontFamily => RobotoFontFamily;

    /// <summary>
    /// Gets the <see cref="FontFamily"/> used for medium and small text, such as body text and labels.
    /// </summary>
    /// <value>
    /// The font family used for normal text. Defaults to <see cref="RobotoFontFamily"/>.
    /// </value>
    public virtual FontFamily NormalTextFontFamily => RobotoFontFamily;

    internal bool IsUndefined { get; }

    /// <summary>
    /// Gets the color scheme for a specified <see cref="ThemeMode"/>.
    /// </summary>
    /// <param name="themeMode">The theme mode for which to retrieve the color scheme.</param>
    /// <returns>The <see cref="ColorScheme"/> for the specified theme mode.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="themeMode"/> is <see cref="ThemeMode.Undefined"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="themeMode"/> is not a valid <see cref="ThemeMode"/>.
    /// </exception>
    public ColorScheme GetColorScheme(ThemeMode themeMode) => themeMode switch
    {
        ThemeMode.Light => GetLightColorScheme(),
        ThemeMode.LightMedium => GetLightMediumColorScheme(),
        ThemeMode.LightHigh => GetLightHighColorScheme(),
        ThemeMode.Dark => GetDarkColorScheme(),
        ThemeMode.DarkMedium => GetDarkMediumColorScheme(),
        ThemeMode.DarkHigh => GetDarkHighColorScheme(),
        ThemeMode.Undefined => throw new ArgumentException(
            $"The value '{ThemeMode.Undefined}' is not supported.",
            nameof(themeMode)),
        _ => throw new ArgumentOutOfRangeException(nameof(themeMode), themeMode, null)
    };

    /// <summary>
    /// Returns a string representation of the theme.
    /// </summary>
    public override string ToString() => $"{Name}{(IsUndefined ? " (undefined)" : string.Empty)}";

    /// <summary>
    /// Gets the color scheme for the light theme with normal contrast.
    /// </summary>
    /// <returns>The <see cref="ColorScheme"/> for the light theme with normal contrast.</returns>
    protected virtual ColorScheme GetLightColorScheme() => ColorScheme.Empty;

    /// <summary>
    /// Gets the color scheme for the light theme with medium contrast.
    /// </summary>
    /// <returns>The <see cref="ColorScheme"/> for the light theme with medium contrast.</returns>
    protected virtual ColorScheme GetLightMediumColorScheme() => ColorScheme.Empty;

    /// <summary>
    /// Gets the color scheme for the light theme with high contrast.
    /// </summary>
    /// <returns>The <see cref="ColorScheme"/> for the light theme with high contrast.</returns>
    protected virtual ColorScheme GetLightHighColorScheme() => ColorScheme.Empty;

    /// <summary>
    /// Gets the color scheme for the dark theme with normal contrast.
    /// </summary>
    /// <returns>The <see cref="ColorScheme"/> for the dark theme with normal contrast.</returns>
    protected virtual ColorScheme GetDarkColorScheme() => ColorScheme.Empty;

    /// <summary>
    /// Gets the color scheme for the dark theme with medium contrast.
    /// </summary>
    /// <returns>The <see cref="ColorScheme"/> for the dark theme with medium contrast.</returns>
    protected virtual ColorScheme GetDarkMediumColorScheme() => ColorScheme.Empty;

    /// <summary>
    /// Gets the color scheme for the dark theme with high contrast.
    /// </summary>
    /// <returns>The <see cref="ColorScheme"/> for the dark theme with high contrast.</returns>
    protected virtual ColorScheme GetDarkHighColorScheme() => ColorScheme.Empty;
}