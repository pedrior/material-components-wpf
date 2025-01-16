using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Material.Components.Components;

namespace Material.Components.Controls;

/// <summary>
/// Represents an icon element that displays a symbol with customizable style, weight, and fill options,
/// using Google's Material Symbols font set.
/// </summary>
/// <remarks>
/// The <see cref="SymbolIcon"/> element renders icons from the Google's Material Symbols font family, 
/// providing a wide range of icons with adjustable visual styles (<see cref="SymbolStyle"/>), 
/// stroke weights (<see cref="SymbolWeight"/>), and fill behaviors (<see cref="Fill"/>).
/// </remarks>
public sealed class SymbolIcon : IconElement
{
    /// <summary>
    /// Identifies the <see cref="Symbol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
        nameof(Symbol),
        typeof(Symbol),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(
            (Symbol)NotDefinedSymbol,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnSymbolChanged));

    /// <summary>
    /// Identifies the <see cref="Kind"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind),
        typeof(SymbolStyle),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(
            SymbolStyle.Rounded,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnKindChanged));

    /// <summary>
    /// Identifies the <see cref="Weight"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WeightProperty = DependencyProperty.Register(
        nameof(Weight),
        typeof(SymbolWeight),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(
            SymbolWeight.Regular,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnWeightChanged));

    /// <summary>
    /// Identifies the <see cref="Fill"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
        nameof(Fill),
        typeof(bool),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, OnFillChanged));

    /// <summary>
    /// Identifies the <see cref="RenderInvalidSymbol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RenderInvalidSymbolProperty = DependencyProperty.Register(
        nameof(RenderInvalidSymbol),
        typeof(bool),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

    internal const ushort NotDefinedSymbol = 0;

    private const string RoundedSymbolsFontResourceKey = "MaterialFontSymbolsRounded";
    private const string RoundedFilledSymbolsFontResourceKey = "MaterialFontSymbolsRoundedFilled";
    private const string OutlinedSymbolsFontResourceKey = "MaterialFontSymbolsOutlined";
    private const string OutlinedFilledSymbolsFontResourceKey = "MaterialFontSymbolsOutlinedFilled";
    private const string SharpSymbolsFontResourceKey = "MaterialFontSymbolsSharp";
    private const string SharpFilledSymbolsFontResourceKey = "MaterialFontSymbolsSharpFilled";

    private readonly float pixelsPerDip;

    private GlyphRun? glyph;
    private FontFamily symbolsFont;

    private Size? lastRenderSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="SymbolIcon"/> class.
    /// </summary>
    public SymbolIcon()
    {
        // Kind = SymbolStyle.Rounded; Fill = false;
        symbolsFont = (FontFamily)FindResource(RoundedSymbolsFontResourceKey);

        pixelsPerDip = (float)VisualTreeHelper.GetDpi(this).PixelsPerDip;
    }

    /// <summary>
    /// Gets or sets the symbol displayed by the icon.
    /// </summary>
    /// <value>
    /// A <see cref="Symbol"/> value representing the symbol to render. 
    /// The default value is a <c>NotDefinedSymbol</c>, an invalid symbol.
    /// </value>
    /// <remarks>
    /// Changing this property will update the displayed symbol. If the symbol is invalid 
    /// and <see cref="RenderInvalidSymbol"/> is <see langword="false"/>, the icon will not render.
    /// </remarks>
    [Bindable(true)]
    [Category("Common")]
    public Symbol Symbol
    {
        get => (Symbol)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// Gets or sets the visual style of the symbol.
    /// </summary>
    /// <value>
    /// A <see cref="SymbolStyle"/> value that defines the shape and design of the symbol. 
    /// The default value is <see cref="SymbolStyle.Rounded"/>.
    /// </value>
    /// <remarks>
    /// This property allows switching between different visual styles such as 
    /// <see cref="SymbolStyle.Rounded"/>, <see cref="SymbolStyle.Outlined"/>, and <see cref="SymbolStyle.Sharp"/>. 
    /// </remarks>
    [Bindable(true)]
    [Category("Appearance")]
    public SymbolStyle Kind
    {
        get => (SymbolStyle)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke weight of the symbol.
    /// </summary>
    /// <value>
    /// A <see cref="SymbolWeight"/> value that controls the thickness of the symbol's stroke. 
    /// The default value is <see cref="SymbolWeight.Regular"/>.
    /// </value>
    /// <remarks>
    /// The <see cref="Weight"/> property allows you to adjust how bold or thin the symbol appears. 
    /// It supports values from <see cref="SymbolWeight.Thin"/> to <see cref="SymbolWeight.Bold"/>.
    /// </remarks>
    [Bindable(true)]
    [Category("Appearance")]
    public SymbolWeight Weight
    {
        get => (SymbolWeight)GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the symbol should be in a filled style.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the symbol should attempt to render with a filled style; 
    /// <see langword="false"/> if it should render with its standard style.
    /// The default value is <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// The behavior of the <see cref="Fill"/> property depends on the specific symbol being rendered. 
    /// Not all symbols support filled style. Setting this property to <see langword="true"/> 
    /// enables a filled style if the symbol can support, while setting it to <see langword="false"/> falls back
    /// to the standard style. This property is useful for customizing the appearance of the icon during
    /// runtime or for providing additional visual feedback to the user based on the current state of the UI.
    /// rendering.
    /// </remarks>
    [Bindable(true)]
    [Category("Appearance")]
    public bool Fill
    {
        get => (bool)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether an invalid symbol should be rendered.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to render a fallback symbol when an invalid symbol is specified; 
    /// <see langword="false"/> to hide the icon when the symbol is invalid. 
    /// The default value is <see langword="true"/>.
    /// </value>
    /// <remarks>
    /// When set to <see langword="true"/>, the icon will display a placeholder glyph if the 
    /// specified <see cref="Symbol"/> is invalid. This can be useful for debugging or indicating 
    /// missing icons in the UI. An invalid symbol is typically a glyph index that does not exist
    /// in the font being used.
    /// </remarks>
    [Bindable(true)]
    [Category("Common")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool RenderInvalidSymbol
    {
        get => (bool)GetValue(RenderInvalidSymbolProperty);
        set => SetValue(RenderInvalidSymbolProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var codepoint = (ushort)Symbol;
        if (codepoint is NotDefinedSymbol && !RenderInvalidSymbol)
        {
            IsRendered = false;
            return;
        }

        var background = Background;
        var foreground = Foreground;

        if (background is null && foreground is null)
        {
            IsRendered = false;
            return;
        }

        var size = RenderSize;

        if (IsGlyphInvalidated(size))
        {
            var (builtGlyph, isNotDefGlyph) = BuildGlyphRun(codepoint, size);
            if (isNotDefGlyph && !RenderInvalidSymbol)
            {
                IsRendered = false;
                return;
            }

            glyph = builtGlyph;
        }

        if (background is not null)
        {
            context.DrawRectangle(background, null, new Rect(size));
        }

        context.DrawGlyphRun(foreground, glyph);

        lastRenderSize = size;
        IsRendered = true;
    }

    private static void OnSymbolChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((SymbolIcon)element).InvalidateGlyph();

    private static void OnKindChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        if (element is SymbolIcon icon)
        {
            icon.InvalidateGlyph();
            icon.UpdateSymbolsFont();
        }
    }

    private static void OnWeightChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((SymbolIcon)element).InvalidateGlyph();

    private static void OnFillChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        if (element is SymbolIcon icon)
        {
            icon.InvalidateGlyph();
            icon.UpdateSymbolsFont();
        }
    }

    private bool IsGlyphInvalidated(Size size) =>
        glyph is null || (lastRenderSize.HasValue && lastRenderSize.Value != size);

    private void UpdateSymbolsFont()
    {
        var resourceKey = (Kind, Fill) switch
        {
            (SymbolStyle.Rounded, false) => RoundedSymbolsFontResourceKey,
            (SymbolStyle.Rounded, true) => RoundedFilledSymbolsFontResourceKey,
            (SymbolStyle.Outlined, false) => OutlinedSymbolsFontResourceKey,
            (SymbolStyle.Outlined, true) => OutlinedFilledSymbolsFontResourceKey,
            (SymbolStyle.Sharp, false) => SharpSymbolsFontResourceKey,
            (SymbolStyle.Sharp, true) => SharpFilledSymbolsFontResourceKey,
            _ => throw new InvalidOperationException(
                "Unable to determine the symbols font family based on the current configuration.")
        };

        symbolsFont = (FontFamily)FindResource(resourceKey);
    }

    private void InvalidateGlyph() => glyph = null;

    private (GlyphRun builtGlyph, bool isNotDefGlyph) BuildGlyphRun(ushort codepoint, Size size)
    {
        var typeface = new Typeface(
            symbolsFont,
            FontStyles.Normal,
            FontWeight.FromOpenTypeWeight((int)Weight),
            FontStretches.Normal);

        _ = typeface.TryGetGlyphTypeface(out var glyphTypeface);

        var glyphIndex = glyphTypeface.CharacterToGlyphMap.TryGetValue(codepoint, out var index)
            ? index
            : NotDefinedSymbol;

        var renderingSize = Math.Min(size.Width, size.Height);
        var baselineOrigin = new Point(
            x: (size.Width - renderingSize) * 0.5,
            y: (size.Height + renderingSize) * 0.5);

        var builtGlyph = new GlyphRun(
            glyphTypeface: glyphTypeface,
            renderingEmSize: renderingSize,
            pixelsPerDip: pixelsPerDip,
            glyphIndices: [glyphIndex],
            baselineOrigin: baselineOrigin,
            advanceWidths: [renderingSize],
            bidiLevel: 0,
            isSideways: false,
            glyphOffsets: null,
            characters: null,
            deviceFontName: null,
            clusterMap: null,
            caretStops: null,
            language: null);

        return (builtGlyph, glyphIndex is NotDefinedSymbol);
    }
}