namespace Material.Components.Controls;

/// <summary>
/// Specifies the stroke weight of a symbol, with a range of predefined weights from thin (100) to bold (700).
/// </summary>
/// <remarks>
/// The <see cref="SymbolWeight"/> enum allows for specifying the visual stroke thickness of a symbol. 
/// Heavier weights result in bolder symbols, while lighter weights create thinner symbols. 
/// The weight can also influence the overall size and visual emphasis of the symbol.
/// </remarks>
public enum SymbolWeight
{
    /// <summary>
    /// Thin weight, with a stroke thickness of 100.
    /// </summary>
    Thin = 100,

    /// <summary>
    /// Extra light weight, with a stroke thickness of 200.
    /// </summary>
    ExtraLight = 200,

    /// <summary>
    /// Light weight, with a stroke thickness of 300.
    /// </summary>
    Light = 300,

    /// <summary>
    /// Regular weight, with a stroke thickness of 400.
    /// </summary>
    Regular = 400,

    /// <summary>
    /// Medium weight, with a stroke thickness of 500.
    /// </summary>
    Medium = 500,

    /// <summary>
    /// Semi-bold weight, with a stroke thickness of 600.
    /// </summary>
    SemiBold = 600,

    /// <summary>
    /// Bold weight, with a stroke thickness of 700.
    /// </summary>
    Bold = 700
}