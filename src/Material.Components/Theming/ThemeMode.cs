using System.ComponentModel;

namespace Material.Components.Theming;

/// <summary>
/// Specifies the theme mode of the application, defining whether the theme is light or dark,
/// as well as the level of contrast applied.
/// </summary>
/// <remarks>
/// This enumeration allows fine-grained control over the application's appearance by specifying
/// not only the base theme (light or dark) but also the contrast level for enhanced accessibility
/// or visual emphasis.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public enum ThemeMode
{
    /// <summary>
    /// Indicates that the application should use the light theme with a normal contrast tone.
    /// Suitable for general usage with balanced brightness and contrast levels.
    /// </summary>
    Light,

    /// <summary>
    /// Indicates that the application should use the light theme with a medium contrast tone.
    /// Provides slightly increased contrast for better readability compared to the normal tone.
    /// </summary>
    LightMedium,

    /// <summary>
    /// Indicates that the application should use the light theme with a high contrast tone.
    /// Ideal for users with visual impairments or those needing a stronger visual distinction.
    /// </summary>
    LightHigh,

    /// <summary>
    /// Indicates that the application should use the dark theme with a normal contrast tone.
    /// A dark background with balanced contrast for a comfortable viewing experience.
    /// </summary>
    Dark,

    /// <summary>
    /// Indicates that the application should use the dark theme with a medium contrast tone.
    /// Enhances the visibility of elements by increasing contrast relative to the normal tone.
    /// </summary>
    DarkMedium,

    /// <summary>
    /// Indicates that the application should use the dark theme with a high contrast tone.
    /// Optimized for users needing maximum visual differentiation and accessibility.
    /// </summary>
    DarkHigh,

    /// <summary>
    /// Indicates that the theme mode is undefined.
    /// This value is used as a default for the <see cref="ThemeDictionary.ThemeMode"/> type and
    /// should not be applied directly in the application's runtime logic.
    /// </summary>
    /// <remarks>
    /// The <c>Undefined</c> value is a placeholder and does not represent a valid theme mode.
    /// It is hidden from common usage and design-time serialization.
    /// </remarks>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    Undefined
}