using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Material.Components.Shapes;

namespace Material.Components.Controls;

/// <summary>
/// Represents a surface control that renders a custom shape.
/// </summary>
/// <remarks>
/// The <see cref="Surface"/> control is a specialized <see cref="Shape"/> built based on the
/// <see cref="Material.Components.Shapes.ShapeAssist"/> attached properties.
/// </remarks>
public class Surface : Shape
{
    private readonly ShapeDefinition shapeDefinition = new();
 
    /// <summary>
    /// Initializes static members of the <see cref="Surface"/> class.
    /// </summary>
    static Surface()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Surface), 
            new FrameworkPropertyMetadata(typeof(Surface)));
    }
    
    /// <summary>
    /// Gets the geometry that defines the shape of the <see cref="Surface"/>.
    /// </summary>
    /// <value>
    /// A <see cref="Geometry"/> object that represents the rendered shape of the surface.
    /// </value>
    /// <remarks>
    /// The <see cref="DefiningGeometry"/> property is overridden to provide the specific geometry 
    /// for the <see cref="Surface"/> control, based on its shape properties such as <see cref="ShapeAssist.GetRadius"/>,
    /// <see cref="ShapeAssist.GetStyle"/>, <see cref="ShapeAssist.GetFamily"/>, and <see cref="ShapeAssist.GetDirection"/>.
    /// </remarks>
    protected override Geometry DefiningGeometry => BuildDefiningGeometry();

    private Geometry BuildDefiningGeometry()
    {
        var thickness = Stroke is not null 
            ? StrokeThickness 
            : 0.0;
        
        var bounds = new Rect(
            thickness * 0.5,
            thickness * 0.5,
            Math.Max(RenderSize.Width - thickness, 0.0),
            Math.Max(RenderSize.Height - thickness, 0.0));

        if (bounds.Size is { Width: 0.0 } or { Height: 0.0 })
        {
            return Geometry.Empty;
        }

        shapeDefinition.Bounds = bounds;
        shapeDefinition.Radius = ShapeAssist.GetRadius(this);
        shapeDefinition.Style = ShapeAssist.GetStyle(this);
        shapeDefinition.Family = ShapeAssist.GetFamily(this);
        shapeDefinition.Direction = ShapeAssist.GetDirection(this);

        return shapeDefinition.BuildGeometry();
    }
}