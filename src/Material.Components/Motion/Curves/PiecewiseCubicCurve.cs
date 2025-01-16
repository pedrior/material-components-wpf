using System.Windows;

namespace Material.Components.Motion.Curves;

/// <summary>
/// Represents a piecewise cubic Bézier curve defined by two curve segments and a midpoint for complex easing.
/// </summary>
/// <remarks>
/// The <see cref="PiecewiseCubicCurve"/> combines two cubic Bézier curves connected at a midpoint, allowing for
/// more complex easing functions.
/// </remarks>
public sealed class PiecewiseCubicCurve : Curve
{
    private readonly Point a1;
    private readonly Point b1;
    private readonly Point mp;
    private readonly Point a2;
    private readonly Point b2;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PiecewiseCubicCurve"/> class with two curve segments and
    /// a midpoint.
    /// </summary>
    /// <param name="a1">The first segment's start control point.</param>
    /// <param name="b1">The first segment's end control point.</param>
    /// <param name="mp">The midpoint connecting the two curve segments.</param>
    /// <param name="a2">The second segment's start control point.</param>
    /// <param name="b2">The second segment's end control point.</param>
    public PiecewiseCubicCurve(Point a1, Point b1, Point mp, Point a2, Point b2)
    {
        ValidatePoint(a1);
        ValidatePoint(b1);
        ValidatePoint(mp);
        ValidatePoint(a2);
        ValidatePoint(b2);

        this.a1 = a1;
        this.b1 = b1;
        this.mp = mp;
        this.a2 = a2;
        this.b2 = b2;
    }
    
    public override double Ease(double t)
    {
        var firstCurve = t < mp.X;

        var scaleX = firstCurve ? mp.X : 1.0 - mp.X;
        var scaleY = firstCurve ? mp.Y : 1.0 - mp.Y;

        var scaledT = (t - (firstCurve ? 0.0 : mp.X)) / scaleX;

        if (firstCurve)
        {
            return CubicCurve.Transform(
                a1.X / scaleX,
                a1.Y / scaleY,
                b1.X / scaleX,
                b1.Y / scaleY,
                scaledT) * scaleY;
        }

        return CubicCurve.Transform(
            (a2.X - mp.X) / scaleX,
            (a2.Y - mp.Y) / scaleY,
            (b2.X - mp.X) / scaleX,
            (b2.Y - mp.Y) / scaleY,
            scaledT) * scaleY + mp.Y;
    }
    
    public override string ToString() =>
        $"{nameof(PiecewiseCubicCurve)}({a1.X}, {a1.Y}, {b1.X}, {b1.Y}, {mp.X}, {mp.Y}, {a2.X}, {a2.Y}, {b2.X}, {b2.Y})";
}