using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Represents a visual layer that responds to interaction states such as hover, press, and drag.
/// </summary>
/// <remarks>
/// The <see cref="StateLayer"/> control dynamically adjusts its visual appearance based on user interactions. 
/// It visually responds to hover, press, and drag states by changing its opacity, providing immediate feedback 
/// to users. The appearance can be customized using the <see cref="Tint"/> and <see cref="Shape"/>
/// properties.
/// </remarks>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class StateLayer : DrawableContainer
{
    /// <summary>
    /// Identifies the <see cref="Tint"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TintProperty = DependencyProperty.Register(
        nameof(Tint),
        typeof(Brush),
        typeof(StateLayer),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Shape"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeProperty = DependencyProperty.Register(
        nameof(Shape),
        typeof(Geometry),
        typeof(StateLayer),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="IsHovered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsHoveredProperty = DependencyProperty.Register(
        nameof(IsHovered),
        typeof(bool),
        typeof(StateLayer),
        new PropertyMetadata(false, OnIsHoveredChanged));

    /// <summary>
    /// Identifies the <see cref="IsPressed"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsPressedProperty = DependencyProperty.Register(
        nameof(IsPressed),
        typeof(bool),
        typeof(StateLayer),
        new PropertyMetadata(false, OnIsPressedChanged));

    /// <summary>
    /// Identifies the <see cref="IsDragged"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDraggedProperty = DependencyProperty.Register(
        nameof(IsDragged),
        typeof(bool),
        typeof(StateLayer),
        new PropertyMetadata(false, OnIsDraggedChanged));

    /// <summary>
    /// Identifies the IsDragging dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.RegisterAttached(
        "IsDragging",
        typeof(bool),
        typeof(StateLayer),
        new FrameworkPropertyMetadata(false));

    private const double HoveredOpacity = 0.08;
    private const double PressedOpacity = 0.10;
    private const double DraggedOpacity = 0.16;

    /// <summary>
    /// Initializes static members of the <see cref="StateLayer"/> class.
    /// </summary>
    static StateLayer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(StateLayer),
            new FrameworkPropertyMetadata(typeof(StateLayer)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StateLayer"/> class.
    /// </summary>
    public StateLayer()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        IsEnabledChanged += OnIsEnabledChanged;
    }
    
    /// <summary>
    /// Gets or sets the brush used to paint the visual layer.
    /// </summary>
    /// <remarks>
    /// The <see cref="Tint"/> property controls the color of the layer. This color changes in opacity based on 
    /// the current state (hovered, pressed, or dragged).
    /// </remarks>
    /// <value>
    /// A <see cref="Brush"/> that defines the color of the state layer.  
    /// The default value is <see cref="Brushes.Black"/>.
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush Tint
    {
        get => (Brush)GetValue(TintProperty);
        set => SetValue(TintProperty, value);
    }

    /// <summary>
    /// Gets or sets the geometry that defines the area where the visual layer is rendered.
    /// </summary>
    /// <remarks>
    /// Use this property to customize the shape of the state layer. By default, this property is set to 
    /// <see langword="null"/>, meaning no specific geometry is applied.
    /// </remarks>
    /// <value>
    /// A <see cref="Geometry"/> that defines the visible area of the state layer.  
    /// The default value is <see langword="null"/>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public Geometry? Shape
    {
        get => (Geometry?)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the layer is in a hovered state.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true"/>, the state layer becomes partially visible with a slight opacity to
    /// indicate a hover interaction.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the layer is hovered; otherwise, <see langword="false"/>.  
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public bool IsHovered
    {
        get => (bool)GetValue(IsHoveredProperty);
        set => SetValue(IsHoveredProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the layer is in a pressed state.
    /// </summary>
    /// <remarks>
    /// When <see langword="true"/>, the state layer appears with increased opacity to indicate that the element
    /// is being pressed or clicked.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the layer is pressed; otherwise, <see langword="false"/>.  
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        set => SetValue(IsPressedProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the layer is in a dragged state.
    /// </summary>
    /// <remarks>
    /// When <see langword="true"/>, the layer displays with the highest opacity to indicate that the element
    /// is actively being dragged.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the layer is in a dragged state; otherwise, <see langword="false"/>.  
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public bool IsDragged
    {
        get => (bool)GetValue(IsDraggedProperty);
        set => SetValue(IsDraggedProperty, value);
    }
    
    protected override DrawableContainerOrder Order => DrawableContainerOrder.ChildThenVisual;
    
    protected sealed override void OnVisualLayerRender(DrawingContext context)
    {
        base.OnVisualLayerRender(context);

        if (Shape is { } geometry)
        {
            context.DrawGeometry(Tint, null, geometry);
        }
    }

    protected override void OnVisualChildrenChanged(DependencyObject added, DependencyObject removed)
    {
        base.OnVisualChildrenChanged(added, removed);

        UpdateLayerState();
    }

    /// <summary>
    /// Sets the value of the <see cref="IsDraggingProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to set the property value.</param>
    /// <param name="value">
    /// A <see cref="bool"/> value indicating whether the element is currently being dragged.
    /// </param>
    /// <remarks>
    /// Setting this property to <see langword="true"/> enables external controls or elements to mark themselves as
    /// being in a dragging state. This property does not directly affect the <see cref="StateLayer"/> itself, but it
    /// can be used in conjunction with it to control drag-related visuals or behavior.
    /// <para>The default value is <see langword="false"/>.</para>
    /// </remarks>
    public static void SetIsDragging(DependencyObject element, bool value) =>
        element.SetValue(IsDraggingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsDraggingProperty"/> attached property for the specified element.
    /// </summary>
    /// <param name="element">The <see cref="DependencyObject"/> for which to retrieve the property value.</param>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    /// <returns>
    /// A <see cref="bool"/> value indicating whether the element is currently being dragged.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static bool GetIsDragging(DependencyObject element) => (bool)element.GetValue(IsDraggingProperty);

    private static void OnIsHoveredChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((StateLayer)element).UpdateLayerState();

    private static void OnIsPressedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((StateLayer)element).UpdateLayerState();

    private static void OnIsDraggedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((StateLayer)element).UpdateLayerState();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        UpdateLayerState();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;
        IsEnabledChanged -= OnIsEnabledChanged;
    }

    private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) => UpdateLayerState();

    private void UpdateLayerState()
    {
        // Precedence: Dragged > Pressed > Hovered

        if (IsDragged)
        {
            VisualLayer.Opacity = DraggedOpacity;
        }
        else if (IsPressed)
        {
            VisualLayer.Opacity = PressedOpacity;
        }
        else if (IsHovered)
        {
            VisualLayer.Opacity = HoveredOpacity;
        }
        else
        {
            VisualLayer.Opacity = 0.0;
        }
    }
}