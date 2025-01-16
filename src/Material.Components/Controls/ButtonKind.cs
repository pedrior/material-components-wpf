namespace Material.Components.Controls;

/// <summary>
/// Specifies the types of buttons, categorized by their appearance and level of emphasis in the UI.
/// </summary>
/// <remarks>
/// Button types are categorized into three levels of
/// emphasis:
/// <list type="bullet">
/// <item>
/// <description>
/// <b>High-emphasis buttons</b> (<see cref="ButtonKind.Filled"/>): Used for primary actions such as "Save", "Confirm",
/// or "Done".
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Medium-emphasis buttons</b> (<see cref="ButtonKind.Tonal"/>, <see cref="ButtonKind.Elevated"/>, and
/// <see cref="ButtonKind.Outlined"/>): Used for secondary actions like "Cancel", "View all", or "Reply".
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Low-emphasis buttons</b> (<see cref="ButtonKind.Text"/>): Used for supplementary actions such as "Learn more",
/// "Change account", or "Yes/No/Maybe".
/// </description>
/// </item>
/// </list>
/// </remarks>
public enum ButtonKind
{
    /// <summary>
    /// A high-emphasis button with a solid background that contrasts with the surface color. Typically used for
    /// primary or high prominent actions.
    /// </summary>
    Filled,

    /// <summary>
    /// A medium-emphasis button with a lighter, tonal background and a dark label. Typically used for secondary but
    /// important actions.
    /// </summary>
    Tonal,

    /// <summary>
    /// A medium-emphasis button with a lighter background color, shadow, and raised appearance. Suitable for less
    /// prominent actions that still require attention.
    /// </summary>
    Elevated,

    /// <summary>
    /// A medium-emphasis button with a transparent background and a border. Typically used for actions where subtle
    /// differentiation is required.
    /// </summary>
    Outlined,

    /// <summary>
    /// A low-emphasis button with a transparent background and no border. Suitable for supplementary or less
    /// prominent actions.
    /// </summary>
    Text
}