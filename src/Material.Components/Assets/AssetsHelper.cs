namespace Material.Components.Assets;

internal static class AssetsHelper
{
    public static Uri MakeUri(string relativePathToAsset) => new(
        $"pack://application:,,,/Material.Components;component/Assets/{relativePathToAsset}");
}