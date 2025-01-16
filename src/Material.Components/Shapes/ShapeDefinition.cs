using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Material.Components.Extensions;

namespace Material.Components.Shapes;

/// <summary>
/// Represents the definition of a shape, including configurable attributes such as shape family, style, direction, 
/// corner radius, and fill/stroke behavior. This class allows for building a <see cref="Geometry"/> based on the
/// provided attributes and bounds.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ShapeDefinition
{
    private bool isDirty = true;
    private Geometry builtGeometry = Geometry.Empty;

    private Rect bounds;
    private ShapeFamily family;
    private ShapeStyle style;
    private ShapeDirection direction;
    private CornerRadius? radius;
    private bool isFilled = true;
    private bool isStroked = true;

    /// <summary>
    /// Gets the actual corner radius of the shape, calculated based on the specified or default shape attributes.
    /// </summary>
    /// <remarks>
    /// The <see cref="ActualRadius"/> property is computed during the <see cref="BuildGeometry"/> process, taking
    /// into account the <see cref="Radius"/>, <see cref="Style"/>, and <see cref="Direction"/>.
    /// </remarks>
    public CornerRadius ActualRadius { get; private set; }

    /// <summary>
    /// Gets or sets the bounds of the shape, which determine its size and position.
    /// </summary>
    public Rect Bounds
    {
        get => bounds;
        set => SetPropertyValue(ref bounds, value);
    }

    /// <summary>
    /// Gets or sets the shape family, which determines the corner type (e.g., rounded or cut).
    /// </summary>
    public ShapeFamily Family
    {
        get => family;
        set => SetPropertyValue(ref family, value);
    }

    /// <summary>
    /// Gets or sets the style of the shape, which determines predefined corner radius for the shape.
    /// </summary>
    /// <remarks>
    /// The <see cref="Style"/> property defines a predefined corner radius that can be overridden by the
    /// <see cref="Radius"/> property.
    /// </remarks>
    public ShapeStyle Style
    {
        get => style;
        set => SetPropertyValue(ref style, value);
    }

    /// <summary>
    /// Gets or sets the direction of the shape, which determines how corner radius are applied to specific corners.
    /// </summary>
    /// <remarks>
    /// The <see cref="Direction"/> property allows symmetric and asymmetric configurations by specifying which corners
    /// are affected. 
    /// </remarks>
    public ShapeDirection Direction
    {
        get => direction;
        set => SetPropertyValue(ref direction, value);
    }

    /// <summary>
    /// Gets or sets the custom corner radius of the shape. When set, it overrides the corner radius defined by the
    /// <see cref="Style"/> property.
    /// </summary>
    public CornerRadius? Radius
    {
        get => radius;
        set => SetPropertyValue(ref radius, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the shape should be filled.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true"/>, the shape will be rendered as a filled geometry. 
    /// </remarks>
    public bool IsFilled
    {
        get => isFilled;
        set => SetPropertyValue(ref isFilled, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the shape should be stroked.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true"/>, the shape will have a stroke outline.
    /// </remarks>
    public bool IsStroked
    {
        get => isStroked;
        set => SetPropertyValue(ref isStroked, value);
    }

    /// <summary>
    /// Builds and returns the <see cref="Geometry"/> for the shape based on the current configuration.
    /// </summary>
    /// <remarks>
    /// If the shape is already up-to-date, this method returns the previously built <see cref="Geometry"/>. Otherwise,
    /// it computes the actual corner radius, constructs the geometry using a <see cref="StreamGeometry"/>, and caches
    /// the result for future calls. The <see cref="Bounds"/> property must be set before calling this method; otherwise,
    /// an empty <see cref="Geometry"/> is built.
    /// </remarks>
    /// <param name="freeze">A value indicating whether the built <see cref="Geometry"/> should be frozen.</param>
    /// <returns>
    /// A <see cref="Geometry"/> object representing the built shape based on the configured attributes.
    /// </returns>
    public Geometry BuildGeometry(bool freeze = true)
    {
        if (!isDirty)
        {
            return builtGeometry;
        }

        if (bounds.Size is { Width: 0.0 } or { Height: 0.0 })
        {
            isDirty = false;
            builtGeometry = Geometry.Empty;

            return builtGeometry;
        }

        ActualRadius = ComputeActualRadius();

        var geometry = new StreamGeometry();
        using (var context = geometry.Open())
        {
            Begin(context);

            TopEdge(context);
            TopRightCorner(context);

            RightEdge(context);
            BottomRightCorner(context);

            BottomEdge(context);
            BottomLeftCorner(context);

            LeftEdge(context);
            TopLeftCorner(context);
        }

        if (freeze)
        {
            geometry.Freeze();
        }

        builtGeometry = geometry;

        isDirty = false;

        return builtGeometry;
    }

    private void SetPropertyValue<T>(ref T shapeFamily, T value)
    {
        if (Equals(shapeFamily, value))
        {
            return;
        }

        shapeFamily = value;
        isDirty = true;
    }

    private CornerRadius ComputeActualRadius()
    {
        CornerRadius computedRadius;

        var smallestDimension = Math.Min(bounds.Width, bounds.Height);

        computedRadius = radius ?? ShapeStyles.GetRadiusForStyle(style, smallestDimension);
        computedRadius.Clamp(0.0, smallestDimension * 0.5);

        if (direction is not ShapeDirection.Symmetric)
        {
            ShapeHelper.EnforceDirectionalRadius(ref computedRadius, direction);
        }

        return computedRadius;
    }

    private void Begin(StreamGeometryContext context)
    {
        var point = new Point(bounds.Left + ActualRadius.TopLeft, bounds.Top);
        context.BeginFigure(point, isFilled, isClosed: true);
    }

    private void TopEdge(StreamGeometryContext context)
    {
        var point = new Point(bounds.Right - ActualRadius.TopRight, bounds.Top);
        context.LineTo(point, isStroked, isSmoothJoin: false);
    }

    private void TopRightCorner(StreamGeometryContext context)
    {
        if (ActualRadius.TopRight is 0.0)
        {
            return;
        }

        var point = new Point(bounds.Right, bounds.Top + ActualRadius.TopRight);

        if (family is ShapeFamily.Rounded)
        {
            context.ArcTo(
                point,
                new Size(ActualRadius.TopRight, ActualRadius.TopRight),
                rotationAngle: 0.0,
                isLargeArc: false,
                SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
        else
        {
            context.LineTo(point, isStroked, isSmoothJoin: false);
        }
    }

    private void RightEdge(StreamGeometryContext context)
    {
        var point = new Point(bounds.Right, bounds.Bottom - ActualRadius.BottomRight);
        context.LineTo(point, isStroked, isSmoothJoin: false);
    }

    private void BottomRightCorner(StreamGeometryContext context)
    {
        if (ActualRadius.BottomRight is 0.0)
        {
            return;
        }

        var point = new Point(bounds.Right - ActualRadius.BottomRight, bounds.Bottom);

        if (family is ShapeFamily.Rounded)
        {
            context.ArcTo(
                point,
                new Size(ActualRadius.BottomRight, ActualRadius.BottomRight),
                rotationAngle: 0.0,
                isLargeArc: false,
                SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
        else
        {
            context.LineTo(point, isStroked, isSmoothJoin: false);
        }
    }

    private void BottomEdge(StreamGeometryContext context)
    {
        var point = new Point(bounds.Left + ActualRadius.BottomLeft, bounds.Bottom);
        context.LineTo(point, isStroked, isSmoothJoin: false);
    }

    private void BottomLeftCorner(StreamGeometryContext context)
    {
        if (ActualRadius.BottomLeft is 0.0)
        {
            return;
        }

        var point = new Point(bounds.Left, bounds.Bottom - ActualRadius.BottomLeft);

        if (family is ShapeFamily.Rounded)
        {
            context.ArcTo(
                point,
                new Size(ActualRadius.BottomLeft, ActualRadius.BottomLeft),
                rotationAngle: 0.0,
                isLargeArc: false,
                SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
        else
        {
            context.LineTo(point, isStroked, isSmoothJoin: false);
        }
    }

    private void LeftEdge(StreamGeometryContext context)
    {
        var point = new Point(bounds.Left, bounds.Top + ActualRadius.TopLeft);
        context.LineTo(point, isStroked, isSmoothJoin: false);
    }

    private void TopLeftCorner(StreamGeometryContext context)
    {
        if (ActualRadius.TopLeft is 0.0)
        {
            return;
        }

        var point = new Point(bounds.Left + ActualRadius.TopLeft, bounds.Top);

        if (family is ShapeFamily.Rounded)
        {
            context.ArcTo(
                point,
                new Size(ActualRadius.TopLeft, ActualRadius.TopLeft),
                rotationAngle: 0.0,
                isLargeArc: false,
                SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
        else
        {
            context.LineTo(point, isStroked, isSmoothJoin: false);
        }
    }
}