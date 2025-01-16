using System.Windows.Media;
using Material.Components.Theming.Colors;

namespace Material.Components.Theming;

/// <summary>
/// Represents the baseline theme of Material Design 3. It uses base colors and the Roboto font for typography,
/// providing a consistent and simple design language.
/// </summary>
/// <remarks>
/// This theme does not define medium or high contrast color schemes. When a medium or high contrast scheme
/// is requested, it defaults to the normal contrast scheme. This makes the baseline theme suitable for
/// applications with basic contrast requirements.
/// </remarks>
public class BaselineTheme() : ThemeDefinition("Baseline")
{
    private static readonly Lazy<ColorScheme> LazyLightColorScheme = new(CreateLightColorScheme, false);
    private static readonly Lazy<ColorScheme> LazyDarkColorScheme = new(CreateDarkColorScheme, false);

    /// <summary>
    /// Gets the light color scheme for the baseline theme.
    /// </summary>
    /// <remarks>
    /// The light color scheme is used when the application theme is set to light mode.
    /// </remarks>
    /// <returns>The <see cref="ColorScheme"/> representing the light mode colors.</returns>
    protected override ColorScheme GetLightColorScheme() => LazyLightColorScheme.Value;

    /// <summary>
    /// Gets the medium contrast light color scheme for the baseline theme.
    /// </summary>
    /// <remarks>
    /// Since the baseline theme does not define medium contrast schemes, this method falls back to the
    /// normal light color scheme.
    /// </remarks>
    /// <returns>The <see cref="ColorScheme"/> representing the light mode colors.</returns>
    protected override ColorScheme GetLightMediumColorScheme() => LazyLightColorScheme.Value;

    /// <summary>
    /// Gets the high contrast light color scheme for the baseline theme.
    /// </summary>
    /// <remarks>
    /// Since the baseline theme does not define high contrast schemes, this method falls back to the
    /// normal light color scheme.
    /// </remarks>
    /// <returns>The <see cref="ColorScheme"/> representing the light mode colors.</returns>
    protected override ColorScheme GetLightHighColorScheme() => LazyLightColorScheme.Value;

    /// <summary>
    /// Gets the dark color scheme for the baseline theme.
    /// </summary>
    /// <remarks>
    /// The dark color scheme is used when the application theme is set to dark mode.
    /// </remarks>
    /// <returns>The <see cref="ColorScheme"/> representing the dark mode colors.</returns>
    protected override ColorScheme GetDarkColorScheme() => LazyDarkColorScheme.Value;

    /// <summary>
    /// Gets the medium contrast dark color scheme for the baseline theme.
    /// </summary>
    /// <remarks>
    /// Since the baseline theme does not define medium contrast schemes, this method falls back to the
    /// normal dark color scheme.
    /// </remarks>
    /// <returns>The <see cref="ColorScheme"/> representing the dark mode colors.</returns>
    protected override ColorScheme GetDarkMediumColorScheme() => LazyDarkColorScheme.Value;

    /// <summary>
    /// Gets the high contrast dark color scheme for the baseline theme.
    /// </summary>
    /// <remarks>
    /// Since the baseline theme does not define high contrast schemes, this method falls back to the
    /// normal dark color scheme.
    /// </remarks>
    /// <returns>The <see cref="ColorScheme"/> representing the dark mode colors.</returns>
    protected override ColorScheme GetDarkHighColorScheme() => LazyDarkColorScheme.Value;

    private static ColorScheme CreateLightColorScheme() => new()
    {
        Primary = Color.FromRgb(101, 85, 143),
        OnPrimary = Color.FromRgb(255, 255, 255),
        Secondary = Color.FromRgb(98, 91, 113),
        OnSecondary = Color.FromRgb(255, 255, 255),
        Tertiary = Color.FromRgb(125, 82, 96),
        OnTertiary = Color.FromRgb(255, 255, 255),
        PrimaryFixed = Color.FromRgb(234, 221, 255),
        OnPrimaryFixed = Color.FromRgb(33, 0, 93),
        PrimaryFixedDim = Color.FromRgb(208, 188, 255),
        OnPrimaryFixedVariant = Color.FromRgb(79, 55, 139),
        SecondaryFixed = Color.FromRgb(232, 222, 248),
        OnSecondaryFixed = Color.FromRgb(29, 25, 43),
        SecondaryFixedDim = Color.FromRgb(204, 194, 220),
        OnSecondaryFixedVariant = Color.FromRgb(74, 68, 88),
        TertiaryFixed = Color.FromRgb(255, 216, 228),
        OnTertiaryFixed = Color.FromRgb(49, 17, 29),
        TertiaryFixedDim = Color.FromRgb(239, 184, 200),
        OnTertiaryFixedVariant = Color.FromRgb(49, 17, 29),
        PrimaryContainer = Color.FromRgb(234, 221, 255),
        OnPrimaryContainer = Color.FromRgb(79, 55, 138),
        SecondaryContainer = Color.FromRgb(232, 222, 248),
        OnSecondaryContainer = Color.FromRgb(74, 68, 89),
        TertiaryContainer = Color.FromRgb(255, 216, 228),
        OnTertiaryContainer = Color.FromRgb(99, 59, 72),
        Error = Color.FromRgb(179, 38, 30),
        OnError = Color.FromRgb(255, 255, 255),
        ErrorContainer = Color.FromRgb(249, 222, 220),
        OnErrorContainer = Color.FromRgb(133, 34, 33),
        Surface = Color.FromRgb(254, 247, 255),
        OnSurface = Color.FromRgb(29, 27, 32),
        OnSurfaceVariant = Color.FromRgb(73, 69, 79),
        Outline = Color.FromRgb(121, 116, 126),
        OutlineVariant = Color.FromRgb(202, 196, 208),
        InverseSurface = Color.FromRgb(50, 47, 53),
        InverseOnSurface = Color.FromRgb(245, 239, 247),
        InversePrimary = Color.FromRgb(208, 188, 255),
        SurfaceDim = Color.FromRgb(222, 216, 225),
        SurfaceBright = Color.FromRgb(254, 247, 255),
        SurfaceContainerLowest = Color.FromRgb(255, 255, 255),
        SurfaceContainerLow = Color.FromRgb(247, 242, 250),
        SurfaceContainer = Color.FromRgb(243, 237, 247),
        SurfaceContainerHigh = Color.FromRgb(236, 230, 240),
        SurfaceContainerHighest = Color.FromRgb(230, 224, 233),
        Scrim = Color.FromRgb(000, 000, 000)
    };

    private static ColorScheme CreateDarkColorScheme() => new()
    {
        Primary = Color.FromRgb(208, 188, 254),
        OnPrimary = Color.FromRgb(56, 30, 114),
        Secondary = Color.FromRgb(204, 194, 220),
        OnSecondary = Color.FromRgb(51, 45, 65),
        Tertiary = Color.FromRgb(239, 184, 200),
        OnTertiary = Color.FromRgb(73, 37, 50),
        PrimaryFixed = Color.FromRgb(234, 221, 255),
        OnPrimaryFixed = Color.FromRgb(33, 0, 93),
        PrimaryFixedDim = Color.FromRgb(208, 188, 255),
        OnPrimaryFixedVariant = Color.FromRgb(79, 55, 139),
        SecondaryFixed = Color.FromRgb(232, 222, 248),
        OnSecondaryFixed = Color.FromRgb(29, 25, 43),
        SecondaryFixedDim = Color.FromRgb(204, 194, 220),
        OnSecondaryFixedVariant = Color.FromRgb(74, 68, 88),
        TertiaryFixed = Color.FromRgb(255, 216, 228),
        OnTertiaryFixed = Color.FromRgb(49, 17, 29),
        TertiaryFixedDim = Color.FromRgb(239, 184, 200),
        OnTertiaryFixedVariant = Color.FromRgb(49, 17, 29),
        PrimaryContainer = Color.FromRgb(79, 55, 139),
        OnPrimaryContainer = Color.FromRgb(234, 221, 255),
        SecondaryContainer = Color.FromRgb(74, 68, 88),
        OnSecondaryContainer = Color.FromRgb(232, 222, 248),
        TertiaryContainer = Color.FromRgb(99, 59, 72),
        OnTertiaryContainer = Color.FromRgb(255, 216, 228),
        Error = Color.FromRgb(242, 184, 181),
        OnError = Color.FromRgb(96, 20, 16),
        ErrorContainer = Color.FromRgb(140, 29, 24),
        OnErrorContainer = Color.FromRgb(249, 222, 220),
        Surface = Color.FromRgb(20, 18, 24),
        OnSurface = Color.FromRgb(230, 224, 233),
        OnSurfaceVariant = Color.FromRgb(202, 196, 208),
        Outline = Color.FromRgb(147, 143, 153),
        OutlineVariant = Color.FromRgb(73, 69, 79),
        InverseSurface = Color.FromRgb(230, 224, 233),
        InverseOnSurface = Color.FromRgb(50, 47, 53),
        InversePrimary = Color.FromRgb(103, 80, 164),
        SurfaceDim = Color.FromRgb(20, 18, 24),
        SurfaceBright = Color.FromRgb(59, 56, 62),
        SurfaceContainerLowest = Color.FromRgb(15, 13, 19),
        SurfaceContainerLow = Color.FromRgb(29, 27, 32),
        SurfaceContainer = Color.FromRgb(33, 31, 38),
        SurfaceContainerHigh = Color.FromRgb(43, 41, 48),
        SurfaceContainerHighest = Color.FromRgb(54, 52, 59),
        Scrim = Color.FromRgb(000, 000, 000)
    };
}