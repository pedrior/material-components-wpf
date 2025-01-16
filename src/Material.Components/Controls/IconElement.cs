using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Material.Components.Controls;

/// <summary>
/// Represents a base class for icon element controls, providing common properties for 
/// customizing the appearance of icons, such as background, foreground, and fill behavior.
/// </summary>
public abstract class IconElement : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Background"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
        nameof(Background),
        typeof(Brush),
        typeof(IconElement),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Foreground"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        nameof(Foreground),
        typeof(Brush),
        typeof(IconElement),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="AutoCollapse"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AutoCollapseProperty = DependencyProperty.Register(
        nameof(AutoCollapse),
        typeof(bool),
        typeof(IconElement),
        new PropertyMetadata(true, OnAutoCollapseChanged));

    private static readonly DependencyPropertyKey IsRenderedPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsRendered),
        typeof(bool),
        typeof(IconElement),
        new PropertyMetadata(false, OnIsRenderedChanged));

    /// <summary>
    /// Identifies the <see cref="IsRendered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsRenderedProperty = IsRenderedPropertyKey.DependencyProperty;

    /// <summary>
    /// Initializes a new instance of the <see cref="IconElement"/> class.
    /// </summary>
    protected IconElement()
    {
        Loaded += OnLoaded;
    }

    /// <summary>
    /// Gets or sets the background brush of the icon element.
    /// </summary>
    /// <value>
    /// A <see cref="Brush"/> that defines the background of the icon. 
    /// The default value is <see langword="null"/>, which indicates no background.
    /// </value>
    /// <remarks>
    /// The <see cref="Background"/> property allows you to specify a brush that fills the background area of the icon.
    /// Use this property to apply solid colors, gradients, or other brush types to the icon's background.
    /// </remarks>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? Background
    {
        get => (Brush?)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the foreground brush of the icon element.
    /// </summary>
    /// <value>
    /// A <see cref="Brush"/> that defines the foreground of the icon. 
    /// The default value is <see cref="Brushes.Black"/>.
    /// </value>
    /// <remarks>
    /// The <see cref="Foreground"/> property allows you to specify a brush that determines the color of the
    /// icon itself. Use this property to customize the stroke or main visual representation of the icon.
    /// </remarks>
    [Bindable(true)]
    [Category("Brush")]
    public Brush? Foreground
    {
        get => (Brush?)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the icon element should automatically collapse 
    /// when it is not rendered.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the icon element should automatically set its <see cref="Visibility"/> 
    /// to <see cref="Visibility.Collapsed"/> when it is not rendered; otherwise, <see langword="false"/>. 
    /// The default value is <see langword="true"/>.
    /// </value>
    /// <remarks>
    /// When <see cref="AutoCollapse"/> is enabled and <see cref="IsRendered"/> is <see langword="false"/>, 
    /// the icon element's <see cref="Visibility"/> property will automatically be set to 
    /// <see cref="Visibility.Collapsed"/>. This behavior is useful for optimizing UI layouts by 
    /// hiding elements that are not being drawn.
    /// </remarks>
    [Bindable(true)]
    [Category("Common")]
    public bool AutoCollapse
    {
        get => (bool)GetValue(AutoCollapseProperty);
        set => SetValue(AutoCollapseProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the icon element is currently rendered in the UI.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the icon is rendered; otherwise, <see langword="false"/>.
    /// The default value depends on the rendering lifecycle of the icon element.
    /// </value>
    /// <remarks>
    /// The <see cref="IsRendered"/> property is updated by the inherited classes to indicate whether 
    /// the icon itself is currently rendered in the UI.
    /// </remarks>
    [Bindable(true)]
    [Category("Common")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool IsRendered
    {
        get => (bool)GetValue(IsRenderedProperty);
        protected set => SetValue(IsRenderedPropertyKey, value);
    }

    private static void OnAutoCollapseChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((IconElement)element).UpdateCollapseBehavior();

    private static void OnIsRenderedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((IconElement)element).UpdateCollapseBehavior();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        UpdateCollapseBehavior();
    }

    private void UpdateCollapseBehavior()
    {
        Visibility = AutoCollapse && !IsRendered
            ? Visibility.Collapsed
            : Visibility.Visible;
    }
}