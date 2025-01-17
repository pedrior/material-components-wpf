using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Material.Components.Motion;

namespace Material.Components.Controls;

/// <summary>
/// Represents a control that provides a ripple effect in response to user interactions.
/// </summary>
/// <remarks>
/// The <see cref="Ripple"/> control creates a circular wave that expands outward from the interaction point, 
/// simulating the behavior of ripples on water. It is commonly used to provide visual feedback for user interactions.
/// </remarks>
public class Ripple : DrawableContainer
{
    /// <summary>
    /// Identifies the <see cref="Tint"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TintProperty = DependencyProperty.Register(
        nameof(Tint),
        typeof(Brush),
        typeof(Ripple),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender |
            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
            propertyChangedCallback: null,
            CoerceTint));

    /// <summary>
    /// Identifies the <see cref="Animate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AnimateProperty = DependencyProperty.Register(
        nameof(Animate),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="IsCentered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCenteredProperty = DependencyProperty.Register(
        nameof(IsCentered),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsUnbounded"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsUnboundedProperty = DependencyProperty.Register(
        nameof(IsUnbounded),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(false, OnIsUnboundedChanged));

    /// <summary>
    /// Identifies the <see cref="DefiningGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefiningGeometryProperty = DependencyProperty.Register(
        nameof(DefiningGeometry),
        typeof(Geometry),
        typeof(Ripple),
        new PropertyMetadata(null, OnDefiningGeometryChanged));

    /// <summary>
    /// Identifies the <see cref="EnableRightClick"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EnableRightClickProperty = DependencyProperty.Register(
        nameof(EnableRightClick),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(false));

    private const double DefaultOpacity = 0.1;

    private readonly Geometry geometry = new EllipseGeometry(new Point(0, 0), 1, 1);

    private readonly ScaleTransform scaleTransform = new();
    private readonly TranslateTransform translateTransform = new();

    private readonly DoubleAnimation enterScaleAnimation = new()
    {
        Duration = AnimationDurations.Medium300,
        EasingFunction = AnimationEasings.Standard
    }; // Do not freeze.

    private readonly DoubleAnimation exitOpacityAnimation = new()
    {
        To = 0.0,
        Duration = AnimationDurations.Medium300,
        EasingFunction = AnimationEasings.Standard
    };

    private bool isRippling;
    private bool isMouseDown;
    private bool isCompleted;
    private bool isPendingStop;

    private bool isCreatedThroughTemplate;

    /// <summary>
    /// Initializes static members of the <see cref="Ripple"/> class.
    /// </summary>
    static Ripple()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Ripple),
            new FrameworkPropertyMetadata(typeof(Ripple)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ripple"/> class.
    /// </summary>
    public Ripple()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        enterScaleAnimation.Completed += OnEnterScaleAnimationCompleted;

        exitOpacityAnimation.Freeze();

        geometry.Transform = new TransformGroup
        {
            Children =
            {
                scaleTransform,
                translateTransform
            }
        };

        VisualLayer.IsHitTestVisible = false;
    }

    /// <summary>
    /// Gets or sets the brush used to paint the ripple effect.
    /// </summary>
    /// <remarks>
    /// This property defines the color or gradient of the ripple. If set to <see langword="null"/>, 
    /// the ripple effect will not be visible.
    /// </remarks>
    /// <value>
    /// A <see cref="Brush"/> that determines the appearance of the ripple. 
    /// The default value is <see langword="null"/>.
    /// </value>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? Tint
    {
        get => (Brush?)GetValue(TintProperty);
        set => SetValue(TintProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect is enabled.
    /// </summary>
    /// <remarks>
    /// When <see langword="true"/>, the ripple animation will play in response to user input. 
    /// If set to <see langword="false"/>, the ripple effect is disabled and no animation will occur.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the ripple effect is enabled; otherwise, <see langword="false"/>.  
    /// The default value is <see langword="true"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public bool Animate
    {
        get => (bool)GetValue(AnimateProperty);
        set => SetValue(AnimateProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect should always originate from the center of the
    /// control.
    /// </summary>
    /// <remarks>
    /// When <see langword="true"/>, the ripple will expand from the center of the control, regardless of the
    /// interaction point. When <see langword="false"/>, the ripple will expand from the interaction point.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> to center the ripple; <see langword="false"/> to start the ripple at the interaction
    /// point.  
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public bool IsCentered
    {
        get => (bool)GetValue(IsCenteredProperty);
        set => SetValue(IsCenteredProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect should be unbounded or clipped to the defined
    /// geometry.
    /// <remarks>
    /// When <see langword="true"/>, the ripple effect will expand beyond the bounds of the control. When
    /// <see langword="false"/>, the ripple effect will be clipped to the bounds of the <see cref="DefiningGeometry"/>.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the ripple effect is unbounded; otherwise, <see langword="false"/>.
    /// The default value is <see langword="false"/>.
    /// </value>
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public bool IsUnbounded
    {
        get => (bool)GetValue(IsUnboundedProperty);
        set => SetValue(IsUnboundedProperty, value);
    }

    /// <summary>
    /// Gets or sets the geometry that defines the shape of the ripple effect.
    /// </summary>
    /// <remarks>
    /// This property defines the shape of the ripple effect. If <see cref="IsUnbounded"/> is set to
    /// <see langword="false"/>, the ripple effect will be clipped to the bounds of this geometry.
    /// </remarks>
    /// <value>
    /// A <see cref="Geometry"/> that defines the shape of the ripple effect.
    /// The default value is <see langword="null"/>.
    /// </value>
    [Bindable(true)]
    [Category("Appearance")]
    public Geometry? DefiningGeometry
    {
        get => (Geometry?)GetValue(DefiningGeometryProperty);
        set => SetValue(DefiningGeometryProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect should respond to right-click interactions.
    /// </summary>
    /// <remarks>
    /// By default, the ripple effect responds only to left-click or touch interactions.  
    /// Setting this property to <see langword="true"/> allows the ripple to be triggered by right-clicks as well.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the ripple responds to right-clicks; otherwise, <see langword="false"/>.  
    /// The default value is <see langword="false"/>.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool EnableRightClick
    {
        get => (bool)GetValue(EnableRightClickProperty);
        set => SetValue(EnableRightClickProperty, value);
    }

    protected override DrawableContainerOrder Order => DrawableContainerOrder.ChildThenVisual;

    private bool CanAnimate => Animate && Tint is not null;

    /// <summary>
    /// Starts the ripple effect at a specific origin.
    /// </summary>
    /// <param name="origin">
    /// The <see cref="Point"/> where the ripple effect should originate. 
    /// Coordinates should be relative to this element.
    /// </param>
    /// <remarks>
    /// This method can be used to programmatically trigger the ripple effect from a specific origin. 
    /// The <paramref name="origin"/> is clamped within the control's bounds.
    /// </remarks>
    public void Start(Point origin)
    {
        if (!CanAnimate)
        {
            return;
        }

        StartInternal(origin with
        {
            X = Math.Clamp(origin.X, 0.0, RenderSize.Width),
            Y = Math.Clamp(origin.Y, 0.0, RenderSize.Height)
        });
    }

    /// <summary>
    /// Stops the ripple effect by fading it out.
    /// </summary>
    /// <remarks>
    /// This method immediately stops the ripple animation. It can be used to programmatically end the ripple effect.
    /// </remarks>
    public void Stop()
    {
        if (isRippling)
        {
            StopInternal();
        }
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

    protected override void OnVisualLayerRender(DrawingContext context)
    {
        base.OnVisualLayerRender(context);

        context.DrawGeometry(Tint, null, geometry);
    }

    private static void OnIsUnboundedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)=> 
        ((Ripple)element).UpdateEffectClipping();

    private static void OnDefiningGeometryChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) => 
        ((Ripple)element).UpdateEffectClipping();

    private static object CoerceTint(DependencyObject element, object value)
    {
        // We're going to animate the tint's opacity, so we need to unfreeze it.
        if (value is not Brush { IsFrozen: true } brush)
        {
            return value;
        }

        brush = brush.CloneCurrentValue();

        brush.Opacity = 0.0; // This matters.

        return brush;
    }

    private void UpdateEffectClipping() => 
        VisualLayer.Clip = IsUnbounded ? null : DefiningGeometry;

    private void SubscribeToEvents(UIElement element)
    {
        element.PreviewMouseDown += OnSubscribedElementMouseDown;
        element.PreviewMouseUp += OnSubscribedElementMouseUp;
        element.MouseLeave += OnSubscribedElementMouseLeave;
    }

    private void UnsubscribedFromEvents(UIElement element)
    {
        element.PreviewMouseDown -= OnSubscribedElementMouseDown;
        element.PreviewMouseUp -= OnSubscribedElementMouseUp;
        element.MouseLeave -= OnSubscribedElementMouseLeave;
    }

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

        UpdateEffectClipping();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;

        enterScaleAnimation.Completed -= OnEnterScaleAnimationCompleted;

        if (isCreatedThroughTemplate && TemplatedParent is UIElement parent)
        {
            UnsubscribedFromEvents(parent);
        }
        else if (Child is not null)
        {
            UnsubscribedFromEvents(Child);
        }
    }

    private void OnSubscribedElementMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!CanAnimate || (e.ChangedButton is MouseButton.Right && !EnableRightClick))
        {
            return;
        }

        isMouseDown = true;

        var location = IsCentered
            ? new Point(RenderSize.Width * 0.5, RenderSize.Height * 0.5)
            : e.GetPosition(this);

        StartInternal(location);
    }

    private void OnSubscribedElementMouseUp(object sender, MouseButtonEventArgs e)
    {
        isMouseDown = false;

        if (isRippling && isPendingStop)
        {
            StopInternal();
        }
    }

    private void OnSubscribedElementMouseLeave(object sender, MouseEventArgs e)
    {
        if (isRippling && isMouseDown)
        {
            StopInternal();
        }
    }

    private void OnEnterScaleAnimationCompleted(object? sender, EventArgs e)
    {
        if (isCompleted)
        {
            return;
        }

        isCompleted = true;

        if (isMouseDown)
        {
            isPendingStop = true;
        }
        else
        {
            StopInternal();
        }
    }

    private void StartInternal(Point origin)
    {
        isRippling = true;
        isCompleted = false;

        Translate(origin);

        SetTintOpacity(DefaultOpacity);

        Scale(GetDesiredScale(origin));
    }

    private void StopInternal()
    {
        isRippling = false;
        isPendingStop = false;

        FadeOut();
    }

    private void SetTintOpacity(double opacity)
    {
        if (Tint is not { } tint)
        {
            return;
        }

        // The exit animation may be holding the value
        tint.BeginAnimation(Brush.OpacityProperty, null);

        tint.Opacity = opacity;
    }

    private double GetDesiredScale(Point origin)
    {
        // The desired scale for the ripple effect is calculated based on the distance between the interaction point
        // and the farthest corner of the control. This ensures that the ripple effect always fills the control.

        var hDist = Math.Max(origin.X, RenderSize.Width - origin.X);
        var vDist = Math.Max(origin.Y, RenderSize.Height - origin.Y);

        return Math.Sqrt(hDist * hDist + vDist * vDist);
    }

    private void Translate(Point origin)
    {
        translateTransform.X = origin.X;
        translateTransform.Y = origin.Y;
    }

    private void Scale(double scale)
    {
        enterScaleAnimation.To = scale;
        enterScaleAnimation.From = scale * 0.2; // I think this is a good starting point.

        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, enterScaleAnimation);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, enterScaleAnimation);
    }

    private void FadeOut() => Tint?.BeginAnimation(Brush.OpacityProperty, exitOpacityAnimation);
}