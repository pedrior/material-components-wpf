using System.Windows.Media;

namespace Material.Components.Controls;

internal sealed class ContainerVisual : DrawingVisual, IContainerVisual
{
    public bool IsHitTestVisible { get; set; } = true;

    protected override HitTestResult? HitTestCore(PointHitTestParameters parameters) => 
        IsHitTestVisible ? base.HitTestCore(parameters) : null;
    
    protected override GeometryHitTestResult? HitTestCore(GeometryHitTestParameters parameters) => 
        IsHitTestVisible ? base.HitTestCore(parameters) : null;
}