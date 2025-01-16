using System.Windows.Media;

namespace Material.Components.Theming.Colors;

internal readonly record struct NamedColor(string Name, Color Color)
{
    public void Deconstruct(out string name, out Color color)
    {
        name = Name;
        color = Color;
    }
}