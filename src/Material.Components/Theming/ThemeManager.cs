using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Material.Components.Assets;
using Material.Components.Theming.XAML;

namespace Material.Components.Theming;

/// <summary>
/// Manages the application's theme by maintaining the theme, mode, and associated resources.
/// This class implements the singleton pattern to ensure a single instance is used throughout the application.
/// </summary>
public sealed class ThemeManager
{
    private const string FontResourceKeyFormat = "MaterialFont{0}";
    private const string ColorResourceKeyFormat = "MaterialColor{0}";

    private static readonly Lazy<ThemeManager> LazyInstance = new(
        () => new ThemeManager(),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private ThemeDefinition appliedTheme = new UndefinedTheme();
    private ThemeMode appliedThemeMode = ThemeMode.Undefined;

    private bool isPendingUpdate;
    private ThemeDictionary? themeDictionary;

    private ThemeManager()
    {
    }

    /// <summary>
    /// Gets the singleton instance of the <see cref="ThemeManager"/> class.
    /// </summary>
    /// <remarks>
    /// The singleton instance is created lazily and is thread-safe.
    /// </remarks>
    public static ThemeManager Instance => LazyInstance.Value;

    /// <summary>
    /// Gets a value indicating whether the theme manager has been initialized.
    /// </summary>
    /// <remarks>
    /// Returns <see langword="true"/> if the theme manager has been initialized; otherwise, <see langword="false"/>.
    /// Initialization is required before the theme and theme mode can be fully applied.
    /// </remarks>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// Gets or sets the application theme.
    /// </summary>
    /// <remarks>
    /// If the theme manager has not been initialized, setting this property defers the update
    /// until initialization is complete. This property may return an <see cref="UndefinedTheme"/>
    /// if the theme has not yet been applied.
    /// </remarks>
    /// <value>
    /// The current <see cref="ThemeDefinition"/> applied to the application.
    /// </value>
    public ThemeDefinition Theme
    {
        get => appliedTheme;
        set
        {
            if (IsInitialized)
            {
                if (value != appliedTheme)
                {
                    UpdateApplicationTheme(value, appliedThemeMode);
                }
            }
            else
            {
                isPendingUpdate = true;
            }

            appliedTheme = value;
        }
    }

    /// <summary>
    /// Gets or sets the application theme mode.
    /// </summary>
    /// <remarks>
    /// If the theme manager has not been initialized, setting this property defers the update
    /// until initialization is complete. This property may return <see cref="ThemeMode.Undefined"/>
    /// if the theme has not yet been applied.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown if attempting to set the theme mode to <see cref="ThemeMode.Undefined"/>.
    /// </exception>
    /// <value>
    /// The current <see cref="ThemeMode"/> applied to the application.
    /// </value>
    public ThemeMode ThemeMode
    {
        get => appliedThemeMode;
        set
        {
            if (value is ThemeMode.Undefined)
            {
                throw new ArgumentException($"The theme cannot be set to {ThemeMode.Undefined}.");
            }

            if (IsInitialized)
            {
                if (value != appliedThemeMode)
                {
                    UpdateApplicationTheme(appliedTheme, value);
                }
            }
            else
            {
                isPendingUpdate = true;
            }

            appliedThemeMode = value;
        }
    }

    internal void Initialize(ThemeDictionary dictionary)
    {
        if (IsInitialized)
        {
            throw new InvalidOperationException("There was an attempt to initialize the theme system more than once, " +
                                                $"which is not allowed. Make sure that the {nameof(ThemeDictionary)} " +
                                                "type, which is responsible for initializing the theme system, is " +
                                                "only instantiated once in the application.");
        }

        themeDictionary = dictionary;
        IsInitialized = true;

        if (isPendingUpdate)
        {
            UpdateApplicationTheme(appliedTheme, appliedThemeMode);
            isPendingUpdate = false;
        }

        PrepareRequiredResources();
    }

    private void PrepareRequiredResources()
    {
        InsertRobotoFontResource();
        InsertSymbolsFontResources();
        InsertXamlResources();
    }

    private void InsertXamlResources()
    {
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("Button.xaml") });
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("Container.xaml") });
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("Divider.xaml") });
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("FocusIndicator.xaml") });
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("Ripple.xaml") });
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("StateLayer.xaml") });
        MergeResourceDictionary(new ResourceDictionary { Source = XamlHelper.MakeUri("SymbolIcon.xaml") });
    }

    private void InsertRobotoFontResource()
    {
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "Roboto"),
            ThemeDefinition.RobotoFontFamily);
    }

    private void InsertSymbolsFontResources()
    {
        var baseSymbolsFontUri = AssetsHelper.MakeUri("Fonts/MaterialSymbols/");

        // Rounded
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "SymbolsRounded"),
            new FontFamily(baseSymbolsFontUri, "./#Material Symbols Rounded"));

        // Rounded Filled
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "SymbolsRoundedFilled"),
            new FontFamily(baseSymbolsFontUri, "./#Material Symbols Rounded Filled"));

        // Outlined
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "SymbolsOutlined"),
            new FontFamily(baseSymbolsFontUri, "./#Material Symbols Outlined"));

        // Outlined Filled
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "SymbolsOutlinedFilled"),
            new FontFamily(baseSymbolsFontUri, "./#Material Symbols Outlined Filled"));

        // Sharp
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "SymbolsSharp"),
            new FontFamily(baseSymbolsFontUri, "./#Material Symbols Sharp"));

        // Sharp Filled
        InsertOrUpdateResource(
            string.Format(FontResourceKeyFormat, "SymbolsSharpFilled"),
            new FontFamily(baseSymbolsFontUri, "./#Material Symbols Sharp Filled"));
    }

    private void UpdateApplicationTheme(ThemeDefinition theme, ThemeMode themeMode)
    {
        if (theme.IsUndefined || themeMode is ThemeMode.Undefined)
        {
            return;
        }

        UpdateFontResources(theme);
        UpdateColorResources(theme, themeMode);
    }

    private void UpdateFontResources(ThemeDefinition theme)
    {
        var largeTextFontFamily = theme.LargeTextFontFamily;
        if (!largeTextFontFamily.Equals(appliedTheme.LargeTextFontFamily))
        {
            InsertOrUpdateResource(string.Format(FontResourceKeyFormat, "Large"), largeTextFontFamily);
        }

        var normalTextFontFamily = theme.NormalTextFontFamily;
        if (!normalTextFontFamily.Equals(appliedTheme.NormalTextFontFamily))
        {
            InsertOrUpdateResource(string.Format(FontResourceKeyFormat, "Normal"), normalTextFontFamily);
        }
    }

    private void UpdateColorResources(ThemeDefinition theme, ThemeMode themeMode)
    {
        var colorScheme = theme.GetColorScheme(themeMode);
        var appliedColorScheme = appliedTheme.GetColorScheme(appliedThemeMode);

        var schemeColors = colorScheme.ListColors();
        var appliedSchemeColors = appliedColorScheme.ListColors();

        for (var index = 0; index < schemeColors.Length; index++)
        {
            var (name, color) = schemeColors[index];
            if (!appliedColorScheme.IsEmpty && color == appliedSchemeColors[index].Color)
            {
                continue;
            }
            
            var brush = new SolidColorBrush(color);
            
            brush.Freeze();

            InsertOrUpdateResource(string.Format(ColorResourceKeyFormat, name), brush);
        }
    }

    private void MergeResourceDictionary(ResourceDictionary dictionary) => 
        themeDictionary!.MergedDictionaries.Add(dictionary);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void InsertOrUpdateResource(object key, object? value) => themeDictionary![key] = value;
}