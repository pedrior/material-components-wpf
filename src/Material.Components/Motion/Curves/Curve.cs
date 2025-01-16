using System.Diagnostics;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace Material.Components.Motion.Curves;

/// <summary>
/// Represents the base class for easing functions that define custom motion curves.
/// </summary>
/// <remarks>
/// The <see cref="Curve"/> class provides a foundation for creating custom easing functions. Derived classes must
/// implement the <see cref="Ease(double)"/> method to define how input progress values are transformed over time.
/// </remarks>
public abstract class Curve : IEasingFunction
{
    /// <summary>
    /// Transforms normalized time into eased progress using the specific curve logic.
    /// </summary>
    /// <param name="t">
    /// A normalized time value between <c>0.0</c> and <c>1.0</c>, where <c>0.0</c> represents 
    /// the start of the animation and <c>1.0</c> represents the end.
    /// </param>
    /// <returns>
    /// A <see cref="double"/> representing the eased progress, typically between <c>0.0</c> and <c>1.0</c>.
    /// </returns>
    public abstract double Ease(double t);

    [DebuggerStepThrough]
    protected static void ValidatePoint(Point point, [CallerArgumentExpression("point")] string? argument = null)
    {
        if (point is { X: < 0.0 or > 1.0 } or { Y: < 0.0 or > 1.0 })
        {
            throw new ArgumentException($"Expected a point in the range [0.0, 1.0], but got {point} for {argument}.");
        }
    }
}