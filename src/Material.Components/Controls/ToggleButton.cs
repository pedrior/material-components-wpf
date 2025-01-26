using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Material.Components.Controls;

/// <summary>
/// Represents an abstraction for toggle button controls.
/// </summary>
/// <remarks>
/// This class extends the functionality of the standard <see cref="System.Windows.Controls.Primitives.ToggleButton"/>
/// to provide additional properties and behaviors.
/// </remarks>
public abstract class ToggleButton : System.Windows.Controls.Primitives.ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="CheckedToolTip"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CheckedToolTipProperty = DependencyProperty.Register(
        nameof(CheckedToolTip),
        typeof(object),
        typeof(ToggleButton),
        new PropertyMetadata(null));

    private object? uncheckedToolTipValue;

    /// <summary>
    /// Gets or sets the tooltip content to display when this button is checked.
    /// </summary>
    /// <remarks>
    /// The <see cref="CheckedToolTip"/> property allows you to specify a different tooltip content
    /// to display when the button is checked. This standard <see cref="FrameworkElement.ToolTip"/>
    /// acts as the unchecked version of the tooltip content.
    /// </remarks>
    /// <value>
    /// A <see langword="object"/> value that represents the tooltip content to display when the button is checked.
    /// </value>
    [Bindable(true)]
    [Category("Common")]
    public object? CheckedToolTip
    {
        get => (object?)GetValue(CheckedToolTipProperty);
        set => SetValue(CheckedToolTipProperty, value);
    }

    protected override void OnChecked(RoutedEventArgs e)
    {
        base.OnChecked(e);

        SetCheckedToolTip();
    }

    protected override void OnUnchecked(RoutedEventArgs e)
    {
        base.OnUnchecked(e);

        SetUncheckedToolTip();
    }

    private void SetCheckedToolTip()
    {
        if (CheckedToolTip is not { } checkedToolTip)
        {
            return;
        }

        uncheckedToolTipValue = GetBindingExpression(ToolTipProperty) 
                           ?? GetValue(ToolTipProperty);
        
        SetCurrentValue(ToolTipProperty, checkedToolTip);
    }

    private void SetUncheckedToolTip()
    {
        switch (uncheckedToolTipValue)
        {
            case null:
                return;
            case BindingExpression expression:
                SetBinding(ToolTipProperty, expression.ParentBinding);
                break;
            default:
                SetValue(ToolTipProperty, uncheckedToolTipValue);
                break;
        }

        uncheckedToolTipValue = null;
    }
}