using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a panel that arranges its children in a single line, either horizontally or vertically.
/// Supports advanced layout features such as alignment, spacing, stretching, reverse order, and merging
/// of adjacent borders.
/// </summary>
public class SpacedPanel : Panel
{
    /// <summary>
    /// Identifies the <see cref="Alignment"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
        nameof(Alignment),
        typeof(Alignment),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(Alignment.Start, FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(
            Orientation.Horizontal,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifies the <see cref="Spacing"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
        nameof(Spacing),
        typeof(double),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure),
        ValidateSpacing);

    /// <summary>
    /// Identifies the <see cref="ReverseOrder"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ReverseOrderProperty = DependencyProperty.Register(
        nameof(ReverseOrder),
        typeof(bool),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifies the <see cref="MergeAdjacentBorders"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MergeAdjacentBordersProperty = DependencyProperty.Register(
        nameof(MergeAdjacentBorders),
        typeof(bool),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// Identifies the <see cref="UniformChildrenThickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UniformChildrenThicknessProperty = DependencyProperty.Register(
        nameof(UniformChildrenThickness),
        typeof(double),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure),
        ValidateUniformChildrenThickness);

    /// <summary>
    /// Identifies the <c>Stretch</c> attached dependency property.
    /// </summary>
    public static readonly DependencyProperty StretchProperty = DependencyProperty.RegisterAttached(
        "Stretch",
        typeof(bool),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(
            false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    private static readonly Size InfiniteConstraint = new(double.PositiveInfinity, double.PositiveInfinity);

    private double measuredWidth;
    private double measuredHeight;

    private int stretchCount;

    /// <summary>
    /// Initializes static members of the <see cref="SpacedPanel"/> class.
    /// </summary>
    static SpacedPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SpacedPanel),
            new FrameworkPropertyMetadata(typeof(SpacedPanel)));
    }

    /// <summary>
    /// Gets or sets the alignment of children within the panel, indicating whether they are aligned
    /// to the start, center, or end of the panel.
    /// </summary>
    /// <remarks>
    /// If the panel has stretchable children, it will behave like <see cref="Alignment.Start"/>.
    /// </remarks>
    /// <value>
    /// One of the <see cref="Alignment"/> values. The default is <see cref="Alignment.Start"/>.
    /// </value>
    public Alignment Alignment
    {
        get => (Alignment)GetValue(AlignmentProperty);
        set => SetValue(AlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the orientation of the panel, indicating whether the children are arranged
    /// horizontally or vertically.
    /// </summary>
    /// <value>
    /// One of the <see cref="Orientation"/> values. The default is <see cref="Orientation.Horizontal"/>.
    /// </value>
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between children.
    /// </summary>
    /// <remarks>
    /// If a child is collapsed or has a desired size of <c>0.0</c> and is not stretchable,
    /// it will be skipped and the spacing will be reduced accordingly.
    /// </remarks>
    /// <value>
    /// A positive and finite <see cref="double"/> value. The default is <c>0.0</c>.
    /// </value>
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether children are arranged in reverse order.
    /// </summary>
    /// <remarks>
    /// Setting this property will only affect the arrangement of children, not their order in the visual tree.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if children are arranged in reverse order; otherwise, <see langword="false"/>.
    /// The default is <see langword="false"/>.
    /// </value>
    public bool ReverseOrder
    {
        get => (bool)GetValue(ReverseOrderProperty);
        set => SetValue(ReverseOrderProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether adjacent borders of children should be merged.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if adjacent borders are merged; otherwise, <see langword="false"/>.
    /// The default is <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// This property is useful when children have uniform borders and <see cref="Spacing"/> is set to <c>0.0</c>.
    /// </remarks>
    public bool MergeAdjacentBorders
    {
        get => (bool)GetValue(MergeAdjacentBordersProperty);
        set => SetValue(MergeAdjacentBordersProperty, value);
    }

    /// <summary>
    /// Gets or sets the uniform thickness value used when merging adjacent borders of children.
    /// </summary>
    /// <remarks>
    /// Setting this property doesn't change the border thickness of the children. It only indicates
    /// the thickness of the borders that will be merged when <see cref="MergeAdjacentBorders"/> is
    /// <see langword="true"/>.
    /// </remarks>
    /// <value>
    /// A positive and finite <see cref="double"/> value. The default is <c>0.0</c>.
    /// </value>
    public double UniformChildrenThickness
    {
        get => (double)GetValue(UniformChildrenThicknessProperty);
        set => SetValue(UniformChildrenThicknessProperty, value);
    }

    private bool ContainsStretchableChildren => stretchCount > 0;

    /// <summary>
    /// Gets the value of the <see cref="StretchProperty"/> attached property for a specified element.
    /// </summary>
    /// <param name="element">The element from which the property value is read.</param>
    /// <returns>The value of the <see cref="StretchProperty"/> attached property.</returns>
    public static bool GetStretch(DependencyObject element) => (bool)element.GetValue(StretchProperty);

    /// <summary>
    /// Sets the value of the <see cref="StretchProperty"/> attached property for a specified element.
    /// </summary>
    /// <param name="element">The element to which the property value is written.</param>
    /// <param name="value">The value to set.</param>
    public static void SetStretch(DependencyObject element, bool value) => element.SetValue(StretchProperty, value);

    protected override Size MeasureOverride(Size constraint)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;

        if (childrenCount is 0)
        {
            return default;
        }

        var spacing = Spacing;
        var orientation = Orientation;
        var thickness = GetUniformThickness();

        var totalSpacing = spacing * (childrenCount - 1);
        var totalThickness = thickness * (childrenCount - 1);

        measuredWidth = 0.0;
        measuredHeight = 0.0;

        stretchCount = 0;

        for (var index = 0; index < childrenCount; index++)
        {
            var child = children[index];

            // Let the child measure itself.
            child.Measure(InfiniteConstraint);
            var childSize = child.DesiredSize;

            if (ShouldSkipChild(child, orientation))
            {
                totalSpacing -= spacing;
                totalThickness -= thickness;

                continue;
            }

            if (orientation is Orientation.Horizontal)
            {
                if (GetStretch(child))
                {
                    stretchCount++;
                }
                else
                {
                    measuredWidth += childSize.Width;
                }

                measuredHeight = Math.Max(measuredHeight, childSize.Height);
            }
            else
            {
                if (GetStretch(child))
                {
                    stretchCount++;
                }
                else
                {
                    measuredHeight += childSize.Height;
                }

                measuredWidth = Math.Max(measuredWidth, childSize.Width);
            }
        }

        // Account for the spacing and uniform thickness between children.
        var additive = totalSpacing - totalThickness;

        // Fix the measured size by adding the spacing and uniform thickness.
        if (orientation is Orientation.Horizontal)
        {
            measuredWidth = Math.Max(0.0, measuredWidth + additive);
        }
        else
        {
            measuredHeight = Math.Max(0.0, measuredHeight + additive);
        }

        return new Size(measuredWidth, measuredHeight);
    }

    protected override Size ArrangeOverride(Size constraint)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;

        if (childrenCount is 0)
        {
            return default;
        }

        var spacing = Spacing;
        var orientation = Orientation;
        var reverseOrder = ReverseOrder;
        var uniformThickness = GetUniformThickness();

        var x = 0.0;
        var y = 0.0;

        var stretchWidth = 0.0;
        var stretchHeight = 0.0;

        // Calculate the available space for each stretchable child.
        if (ContainsStretchableChildren)
        {
            stretchWidth = (constraint.Width - measuredWidth) / stretchCount;
            stretchHeight = (constraint.Height - measuredHeight) / stretchCount;
        }

        // Calculate the starting offset for the arrangement based on the alignment.
        if (orientation is Orientation.Horizontal)
        {
            x = GetArrangeOffset(constraint.Width, measuredWidth);
        }
        else
        {
            y = GetArrangeOffset(constraint.Height, measuredHeight);
        }

        for (var index = 0; index < childrenCount; index++)
        {
            // Gets the child in the correct order.
            var child = children[reverseOrder ? childrenCount - 1 - index : index];

            var childSize = child.DesiredSize;

            if (ShouldSkipChild(child, orientation))
            {
                continue;
            }

            if (orientation is Orientation.Horizontal)
            {
                if (GetStretch(child))
                {
                    childSize.Width = stretchWidth;
                    child.SetValue(WidthProperty, stretchWidth);
                }

                var yy = 0.0;

                // Handle vertical alignment.
                var verticalAlignment = (VerticalAlignment)child.GetValue(VerticalAlignmentProperty);
                if (verticalAlignment is VerticalAlignment.Stretch)
                {
                    childSize.Height = Math.Max(childSize.Height, constraint.Height);
                }
                else
                {
                    yy = GetArrangeChildOffset(verticalAlignment, constraint.Height, childSize.Height);
                }

                child.Arrange(new Rect(x, yy, childSize.Width, childSize.Height));

                // Update the horizontal offset for the next child.
                x += Math.Round(childSize.Width);

                if (index < childrenCount - 1)
                {
                    x += spacing - uniformThickness;
                }
            }
            else
            {
                if (GetStretch(child))
                {
                    childSize.Height = stretchHeight;
                    child.SetValue(HeightProperty, stretchHeight);
                }

                var xx = 0.0;

                // Handle horizontal alignment.
                var horizontalAlignment = (HorizontalAlignment)child.GetValue(HorizontalAlignmentProperty);
                if (horizontalAlignment is HorizontalAlignment.Stretch)
                {
                    childSize.Width = Math.Max(childSize.Width, constraint.Width);
                }
                else
                {
                    xx = GetArrangeChildOffset(horizontalAlignment, constraint.Width, childSize.Width);
                }

                child.Arrange(new Rect(xx, y, childSize.Width, childSize.Height));

                // Update the vertical offset for the next child.
                y += Math.Round(childSize.Height);

                if (index < childrenCount - 1)
                {
                    y += spacing - uniformThickness;
                }
            }
        }

        return constraint;
    }

    private static bool ValidateSpacing(object value) =>
        value is double v && double.IsPositive(v) && double.IsFinite(v);

    private static bool ValidateUniformChildrenThickness(object value) =>
        value is double v && double.IsPositive(v) && double.IsFinite(v);

    private static bool ShouldSkipChild(UIElement child, Orientation orientation)
    {
        if (child.Visibility is Visibility.Collapsed)
        {
            return true;
        }

        // Skip children with a desired size of 0.0 and that are not stretchable.
        return (orientation is Orientation.Horizontal && child.DesiredSize.Width is 0.0 && !GetStretch(child)) ||
               (orientation is Orientation.Vertical && child.DesiredSize.Height is 0.0 && !GetStretch(child));
    }

    private double GetUniformThickness() =>
        MergeAdjacentBorders && Spacing is 0.0 ? UniformChildrenThickness : 0.0;

    private double GetArrangeOffset(double constraint, double size)
    {
        // The starting offset must be 0.0 if the panel contains stretchable children.
        if (ContainsStretchableChildren)
        {
            return 0.0;
        }

        return Alignment switch
        {
            Alignment.Center => (constraint - size) * 0.5,
            Alignment.End => constraint - size,
            _ => 0.0
        };
    }

    private static double GetArrangeChildOffset(Enum alignment, double constraint, double size) => alignment switch
    {
        VerticalAlignment.Center or HorizontalAlignment.Center => (constraint - size) * 0.5,
        VerticalAlignment.Bottom or HorizontalAlignment.Right => constraint - size,
        _ => 0.0
    };
}