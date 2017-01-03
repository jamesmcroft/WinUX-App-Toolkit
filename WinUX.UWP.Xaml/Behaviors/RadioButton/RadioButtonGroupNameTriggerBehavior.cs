namespace WinUX.Xaml.Behaviors.RadioButton
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for applying a group name to a RadioButton.
    /// </summary>
    public sealed class RadioButtonGroupNameTriggerBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="SetGroupName"/>.
        /// </summary>
        public static readonly DependencyProperty SetGroupNameProperty =
            DependencyProperty.Register(
                nameof(SetGroupName),
                typeof(bool),
                typeof(RadioButtonGroupNameTriggerBehavior),
                new PropertyMetadata(
                    null,
                    (d, e) => ((RadioButtonGroupNameTriggerBehavior)d).UpdateRadioButtonGroupName((bool)e.NewValue)));

        private RadioButton RadioButton => this.AssociatedObject as RadioButton;

        /// <summary>
        /// Gets or sets the group name to set on the associated <see cref="RadioButton"/>.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to set the group name.
        /// </summary>
        public bool SetGroupName
        {
            get
            {
                return (bool)this.GetValue(SetGroupNameProperty);
            }
            set
            {
                this.SetValue(SetGroupNameProperty, value);
            }
        }

        private void UpdateRadioButtonGroupName(bool shouldSet)
        {
            if (this.RadioButton != null)
            {
                this.RadioButton.GroupName = shouldSet ? this.GroupName : Guid.NewGuid().ToString();
            }
        }
    }
}