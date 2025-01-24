using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Material.Components.Controls;

internal sealed class Modal : FrameworkElement, IModal
{
    private static readonly TraversalRequest DefaultFocusTraversalRequest = new(FocusNavigationDirection.First);

    private readonly ModalHost host;

    private bool isEffectiveOpen;

    private bool isFocusMoved;
    private bool isPendingFocus;
    private bool isPendingClose;

    private WeakReference<IInputElement>? lastFocusedElementReference;

    internal Modal(ModalHost host, UIElement content)
    {
        this.host = host;
        Content = content;

        Loaded += OnLoaded;

        AddVisualChild(Content);

        // Ensures that the focus doesn't leave the modal when the user navigates using the keyboard.
        KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
        KeyboardNavigation.SetControlTabNavigation(this, KeyboardNavigationMode.Cycle);
    }

    public UIElement Content { get; }

    public bool IsOpen { get; internal set; }

    public bool IsEffectiveOpen
    {
        get => isEffectiveOpen;
        set
        {
            if (isEffectiveOpen == value)
            {
                return;
            }

            isEffectiveOpen = value;

            if (isEffectiveOpen)
            {
                OpenedCallback?.Invoke(this);
            }
            else
            {
                ClosedCallback?.Invoke(this);
            }

            if (isPendingClose)
            {
                host.CloseModal(this);
            }
        }
    }
    
    public required ModalPriority Priority { get; init; }

    public required ModalPlacementMode Placement { get; init; }

    public required ModalOptions Options { get; init; }
    
    public Rect PlacementRectangle { get; private set; }

    internal required Duration EnterAnimationDuration { get; init; }

    internal required IEasingFunction EnterAnimationEasing { get; init; }

    internal required Duration ExitAnimationDuration { get; init; }

    internal required IEasingFunction ExitAnimationEasing { get; init; }

    internal ModalPlacementCallback? PlacementCallback { get; init; }

    internal int ZIndex
    {
        get => Panel.GetZIndex(this);
        set => Panel.SetZIndex(this, value);
    }

    internal Action<IModal>? OpenedCallback { get; init; }

    internal Action<IModal>? ClosedCallback { get; init; }

    internal bool IsAnimate => HasOptionFlag(ModalOptions.Animate);

    internal bool UseScrimOverlay => HasOptionFlag(ModalOptions.UseScrimOverlay);

    internal bool CloseOnEscapeKeyPress => HasOptionFlag(ModalOptions.CloseOnEscapeKey);

    internal bool CloseOnScrimClick => HasOptionFlag(ModalOptions.CloseOnScrimClick);

    public void Close()
    {
        if (IsEffectiveOpen)
        {
            host.CloseModal(this);
        }
        else
        {
            isPendingClose = true;
        }
    }

    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int _) => Content;

    protected override Size MeasureOverride(Size constraint)
    {
        Content.Measure(constraint);

        return constraint; // Just return the available size.
    }

    protected override Size ArrangeOverride(Size constraint)
    {
        var size = Content.DesiredSize;
        
        PlacementRectangle = PlacementCallback?.Invoke(constraint, size)
                             ?? ComputePlacementRectangle(constraint, size, Placement);

        Content.Arrange(PlacementRectangle);

        return constraint;
    }

    public override string ToString() => $"{{Content: {Content}, Priority: {Priority}, Placement: " +
                                         $"{(PlacementCallback is not null ? "Custom" : Placement.ToString())}}}";

    internal void MoveFocus()
    {
        // Attempts to move the focus to the last focused element within the modal.
        if (lastFocusedElementReference is not null)
        {
            if (lastFocusedElementReference.TryGetTarget(out var lastFocusedElement))
            {
                lastFocusedElement.Focus();
            }
            else
            {
                // The last focused element is no longer available, so we move the focus to the first 
                // focusable element in the modal, as a fallback.
                MoveFocus(DefaultFocusTraversalRequest);
            }

            lastFocusedElementReference = null;
        }
        else if (IsLoaded)
        {
            // Move the focus to the first focusable element in the modal.
            MoveFocus(DefaultFocusTraversalRequest);
            isFocusMoved = true;
        }
        else
        {
            // The modal is not loaded yet, so we defer the focus operation until it's loaded.
            isPendingFocus = true;
        }
    }

    internal void SaveFocus()
    {
        if (isFocusMoved)
        {
            // Saves the last focused element within the modal.
            lastFocusedElementReference = new WeakReference<IInputElement>(Keyboard.FocusedElement);
        }
    }

    private static Rect ComputePlacementRectangle(Size constraint, Size size, ModalPlacementMode placement)
    {
        var rect = new Rect(size);
        switch (placement)
        {
            case ModalPlacementMode.Center:
                rect.X = (constraint.Width - size.Width) * 0.5;
                rect.Y = (constraint.Height - size.Height) * 0.5;
                break;
            case ModalPlacementMode.Top:
                rect.X = (constraint.Width - size.Width) * 0.5;
                rect.Y = 0.0;
                break;
            case ModalPlacementMode.Bottom:
                rect.X = (constraint.Width - size.Width) * 0.5;
                rect.Y = constraint.Height - size.Height;
                break;
            case ModalPlacementMode.Left:
                rect.X = 0.0;
                rect.Y = (constraint.Height - size.Height) * 0.5;
                break;
            case ModalPlacementMode.Right:
                rect.X = constraint.Width - size.Width;
                rect.Y = (constraint.Height - size.Height) * 0.5;
                break;
            case ModalPlacementMode.FullScreen:
            default:
                // Tries to fill the available space.
                rect.Width = constraint.Width;
                rect.Height = constraint.Height;
                break;
        }

        return rect;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        if (isPendingFocus)
        {
            MoveFocus();
        }
    }

    private bool HasOptionFlag(ModalOptions flag) => (Options & flag) != 0;
}