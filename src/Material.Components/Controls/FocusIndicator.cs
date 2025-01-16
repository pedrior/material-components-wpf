using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Represents a visual indicator that highlights a UI element when it receives focus.
/// </summary>
/// <remarks>
/// The <see cref="FocusIndicator"/> provides a customizable focus effect for UI elements. 
/// It displays a custom indicator around the focused element to give users clear visual feedback. 
/// </remarks>
public class FocusIndicator : DrawableContainer
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            Brushes.Black,
            FrameworkPropertyMetadataOptions.AffectsRender |
            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
            OnBrushChanged));

    /// <summary>
    /// Identifies the <see cref="DefiningGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefiningGeometryProperty = DependencyProperty.Register(
        nameof(DefiningGeometry),
        typeof(Geometry),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            Geometry.Empty,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnDefiningGeometryChanged));

    /// <summary>
    /// Identifies the <see cref="Padding"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
        nameof(Padding),
        typeof(double),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            4.0,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnPaddingChanged,
            CoercePadding),
        ValidatePadding);

    /// <summary>
    /// Identifies the <see cref="Thickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness),
        typeof(double),
        typeof(FocusIndicator),
        new FrameworkPropertyMetadata(
            2.0,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnThicknessChanged,
            CoerceThickness),
        ValidateThickness);

    /// <summary>
    /// Identifies the <see cref="Device"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DeviceProperty = DependencyProperty.Register(
        nameof(Device),
        typeof(FocusDevice),
        typeof(FocusIndicator),
        new PropertyMetadata(FocusDevice.Keyboard));

    private bool gotFocus;

    private Pen? borderPen;
    private Geometry? renderGeometry;

    private bool isCreatedThroughTemplate;

    static FocusIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FocusIndicator),
            new FrameworkPropertyMetadata(typeof(FocusIndicator)));
    }

    public FocusIndicator()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        VisualLayer.IsHitTestVisible = false;
    }

    /// <summary>
    /// Gets or sets the brush used to paint the focus indicator.
    /// </summary>
    /// <remarks>
    /// The <see cref="Brush"/> property determines the color or gradient used for the focus indicator.  
    /// This allows customization of the indicator's appearance to match the application's theme.  
    /// If not set, the default color is <see cref="Brushes.Black"/>.
    /// </remarks>
    /// <value>
    /// A <see cref="Brush"/> that defines the color of the focus indicator.  
    /// The default value is <see cref="Brushes.Black"/>.
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? Brush
    {
        get => (Brush?)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the geometric shape used to define the focus indicator shape.
    /// </summary>
    /// <remarks>
    /// By default, the focus indicator uses a rectangular shape.  
    /// You can customize this shape by providing a different <see cref="Geometry"/>.  
    /// If not set, the focus indicator uses <see cref="Geometry.Empty"/>.
    /// </remarks>
    /// <value>
    /// A <see cref="Geometry"/> object that defines the shape of the focus indicator.  
    /// The default value is <see cref="Geometry.Empty"/>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public Geometry? DefiningGeometry
    {
        get => (Geometry?)GetValue(DefiningGeometryProperty);
        set => SetValue(DefiningGeometryProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the focus indicator..
    /// </summary>
    /// <remarks>
    /// The <see cref="Thickness"/> property defines the width of the focus border.  
    /// A thicker border makes the focus indicator more prominent.  
    /// This property must be a non-negative value.
    /// </remarks>
    /// <value>
    /// A <see cref="double"/> specifying the border thickness.  
    /// The default value is <c>2.0</c>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the padding between the focused element and the focus indicator.
    /// </summary>
    /// <remarks>
    /// The <see cref="Padding"/> property defines the space between the focus indicator and the element it surrounds.  
    /// This property can be used to offset the indicator for design consistency.  
    /// Must be a non-negative value.
    /// </remarks>
    /// <value>
    /// A <see cref="double"/> specifying the padding between the focus indicator and the focused element. 
    /// The default value is <c>4.0</c>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public double Padding
    {
        get => (double)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets the input device type that can trigger the focus indicator.
    /// </summary>
    /// <remarks>
    /// The <see cref="Device"/> property specifies whether the focus indicator responds to focus changes 
    /// triggered an input device.  
    /// By default, the focus indicator responds only to keyboard interactions.
    /// </remarks>
    /// <value>
    /// A <see cref="FocusDevice"/> value that determines which input devices can activate the focus indicator.  
    /// The default value is <see cref="FocusDevice.Keyboard"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public FocusDevice Device
    {
        get => (FocusDevice)GetValue(DeviceProperty);
        set => SetValue(DeviceProperty, value);
    }

    protected override DrawableContainerOrder Order => DrawableContainerOrder.ChildThenVisual;

    protected sealed override void OnVisualLayerRender(DrawingContext context)
    {
        base.OnVisualLayerRender(context);

        if (!gotFocus)
        {
            // By returning early, we avoid rendering the indicator.
            return;
        }

        var thickness = Thickness;

        if (borderPen is null)
        {
            EnsureBorderPen(thickness);
        }

        if (renderGeometry is null || RenderSizeHasChanged)
        {
            EnsureRenderGeometry(Padding, thickness);
        }

        context.DrawGeometry(brush: null, borderPen, renderGeometry);
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
        }

        if (visualAdded is UIElement newElement)
        {
            SubscribeToEvents(newElement);
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
        }

        if (VisualParent is UIElement newElement)
        {
            SubscribeToEvents(newElement);
        }
    }

    internal static object CoercePadding(DependencyObject _, object value) => Math.Max(0.0, (double)value);

    internal static object CoerceThickness(DependencyObject _, object value) => Math.Max(0.0, (double)value);

    internal static bool ValidatePadding(object value)
    {
        var padding = (double)value;
        return !double.IsNaN(padding) && !double.IsInfinity(padding);
    }

    internal static bool ValidateThickness(object value)
    {
        var thickness = (double)value;
        return !double.IsNaN(thickness) && !double.IsInfinity(thickness);
    }

    private static void OnBrushChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((FocusIndicator)element).InvalidateBorderPen();

    private static void OnDefiningGeometryChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((FocusIndicator)element).InvalidateRenderGeometry();

    private static void OnPaddingChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((FocusIndicator)element).InvalidateRenderGeometry();

    private static void OnThicknessChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        if (element is FocusIndicator indicator)
        {
            indicator.InvalidateBorderPen();
            indicator.InvalidateRenderGeometry();
        }
    }

    private static bool CheckInputDeviceAgainstFocusDevice(FocusDevice device, InputDevice? input) => device switch
    {
        FocusDevice.Keyboard => input is KeyboardDevice,
        FocusDevice.Mouse => input is MouseDevice,
        _ => true // focused programmatically
    };

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        if (TemplatedParent is UIElement parent)
        {
            SubscribeToEvents(parent);
            isCreatedThroughTemplate = true;
        }
        else if (Child is not null)
        {
            SubscribeToEvents(Child);
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

    private void OnSubscribedElementGotFocus(object sender, RoutedEventArgs e)
    {
        var mostRecentInputDevice = InputManager.Current.MostRecentInputDevice;
        SetGotFocus(CheckInputDeviceAgainstFocusDevice(Device, mostRecentInputDevice));
    }

    private void OnSubscribedElementLostFocus(object sender, RoutedEventArgs e) => SetGotFocus(false);

    private void SetGotFocus(bool value)
    {
        if (gotFocus == value)
        {
            return;
        }

        gotFocus = value;

        InvalidateVisual();
    }

    private void InvalidateBorderPen() => borderPen = null;

    private void InvalidateRenderGeometry() => renderGeometry = null;

    private void EnsureBorderPen(double thickness)
    {
        borderPen = new Pen(Brush, thickness);
        borderPen.Freeze();
    }

    private void EnsureRenderGeometry(double padding, double thickness)
    {
        var geometry = DefiningGeometry;
        if (geometry is null || geometry.IsEmpty())
        {
            geometry = new RectangleGeometry(new Rect(RenderSize));
        }

        if (geometry.IsFrozen)
        {
            geometry = geometry.Clone();
        }

        var width = geometry.Bounds.Width;
        var height = geometry.Bounds.Height;

        // Inflate the geometry by scaling
        geometry.Transform = new ScaleTransform(
            scaleX: 1.0 + (thickness + padding) / width,
            scaleY: 1.0 + (thickness + padding) / height,
            centerX: width * 0.5,
            centerY: height * 0.5);

        geometry.Freeze();

        renderGeometry = geometry;
    }
}