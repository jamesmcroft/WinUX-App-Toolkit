namespace WinUX.Xaml.Behaviors.Common
{
    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for detecting key presses on the associated control.
    /// </summary>
    public sealed class KeyPressedBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Key"/>.
        /// </summary>
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            nameof(Key),
            typeof(VirtualKey),
            typeof(KeyPressedBehavior),
            new PropertyMetadata(VirtualKey.Enter));

        /// <summary>
        /// Defines the dependency property for <see cref="Actions"/>.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            nameof(Actions),
            typeof(ActionCollection),
            typeof(KeyPressedBehavior),
            new PropertyMetadata(default(ActionCollection)));

        private Control Control => this.AssociatedObject as Control;

        /// <summary>
        /// Gets or sets the key to listen for.
        /// </summary>
        public VirtualKey Key
        {
            get
            {
                return (VirtualKey)this.GetValue(KeyProperty);
            }
            set
            {
                this.SetValue(KeyProperty, value);
            }
        }

        /// <summary>
        /// Gets the collection of actions to perform when clicking the associated button.
        /// </summary>
        public ActionCollection Actions
        {
            get
            {
                ActionCollection actionCollection = (ActionCollection)this.GetValue(ActionsProperty);
                if (actionCollection != null)
                {
                    return actionCollection;
                }

                actionCollection = new ActionCollection();
                this.SetValue(ActionsProperty, actionCollection);
                return actionCollection;
            }
        }

        /// <summary>
        /// Called after the behavior is attached to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnAttached()
        {
            if (this.Control != null)
            {
                this.Control.KeyDown += this.Control_OnKeyDown;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.Control != null)
            {
                this.Control.KeyDown -= this.Control_OnKeyDown;
            }
        }

        private void Control_OnKeyDown(object sender, KeyRoutedEventArgs args)
        {
            var control = sender as Control;
            if (control != null && args.Key == this.Key)
            {
                Interaction.ExecuteActions(control, this.Actions, args);
            }
        }
    }
}