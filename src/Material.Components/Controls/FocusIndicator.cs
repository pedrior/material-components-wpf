using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Provides a visual focus indicator that highlights a UI element when it gains focus, providing
/// clear and customizable visual feedback to enhance user interaction and accessibility.
/// </summary>
/// <remarks>
/// The <see cref="FocusIndicator"/> can be used in two primary ways:  
/// <list type="bullet">
/// <item>
/// <term>Standalone Control:</term>
/// <description>
/// When used as a standalone control, the focus indicator monitors and responds to 
/// focus events on its child element.
/// </description>
/// </item>
/// <item>
/// <term>Within a Templated Parent:</term>
/// <description>
/// When used inside a templated parent (e.g., within a <see cref="System.Windows.Controls.ControlTemplate"/>), 
/// the focus indicator monitors and responds to focus events on its templated parent element.
/// </description>
/// </item>
/// </list>
/// </remarks>
public class FocusIndicator : DrawableContainer
{
    /// <summary>
    /// Identifies the <see cref="Shape"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeProperty = DependencyProperty.Register(
        nameof(Shape),
        typeof(Geometry),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnDefiningGeometryChanged));

    /// <summary>
    /// Identifies the <c>Brush</c> attached dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.RegisterAttached(
        nameof(Brush),
        typeof(Brush),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            Brushes.Black,
            FrameworkPropertyMetadataOptions.AffectsRender |
            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
            OnStrokeChanged));

    /// <summary>
    /// Identifies the<c>Offset</c> attached dependency property.
    /// </summary>
    public static readonly DependencyProperty OffsetProperty = DependencyProperty.RegisterAttached(
        nameof(Offset),
        typeof(double),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            2.0,
            FrameworkPropertyMetadataOptions.Inherits |
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnOffsetChanged),
        ValidateOffset);

    /// <summary>
    /// Identifies the <c>Thickness</c> attached dependency property.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
        nameof(Thickness),
        typeof(double),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            3.0,
            FrameworkPropertyMetadataOptions.Inherits |
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnThicknessChanged),
        ValidateThickness);

    /// <summary>
    /// Identifies the <c>Device</c> attached dependency property.
    /// </summary>
    public static readonly DependencyProperty DeviceProperty = DependencyProperty.RegisterAttached(
        nameof(Device),
        typeof(FocusDevice),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(FocusDevice.Keyboard));

    private Pen? pen;
    private Geometry? geometry;

    private bool isFocused;
    private bool isCreatedThroughTemplate;

    /// <summary>
    /// Initializes static members of the <see cref="FocusIndicator"/> class.
    /// </summary>
    static FocusIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FocusIndicator),
            new FrameworkPropertyMetadata(typeof(FocusIndicator)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FocusIndicator"/> class.
    /// </summary>
    public FocusIndicator()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        // Disable hit testing for the focus indicator.
        VisualLayer.IsHitTestVisible = false;
    }

    /// <summary>
    /// Gets or sets the geometry used to define the shape of the focus indicator.
    /// </summary>
    /// <value>A <see cref="Geometry"/> object representing the shape.</value>
    /// <remarks>
    /// In instances where no geometry has been specified, a default rectangle geometry is used
    /// as a fallback, with the dimensions based on the element's bounds.
    /// </remarks>
    [Bindable(true)]
    [Category("Appearance")]
    public Geometry? Shape
    {
        get => (Geometry?)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush that defines the color or style of the focus indicator.
    /// </summary>
    /// <value>A <see cref="System.Windows.Media.Brush"/> used to render the focus indicator.</value>
    /// <remarks>
    /// The default value is <see cref="Brushes.Black"/>.
    /// </remarks>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? Brush
    {
        get => (Brush?)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the focus indicator.
    /// </summary>
    /// <value>A non-negative and finite <see cref="double"/> representing the border thickness.</value>
    /// <remarks>
    /// The default value is <c>3.0</c>.
    /// </remarks>
    [Bindable(true)]
    [Category("Appearance")]
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the offset of the focus indicator to the boundaries defined by the <see cref="Shape"/>.
    /// </summary>
    /// <value>A non-negative and finite <see cref="double"/> value representing the offset distance.</value>
    /// <remarks>
    /// The default value is <c>2.0</c>.
    /// </remarks>
    [Bindable(true)]
    [Category("Appearance")]
    public double Offset
    {
        get => (double)GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    /// <summary>
    /// Gets or sets the input device that triggers the focus indicator.
    /// </summary>
    /// <value>A <see cref="FocusDevice"/> indicating the source of focus.</value>
    /// <remarks>
    /// <para>The default value is <see cref="FocusDevice.Keyboard"/>.</para>
    /// <para>
    /// Programmatic focus (e.g., calling <see cref="UIElement.Focus"/> or <see cref="Keyboard.Focus"/>)
    /// activates the focus indicator regardless of the device.
    /// </para>
    /// </remarks>
    [Bindable(true)]
    [Category("Common")]
    public FocusDevice Device
    {
        get => (FocusDevice)GetValue(DeviceProperty);
        set => SetValue(DeviceProperty, value);
    }

    protected override DrawableContainerOrder Order => DrawableContainerOrder.ChildThenVisual;

    /// <summary>
    /// Gets the value of the <see cref="BrushProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element from which to retrieve the value.</param>
    /// <remarks>
    /// The <c>Brush</c> attached property defines the color or style of the focus indicator.
    /// The default value is <see cref="Brushes.Black"/>.
    /// </remarks>
    /// <returns>
    /// The value of the <see cref="BrushProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </returns>
    public static Brush? GetBrush(DependencyObject element) => (Brush?)element.GetValue(BrushProperty);

    /// <summary>
    /// Sets the value of the <see cref="BrushProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element to which the value will be set.</param>
    /// <param name="value">The value to set.</param>
    /// <remarks>
    /// The <c>Brush</c> attached property defines the color or style of the focus indicator.
    /// The default value is <see cref="Brushes.Black"/>.
    /// </remarks>
    public static void SetBrush(DependencyObject element, Brush? value) => element.SetValue(BrushProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="OffsetProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element from which to retrieve the value.</param>
    /// <remarks>
    /// The <c>Offset</c> attached property specifies the offset of the focus indicator to the boundaries
    /// defined by the <see cref="Shape"/>. The default value is <c>2.0</c>.
    /// </remarks>
    /// <returns>
    /// The value of the <see cref="OffsetProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </returns>
    public static double GetOffset(DependencyObject element) => (double)element.GetValue(OffsetProperty);

    /// <summary>
    /// Sets the value of the <see cref="OffsetProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element to which the value will be set.</param>
    /// <param name="value">The value to set.</param>
    /// <remarks>
    /// The <c>Offset</c> attached property specifies the offset of the focus indicator to the boundaries
    /// defined by the <see cref="Shape"/>. The default value is <c>2.0</c>.
    /// </remarks>
    public static void SetOffset(DependencyObject element, double value) => element.SetValue(OffsetProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="ThicknessProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element from which to retrieve the value.</param>
    /// <remarks>
    /// The <c>Offset</c> attached property specifies the thickness of the focus indicator.  
    /// The default value is <c>3.0</c>.
    /// </remarks>
    /// <returns>
    /// The value of the <see cref="ThicknessProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </returns>
    public static double GetThickness(DependencyObject element) => (double)element.GetValue(ThicknessProperty);

    /// <summary>
    /// Sets the value of the <see cref="ThicknessProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element to which the value will be set.</param>
    /// <param name="value">The value to set.</param>
    /// <remarks>
    /// The <c>Offset</c> attached property specifies the thickness of the focus indicator.  
    /// The default value is <c>3.0</c>.
    /// </remarks>
    public static void SetThickness(DependencyObject element, double value) =>
        element.SetValue(ThicknessProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="DeviceProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element from which to retrieve the value.</param>
    /// <remarks>
    /// The <c>Device</c> attached property specifies the input device that triggers the focus indicator.  
    /// The default value is <see cref="FocusDevice.Keyboard"/>.
    /// </remarks>
    /// <returns>
    /// The value of the <see cref="DeviceProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </returns>
    public static FocusDevice GetDevice(DependencyObject element) => (FocusDevice)element.GetValue(DeviceProperty);

    /// <summary>
    /// Sets the value of the <see cref="DeviceProperty"/> for the specified <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="element">The element to which the value will be set.</param>
    /// <param name="value">The value to set.</param>
    /// <remarks>
    /// The <c>Device</c> attached property specifies the input device that triggers the focus indicator.  
    /// The default value is <see cref="FocusDevice.Keyboard"/>.
    /// </remarks>
    public static void SetDevice(DependencyObject element, FocusDevice value) =>
        element.SetValue(DeviceProperty, value);

    private static bool ValidateOffset(object value) => 
        value is double v and >= 0.0 && !double.IsNaN(v) && !double.IsInfinity(v);
    
    private static bool ValidateThickness(object value) => 
        value is double v and >= 0.0 && !double.IsNaN(v) && !double.IsInfinity(v);
    
    protected sealed override void OnVisualLayerRender(DrawingContext context)
    {
        base.OnVisualLayerRender(context);

        if (!isFocused)
        {
            return;
        }

        var thickness = Thickness;

        if (pen is null)
        {
            CreatePen(thickness);
        }

        if (geometry is null || RenderSizeHasChanged)
        {
            geometry = CreateGeometry(thickness, Offset);
        }

        context.DrawGeometry(brush: null, pen, geometry);
    }

    protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
    {
        base.OnVisualChildrenChanged(visualAdded, visualRemoved);

        if (!IsLoaded || isCreatedThroughTemplate)
        {
            return;
        }

        if (visualRemoved is UIElement oldElement)
        {
            UnsubscribedFromEvents(oldElement);
            SetIsFocused(false);
        }

        if (visualAdded is UIElement newElement)
        {
            SubscribeToEvents(newElement);
            SetIsFocused(newElement.IsFocused);
        }
    }

    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
        base.OnVisualParentChanged(oldParent);

        if (!IsLoaded || !isCreatedThroughTemplate)
        {
            return;
        }

        if (oldParent is UIElement oldElement)
        {
            UnsubscribedFromEvents(oldElement);
            SetIsFocused(false);
        }

        if (VisualParent is UIElement newElement)
        {
            SubscribeToEvents(newElement);
            SetIsFocused(newElement.IsFocused);
        }
    }

    private static void OnStrokeChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        (element as FocusIndicator)?.InvalidatePen();

    private static void OnDefiningGeometryChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        (element as FocusIndicator)?.InvalidateGeometry();

    private static void OnOffsetChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        (element as FocusIndicator)?.InvalidateGeometry();

    private static void OnThicknessChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        if (element is FocusIndicator indicator)
        {
            indicator.InvalidatePen();
            indicator.InvalidateGeometry();
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        if (TemplatedParent is UIElement parent)
        {
            SubscribeToEvents(parent);
            SetIsFocused(parent.IsFocused);

            isCreatedThroughTemplate = true;
        }
        else if (Child is not null)
        {
            SubscribeToEvents(Child);
            SetIsFocused(Child.IsFocused);
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;

        if (isCreatedThroughTemplate && TemplatedParent is UIElement parent)
        {
            UnsubscribedFromEvents(parent);
        }
        else if (Child is not null)
        {
            UnsubscribedFromEvents(Child);
        }
    }

    private void SubscribeToEvents(UIElement element)
    {
        element.GotFocus += OnSubscribedElementGotFocus;
        element.LostFocus += OnSubscribedElementLostFocus;
    }

    private void UnsubscribedFromEvents(UIElement element)
    {
        element.GotFocus -= OnSubscribedElementGotFocus;
        element.LostFocus -= OnSubscribedElementLostFocus;
    }

    private void OnSubscribedElementGotFocus(object sender, RoutedEventArgs e) => UpdateFocusState();

    private void OnSubscribedElementLostFocus(object sender, RoutedEventArgs e) => SetIsFocused(false);

    private void UpdateFocusState()
    {
        var inputDevice = InputManager.Current.MostRecentInputDevice;
        SetIsFocused(inputDevice is null || (Device, inputDevice) switch
        {
            (FocusDevice.Keyboard, KeyboardDevice) => true,
            (FocusDevice.Mouse, MouseDevice) => true,
            _ => false
        });
    }

    private void SetIsFocused(bool value)
    {
        if (isFocused == value)
        {
            return;
        }

        isFocused = value;

        InvalidateVisual();
    }

    private void InvalidatePen() => pen = null;

    private void InvalidateGeometry() => geometry = null;

    private void CreatePen(double thickness)
    {
        pen = new Pen(Brush, thickness);
        pen.Freeze();
    }

    private Geometry CreateGeometry(double thickness, double offset)
    {
        var shape = Shape;
        if (shape is null || shape.IsEmpty())
        {
            shape = new RectangleGeometry(new Rect(RenderSize));
        }

        if (shape.IsFrozen)
        {
            shape = shape.Clone();
        }

        var width = shape.Bounds.Width;
        var height = shape.Bounds.Height;

        // Scale to include the thickness and offset.
        shape.Transform = new ScaleTransform(
            scaleX: 1.0 + (thickness + offset * 2) / width,
            scaleY: 1.0 + (thickness + offset * 2) / height,
            centerX: width * 0.5,
            centerY: height * 0.5);

        shape.Freeze();

        return shape;
    }
}