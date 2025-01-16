using System.Windows;

namespace Material.Components.Motion;

/// <summary>
/// Provides a set of predefined durations for animations to ensure consistency in timing across user interface
/// transitions and effects.
/// </summary>
/// <remarks>
/// The <see cref="AnimationDurations"/> class defines commonly used animation durations categorized into 
/// short, medium, long, and extra-long intervals.
/// </remarks>
public static class AnimationDurations
{
    /// <summary>
    /// Represents a very short animation duration of 50 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 50 milliseconds.</value>
    public static readonly Duration Short50 = TimeSpan.FromMilliseconds(50);

    /// <summary>
    /// Represents a short animation duration of 100 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 100 milliseconds.</value>
    public static readonly Duration Short100 = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Represents a short animation duration of 150 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 150 milliseconds.</value>
    public static readonly Duration Short150 = TimeSpan.FromMilliseconds(150);

    /// <summary>
    /// Represents a short animation duration of 200 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 200 milliseconds.</value>
    public static readonly Duration Short200 = TimeSpan.FromMilliseconds(200);

    /// <summary>
    /// Represents a medium animation duration of 250 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 250 milliseconds.</value>
    public static readonly Duration Medium250 = TimeSpan.FromMilliseconds(250);

    /// <summary>
    /// Represents a medium animation duration of 300 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 300 milliseconds.</value>
    public static readonly Duration Medium300 = TimeSpan.FromMilliseconds(300);

    /// <summary>
    /// Represents a medium animation duration of 350 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 350 milliseconds.</value>
    public static readonly Duration Medium350 = TimeSpan.FromMilliseconds(350);

    /// <summary>
    /// Represents a medium animation duration of 400 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 400 milliseconds.</value>
    public static readonly Duration Medium400 = TimeSpan.FromMilliseconds(400);

    /// <summary>
    /// Represents a long animation duration of 450 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 450 milliseconds.</value>
    public static readonly Duration Long450 = TimeSpan.FromMilliseconds(450);

    /// <summary>
    /// Represents a long animation duration of 500 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 500 milliseconds.</value>
    public static readonly Duration Long500 = TimeSpan.FromMilliseconds(500);

    /// <summary>
    /// Represents a long animation duration of 550 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 550 milliseconds.</value>
    public static readonly Duration Long550 = TimeSpan.FromMilliseconds(550);

    /// <summary>
    /// Represents a long animation duration of 600 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 600 milliseconds.</value>
    public static readonly Duration Long600 = TimeSpan.FromMilliseconds(600);

    /// <summary>
    /// Represents an extra-long animation duration of 700 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 700 milliseconds.</value>
    public static readonly Duration ExtraLong700 = TimeSpan.FromMilliseconds(700);

    /// <summary>
    /// Represents an extra-long animation duration of 800 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 800 milliseconds.</value>
    public static readonly Duration ExtraLong800 = TimeSpan.FromMilliseconds(800);

    /// <summary>
    /// Represents an extra-long animation duration of 900 milliseconds.
    /// </summary>
    /// <value>A <see cref="Duration"/> of 900 milliseconds.</value>
    public static readonly Duration ExtraLong900 = TimeSpan.FromMilliseconds(900);

    /// <summary>
    /// Represents an extra-long animation duration of 1000 milliseconds (1 second).
    /// </summary>
    /// <value>A <see cref="Duration"/> of 1000 milliseconds.</value>
    public static readonly Duration ExtraLong1000 = TimeSpan.FromMilliseconds(1000);
}
