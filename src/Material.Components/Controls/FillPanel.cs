using System.Windows;
using System.Windows.Controls;

namespace Material.Components.Controls;

/// <summary>
/// Represents a simple panel that arranges its child elements to fill the available space.
/// </summary>
/// <remarks>
/// This <see cref="FillPanel"/> does not provide advanced any layout functionality, and is intended for scenarios
/// where a straightforward layout with all children filling the panel is sufficient.
/// </remarks>
public class FillPanel : Panel
{
    /// <summary>
    /// Initializes static members of the <see cref="FillPanel"/> class.
    /// </summary>
    static FillPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FillPanel),
            new FrameworkPropertyMetadata(typeof(FillPanel)));
    }

    protected override Size MeasureOverride(Size constraint)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;

        if (childrenCount is 0)
        {
            return default;
        }

        var width = 0.0;
        var height = 0.0;

        for (var i = 0; i < childrenCount; i++)
        {
            var child = children[i];
            child.Measure(constraint);

            var childSize = child.DesiredSize;

            width = Math.Max(width, childSize.Width);
            height = Math.Max(height, childSize.Height);
        }

        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size constraint)
    {
        var children = InternalChildren;
        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];
            if (child.Visibility is not Visibility.Collapsed)
            {
                child.Arrange(new Rect(constraint));
            }
        }

        return constraint;
    }
}