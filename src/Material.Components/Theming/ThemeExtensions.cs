namespace Material.Components.Theming;

/// <summary>
/// Provides extensions methods related to themes.
/// </summary>
public static class ThemeExtensions
{
    /// <summary>
    /// Determines whether the specified <see cref="ThemeMode"/> is a light theme mode, regardless of contrast level.
    /// </summary>
    /// <param name="themeMode">The <see cref="ThemeMode"/> to evaluate.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="themeMode"/> is <see cref="ThemeMode.Light"/>, 
    /// <see cref="ThemeMode.LightMedium"/>, or <see cref="ThemeMode.LightHigh"/>; otherwise, <see langword="false"/>.
    /// </returns>
    /// <example>
    /// <code>
    /// if (themeMode.IsLightThemeMode())
    /// {
    ///     // Apply light theme-specific UI adjustments
    /// }
    /// </code>
    /// </example>
    public static bool IsLightThemeMode(this ThemeMode themeMode) =>
        themeMode is ThemeMode.Light or ThemeMode.LightMedium or ThemeMode.LightHigh;

    /// <summary>s
    /// Determines whether the specified <see cref="ThemeMode"/> is a dark theme mode, regardless of contrast level.
    /// </summary>
    /// <param name="themeMode">The <see cref="ThemeMode"/> to evaluate.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="themeMode"/> is <see cref="ThemeMode.Dark"/>, 
    /// <see cref="ThemeMode.DarkMedium"/>, or <see cref="ThemeMode.DarkHigh"/>; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Internally, this method is the inverse of <see cref="IsLightThemeMode(ThemeMode)"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// if (themeMode.IsDarkThemeMode())
    /// {
    ///     // Apply dark theme-specific UI adjustments
    /// }
    /// </code>
    /// </example>
    public static bool IsDarkThemeMode(this ThemeMode themeMode) => !IsLightThemeMode(themeMode);
}