using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Material.Components.Elevations;
using Material.Components.Extensions;
using Material.Components.Shapes;

namespace Material.Components.Controls;

/// <summary>
/// Represents a stylized container control that provides advanced customization options.
/// </summary>
/// <remarks>
/// The <see cref="Container"/> extends <see cref="DrawableContainer"/> by introducing styling properties such as
/// background, border, and opacity. It also integrates with <see cref="ShapeAssist"/> for shape customization and
/// <see cref="ElevationAssist"/> for elevation effects.
/// <para>The <see cref="Container"/> draws it's surface on the <see cref="DrawableContainer.VisualLayer"/>.</para>
/// </remarks>
public class Container : DrawableContainer
{
    /// <summary>
    /// Identifies the <see cref="Background"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
        nameof(Background),
        typeof(Brush),
        typeof(Container),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender |
            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

    /// <summary>
    /// Identifies the <see cref="BorderBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
        nameof(BorderBrush),
        typeof(Brush),
        typeof(Container),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender |
            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
            OnBorderBrushChanged));

    /// <summary>
    /// Identifies the <see cref="BorderThickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
        nameof(BorderThickness),
        typeof(Thickness),
        typeof(Container),
        new FrameworkPropertyMetadata(
            new Thickness(0.0),
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnBorderThicknessChanged),
        ValidateBorderThickness);

    /// <summary>
    /// Identifies the <see cref="ChildOpacity"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ChildOpacityProperty = DependencyProperty.Register(
        nameof(ChildOpacity),
        typeof(double),
        typeof(Container),
        new PropertyMetadata(1.0, propertyChangedCallback: null, CoerceOpacity));

    /// <summary>
    /// Identifies the <see cref="SurfaceOpacity"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SurfaceOpacityProperty = DependencyProperty.Register(
        nameof(SurfaceOpacity),
        typeof(double),
        typeof(Container),
        new PropertyMetadata(1.0, OnSurfaceOpacityChanged, CoerceOpacity));

    private static readonly DependencyPropertyKey SurfaceGeometryPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(SurfaceGeometry),
        typeof(Geometry),
        typeof(Container),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="SurfaceGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SurfaceGeometryProperty = SurfaceGeometryPropertyKey.DependencyProperty;

    private static readonly DependencyProperty[] ShapeProperties =
    [
        ShapeAssist.StyleProperty,
        ShapeAssist.FamilyProperty,
        ShapeAssist.DirectionProperty,
        ShapeAssist.RadiusProperty
    ];

    private readonly ShapeDefinition shapeDefinition = new();

    private Pen? borderPen;
    private Geometry? renderGeometry;

    private object? originalChildOpacity;

    /// <summary>
    /// Initializes static members of the <see cref="Container"/> class.
    /// </summary>
    static Container()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Container),
            new FrameworkPropertyMetadata(typeof(Container)));

        ElevationAssist.LevelProperty.OverrideMetadata(
            typeof(Container),
            new FrameworkPropertyMetadata(
                ElevationAssist.LevelProperty.DefaultMetadata.DefaultValue,
                OnElevationLevelChanged));

        foreach (var property in ShapeProperties)
        {
            property.OverrideMetadata(
                typeof(Container),
                new FrameworkPropertyMetadata(
                    property.DefaultMetadata.DefaultValue,
                    OnShapeAssistPropertyChanged));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class.
    /// </summary>
    public Container() => Loaded += OnLoaded;

    /// <summary>
    /// Occurs when the container's surface has been rendered.
    /// </summary>
    /// <remarks>
    /// This event is raised after the container's surface geometry have been drawn, allowing for post-render
    /// operations.
    /// </remarks>
    [Category("Behavior")]
    public event EventHandler? SurfacedRendered;

    /// <summary>
    /// Gets or sets the child content of the container.
    /// </summary>
    /// <remarks>
    /// The opacity of the child content is automatically bound to the <see cref="ChildOpacity"/> property. 
    /// Changing the child content will restore the original opacity of the previous child.
    /// </remarks>
    /// <value>
    /// A <see cref="UIElement"/> that represents the child of the container.
    /// The default value is <see langword="null"/>.
    /// </value>
    public override UIElement? Child
    {
        get => base.Child;
        set
        {
            // Gives back the original opacity value to the old child.
            base.Child?.SetValue(OpacityProperty, originalChildOpacity);

            base.Child = value;
            if (value is null)
            {
                return;
            }

            // Saves the original opacity of the new child and binds it to the ChildOpacity property.
            originalChildOpacity = value.GetValue(OpacityProperty);
            BindingOperations.SetBinding(value, OpacityProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(ChildOpacityProperty)
            });
        }
    }

    /// <summary>
    /// Gets or sets the brush used to paint the background of the container.
    /// </summary>
    /// <remarks>
    /// The <see cref="Background"/> property defines the fill of the container's surface behind the child content 
    /// and border. It can be set to any <see cref="Brush"/>, such as a <see cref="SolidColorBrush"/>, 
    /// <see cref="LinearGradientBrush"/>, or <see cref="ImageBrush"/> to achieve the desired visual.
    /// </remarks>
    /// <value>
    /// A <see cref="Brush"/> that specifies how the background of the container is painted. 
    /// The default value is <see langword="null"/>, meaning no background is rendered.
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? Background
    {
        get => (Brush?)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the container's border.
    /// </summary>
    /// <remarks>
    /// The <see cref="BorderBrush"/> property defines the stroke of the container's surface border.
    /// It can be set to any <see cref="Brush"/>, such as a <see cref="SolidColorBrush"/>, 
    /// <see cref="LinearGradientBrush"/>, or <see cref="ImageBrush"/> to achieve the desired visual. This property
    /// works in conjunction with <see cref="BorderThickness"/> to render the border. If set to <see langword="null"/>,
    /// no border is drawn.
    /// </remarks>
    /// <value>
    /// A <see cref="Brush"/> that specifies the appearance of the container's border. 
    /// The default value is <see langword="null"/>, indicating no border is drawn.
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the container's border.
    /// </summary>
    /// <remarks>
    /// The <see cref="BorderThickness"/> property determines the width of the border surrounding 
    /// the container. This property must be set with uniform thickness (equal values on all sides). 
    /// A value of <c>0.0</c> disables the border rendering.
    /// </remarks>
    /// <value>
    /// A <see cref="Thickness"/> value that specifies the width of the border on all sides. 
    /// The default value is <c>0.0</c>, meaning no border is drawn.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity level of the child content of the container.
    /// </summary>
    /// <remarks>
    /// The <see cref="ChildOpacity"/> property controls the transparency of the child content. 
    /// This is useful for creating fade-in/out effects or layering visuals. The opacity value is 
    /// automatically bound to the child content, ensuring consistent transparency behavior.
    /// </remarks>
    /// <value>
    /// A <see langword="double"/> between <c>0.0</c> (fully transparent) and <c>1.0</c> (fully opaque). 
    /// The default value is <c>1.0</c>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double ChildOpacity
    {
        get => (double)GetValue(ChildOpacityProperty);
        set => SetValue(ChildOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity level of the container's surface, including the background and border.
    /// </summary>
    /// <remarks>
    /// The <see cref="SurfaceOpacity"/> property affects the visibility of the container's visual surface, 
    /// impacting both the <see cref="Background"/> and <see cref="BorderBrush"/>. This property is independent 
    /// of the <see cref="ChildOpacity"/> property, allowing separate control over the child content and 
    /// the container's appearance.
    /// </remarks>
    /// <value>
    /// A <see langword="double"/> between <c>0.0</c> (fully transparent) and <c>1.0</c> (fully opaque). 
    /// The default value is <c>1.0</c>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double SurfaceOpacity
    {
        get => (double)GetValue(SurfaceOpacityProperty);
        set => SetValue(SurfaceOpacityProperty, value);
    }

    /// <summary>
    /// Gets the geometry that defines the surface outline of the container.
    /// </summary>
    /// <remarks>
    /// The <see cref="SurfaceGeometry"/> reflects the container's current shape, including any applied 
    /// shape property. This property updates dynamically when the container's size or shape-related properties change.
    /// It can be used for advanced rendering, hit-testing scenarios, or any custom shape-related logic.
    /// </remarks>
    /// <value>
    /// A <see cref="Geometry"/> object representing the shape of the container's surface. 
    /// The default value is <see langword="null"/>, indicating no defined geometry.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Geometry? SurfaceGeometry
    {
        get => (Geometry?)GetValue(SurfaceGeometryProperty);
        private set => SetValue(SurfaceGeometryPropertyKey, value);
    }

    protected override DrawableContainerOrder Order => DrawableContainerOrder.VisualThenChild;

    protected virtual void OnSurfaceRendered() => SurfacedRendered?.Invoke(this, EventArgs.Empty);

    protected sealed override void OnVisualLayerRender(DrawingContext context)
    {
        base.OnVisualLayerRender(context);

        var border = BorderBrush;
        var thickness = border is null
            ? 0.0
            : BorderThickness.Left;

        if (borderPen is null && thickness > 0.0)
        {
            EnsureBorderPen(border!, thickness);
        }

        var isSurfaceGeometryDirty = false;
        if (renderGeometry is null || RenderSizeHasChanged)
        {
            isSurfaceGeometryDirty = true;
            EnsureRenderGeometry(thickness);
        }

        // Draws background and border.
        context.DrawGeometry(Background, borderPen, renderGeometry);

        if (isSurfaceGeometryDirty)
        {
            UpdateSurfaceGeometry(thickness);
        }
    }

    private static bool ValidateBorderThickness(object value)
    {
        var thickness = (Thickness)value;
        if (!thickness.IsUniform())
        {
            return false;
        }

        var left = thickness.Left;
        return !double.IsNaN(left) && !double.IsInfinity(left) && left >= 0.0;
    }

    private static void OnElevationLevelChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Container)element).UpdateSurfaceElevation((ElevationLevel)e.NewValue);

    private static void OnShapeAssistPropertyChanged(DependencyObject element, DependencyPropertyChangedEventArgs _) =>
        ((Container)element).InvalidateRenderGeometry();

    private static void OnBorderBrushChanged(DependencyObject element, DependencyPropertyChangedEventArgs _) =>
        ((Container)element).InvalidateBorderPen();

    private static void OnBorderThicknessChanged(DependencyObject element, DependencyPropertyChangedEventArgs _) =>
        ((Container)element).InvalidateBorderPen();

    private static void OnSurfaceOpacityChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Container)element).UpdateSurfaceOpacity((double)e.NewValue);

    private static object CoerceOpacity(DependencyObject element, object value) =>
        Math.Clamp((double)value, 0.0, 1.0);

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        UpdateSurfaceOpacity(SurfaceOpacity);
    }

    private void UpdateSurfaceElevation(ElevationLevel level) =>
        VisualLayer.Effect = Elevations.Elevations.FromLevel(level);

    private void UpdateSurfaceOpacity(double value) => VisualLayer.Opacity = value;

    private void InvalidateBorderPen() => borderPen = null;

    private void InvalidateRenderGeometry() => renderGeometry = null;

    private void EnsureBorderPen(Brush border, double thickness)
    {
        borderPen = new Pen(border, thickness);
        borderPen.Freeze();
    }

    private void EnsureRenderGeometry(double thickness)
    {
        var bounds = new Rect(
            thickness * 0.5,
            thickness * 0.5,
            Math.Max(RenderSize.Width - thickness, 0.0),
            Math.Max(RenderSize.Height - thickness, 0.0));

        if (bounds.Size is { Width: 0.0 } or { Height: 0.0 })
        {
            return;
        }

        shapeDefinition.Bounds = bounds;
        shapeDefinition.Radius = ShapeAssist.GetRadius(this);
        shapeDefinition.Style = ShapeAssist.GetStyle(this);
        shapeDefinition.Family = ShapeAssist.GetFamily(this);
        shapeDefinition.Direction = ShapeAssist.GetDirection(this);

        renderGeometry = shapeDefinition.BuildGeometry();
    }

    private void UpdateSurfaceGeometry(double thickness)
    {
        // Check if we've drawn something.
        if (renderGeometry is null || renderGeometry.IsEmpty())
        {
            SurfaceGeometry = Geometry.Empty;
        }
        else
        {
            shapeDefinition.Bounds = new Rect(RenderSize);
            shapeDefinition.Radius = shapeDefinition.ActualRadius;

            // If we have a radius, inflate it to account for the border thickness.
            if (!shapeDefinition.Radius?.IsZero() ?? false)
            {
                shapeDefinition.Radius!.Value.Inflate(thickness * 0.5);

                // We need to rebuild the geometry with the new radius.
                SurfaceGeometry = shapeDefinition.BuildGeometry();
            }
            else // Just clone our rendered geometry.
            {
                SurfaceGeometry = renderGeometry.Clone();
                SurfaceGeometry.Freeze();
            }
        }

        OnSurfaceRendered();
    }
}