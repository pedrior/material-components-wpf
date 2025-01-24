using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Material.Components.Motion;

namespace Material.Components.Controls;

/// <summary>
/// Represents a container that manages modals elements.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="ModalHost"/> is responsible for managing the lifecycle, animation, and interactions of multiple
/// modals within a single host container. It ensures proper stacking, keyboard focus management and interaction.
/// </para>
/// 
/// <para>
/// When multiple modals are pushed, the modal with the highest priority always appears on top and receives keyboard
/// focus. If multiple modals share the same priority, the last pushed modal takes precedence.
/// </para>
/// </remarks>
[ContentProperty(nameof(Content))]
public class ModalHost : FrameworkElement
{
    private readonly FillPanel panel;
    private readonly ContentPresenter contentPresenter = new();
    private readonly ScrimOverlay scrimOverlay = new() { IsHitTestVisible = false };

    private readonly List<Modal> modalStack = []; // Sorted by priority, from highest to lowest.

    // Holds a reference to the last focused element before the first modal was opened.
    // This is used to restore focus to the last focused element when all modals are closed.
    private WeakReference<IInputElement>? lastFocusedElementReference;

    /// <summary>
    /// Initializes static members of the <see cref="ModalHost"/> class.
    /// </summary>
    static ModalHost()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ModalHost),
            new FrameworkPropertyMetadata(typeof(ModalHost)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModalHost"/> class.
    /// </summary>
    public ModalHost()
    {
        panel = new FillPanel
        {
            Children =
            {
                contentPresenter,
                scrimOverlay
            }
        };

        Unloaded += OnUnloaded;

        scrimOverlay.PreviewMouseLeftButtonDown += OnScrimOverlayMouseDown;

        AddVisualChild(panel);
    }

    /// <summary>
    /// Gets or sets the main content displayed within the modal host.
    /// </summary>
    /// <value>The <see cref="UIElement"/> content to be rendered within the modal host.</value>
    /// <remarks>
    /// <para>This content is independent of the modals managed by the host. </para>
    /// <para>
    /// When a modal is active, interaction with the main content might be restricted based on the modal options.
    /// </para>
    /// </remarks>
    [Bindable(true)]
    [Category("Content")]
    public object? Content
    {
        get => (object?)contentPresenter.Content;
        set => contentPresenter.Content = value;
    }

    /// <summary>
    /// Gets a read-only stack of currently open modals in the host.
    /// </summary>
    /// <value>A collection of <see cref="IModal"/> instances representing the active modals.</value>
    /// <remarks>
    /// The stack is ordered by modal priority, with the highest priority modal appearing first.
    /// </remarks>
    public IReadOnlyList<IModal> ModalStack => modalStack.AsReadOnly();

    protected override int VisualChildrenCount => 1;

    private bool ContainsModals => modalStack.Count > 0;

    private Modal? TopmostModal => ContainsModals ? modalStack[0] : null;

    /// <summary>
    /// Pushes a new modal into the host with the specified content, priority, and options.
    /// </summary>
    /// <param name="content">The UI content of the modal.</param>
    /// <param name="priority">The priority level of the modal, affecting stacking order and focus.</param>
    /// <param name="placement">The placement mode of the modal within the host.</param>
    /// <param name="placementCallback">Optional callback to dynamically position the modal.</param>
    /// <param name="options">Flags that control the modal's behavior and appearance.</param>
    /// <param name="enterAnimationDuration">Duration of the enter animation.</param>
    /// <param name="enterAnimationEasing">Easing function applied to the enter animation.</param>
    /// <param name="exitAnimationDuration">Duration of the exit animation.</param>
    /// <param name="exitAnimationEasing">Easing function applied to the exit animation.</param>
    /// <param name="openedCallback">Callback invoked when the modal is fully opened.</param>
    /// <param name="closedCallback">Callback invoked when the modal is fully closed.</param>
    /// <returns>An <see cref="IModal"/> instance representing the newly created modal.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="content"/> is null.</exception>
    /// <remarks>
    /// <para>The modal is added to the visual tree and displayed with the specified configuration.</para>
    /// </remarks>
    public IModal PushModal(
        UIElement content,
        ModalPriority priority = ModalPriority.Normal,
        ModalPlacementMode placement = ModalPlacementMode.Center,
        ModalPlacementCallback? placementCallback = null,
        ModalOptions options = ModalOptions.Default,
        Duration? enterAnimationDuration = null,
        IEasingFunction? enterAnimationEasing = null,
        Duration? exitAnimationDuration = null,
        IEasingFunction? exitAnimationEasing = null,
        Action<IModal>? openedCallback = null,
        Action<IModal>? closedCallback = null)
    {
        ArgumentNullException.ThrowIfNull(content);

        var modal = new Modal(this, content)
        {
            Opacity = 0.0, // Hidden by default.
            Priority = priority,
            Placement = placement,
            Options = options,
            PlacementCallback = placementCallback,
            OpenedCallback = openedCallback,
            ClosedCallback = closedCallback,
            EnterAnimationDuration = enterAnimationDuration ?? AnimationDurations.Long500,
            EnterAnimationEasing = enterAnimationEasing ?? AnimationEasings.EmphasizedDecelerated,
            ExitAnimationDuration = exitAnimationDuration ?? AnimationDurations.Short200,
            ExitAnimationEasing = exitAnimationEasing ?? AnimationEasings.EmphasizedAccelerated
        };

        OpenModal(modal);

        return modal;
    }

    protected override Visual GetVisualChild(int _) => panel;

    protected override Size MeasureOverride(Size constraint)
    {
        panel.Measure(constraint);
        return panel.DesiredSize;
    }

    protected override Size ArrangeOverride(Size constraint)
    {
        panel.Arrange(new Rect(constraint));
        return constraint;
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (e.Key is Key.Escape && TopmostModal is { CloseOnEscapeKeyPress: true } modal)
        {
            CloseModal(modal);
        }
    }

    private static void BeginFadeAnimation(
        UIElement element,
        double toValue,
        Duration duration,
        IEasingFunction easingFunction,
        Action? completedCallback = null)
    {
        var animation = new DoubleAnimation
        {
            To = toValue,
            Duration = duration,
            EasingFunction = easingFunction
        };

        if (completedCallback is not null)
        {
            void OnAnimationOnCompleted(object? sender, EventArgs e)
            {
                animation.Completed -= OnAnimationOnCompleted;

                completedCallback();
            }

            animation.Completed += OnAnimationOnCompleted;
        }
        else // We can freeze the animation if no callback is provided.
        {
            animation.Freeze();
        }

        element.BeginAnimation(OpacityProperty, animation);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;

        scrimOverlay.PreviewMouseLeftButtonDown -= OnScrimOverlayMouseDown;
    }

    private void OnScrimOverlayMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (TopmostModal is { CloseOnScrimClick: true } modal)
        {
            CloseModal(modal);
        }
    }

    private void OpenModal(Modal modal)
    {
        // Add the modal to the panel.
        panel.Children.Add(modal);

        // Insert the modal into the priority-sorted modals list.
        modalStack.Insert(FindModalIndex(p => modal.Priority >= p), modal);

        // Gives the modals and scrim overlay the correct z-indexes.
        UpdateModalsAndScrimZIndexes();

        if (ReferenceEquals(modal, TopmostModal)) // Is the newly opened modal also the topmost?
        {
            // If there is more than one modal open (including the newly opened one), we need to save the focus for
            // the second topmost modal, so that we can restore it when the newly topmost modal is closed. In case
            // the newly topmost modal is also the first modal open, we need to save the focus for the main content.
            if (modalStack.Count > 1)
            {
                modalStack[1].SaveFocus();
            }
            else
            {
                lastFocusedElementReference = new WeakReference<IInputElement>(Keyboard.FocusedElement);
            }

            // Move the focus to the newly opened modal.
            modal.MoveFocus();
        }

        var showScrimOverlay = modalStack.Count is 1 && modal.UseScrimOverlay;

        if (modal.IsAnimate)
        {
            if (showScrimOverlay)
            {
                scrimOverlay.IsHitTestVisible = true;

                BeginFadeAnimation(
                    scrimOverlay,
                    ScrimOverlay.ActiveOpacity,
                    modal.EnterAnimationDuration,
                    modal.EnterAnimationEasing);
            }

            modal.IsOpen = true;

            BeginFadeAnimation(
                modal,
                1.0,
                modal.EnterAnimationDuration,
                modal.EnterAnimationEasing,
                completedCallback: () => modal.IsEffectiveOpen = true);
        }
        else
        {
            if (showScrimOverlay)
            {
                scrimOverlay.IsHitTestVisible = true;
                scrimOverlay.Opacity = ScrimOverlay.ActiveOpacity;
            }

            modal.Opacity = 1.0;

            modal.IsOpen = true;
            modal.IsEffectiveOpen = true;
        }
    }

    internal void CloseModal(Modal modal)
    {
        // Immediately remove the modal from the modal stack.
        modalStack.Remove(modal);

        // We still need to update the z-indexes of the remaining modals and the scrim overlay.
        UpdateModalsAndScrimZIndexes();

        if (TopmostModal is { } secondTopmostModal)
        {
            // Restore the focus to the second topmost modal.
            secondTopmostModal.MoveFocus();
        }
        else // No modals left.
        {
            // Restore the focus to the last focused element.
            if (lastFocusedElementReference?.TryGetTarget(out var lastFocusedElement) ?? false)
            {
                Keyboard.Focus(lastFocusedElement);
            }
            else
            {
                // Last focused element is no longer available, find the first focusable element in the
                // main content and move the focus to it.
                contentPresenter.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }

            lastFocusedElementReference = null;
        }

        var hideScrimOverlay = !ContainsModals && scrimOverlay.Opacity is not 0.0;

        if (modal.IsAnimate)
        {
            if (hideScrimOverlay)
            {
                scrimOverlay.IsHitTestVisible = false;

                BeginFadeAnimation(
                    scrimOverlay,
                    ScrimOverlay.InactiveOpacity,
                    modal.ExitAnimationDuration,
                    modal.ExitAnimationEasing);
            }

            modal.IsOpen = false;

            BeginFadeAnimation(
                modal,
                0.0,
                modal.ExitAnimationDuration,
                modal.ExitAnimationEasing,
                completedCallback: () =>
                {
                    // We can finally remove the modal from the panel. 
                    panel.Children.Remove(modal);

                    modal.IsEffectiveOpen = false;
                });
        }
        else
        {
            if (hideScrimOverlay)
            {
                scrimOverlay.IsHitTestVisible = false;
                scrimOverlay.Opacity = ScrimOverlay.InactiveOpacity;
            }

            modal.IsOpen = false;
            modal.IsEffectiveOpen = false;

            panel.Children.Remove(modal);
        }
    }

    private int FindModalIndex(Func<ModalPriority, bool> compare)
    {
        // We're use a binary search algorithm to find the index where the new modal
        // should be inserted based on its priority.

        var low = 0;
        var high = modalStack.Count - 1;

        while (low <= high)
        {
            var mid = low + (high - low) / 2;

            if (compare(modalStack[mid].Priority))
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return low;
    }

    private void UpdateModalsAndScrimZIndexes()
    {
        for (var index = 0; index < modalStack.Count; index++)
        {
            // Gives higher z-index to higher priority modals.
            modalStack[index].ZIndex = modalStack.Count - 1 - index;
        }

        // By giving the scrim overlay the z-index of the topmost modal, we ensure
        // it will always appear behind the topmost modal within the panel.
        scrimOverlay.ZIndex = modalStack.Count is 0 ? 0 : modalStack[0].ZIndex;
    }
}