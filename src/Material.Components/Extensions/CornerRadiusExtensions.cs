using System.Windows;

namespace Material.Components.Extensions;

internal static class CornerRadiusExtensions
{
    public static bool IsZero(this CornerRadius radius)
    {
        return radius is
        {
            TopLeft: 0.0,
            TopRight: 0.0,
            BottomRight: 0.0,
            BottomLeft: 0.0
        };
    }

    public static void Inflate(this CornerRadius radius, double value)
    {
        radius.TopLeft += value;
        radius.TopRight += value;
        radius.BottomLeft += value;
        radius.BottomLeft += value;
    }
    
    public static void Clamp(this ref CornerRadius radius, double min, double max)
    {
        radius.TopLeft = Math.Clamp(radius.TopLeft, min, max);
        radius.TopRight = Math.Clamp(radius.TopRight, min, max);
        radius.BottomRight = Math.Clamp(radius.BottomRight, min, max);
        radius.BottomLeft = Math.Clamp(radius.BottomLeft, min, max);
    }
}