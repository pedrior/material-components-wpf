using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;

namespace Material.Components.Theming.Colors;

/// <summary>
/// Represents a Material Design 3 color scheme, which defines a comprehensive set of colors and their associated
/// contrasts for styling an application's UI. This class encapsulates the primary, secondary, and tertiary colors,
/// as well as various container, surface, and contrast colors that adhere to Material Design 3 guidelines.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ColorScheme
{
    /// <summary>
    /// Identifies an empty <see cref="ColorScheme"/> instance.
    /// </summary>
    /// <remarks>
    /// The <c>Empty</c> color scheme is used as a placeholder and indicates the absence
    /// of defined colors. It cannot be modified.
    /// </remarks>
    public static readonly ColorScheme Empty = new(true);

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorScheme"/> struct.
    /// </summary>
    public ColorScheme()
    {
    }

    private ColorScheme(bool isEmpty) => IsEmpty = isEmpty;

    /// <summary>
    /// Gets the primary color of the scheme, typically used for prominent UI elements.
    /// </summary>
    public Color Primary { get; init; }

    /// <summary>
    /// Gets the contrasting color for the primary color, often used for text or icons.
    /// </summary>
    public Color OnPrimary { get; init; }

    /// <summary>
    /// Gets the secondary color of the scheme, used for less prominent UI elements.
    /// </summary>
    public Color Secondary { get; init; }

    /// <summary>
    /// Gets the contrasting color for the secondary color, often used for text or icons.
    /// </summary>
    public Color OnSecondary { get; init; }

    /// <summary>
    /// Gets the tertiary color of the scheme, used for accents or supplementary UI elements.
    /// </summary>
    public Color Tertiary { get; init; }

    /// <summary>
    /// Gets the contrasting color for the tertiary color, often used for text or icons.
    /// </summary>
    public Color OnTertiary { get; init; }

    /// <summary>
    /// Gets the fixed primary color of the scheme, which remains consistent across states.
    /// </summary>
    public Color PrimaryFixed { get; init; }

    /// <summary>
    /// Gets the contrasting color for the fixed primary color.
    /// </summary>
    public Color OnPrimaryFixed { get; init; }

    /// <summary>
    /// Gets the dimmed variant of the fixed primary color.
    /// </summary>
    public Color PrimaryFixedDim { get; init; }

    /// <summary>
    /// Gets the contrasting variant for the dimmed fixed primary color.
    /// </summary>
    public Color OnPrimaryFixedVariant { get; init; }

    /// <summary>
    /// Gets the fixed secondary color of the scheme, which remains consistent across states.
    /// </summary>
    public Color SecondaryFixed { get; init; }

    /// <summary>
    /// Gets the contrasting color for the fixed secondary color.
    /// </summary>
    public Color OnSecondaryFixed { get; init; }

    /// <summary>
    /// Gets the dimmed variant of the fixed secondary color.
    /// </summary>
    public Color SecondaryFixedDim { get; init; }

    /// <summary>
    /// Gets the contrasting variant for the dimmed fixed secondary color.
    /// </summary>
    public Color OnSecondaryFixedVariant { get; init; }

    /// <summary>
    /// Gets the fixed tertiary color of the scheme, which remains consistent across states.
    /// </summary>
    public Color TertiaryFixed { get; init; }

    /// <summary>
    /// Gets the contrasting color for the fixed tertiary color.
    /// </summary>
    public Color OnTertiaryFixed { get; init; }

    /// <summary>
    /// Gets the dimmed variant of the fixed tertiary color.
    /// </summary>
    public Color TertiaryFixedDim { get; init; }

    /// <summary>
    /// Gets the contrasting variant for the dimmed fixed tertiary color.
    /// </summary>
    public Color OnTertiaryFixedVariant { get; init; }

    /// <summary>
    /// Gets the primary container color, typically used for backgrounds.
    /// </summary>
    public Color PrimaryContainer { get; init; }

    /// <summary>
    /// Gets the contrasting color for the primary container.
    /// </summary>
    public Color OnPrimaryContainer { get; init; }

    /// <summary>
    /// Gets the secondary container color, typically used for secondary backgrounds.
    /// </summary>
    public Color SecondaryContainer { get; init; }

    /// <summary>
    /// Gets the contrasting color for the secondary container.
    /// </summary>
    public Color OnSecondaryContainer { get; init; }

    /// <summary>
    /// Gets the tertiary container color, used for supplementary backgrounds.
    /// </summary>
    public Color TertiaryContainer { get; init; }

    /// <summary>
    /// Gets the contrasting color for the tertiary container.
    /// </summary>
    public Color OnTertiaryContainer { get; init; }

    /// <summary>
    /// Gets the error color, typically used to indicate errors or critical states.
    /// </summary>
    public Color Error { get; init; }

    /// <summary>
    /// Gets the contrasting color for the error color.
    /// </summary>
    public Color OnError { get; init; }

    /// <summary>
    /// Gets the color used for error containers.
    /// </summary>
    public Color ErrorContainer { get; init; }

    /// <summary>
    /// Gets the contrasting color for error containers.
    /// </summary>
    public Color OnErrorContainer { get; init; }

    /// <summary>
    /// Gets the surface color, used as the base background for surfaces.
    /// </summary>
    public Color Surface { get; init; }

    /// <summary>
    /// Gets the contrasting color for the surface.
    /// </summary>
    public Color OnSurface { get; init; }

    /// <summary>
    /// Gets the variant color that contrasts with the surface.
    /// </summary>
    public Color OnSurfaceVariant { get; init; }

    /// <summary>
    /// Gets the outline color, used for elements such as dividers or outlines.
    /// </summary>
    public Color Outline { get; init; }

    /// <summary>
    /// Gets the contrasting variant for the outline color.
    /// </summary>
    public Color OutlineVariant { get; init; }

    /// <summary>
    /// Gets the inverse surface color, used for overlays or emphasis.
    /// </summary>
    public Color InverseSurface { get; init; }

    /// <summary>
    /// Gets the contrasting color for the inverse surface.
    /// </summary>
    public Color InverseOnSurface { get; init; }

    /// <summary>
    /// Gets the inverse primary color, used for contrasting UI elements.
    /// </summary>
    public Color InversePrimary { get; init; }

    /// <summary>
    /// Gets the dimmed surface color, used for subtle backgrounds.
    /// </summary>
    public Color SurfaceDim { get; init; }

    /// <summary>
    /// Gets the bright surface color.
    /// </summary>
    public Color SurfaceBright { get; init; }

    /// <summary>
    /// Gets the lowest surface container color.
    /// </summary>
    public Color SurfaceContainerLowest { get; init; }

    /// <summary>
    /// Gets the low surface container color.
    /// </summary>
    public Color SurfaceContainerLow { get; init; }

    /// <summary>
    /// Gets the standard container color.
    /// </summary>
    public Color SurfaceContainer { get; init; }

    /// <summary>
    /// Gets the high surface container color.
    /// </summary>
    public Color SurfaceContainerHigh { get; init; }

    /// <summary>
    /// Gets the highest surface container color.
    /// </summary>
    public Color SurfaceContainerHighest { get; init; }

    /// <summary>
    /// Gets the scrim color, used for overlays or dialogs.
    /// </summary>
    public Color Scrim { get; init; }

    internal bool IsEmpty { get; }

    /// <summary>
    /// Returns a string representation of the color scheme.
    /// </summary>
    public override string ToString() => IsEmpty ? "(Empty)" : $"{Primary}, {Secondary}, {Tertiary}...";

    internal NamedColor[] ListColors()
    {
        if (IsEmpty)
        {
            return [];
        }

        return
        [
            new(nameof(Primary), Primary),
            new(nameof(OnPrimary), OnPrimary),
            new(nameof(Secondary), Secondary),
            new(nameof(OnSecondary), OnSecondary),
            new(nameof(Tertiary), Tertiary),
            new(nameof(OnTertiary), OnTertiary),
            new(nameof(PrimaryFixed), PrimaryFixed),
            new(nameof(OnPrimaryFixed), OnPrimaryFixed),
            new(nameof(PrimaryFixedDim), PrimaryFixedDim),
            new(nameof(OnPrimaryFixedVariant), OnPrimaryFixedVariant),
            new(nameof(SecondaryFixed), SecondaryFixed),
            new(nameof(OnSecondaryFixed), OnSecondaryFixed),
            new(nameof(SecondaryFixedDim), SecondaryFixedDim),
            new(nameof(OnSecondaryFixedVariant), OnSecondaryFixedVariant),
            new(nameof(TertiaryFixed), TertiaryFixed),
            new(nameof(OnTertiaryFixed), OnTertiaryFixed),
            new(nameof(TertiaryFixedDim), TertiaryFixedDim),
            new(nameof(OnTertiaryFixedVariant), OnTertiaryFixedVariant),
            new(nameof(PrimaryContainer), PrimaryContainer),
            new(nameof(OnPrimaryContainer), OnPrimaryContainer),
            new(nameof(SecondaryContainer), SecondaryContainer),
            new(nameof(OnSecondaryContainer), OnSecondaryContainer),
            new(nameof(TertiaryContainer), TertiaryContainer),
            new(nameof(OnTertiaryContainer), OnTertiaryContainer),
            new(nameof(Error), Error),
            new(nameof(OnError), OnError),
            new(nameof(ErrorContainer), ErrorContainer),
            new(nameof(OnErrorContainer), OnErrorContainer),
            new(nameof(Surface), Surface),
            new(nameof(OnSurface), OnSurface),
            new(nameof(OnSurfaceVariant), OnSurfaceVariant),
            new(nameof(Outline), Outline),
            new(nameof(OutlineVariant), OutlineVariant),
            new(nameof(InverseSurface), InverseSurface),
            new(nameof(InverseOnSurface), InverseOnSurface),
            new(nameof(InversePrimary), InversePrimary),
            new(nameof(SurfaceDim), SurfaceDim),
            new(nameof(SurfaceBright), SurfaceBright),
            new(nameof(SurfaceContainerLowest), SurfaceContainerLowest),
            new(nameof(SurfaceContainerLow), SurfaceContainerLow),
            new(nameof(SurfaceContainer), SurfaceContainer),
            new(nameof(SurfaceContainerHigh), SurfaceContainerHigh),
            new(nameof(SurfaceContainerHighest), SurfaceContainerHighest),
            new(nameof(Scrim), Scrim),
        ];
    }
}