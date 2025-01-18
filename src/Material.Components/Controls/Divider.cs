using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Represents a divider element used to separate content or define visual boundaries in the UI.
/// </summary>
/// <remarks>
/// The <see cref="Divider"/> control provides a lightweight visual separator, commonly used to delineate 
/// different sections or groups of content in a UI. It supports both horizontal and vertical orientations 
/// and can be customized with thickness, color, and insets.
/// 
/// Use the <see cref="Brush"/> property to define the color of the divider, the <see cref="Thickness"/> property 
/// to specify its thickness, and the <see cref="Orientation"/> property to control whether the divider is 
/// horizontal or vertical. The <see cref="InsetStart"/> and <see cref="InsetEnd"/> properties allow you to 
/// add padding to the start and end of the divider, respectively.
/// </remarks>
public class Divider : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(Divider),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Thickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness),
        typeof(double),
        typeof(Divider),
        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(Divider),
        new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="InsetStart"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InsetStartProperty = DependencyProperty.Register(
        nameof(InsetStart),
        typeof(double),
        typeof(Divider),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="InsetEnd"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InsetEndProperty = DependencyProperty.Register(
        nameof(InsetEnd),
        typeof(double),
        typeof(Divider),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Initializes static members of the <see cref="Divider"/> class.
    /// </summary>
    static Divider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Divider),
            new FrameworkPropertyMetadata(typeof(Divider)));
    }

    /// <summary>
    /// Gets or sets the brush used to paint the divider.
    /// </summary>
    /// <value>
    /// A <see cref="Brush"/> that specifies the color of the divider.
    /// <para>The default value is <see cref="Brushes.Black"/>.</para>
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush Brush
    {
        get => (Brush)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the divider.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value that specifies the thickness of the divider.
    /// <para>The default value is <c>1.0</c>.</para>
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the orientation of the divider.
    /// </summary>
    /// <value>
    /// An <see cref="Orientation"/> value that specifies whether the divider is horizontal or vertical. 
    /// <para>The default value is <see cref="Orientation.Horizontal"/>.</para>
    /// </value>
    [Bindable(true)]
    [Category("Layout")]
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets the amount of padding at the start of the divider.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value that specifies the inset at the start of the divider.
    /// <para> The default value is <c>0.0</c>.</para>
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double InsetStart
    {
        get => (double)GetValue(InsetStartProperty);
        set => SetValue(InsetStartProperty, value);
    }

    /// <summary>
    /// Gets or sets the amount of padding at the end of the divider.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value that specifies the inset at the end of the divider.
    /// <para>The default value is <c>0.0</c>.</para>
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double InsetEnd
    {
        get => (double)GetValue(InsetEndProperty);
        set => SetValue(InsetEndProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        var thickness = Thickness;
        var halfThickness = thickness * 0.5;

        Point start, end;
        if (Orientation is Orientation.Horizontal)
        {
            start = new Point(InsetStart, halfThickness);
            end = new Point(ActualWidth - InsetEnd, halfThickness);
        }
        else
        {
            start = new Point(halfThickness, InsetStart);
            end = new Point(halfThickness, ActualHeight - InsetEnd);
        }
        
        var pen = new Pen(Brush, thickness);
        context.DrawLine(pen, start, end);
    }

    protected override Size MeasureOverride(Size constraint)
    {
        return Orientation is Orientation.Horizontal
            ? new Size(Math.Min(constraint.Width, 0.0), Thickness)
            : new Size(Thickness, Math.Min(constraint.Height, 0.0));
    }
}