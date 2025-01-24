using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Material.Components.Controls;

internal sealed class ScrimOverlay : FrameworkElement
{
    internal const double ActiveOpacity = 0.3;
    internal const double InactiveOpacity = 0.0;
    
    public static readonly DependencyProperty TintProperty = DependencyProperty.Register(
        nameof(Tint),
        typeof(Brush),
        typeof(ScrimOverlay),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    static ScrimOverlay()
    {
        OpacityProperty.OverrideMetadata(typeof(ScrimOverlay), new UIPropertyMetadata(InactiveOpacity));
        FocusVisualStyleProperty.OverrideMetadata(typeof(ScrimOverlay), new FrameworkPropertyMetadata(null));
    }
    
    public ScrimOverlay() => SetResourceReference(TintProperty, "MaterialColorScrim");

    public Brush? Tint
    {
        get => (Brush?)GetValue(TintProperty);
        set => SetValue(TintProperty, value);
    }

    internal int ZIndex
    {
        get => Panel.GetZIndex(this);
        set => Panel.SetZIndex(this, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        if (Tint is { } tint)
        {
            context.DrawRectangle(tint, pen: null, new Rect(RenderSize));
        }
    }
}