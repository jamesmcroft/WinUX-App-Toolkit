namespace WinUX.Xaml.Behaviors.Button
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for buttons which performs actions on the click event.
    /// </summary>
    public sealed class ButtonClickBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Actions"/>.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            nameof(Actions),
            typeof(ActionCollection),
            typeof(ButtonClickBehavior),
            new PropertyMetadata(default(ActionCollection)));

        private Button Button => this.AssociatedObject as Button;

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
            if (this.Button != null)
            {
                this.Button.Click += this.OnButtonClicked;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.Button != null)
            {
                this.Button.Click -= this.OnButtonClicked;
            }
        }

        private void OnButtonClicked(object sender, RoutedEventArgs args)
        {
            Interaction.ExecuteActions(this.Button, this.Actions, args);
        }
    }
}