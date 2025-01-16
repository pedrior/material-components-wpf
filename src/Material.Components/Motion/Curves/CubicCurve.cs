using System.Windows;

namespace Material.Components.Motion.Curves;

/// <summary>
/// Represents a cubic Bézier easing function for customizable animation curves.
/// </summary>
/// <remarks>
/// The <see cref="CubicCurve"/> uses two control points to define a cubic Bézier curve.
/// </remarks>
public sealed class CubicCurve : Curve
{
    private const double CubicErrorBound = 0.001;

    private readonly Point a;
    private readonly Point b;

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicCurve"/> class with two control points.
    /// </summary>
    /// <param name="a">The first control point, influencing the curve's initial slope.</param>
    /// <param name="b">The second control point, influencing the curve's final slope.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if either control point has coordinates outside the range [0.0, 1.0].
    /// </exception>
    public CubicCurve(Point a, Point b)
    {
        ValidatePoint(a);
        ValidatePoint(b);

        this.a = a;
        this.b = b;
    }

    public override double Ease(double t) => Transform(a.X, a.Y, b.X, b.Y, t);

    public override string ToString() => $"{nameof(CubicCurve)}({a.X}, {a.Y}, {b.X}, {b.Y})";

    internal static double Transform(double ax, double ay, double bx, double by, double t)
    {
        var start = 0.0;
        var end = 1.0;

        while (true)
        {
            var midpoint = (start + end) * 0.5;
            var estimate = Transform(ax, bx, midpoint);

            if (Math.Abs(t - estimate) < CubicErrorBound)
            {
                return Transform(ay, by, midpoint);
            }

            if (estimate < t)
            {
                start = midpoint;
            }
            else
            {
                end = midpoint;
            }
        }
    }

    private static double Transform(double a, double b, double t) =>
        3 * a * (1 - t) * (1 - t) * t + 3 * b * (1 - t) * t * t + t * t * t;
}