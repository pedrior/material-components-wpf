namespace Material.Components.Controls;

/// <summary>
/// Defines various options that can be applied to a <b>single</b> modal to customize its appearance
/// and behavior within a modal host.
/// </summary>
/// <remarks>
/// <para>
/// These options apply to an individual modal instance only. A modal host may contain multiple modals,
/// each with its own set of options.
/// </para>
/// 
/// <para>
/// Options can be combined using bitwise operations. The <see cref="Default"/> set provides a
/// recommended combination of options for most use cases.
/// </para>
/// </remarks>
[Flags]
public enum ModalOptions
{
    /// <summary>
    /// Enables animation transitions when the modal opens or closes.
    /// </summary>
    Animate = 1 << 1,

    /// <summary>
    /// Enables closing the <b>topmost</b> modal when the scrim overlay is clicked.
    /// </summary>
    CloseOnScrimClick = 1 << 2,

    /// <summary>
    /// Enables closing the <b>topmost</b> modal by pressing the Escape (ESC) key.
    /// </summary>
    CloseOnEscapeKey = 1 << 3,

    /// <summary>
    /// Enables a scrim overlay behind the modal to provide visual separation from the background.
    /// </summary>
    UseScrimOverlay = 1 << 4,

    /// <summary>
    /// Represents the recommended default options for a modal, 
    /// combining animation, scrim click to close, escape key to close, and scrim overlay.
    /// </summary>
    Default = Animate | CloseOnScrimClick | CloseOnEscapeKey | UseScrimOverlay
}
