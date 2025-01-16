namespace Material.Components.Theming.XAML;

internal static class XamlHelper
{
    public static Uri MakeUri(string relativePathToXaml) => new(
        $"pack://application:,,,/Material.Components;component/Theming/XAML/{relativePathToXaml}");
}