using System.Windows;
using System.Windows.Media.Animation;
using Material.Components.Motion.Curves;

namespace Material.Components.Motion;

/// <summary>
/// Provides a set of predefined easing functions that simulate natural motion in UI transitions.
/// </summary>
/// <remarks>
/// This class offers two main sets of easing functions:
/// <list type="bullet">
/// <item>
/// <description><b>Emphasized Easing:</b> Recommended for most transitions to reflect the expressive motion of
/// Material Design 3.</description>
/// </item>
/// <item>
/// <description><b>Standard Easing:</b> Designed for quick and utility-focused transitions.</description>
/// </item>
/// </list>
/// </remarks>
public static class AnimationEasings
{
    /// <summary>
    /// Provides a dynamic easing function that accelerates quickly and decelerates smoothly, 
    /// creating natural and expressive motion.
    /// </summary>
    /// <remarks>
    /// Use this easing for transitions that begin and end on the screen to emphasize both entry and exit.
    /// This easing creates smooth and responsive animations for UI components like modals or drawers.
    /// </remarks>
    /// <value>
    /// A <see cref="Curve"/> designed for emphasized transitions.
    /// </value>
    public static readonly Curve Emphasized = new PiecewiseCubicCurve(
        a1: new Point(0.05, 0.0),
        b1: new Point(0.133333, 0.06),
        mp: new Point(0.166666, 0.4),
        a2: new Point(0.2083333, 0.82),
        b2: new Point(0.25, 1));

    /// <summary>
    /// Provides an emphasized easing function with rapid acceleration and minimal deceleration, 
    /// suitable for fast exit transitions.
    /// </summary>
    /// <remarks>
    /// Best used for permanently removing elements from the screen, such as deleting a notification. 
    /// This easing starts slowly and exits at peak velocity, signaling that the component is gone permanently.
    /// </remarks>
    /// <value>
    /// An <see cref="IEasingFunction"/> for accelerated exit animations.
    /// </value>
    public static readonly IEasingFunction EmphasizedAccelerated = new CubicCurve(
        a: new Point(0.3, 0.0),
        b: new Point(0.8, 0.15));

    /// <summary>
    /// Provides an emphasized easing function that starts with high speed and decelerates smoothly, 
    /// ideal for entering transitions.
    /// </summary>
    /// <remarks>
    /// Use this easing when components enter the screen. It begins at peak velocity and slows down gently, 
    /// drawing attention to the newly appearing element.
    /// </remarks>
    /// <value>
    /// An <see cref="IEasingFunction"/> for emphasized entry animations.
    /// </value>
    public static readonly IEasingFunction EmphasizedDecelerated = new CubicCurve(
        a: new Point(0.5, 0.7),
        b: new Point(0.1, 1.0));

    /// <summary>
    /// Provides a balanced easing function with moderate acceleration and deceleration for standard transitions.
    /// </summary>
    /// <remarks>
    /// This easing is suitable for general-purpose UI transitions, such as opening menus or navigating between views.
    /// It offers a smooth and consistent motion.
    /// </remarks>
    /// <value>
    /// An <see cref="IEasingFunction"/> for standard transitions.
    /// </value>
    public static readonly IEasingFunction Standard = new CubicCurve(
        a: new Point(0.2, 0.0),
        b: new Point(0.0, 1.0));

    /// <summary>
    /// Provides a standard easing function with faster acceleration and gradual deceleration for quick entry
    /// transitions.
    /// </summary>
    /// <remarks>
    /// Use this easing for quick entry transitions like expanding dropdowns or lightweight UI components 
    /// that need to appear swiftly without abrupt stops.
    /// </remarks>
    /// <value>
    /// An <see cref="IEasingFunction"/> for fast entry transitions.
    /// </value>
    public static readonly IEasingFunction StandardAccelerated = new CubicCurve(
        a: new Point(0.3, 0.0),
        b: new Point(1.0, 1.0));

    /// <summary>
    /// Provides a standard easing function with minimal acceleration and smooth deceleration ideal for subtle
    /// exit transitions.
    /// </summary>
    /// <remarks>
    /// This easing is suited for temporary exits, where the element might return, such as closing a panel or
    /// hiding some content.
    /// </remarks>
    /// <value>
    /// An <see cref="IEasingFunction"/> for gentle exit transitions.
    /// </value>
    public static readonly IEasingFunction StandardDecelerated = new CubicCurve(
        a: new Point(0.0, 0.0),
        b: new Point(0.0, 1.0));
}