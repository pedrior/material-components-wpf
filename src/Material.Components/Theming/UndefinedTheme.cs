using System.Windows.Media;

namespace Material.Components.Theming;

internal sealed class UndefinedTheme() : ThemeDefinition("Undefined", isUndefined: true)
{
    public override FontFamily LargeTextFontFamily => null!;

    public override FontFamily NormalTextFontFamily => null!;
}