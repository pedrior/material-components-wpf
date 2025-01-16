using System.Windows;

namespace Material.Components.Shapes;

internal static class ShapeHelper
{
    public static void EnforceDirectionalRadius(ref CornerRadius radius, ShapeDirection direction)
    {
        switch (direction)
        {
            case ShapeDirection.Top:
                radius.BottomLeft = 0.0;
                radius.BottomRight = 0.0;
                break;
            case ShapeDirection.Bottom:
                radius.TopLeft = 0.0;
                radius.TopRight = 0.0;
                break;
            case ShapeDirection.Start:
                radius.TopRight = 0.0;
                radius.BottomRight = 0.0;
                break;
            case ShapeDirection.End:
                radius.TopLeft = 0.0;
                radius.BottomLeft = 0.0;
                break;
        }
    }
}