using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

// #if DEBUG
// using System.Windows.Media;
// #endif

namespace Material.Components.Controls;

/// <summary>
/// Represents a lightweight panel that arranges its child elements in a single line, either horizontally or
/// vertically, with configurable spacing, alignment, and other layout properties.
/// </summary>
public class SpacedPanel : Panel
{
    /// <summary>
    /// Identifies the <see cref="Spacing"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
        nameof(Spacing),
        typeof(double),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(
            0.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure,
            propertyChangedCallback: null,
            CoerceSpacing));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(SpacedPanel),
        new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

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
        new FrameworkPropertyMetadata(
            0.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure,
            propertyChangedCallback: null,
            CoerceThickness));

    private static readonly Size InfiniteConstraint = new(double.PositiveInfinity, double.PositiveInfinity);

    private double measuredWidth;
    private double measuredHeight;

    // Total children that specify stretch alignment.
    // This value is computed during the measure pass.
    private int stretchableChildrenCount;

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
    /// Gets or sets the amount of spacing between child elements in the panel.
    /// </summary>
    /// <remarks>
    /// The <see cref="Spacing"/> property defines the amount of space, in device-independent units (DIPs),
    /// to insert between child elements. Increasing this value will space out the elements further apart.
    /// </remarks>
    /// <value>
    /// A <see cref="double"/> representing the spacing between child elements. The default value is <c>0.0</c>.
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the orientation describing how the panel arranges its child elements.
    /// </summary>
    /// <remarks>
    /// The <see cref="Orientation"/> property determines whether the panel arranges its child elements
    /// horizontally or vertically.
    /// </remarks>
    /// <value>
    /// One of the <see cref="Orientation"/> enumeration values that specifies the orientation of the panel.
    /// The default value is <see cref="Orientation.Horizontal"/>.
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the order of the child elements should be reversed.
    /// </summary>
    /// <remarks>
    /// The <see cref="ReverseOrder"/> property, when set to <see langword="true"/>, causes the panel to arrange
    /// its child elements in reverse order. This can be useful for scenarios where the display order of elements
    /// needs to be inverted.
    /// </remarks>
    /// <value>
    /// A <see cref="bool"/> value indicating whether the child elements are arranged in reverse order.
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool ReverseOrder
    {
        get => (bool)GetValue(ReverseOrderProperty);
        set => SetValue(ReverseOrderProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether adjacent borders of child elements should be merged.
    /// </summary>
    /// <remarks>
    /// The <see cref="MergeAdjacentBorders"/> property, when set to <see langword="true"/>, merges the borders
    /// of adjacent child elements to create a seamless layout. This is only applicable when the spacing
    /// between elements is set to <c>0.0</c> and <see cref="UniformChildrenThickness"/> is greater than <c>0.0</c>.
    /// </remarks>
    /// <value>
    /// A <see cref="bool"/> value indicating whether adjacent borders are merged.
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool MergeAdjacentBorders
    {
        get => (bool)GetValue(MergeAdjacentBordersProperty);
        set => SetValue(MergeAdjacentBordersProperty, value);
    }

    /// <summary>
    /// Gets or sets the uniform thickness value used by the panel when merging adjacent borders of child elements.
    /// </summary>
    /// <remarks>
    /// The <see cref="UniformChildrenThickness"/> property specifies the assumed border thickness of all child
    /// elements. This value is used by the panel to calculate the space needed when merging adjacent borders
    /// (see <see cref="MergeAdjacentBorders"/>).This property does not apply any thickness to child elements;
    /// it only defines the value the panel should consider during layout calculations. 
    /// </remarks>
    /// <value>
    /// A <see cref="double"/> representing the assumed uniform border thickness of child elements. 
    /// The default value is <c>0.0</c>.
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public double UniformChildrenThickness
    {
        get => (double)GetValue(UniformChildrenThicknessProperty);
        set => SetValue(UniformChildrenThicknessProperty, value);
    }

// #if DEBUG
//     protected override void OnRender(DrawingContext context)
//     {
//         base.OnRender(context);
//
//         var width = RenderSize.Width;
//         var height = RenderSize.Height;
//
//         var x = width * 0.5;
//         var y = height * 0.5;
//
//         context.PushGuidelineSet(new GuidelineSet([x], [0.0, height]));
//         context.DrawLine(new Pen(Brushes.Black, 1.0), new Point(x, 0), new Point(x, height));
//         context.Pop();
//
//         context.PushGuidelineSet(new GuidelineSet([0.0, width], [y]));
//         context.DrawLine(new Pen(Brushes.Black, 1.0), new Point(0, y), new Point(width, y));
//         context.Pop();
//     }
// #endif

    protected override Size MeasureOverride(Size constraint)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;

        if (childrenCount is 0)
        {
            return base.MeasureOverride(constraint);
        }

        var spacing = Spacing;
        var orientation = Orientation;
        var uniformChildrenThickness = UniformChildrenThickness;

        var totalSpacing = spacing * (childrenCount - 1);

        var totalThicknessToRemove = CanMergeAdjacentBorders(spacing, uniformChildrenThickness)
            ? uniformChildrenThickness * (childrenCount - 1)
            : 0.0;

        var width = 0.0;
        var height = 0.0;

        stretchableChildrenCount = 0;

        for (var index = 0; index < childrenCount; index++)
        {
            var child = children[index];

            // Measures the size of the child.
            child.Measure(InfiniteConstraint);

            var size = child.DesiredSize;

            // Skips the child if it is collapsed or has no size.
            if (ShouldSkipLayoutPass(child.Visibility, size, orientation))
            {
                totalSpacing = Math.Max(0.0, totalSpacing - spacing);
                totalThicknessToRemove = Math.Max(0.0, totalThicknessToRemove - uniformChildrenThickness);

                continue;
            }

            // Adjusts the total size of the panel based on the orientation.
            if (orientation is Orientation.Horizontal)
            {
                width += size.Width;
                height = Math.Max(height, size.Height);

                // Check that the child is stretching horizontally.
                if ((HorizontalAlignment)child.GetValue(HorizontalAlignmentProperty) is HorizontalAlignment.Stretch)
                {
                    stretchableChildrenCount++;
                }
            }
            else
            {
                width = Math.Max(width, size.Width);
                height += size.Height;

                // Check that the child is stretching vertically.
                if ((VerticalAlignment)child.GetValue(VerticalAlignmentProperty) is VerticalAlignment.Stretch)
                {
                    stretchableChildrenCount++;
                }
            }
        }

        // Accounts for spacing and merging borders.
        if (orientation is Orientation.Horizontal)
        {
            width += totalSpacing - totalThicknessToRemove;
        }
        else
        {
            height += totalSpacing - totalThicknessToRemove;
        }

        measuredWidth = width;
        measuredHeight = height;

        // Console.WriteLine($"Measured: {measuredWidth} x {measuredHeight}");

        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size constraint)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;

        if (childrenCount is 0)
        {
            return base.ArrangeOverride(constraint);
        }

        var spacing = Spacing;
        var orientation = Orientation;
        var reverseOrder = ReverseOrder;
        var uniformChildrenThickness = UniformChildrenThickness;

        // The thickness of each child to remove when merging adjacent borders.
        var thicknessToRemove = CanMergeAdjacentBorders(spacing, uniformChildrenThickness)
            ? uniformChildrenThickness
            : 0.0;

        var stretchWidth = 0.0; // The available width for each child that is stretching horizontally.
        var stretchHeight = 0.0; // The available height for each child that is stretching vertically.

        if (stretchableChildrenCount > 0)
        {
            // The available space for each child that is stretching is calculated based on the remaining space 
            // after the measured size of the panel, divided by the total number of children that are stretching.
            stretchWidth = Math.Max(0.0, constraint.Width - measuredWidth) / stretchableChildrenCount;
            stretchHeight = Math.Max(0.0, constraint.Height - measuredHeight) / stretchableChildrenCount;
        }

        // Console.WriteLine($"Stretchable: {stretchableChildrenCount} ({stretchWidth} x {stretchHeight})");

        var x = 0.0; // Holds the x-coordinate for the next child in the horizontal line.
        var y = 0.0; // Holds the y-coordinate for the next child in the vertical line.

        for (var index = 0; index < childrenCount; index++)
        {
            // Gets the child in the correct order.
            var child = children[!reverseOrder ? index : childrenCount - 1 - index];

            // Gets the computed size of the child.
            var size = child.DesiredSize;

            // Skips the child if it is collapsed or has no size.
            if (ShouldSkipLayoutPass(child.Visibility, size, orientation))
            {
                continue;
            }
            
            var horizontalAlignment = (HorizontalAlignment)child.GetValue(HorizontalAlignmentProperty);
            var verticalAlignment = (VerticalAlignment)child.GetValue(VerticalAlignmentProperty);

            if (orientation is Orientation.Horizontal)
            {
                var yy = 0.0;
                if (verticalAlignment is VerticalAlignment.Stretch)
                {
                    // The child wants to stretch vertically, so we give it the final height.
                    size.Height = Math.Max(size.Height, constraint.Height);
                }
                else
                {
                    // Aligns the child vertically based on the specified alignment (Top, Center, Bottom).
                    yy = CalcChildVerticalOffset(size.Height, constraint.Height, verticalAlignment);
                }

                if (horizontalAlignment is HorizontalAlignment.Stretch)
                {
                    // Adjusts the size of the child if it is stretching horizontally.
                    // The child is given the available width for stretching, respecting its maximum width.
                    var maxWidth = (double)child.GetValue(MaxWidthProperty);
                    size.Width = Math.Min(size.Width + stretchWidth, maxWidth);
                }

                child.Arrange(new Rect(x, yy, size.Width, size.Height));

                // Updates the offsets for the next child in the horizontal line.
                x += Math.Round(size.Width); // Rounding avoids subpixel rendering.

                // Accounts for spacing and merging borders.
                if (index < childrenCount - 1)
                {
                    x += spacing - thicknessToRemove;
                }

                y = Math.Max(y, size.Height);
            }
            else
            {
                var xx = 0.0;
                if (horizontalAlignment is HorizontalAlignment.Stretch)
                {
                    // The child wants to stretch horizontally, so we give it the final width.
                    size.Width = Math.Max(size.Width, constraint.Width);
                }
                else
                {
                    // Aligns the child horizontally based on the specified alignment (Left, Center, Right).
                    xx = CalcChildHorizontalOffset(size.Width, constraint.Width, horizontalAlignment);
                }

                if (verticalAlignment is VerticalAlignment.Stretch)
                {
                    // Adjusts the size of the child if it is stretching vertically.
                    // The child is given the available height for stretching, respecting its maximum height.
                    var maxHeight = (double)child.GetValue(MaxHeightProperty);
                    size.Height = Math.Min(size.Height + stretchHeight, maxHeight);
                }

                child.Arrange(new Rect(xx, y, size.Width, size.Height));

                // Updates the offsets for the next child in the vertical line.
                y += Math.Round(size.Height); // Rounding avoids subpixel rendering.

                // Accounts for spacing and merging borders.
                if (index < childrenCount - 1)
                {
                    y += spacing - thicknessToRemove;
                }

                x = Math.Max(x, size.Width);
            }
        }

        // Console.WriteLine($"Arranged: {x} x {y}");

        return constraint;
    }

    private static object CoerceSpacing(DependencyObject _, object value) =>
        Math.Max(0.0, Math.Round((double)value));

    private static object CoerceThickness(DependencyObject _, object value) =>
        Math.Max(0.0, Math.Round((double)value));

    private static bool ShouldSkipLayoutPass(Visibility visibility, Size size, Orientation orientation)
    {
        return visibility is Visibility.Collapsed ||
               (size.Width is 0.0 && orientation is Orientation.Horizontal) ||
               (size.Height is 0.0 && orientation is Orientation.Vertical);
    }

    private static double CalcChildVerticalOffset(double height, double maxHeight, VerticalAlignment alignment)
    {
        var y = alignment switch
        {
            VerticalAlignment.Center => (maxHeight - height) * 0.5,
            VerticalAlignment.Bottom => maxHeight - height,
            _ => 0.0
        };

        return Math.Max(0.0, y);
    }

    private static double CalcChildHorizontalOffset(double width, double maxWidth, HorizontalAlignment alignment)
    {
        var x = alignment switch
        {
            HorizontalAlignment.Center => (maxWidth - width) * 0.5,
            HorizontalAlignment.Right => maxWidth - width,
            _ => 0.0
        };

        return Math.Max(0.0, x);
    }

    private bool CanMergeAdjacentBorders(double spacing, double thickness) =>
        spacing is 0.0 && thickness > 0.0 && MergeAdjacentBorders;
}