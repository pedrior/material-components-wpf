using System.Windows.Media.Effects;

namespace Material.Components.Elevations;

/// <summary>
/// Provides predefined <see cref="DropShadowEffect"/> instances for each elevation level.
/// </summary>
/// <remarks>
/// The <see cref="Elevations"/> class contains static instances of <see cref="DropShadowEffect"/> for each
/// elevation level (1 to 5). These effects can be directly applied to elements to visually represent their
/// elevation in the UI. Shadows are generated with consistent opacity, direction, and softness, in alignment
/// with Material Design 3 guidelines.
/// </remarks>
public static class Elevations
{
    /// <summary>
    /// A predefined <see cref="DropShadowEffect"/> for elevation level 1, representing a small and sharp shadow.
    /// </summary>
    public static readonly DropShadowEffect Level1 = CreateShadowEffect(1, 3);

    /// <summary>
    /// A predefined <see cref="DropShadowEffect"/> for elevation level 2, representing a slightly larger shadow.
    /// </summary>
    public static readonly DropShadowEffect Level2 = CreateShadowEffect(2, 6);

    /// <summary>
    /// A predefined <see cref="DropShadowEffect"/> for elevation level 3, representing a medium-sized shadow.
    /// </summary>
    public static readonly DropShadowEffect Level3 = CreateShadowEffect(4, 8);

    /// <summary>
    /// A predefined <see cref="DropShadowEffect"/> for elevation level 4, representing a larger, softer shadow.
    /// </summary>
    public static readonly DropShadowEffect Level4 = CreateShadowEffect(6, 10);

    /// <summary>
    /// A predefined <see cref="DropShadowEffect"/> for elevation level 5, representing the largest and softest shadow.
    /// </summary>
    public static readonly DropShadowEffect Level5 = CreateShadowEffect(8, 12);

    private const double Opacity = 0.3;
    private const double Direction = 270.0;
    
    internal static DropShadowEffect? FromLevel(ElevationLevel level) => level switch
    {
        ElevationLevel.Level1 => Level1,
        ElevationLevel.Level2 => Level2,
        ElevationLevel.Level3 => Level3,
        ElevationLevel.Level4 => Level4,
        ElevationLevel.Level5 => Level5,
        _ => null
    }; 

    private static DropShadowEffect CreateShadowEffect(double depth, double blur)
    {
        var effect = new DropShadowEffect
        {
            Opacity = Opacity,
            Direction = Direction,
            ShadowDepth = depth,
            BlurRadius = blur
        };

        effect.Freeze();

        return effect;
    }
}