using System.Windows;

namespace Material.Components.Extensions;

internal static class ThicknessExtensions
{
    public static bool IsUniform(this Thickness thickness)
    {
        return Math.Abs(thickness.Left - thickness.Top) < double.Epsilon &&
               Math.Abs(thickness.Left - thickness.Right) < double.Epsilon &&
               Math.Abs(thickness.Left - thickness.Bottom) < double.Epsilon;
    }
}