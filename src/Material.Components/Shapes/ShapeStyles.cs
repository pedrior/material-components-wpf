using System.Windows;

namespace Material.Components.Shapes;

internal static class ShapeStyles
{
    private static readonly CornerRadius NoneRadius = default;
    private static readonly CornerRadius ExtraSmallRadius = new(4.0);
    private static readonly CornerRadius SmallRadius = new(8.0);
    private static readonly CornerRadius MediumRadius = new(12.0);
    private static readonly CornerRadius LargeRadius = new(16.0);
    private static readonly CornerRadius ExtraLargeRadius = new(28.0);

    public static CornerRadius GetRadiusForStyle(ShapeStyle style, double smallestDimension)
    {
        return style switch
        {
            ShapeStyle.None => NoneRadius,
            ShapeStyle.ExtraSmall => ExtraSmallRadius,
            ShapeStyle.Small => SmallRadius,
            ShapeStyle.Medium => MediumRadius,
            ShapeStyle.Large => LargeRadius,
            ShapeStyle.ExtraLarge => ExtraLargeRadius,
            ShapeStyle.Full => new CornerRadius(smallestDimension * 0.5),
            _ => NoneRadius
        };
    }
}